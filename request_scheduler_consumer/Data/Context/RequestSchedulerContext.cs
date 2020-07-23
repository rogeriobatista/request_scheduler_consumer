using System;
using Microsoft.EntityFrameworkCore;
using request_scheduler_consumer.Domain.MauticForms.Entities;
using request_scheduler_consumer.Generics.Http.Enums;

namespace request_scheduler_consumer.Data.Context
{
    public class RequestSchedulerContext : DbContext
    {
        public DbSet<MauticForm> MauticFormRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=requestscheduler;Pooling=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MauticForm>()
                .Property(p => p.HttpMethod).HasDefaultValue(HttpMethod.Post);
        }
    }
}
