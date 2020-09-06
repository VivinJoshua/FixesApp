using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FixesApp.Models
{
    public class FixesAppContext : DbContext
    {
        public FixesAppContext() : base("name=ConnectionString") { }

        public DbSet<Services> Services { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<Work> Work { get; set; }
        public DbSet<WorkDetails> WorkDetails { get; set; }
        public DbSet<Feedback> Feedback { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkDetails>()
            .Property(f => f.RequestDT)
            .HasColumnType("datetime2");

            modelBuilder.Entity<WorkDetails>()
           .Property(f => f.WorkdoneDT)
           .HasColumnType("datetime2");

            modelBuilder.Entity<WorkDetails>()
            .HasOptional<Worker>(c => c.Worker)
            .WithMany()
            .WillCascadeOnDelete(false);

           modelBuilder.Entity<Feedback>()
               .HasOptional(s => s.Works) // Mark Address property optional in Student entity
               .WithRequired(ad => ad.Feedbacks);


            modelBuilder.Entity<Services>()
             .HasKey(x => x.ServicesId);
            modelBuilder.Entity<Services>()
               .Property(x => x.ServicesId)
               .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
        }

    }
}