using Microsoft.AspNetCore.Mvc;
using OrderService.Insfrastructure.DataBase;
using OrderService.Insfrastructure.DataLayer;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("Orders")]
    public class OrderController : Controller
    {
        #region Propriétés
        private readonly OrderDataLayer _orderDataLayer;
        #endregion

        #region Constructeur
        public OrderController(DefaultDbContext context)
        {
            _orderDataLayer = new(context);   
        }
        #endregion

        #region Méthodes publiques
        [HttpGet("Hi")]
        public async Task<ActionResult<string>> ImAlive()
        {
            return "I'm alive !";
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<OrderHeader>>> GetAll()
        {
            return _orderDataLayer.GetAll();
        }

        [HttpPost("AddOne")]
        public async Task<ActionResult> AddOne(OrderHeader orderHeader)
        {
            _orderDataLayer.AddOrderHeader(orderHeader);

            return Created();
        }
        #endregion

        #region Méthodes privées

        #endregion
    }
}
