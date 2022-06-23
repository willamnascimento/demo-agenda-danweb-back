using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Data.Mappings
{
    public class CalendarSpecificationsMapping : IEntityTypeConfiguration<CalendarSpecifications>
    {
        public void Configure(EntityTypeBuilder<CalendarSpecifications> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.SpecificationId)
                .IsRequired();

            builder.Property(c => c.CalendarId)
                .IsRequired();

            builder.Property(c => c.Active)
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}
