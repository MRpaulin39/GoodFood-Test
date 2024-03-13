using Microsoft.AspNetCore.Mvc;
using OrderService.Insfrastructure.DataBase;
using OrderService.Insfrastructure.DataLayer;
using OrderService.Models;
using OrderService.RabbitMq;
using System.Text.Json;

namespace OrderService.Controllers
{
    [ApiController]
    //[Route("Orders")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        #region Propriétés
        private readonly OrderDataLayer _orderDataLayer;
        private readonly RabbitMqProducer _rabbitMqProducer;
        #endregion

        #region Constructeur
        public OrderController(DefaultDbContext context)
        {
            _orderDataLayer = new(context);
            _rabbitMqProducer = new();
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
            return await _orderDataLayer.GetAll();
        }

        [HttpGet("GetOne/{idOrderHeader}", Name = "GetOneOrderHeader")]
        public async Task<ActionResult<OrderHeader>> GetOne(Guid idOrderHeader)
        {
            return await _orderDataLayer.GetOne(idOrderHeader);
        }

        /// <summary>
        /// Permet d'ajouter une nouvelle commande
        /// </summary>
        /// <param name="orderHeader">Objet entête de commande</param>
        /// <returns>Le nouvelle entête de commande</returns>
        /// <response code="201">La commande a été crée</response>
        [HttpPost("AddOne")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> AddOne(OrderHeader orderHeader)
        {
            orderHeader = _orderDataLayer.AddOrderHeader(orderHeader);
            _rabbitMqProducer.sendMessage(JsonSerializer.Serialize(orderHeader), "OrderCreation");

            return CreatedAtRoute("GetOneOrderHeader", new { idOrderHeader = orderHeader.IdOrder }, orderHeader);
        }
        #endregion

        #region Méthodes privées

        #endregion
    }
}
