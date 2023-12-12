namespace OrderService.Models
{
    public class OrderHeader
    {
        /// <summary>
        /// Clé primaire
        /// </summary>
        public Guid IdOrder { get; set; }

        /// <summary>
        /// Clé étrangère de la photo de preuve de livraison
        /// </summary>
        public Guid IdPicture { get; set; }

        /// <summary>
        /// Clé étrangère de l'adresse de livraison
        /// </summary>
        public Guid IdOrderDelivery { get; set; }

        /// <summary>
        /// Clé étrangère de l'id de l'utilisateur
        /// </summary>
        public Guid IdUser { get; set; }

        /// <summary>
        /// Date de la commande
        /// </summary>
        public DateTime OrderDate { get; set; }
    }
}
