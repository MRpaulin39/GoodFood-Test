using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Models;

namespace OrderService.Insfrastructure.DataBase.EntityConfiguration
{
    public class OrderLineEntityTypeConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            //Défini la clé primaire de la BDD
            builder.HasKey(item => item.IdOrder);

            //Défini le nom de la table (Utile si le nom du model et de la table différent)
            builder.ToTable("OrderLine");
        }
    }
}
