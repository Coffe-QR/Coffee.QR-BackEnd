using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace Coffee.QR.Core.Services
{
    public class SupplyItemService : CrudService<SupplyItemDto, SupplyItem>, ISupplyItemService
    {
        private readonly ISupplyItemRepository _supplyItemRepository;
        private readonly ICompanyRepository _companyRepository;
        private IItemRepository _itemRepository;

        public SupplyItemService(ICrudRepository<SupplyItem> crudRepository, IMapper mapper, ISupplyItemRepository supplyItemRepository, ICompanyRepository companyRepository, IItemRepository itemRepository)
            : base(crudRepository, mapper)
        {
            _supplyItemRepository = supplyItemRepository;
            _companyRepository = companyRepository;
            _itemRepository = itemRepository;
        }

        public Result<SupplyItemDto> CreateSupplyItem(SupplyItemDto supplyItemDto)
        {
            try
            {
                var supplyItemt = _supplyItemRepository.Create(new SupplyItem(supplyItemDto.SupplyId, supplyItemDto.ItemId, supplyItemDto.Quantity, supplyItemDto.Price));

                SupplyItemDto resultDto = new SupplyItemDto
                {
                    Id = supplyItemt.Id,
                    Quantity = supplyItemt.Quantity,
                    Price = supplyItemt.Price,
                    SupplyId = supplyItemt.SupplyId,
                    ItemId = supplyItemt.ItemId,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<SupplyItemDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<SupplyItemDto>> GetAllSupplyItems()
        {
            try
            {
                var supplyItems = _supplyItemRepository.GetAll();
                var supplyItemDtos = supplyItems.Select(s => new SupplyItemDto
                {
                    Id = s.Id,
                    Quantity = s.Quantity,
                    Price = s.Price,
                    SupplyId = s.SupplyId,
                    ItemId = s.ItemId,
                }).ToList();

                return Result.Ok(supplyItemDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<SupplyItemDto>>("Failed to retrieve supplyItems").WithError(e.Message);
            }
        }


        public bool DeleteSupplyItem(long supplyItemId)
        {
            var supplyItemToDelete = _supplyItemRepository.Delete(supplyItemId);
            return supplyItemToDelete != null;
        }


        public Task<Result<SupplyItemDto>> GetSupplyItemByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<SupplyItemDto>> UpdateSupplyItemAsync(SupplyItemDto supplyItemDto)
        {
            throw new NotImplementedException();
        }

        private void SendEmail(List<SupplyItem> supplyItems)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("markoandjelicpsw@gmail.com", "yasg svva qiep ddfo"),
                EnableSsl = true,
            };

            string value = "";
            foreach(var si in supplyItems)
            {
                value += si.Item.Name + " kom: " + si.Quantity + "<br>";
            }

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("markoandjelicpsw@gmail.com"),
                To = { "milospisaric001@gmail.com" },
                Subject = "Potvrdjena narudzbina",
                Body = "Postovani,<br>" +
                    "Narudzbina Vam je poslata, stize u roku od " + supplyItems[0].Item.Company.DaysDelivery + " dana.<br>" +
                    "Sadrzaj narudzbine je " +
                    value +
                    "<br>Srdacan pozdrav,<br>" +
                    supplyItems[0].Item.Company.Name,
                IsBodyHtml = true
            };


            try
            {
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to send email: {ex.Message}");
            }
            finally
            {
                mailMessage.Dispose();
                client.Dispose();
            }
        }

        public Result<List<SupplyItemDto>> CreateSupplyItems(List<SupplyItemDto> supplyItemDtos)
        {
            try
            {
                List<SupplyItem> supplyItems = new();

                foreach (var item in supplyItemDtos)
                {
                    supplyItems.Add(_supplyItemRepository.Get(item.ItemId));
                }

                List<long> companyIds = new();

                foreach (var item in supplyItems)
                {
                    if (companyIds.FirstOrDefault(c => c == item.Supply.CompanyId) == null) continue;
                    List<SupplyItem> dtos = supplyItems.Where(s => s.Item.CompanyId == item.Item.CompanyId).ToList();
                    SendEmail(dtos);
                    companyIds.Add(item.Supply.CompanyId);

                    //    dtos.ForEach(dto => supplyItems.Remove(dto));
                }
                return MapToDto(supplyItems);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<List<SupplyItemDto>>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<List<SupplyItemDto>> GetAllForSupply(long supplyId)
        {
            try
            {
                List<SupplyItemDto> dtos = new();
                foreach (var item in GetAllSupplyItems().Value)
                {
                    if (item.SupplyId == supplyId) dtos.Add(item);
                }
                return Result.Ok(dtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<SupplyItemDto>>("Failed to retrieve supplyItems").WithError(e.Message);
            }
        }
    }
}
