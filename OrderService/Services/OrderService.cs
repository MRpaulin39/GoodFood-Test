using Microsoft.AspNetCore.Mvc;
using OrderService.Insfrastructure.DataBase;
using OrderService.Insfrastructure.DataLayer;
using OrderModel = OrderService.Models.OrderHeader;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using OrderService.Models;

namespace OrderService.Services
{
    public class OrderService : Order.OrderBase
    {
        #region Propriétés
        private readonly OrderDataLayer _orderDataLayer;
        #endregion

        #region Constructeur
        public OrderService(DefaultDbContext context)
        {
            _orderDataLayer = new(context);
        }
        #endregion

        #region Méthodes publiques
        public override async Task<OrderReply> AddOne(OrderRequest request, ServerCallContext context)
        {
            OrderModel order = new()
            {
                IdPicture = Guid.Parse(request.IdPicture),
                IdOrderDelivery = Guid.Parse(request.IdOrderDelivery),
                IdUser = Guid.Parse(request.IdUser),
                OrderDate = DateTime.Now
            };

            order = _orderDataLayer.AddOrderHeader(order);

            return new()
            {
                IdOrder = order.IdOrder.ToString(),
                IdPicture = order.IdPicture.ToString(),
                IdOrderDelivery = order.IdOrderDelivery.ToString(),
                IdUser = order.IdUser.ToString(),
                OrderDate = new Timestamp() { Seconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds() }
            };
        }

        public override async Task<OrdersReply> GetAll(OrderQuery request, ServerCallContext callContext)
        {
            List<OrderModel> ordersList = await _orderDataLayer.GetAll();
            List<OrderReply> orderReplies = ordersList.Select(item => new OrderReply
            {
                IdOrder = item.IdOrder.ToString(),
                IdPicture = item.IdPicture.ToString(),
                IdOrderDelivery = item.IdOrderDelivery.ToString(),
                IdUser = item.IdUser.ToString(),
                OrderDate = new Timestamp() { Seconds = DateTimeOffset.FromFileTime(item.OrderDate.ToFileTimeUtc()).ToUnixTimeSeconds() }
            }).ToList();

            return new OrdersReply
            {
                Orders = { orderReplies }
            };
        }

        public override async Task<OrderReply> GetOne(OrderQuery request, ServerCallContext context)
        {
            OrderModel orderHeader = await _orderDataLayer.GetOne(Guid.Parse(request.IdOrder));

            return new OrderReply()
            {
                IdOrder = orderHeader.IdOrder.ToString(),
                IdPicture = orderHeader.IdPicture.ToString(),
                IdOrderDelivery = orderHeader.IdOrderDelivery.ToString(),
                IdUser = orderHeader.IdUser.ToString(),
                OrderDate = new Timestamp() { Seconds = DateTimeOffset.FromFileTime(orderHeader.OrderDate.ToFileTimeUtc()).ToUnixTimeSeconds() }
            };
        }        
        #endregion
    }
}
