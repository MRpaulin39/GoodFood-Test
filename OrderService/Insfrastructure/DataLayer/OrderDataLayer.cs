using Microsoft.EntityFrameworkCore;
using OrderService.Insfrastructure.DataBase;
using OrderService.Models;

namespace OrderService.Insfrastructure.DataLayer
{
    public class OrderDataLayer
    {
        private readonly DefaultDbContext _context = null;

        public OrderDataLayer(DefaultDbContext context)
        {
            _context = context;
        }

        public List<OrderHeader> GetAll()
        {
            return _context.OrderHeader.ToList();
        }

        public string AddOrderHeader(OrderHeader orderHeader)
        {
            OrderHeader newOrderHeader = new()
            {
                IdOrderDelivery = Guid.NewGuid(),
                IdPicture = Guid.NewGuid(),
                IdUser = Guid.NewGuid(),
                OrderDate = DateTime.Now,
            };

            _context.OrderHeader.Add(newOrderHeader);
            _context.SaveChanges();

            return $"Commande {newOrderHeader.IdOrder} ajoutée !";
        }
    }
}
