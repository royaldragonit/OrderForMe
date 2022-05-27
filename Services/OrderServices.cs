using ADO.Extension.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderForMeProject.Commons;
using OrderForMeProject.Interfaces;
using OrderForMeProject.Models.CustomModels;
using OrderForMeProject.Models.Entities;
using OrderForMeProject.Models.OrdersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Services
{
    public class OrderServices : BaseServices, IOrderServices
    {
        public OrderServices(OrderformeContext db, IConfiguration config, IWebHostEnvironment environment, IParameterMapper parameterMapper, IHttpContextAccessor httpContext, IMapper mapper, IUnitOfWork unitOfWork) : base(db, config, environment, parameterMapper, httpContext, mapper, unitOfWork)
        {
        }

        public async Task CreateOrder(CreateOrdersModel orders)
        {
            Setting setting = await _db.Setting.FirstOrDefaultAsync();
            SiteBuy site = await _db.SiteBuy.FirstOrDefaultAsync(x => x.SiteBuyId == orders.SiteBuyId);
            orders.StateOrderId = orders.StateOrderId;
            orders.FeeServices = setting.FeeServices.ToInt();
            orders.FeeShippingToVn = setting.FeeShippingToVn.ToInt();
            float groupMoney = await _db.GroupProduct.Where(x => x.GroupProductId == orders.GroupProductId).Select(x => x.Price).FirstOrDefaultAsync();
            float totalMoney = 0;

            orders.TotalMoney = totalMoney.ToInt();
            Orders o = _mapper.Map<Orders>(orders);
            o.CreateOn = DateTime.Now;
            o.IsActive = true;
            _db.Entry(o).State = EntityState.Added;
            await _db.SaveChangesAsync();
            for (int i = 0; i < orders.LinkProduct.Length; i++)
            {

                totalMoney =
       /*Giá tiền * số lượng * tỷ giá USD*/
       orders.Price[i] * orders.Quantity[i] * site.RateUsdtoVnd
    /*Phí theo nhóm hàng*/ + groupMoney
       /*Phí khác*/ + orders.OtherFee
      /*Phí vc nội địa*/ + orders.FeeShipping * orders.RateUsdvnd
      /*Thuế nội địa */ + orders.Tax * site.RateUsdtoVnd
      /*Phí vc về VN */ + setting.FeeShippingToVn
      /*Phí dịch vụ %*/ + (setting.FeeServices / 100) * orders.Price[i] * orders.Quantity[i] * site.RateUsdtoVnd;
                if (orders.IsShippingToVn)
                {
                    totalMoney -= groupMoney;
                    totalMoney -= orders.OtherFee;
                }
                LinkProduct linkProduct = new LinkProduct();
                linkProduct.Link = orders.LinkProduct[i];
                linkProduct.Quantity = orders.Quantity[i];
                linkProduct.Price = orders.Price[i];
                linkProduct.RateUsdtoVnd = orders.RateUsdvnd;
                linkProduct.TotalMoney = totalMoney.ToInt();
                linkProduct.OrdersId = o.OrdersId;
                _db.Entry(linkProduct).State = EntityState.Added;
            }
            await _db.SaveChangesAsync();
        }
        public async Task UpdateOrder(CreateOrdersModel orders)
        {
            Setting setting = await _db.Setting.FirstOrDefaultAsync();
            SiteBuy site = await _db.SiteBuy.FirstOrDefaultAsync(x => x.SiteBuyId == orders.SiteBuyId);
            orders.StateOrderId = orders.StateOrderId;
            orders.FeeServices = setting.FeeServices.ToInt();
            orders.FeeShippingToVn = setting.FeeShippingToVn.ToInt();
            float groupMoney = await _db.GroupProduct.Where(x => x.GroupProductId == orders.GroupProductId).Select(x => x.Price).FirstOrDefaultAsync();
            float totalMoney = 0;
            await _sp.Dr_DeleteLinkProductInOrderAsync(orders.OrdersId);
            for (int i = 0; i < orders.LinkProduct.Length; i++)
            {
                /*Giá tiền * số lượng * tỷ giá USD*/
                totalMoney = orders.Price[i] * orders.Quantity[i] * site.RateUsdtoVnd
    /*Phí theo nhóm hàng*/ + groupMoney
       /*Phí khác*/ + orders.OtherFee
      /*Phí vc nội địa*/ + orders.FeeShipping * orders.RateUsdvnd
      /*Thuế nội địa */ + orders.Tax * site.RateUsdtoVnd
      /*Phí vc về VN */ + setting.FeeShippingToVn
      /*Phí dịch vụ %*/ + (setting.FeeServices / 100) * orders.Price[i] * orders.Quantity[i] * site.RateUsdtoVnd;
                if (orders.IsShippingToVn)
                {
                    totalMoney -= groupMoney;
                    totalMoney -= orders.OtherFee;
                }
                LinkProduct linkProduct = new LinkProduct();
                linkProduct.Link = orders.LinkProduct[i];
                linkProduct.Quantity = orders.Quantity[i];
                linkProduct.Price = orders.Price[i];
                linkProduct.RateUsdtoVnd = orders.RateUsdvnd;
                linkProduct.TotalMoney = totalMoney.ToInt();
                linkProduct.OrdersId = orders.OrdersId;
                _db.Entry(linkProduct).State = EntityState.Added;
            }
            Orders o = _mapper.Map<Orders>(orders);
            o.IsActive = true;
            orders.TotalMoney = totalMoney.ToInt();
            _db.Entry(o).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<ResultCustomModel<InitCreateOrders>> GetInfoCreateOrder()
        {
            var param = new
            {

            };

            var spParams = _parameterMapper.Map(param);
            var execute = await _db.QueryMultipleResults(sql: "Dr_GetInfoModifyOrder").ExecuteMultipleResults(spParams.Parameters, typeof(SelectListItem), typeof(SiteBuy), typeof(SelectListItem), typeof(SelectListCustom), typeof(SelectListItem), typeof(Setting), typeof(LinkProduct));
            List<SelectListItem> lstSourceBuy = new List<SelectListItem>();
            foreach (SelectListItem item in execute[0])
            {
                lstSourceBuy.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }
            List<SiteBuy> lstSiteBuy = new List<SiteBuy>();
            foreach (SiteBuy item in execute[1])
            {
                lstSiteBuy.Add(item);
            }
            List<SelectListItem> lstCustomer = new List<SelectListItem>();
            foreach (SelectListItem item in execute[2])
            {
                lstCustomer.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }
            List<SelectListCustom> lstGroupProduct = new List<SelectListCustom>();
            foreach (SelectListCustom item in execute[3])
            {
                lstGroupProduct.Add(item);
            }
            List<SelectListItem> lstStateOrder = new List<SelectListItem>();
            foreach (SelectListItem item in execute[4])
            {
                lstStateOrder.Add(item);
            }
            Setting settings = execute[5][0];
            List<LinkProduct> lstLinkProduct = new List<LinkProduct>();
            foreach (LinkProduct item in execute[6])
            {
                lstLinkProduct.Add(item);
            }
            var data = new ResultCustomModel<InitCreateOrders>
            {
                Code = 200,
                Success = true,
                Data = new InitCreateOrders
                {
                    ListCustomer = lstCustomer,
                    ListSiteBuy = lstSiteBuy,
                    ListSourceBuy = lstSourceBuy,
                    ListGroupProduct = lstGroupProduct,
                    ListStateOrders = lstStateOrder,
                    Settings = settings,
                    ListLinkProduct = lstLinkProduct
                }
            };
            return data;
        }

        public async Task<ResultCustomModel<bool>> SplitOrder(int ordersId, List<SplitOrdersModel> orders)
        {
            Orders orderRoot = await _db.Orders.FirstOrDefaultAsync(x => x.OrdersId == ordersId);
            if (orderRoot == null)
            {
                return new ResultCustomModel<bool>
                {
                    Success = false,
                    Code = 404,
                    Message = "Không tìm thấy đơn hàng"
                };
            }
            Orders orderSplit = new Orders();
            orderSplit.CreateBy = orderRoot.CreateBy;
            orderSplit.CreateOn = DateTime.Now;
            orderSplit.Currency = orderRoot.Currency;
            orderSplit.DateBuy = orderRoot.DateBuy;
            orderSplit.FeeServices = orderRoot.FeeServices;
            orderSplit.FeeServicesOther = orderRoot.FeeServicesOther;
            orderSplit.FeeShipping = orderRoot.FeeShipping;
            orderSplit.FeeShippingToVn = orderRoot.FeeShippingToVn;
            orderSplit.GroupProductId = orderRoot.GroupProductId;
            orderSplit.IsActive = true;
            orderSplit.IsCompleted = orderRoot.IsCompleted;
            orderSplit.Note = orderRoot.Note;
            orderSplit.OrderRoot = orderRoot.OrdersId;
            orderSplit.OtherFee = orderRoot.OtherFee;
            orderSplit.PaymentType = orderRoot.PaymentType;
            orderSplit.RateUsdvnd = orderRoot.RateUsdvnd;
            orderSplit.SiteBuyId = orderRoot.SiteBuyId;
            orderSplit.SourceBuyId = orderRoot.SourceBuyId;
            orderSplit.Size = orderRoot.Size;
            orderSplit.StateOrderId = orderRoot.StateOrderId;
            orderSplit.Tax = orderRoot.Tax;
            orderSplit.UsersId = orderRoot.UsersId;
            orderSplit.FirstName = orderRoot.FirstName;
            orderSplit.LastName = orderRoot.LastName;
            orderSplit.Address1 = orderRoot.Address1;
            orderSplit.Address2 = orderRoot.Address2;
            orderSplit.City = orderRoot.City;
            orderSplit.State = orderRoot.State;
            orderSplit.ZipCode = orderRoot.ZipCode;
            orderSplit.PhoneNumber = orderRoot.PhoneNumber;
            _db.Entry(orderSplit).State = EntityState.Added;
            await _db.SaveChangesAsync();
            IEnumerable<int> lstLinkProductId = orders.Select(x => x.LinkProductId);
            List<LinkProduct> lstLinkProduct = await _db.LinkProduct.Where(x => lstLinkProductId.Contains(x.LinkProductId)).ToListAsync();
            foreach (var item in lstLinkProduct)
            {
                var itemUpdate = orders.FirstOrDefault(x => x.LinkProductId == item.LinkProductId);
                if (itemUpdate.Quantity == item.Quantity)
                {
                    item.OrdersId = orderSplit.OrdersId;
                    item.Price = itemUpdate.Price;
                    item.Quantity = itemUpdate.Quantity;
                    _db.Entry(item).State = EntityState.Modified;
                }
                else if (itemUpdate.Quantity < item.Quantity)
                {
                    item.Quantity = item.Quantity - itemUpdate.Quantity;
                    _db.Entry(item).State = EntityState.Modified;
                    LinkProduct link = new LinkProduct();
                    link.Link = item.Link;
                    //Tạo order mới
                    link.OrdersId = orderSplit.OrdersId;
                    //Giá mới
                    link.Price = itemUpdate.Price;
                    link.Quantity = itemUpdate.Quantity;
                    link.RateUsdtoVnd = item.RateUsdtoVnd;
                }
                else
                {
                    return new ResultCustomModel<bool>
                    {
                        Success = false,
                        Code = 500,
                        Message = "Số lượng tách đơn phải nhỏ hơn hoặc bằng đơn gốc"
                    };
                }
            }
            await _db.SaveChangesAsync();
            //Nếu không còn link nào thì xoá luôn thằng đơn root
            bool isExistOrder = await _db.LinkProduct.AnyAsync(x => x.OrdersId == ordersId);
            if (!isExistOrder)
            {
                _db.Entry(orderRoot).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
            }
            return new ResultCustomModel<bool>
            {
                Success = true,
                Code = 200,
                Data = isExistOrder,
                Message = "Tách đơn thành công với mã đơn mới là " + orderSplit.OrdersId
            };
        }

        public async Task<Users> GetInfoCustomer(int usersId)
        {
            var data = await _db.Users.FirstOrDefaultAsync(x => x.UsersId == usersId);
            return data;
        }
    }
    public interface IOrderServices
    {
        Task<ResultCustomModel<InitCreateOrders>> GetInfoCreateOrder();
        Task CreateOrder(CreateOrdersModel orders);
        Task UpdateOrder(CreateOrdersModel orders);
        Task<ResultCustomModel<bool>> SplitOrder(int ordersId, List<SplitOrdersModel> orders);
        Task<Users> GetInfoCustomer(int usersId);
    }
}
