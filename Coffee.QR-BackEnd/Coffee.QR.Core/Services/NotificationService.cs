using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class NotificationService : CrudService<NotificationDto, Notification>, INotificationService
    {
        private readonly INotificationRepository _notificationRepository;


        public NotificationService(ICrudRepository<Notification> crudRepository, IMapper mapper, INotificationRepository notificationRepository)
            : base(crudRepository, mapper)
        {
            _notificationRepository = notificationRepository;
        }

        public Result<NotificationDto> CreateNotification(NotificationDto notificationDto)
        {
            try
            {
                var notificationt = _notificationRepository.Create(new Notification(notificationDto.Message, notificationDto.DateTime, notificationDto.IsActive, notificationDto.TableId, notificationDto.LocalId));

                NotificationDto resultDto = new NotificationDto
                {
                    Id = notificationt.Id,
                    Message = notificationt.Message,
                    DateTime = notificationt.DateTime,
                    IsActive = notificationt.IsActive,
                    TableId = notificationt.TableId,
                    LocalId = notificationt.LocalId,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<NotificationDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<NotificationDto>> GetAllNotifications()
        {
            try
            {
                var notifications = _notificationRepository.GetAll();
                var notificationDtos = notifications.Select(n => new NotificationDto
                {
                    Id = n.Id,
                    Message = n.Message,
                    DateTime = n.DateTime,
                    IsActive = n.IsActive,
                    TableId = n.TableId,
                    LocalId = n.LocalId,
                }).ToList();

                return Result.Ok(notificationDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<NotificationDto>>("Failed to retrieve notifications").WithError(e.Message);
            }
        }

        public Result<List<NotificationDto>> GetAllActiveNotifications(long localId)
        {
            try
            {
                var notifications = _notificationRepository.GetAllActive(localId);
                var notificationDtos = notifications.Select(n => new NotificationDto
                {
                    Id = n.Id,
                    Message = n.Message,
                    DateTime = n.DateTime,
                    IsActive = n.IsActive,
                    TableId = n.TableId,
                    LocalId = n.LocalId,
                }).ToList();

                return Result.Ok(notificationDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<NotificationDto>>("Failed to retrieve notifications").WithError(e.Message);
            }
        }

        public void DeactivateNotification(long notificationId)
        {
            _notificationRepository.UpdateNotificationIsActive(notificationId, false);
        }

        public bool DeleteNotification(long notificationId)
        {
            var notificationToDelete = _notificationRepository.Delete(notificationId);
            return notificationToDelete != null;
        }
    }
}
