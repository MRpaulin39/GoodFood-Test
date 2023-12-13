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

            //Permet de créer la base de données si non existante
            _context.Database.EnsureCreated();
        }

        public List<OrderHeader> GetAll()
        {
            return _context.OrderHeader.ToList();
        }

        public OrderHeader GetOne(Guid idOrderHeader)
        {
            return _context.OrderHeader.Where(item => item.IdOrder == idOrderHeader).Single();
        }

        public OrderHeader AddOrderHeader(OrderHeader orderHeader)
        {
            _context.OrderHeader.Add(orderHeader);
            _context.SaveChanges();

            return orderHeader;
        }
    }
}
