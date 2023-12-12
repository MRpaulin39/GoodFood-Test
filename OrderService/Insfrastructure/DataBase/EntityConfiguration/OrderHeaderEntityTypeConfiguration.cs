using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Models;

namespace OrderService.Insfrastructure.DataBase.EntityConfiguration
{
    public class OrderHeaderEntityTypeConfiguration : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            //Défini la clé primaire de la BDD
            builder.HasKey(item => item.IdOrder);

            //Défini le nom de la table (Utile si le nom du model et de la table différent)
            builder.ToTable("OrderHeader");
        }
    }
}
