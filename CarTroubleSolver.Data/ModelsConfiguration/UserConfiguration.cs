﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Data.ModelsConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Id).IsUnique();

            builder.HasMany(u => u.Accident)
                .WithOne(a => a.Applicant)
                .HasForeignKey(a => a.ApplicantUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
