using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Business.Service;

namespace Business.Services
{
    public interface IEventService
    {
        Task<List<EventModel>> GetAllEventsAsync();
        Task<EventModel?> GetEventByIdAsync(int id);
        Task<EventModel> CreateEventAsync(EventModel model);
        Task<bool> UpdateEventAsync(int id, EventModel model);
        Task<bool> DeleteEventAsync(int id);
    }

    public class EventServiceMVP : IEventService
    {
        private readonly DataContext _context;

        public EventServiceMVP(DataContext context)
        {
            _context = context;
        }

        public async Task<List<EventModel>> GetAllEventsAsync()
        {
            return await _context.Events
                .Select(e => new EventModel
                {
                    Id = e.Id,
                    Image = e.Image,
                    Name = e.Name,
                    Date = e.Date,
                    Location = e.Location,
                    Description = e.Description,
                    IsActive = e.IsActive
                })
                .ToListAsync();
        }

        public async Task<EventModel?> GetEventByIdAsync(int id)
        {
            var entity = await _context.Events.FindAsync(id);
            if (entity == null) return null;

            return new EventModel
            {
                Id = entity.Id,
                Image = entity.Image,
                Name = entity.Name,
                Date = entity.Date,
                Location = entity.Location,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public async Task<EventModel> CreateEventAsync(EventModel model)
        {
            var entity = new EventEntity
            {
                Image = model.Image,
                Name = model.Name,
                Date = model.Date,
                Location = model.Location,
                Description = model.Description,
                IsActive = model.IsActive
            };

            _context.Events.Add(entity);
            await _context.SaveChangesAsync();

            model.Id = entity.Id;
            return model;
        }

        public async Task<bool> UpdateEventAsync(int id, EventModel updatedEvent)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
                return false;

            existingEvent.Image = updatedEvent.Image;
            existingEvent.Name = updatedEvent.Name;
            existingEvent.Date = updatedEvent.Date;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.Location = updatedEvent.Location;
            existingEvent.IsActive = updatedEvent.IsActive;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
                return false;

            _context.Events.Remove(existingEvent);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
