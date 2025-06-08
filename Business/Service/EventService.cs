using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Business.Service;
using Infrastructure.Models;

namespace Business.Services
{
    public interface IEventService
    {
        Task<List<EventModel>> GetAllEventsAsync();
        Task<EventModel?> GetEventByIdAsync(string id);
        Task<RepositoryResult<EventModel>> CreateEventAsync(EventModel model);

        Task<bool> UpdateEventAsync(string id, EventModel model);
        Task<bool> DeleteEventAsync(string id);
    }

    public class EventServiceMVP : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventServiceMVP(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<EventModel>> GetAllEventsAsync()
        {
            var result = await _eventRepository.GetAllAsync();
            if (!result.IsSuccess || result.Result == null)
                return [];

            return result.Result.Select(e => new EventModel
            {
                Id = e.Id,
                Image = e.Image,
                Name = e.Name,
                Date = e.Date,
                Location = e.Location,
                Description = e.Description,
                IsActive = e.IsActive
            }).ToList();
        }

        public async Task<EventModel?> GetEventByIdAsync(string id)
        {
            var result = await _eventRepository.GetByIdAsync(id);
            if (!result.IsSuccess || result.Result == null)
                return null;

            var entity = result.Result;
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

        public async Task<RepositoryResult<EventModel>> CreateEventAsync(EventModel model)
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

            var result = await _eventRepository.AddAsync(entity);

            if (!result.IsSuccess || result.Result == null)
            {
                //Tagit hjälp av ChatGPT
                return new RepositoryResult<EventModel>
                {
                    IsSuccess = false,
                    ErrorMessage = result.ErrorMessage
                };
            }

            var createdModel = new EventModel
            {
                Id = result.Result.Id,
                Image = result.Result.Image,
                Name = result.Result.Name,
                Date = result.Result.Date,
                Location = result.Result.Location,
                Description = result.Result.Description,
                IsActive = result.Result.IsActive
            };

            return new RepositoryResult<EventModel>
            {
                IsSuccess = true,
                Result = createdModel
            };
        }


        public async Task<bool> UpdateEventAsync(string id, EventModel updatedEvent)
        {
            var existingResult = await _eventRepository.GetByIdAsync(id);
            if (!existingResult.IsSuccess || existingResult.Result == null)
                return false;

            var existingEvent = existingResult.Result;
            existingEvent.Image = updatedEvent.Image;
            existingEvent.Name = updatedEvent.Name;
            existingEvent.Date = updatedEvent.Date;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.Location = updatedEvent.Location;
            existingEvent.IsActive = updatedEvent.IsActive;

            var updateResult = await _eventRepository.UpdateAsync(existingEvent);
            return updateResult.IsSuccess && updateResult.Result;
        }

        public async Task<bool> DeleteEventAsync(string id)
        {
            var existingResult = await _eventRepository.GetByIdAsync(id);
            if (!existingResult.IsSuccess || existingResult.Result == null)
                return false;

            var deleteResult = await _eventRepository.DeleteAsync(existingResult.Result);
            return deleteResult.IsSuccess && deleteResult.Result;
        }
    }
}