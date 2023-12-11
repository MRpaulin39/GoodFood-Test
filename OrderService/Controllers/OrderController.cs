using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("Orders")]
    public class OrderController : Controller
    {
        #region Propriétés

        #endregion

        #region Constructeur

        #endregion

        #region Méthodes publiques
        [HttpGet("Hi")]
        public async Task<ActionResult<string>> ImAlive()
        {
            return "I'm alive !";
        }
        #endregion

        #region Méthodes privées

        #endregion
    }
}
