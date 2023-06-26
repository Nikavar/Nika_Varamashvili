using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
	public class TabConfiguration : IEntityTypeConfiguration<Tab>
	{
		public void Configure(EntityTypeBuilder<Tab> builder)
		{
			builder.ToTable("TabsTable");

			builder.HasKey(x => x.Id);
			builder.Property(t => t.TabName)
				.IsUnicode(false);

			builder.Property(x => x.ParentId)
				.HasColumnName("ParentId");

		}
	}
}
