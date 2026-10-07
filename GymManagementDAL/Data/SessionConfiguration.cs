using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Data
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("SessionCapacity", "Capacity Between 1 and 25");
                tb.HasCheckConstraint("SessionEndDate", "StartTime < EndTime");
            });

            builder.HasOne(x => x.Category)
                .WithMany(x => x.CategorySessions)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.Trainer)
                .WithMany(x => x.TrainerSessions)
                .HasForeignKey(x => x.TrainerId);
        }
    }
}
