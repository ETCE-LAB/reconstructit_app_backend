using Backend_Platform.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<CommunityPrintRequest> CommunityPrintRequests { get; set; }
        public DbSet<ConstructionFile> ConstructionFiles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PrintContract> PrintContracts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentValue> PaymentValues { get; set; }
        public DbSet<PaymentAttribute> PaymentAttributes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // item 1 => 0..1 communityprintrequest 
            /*
            modelBuilder.Entity<CommunityPrintRequest>()
        .HasOne(cpr => cpr.Item)
        .WithOne(item => item.CommunityPrintRequest)
        .HasForeignKey<CommunityPrintRequest>(cpr => cpr.ItemId)
        .OnDelete(DeleteBehavior.Cascade);
            */
            // PrintContract 1 => 0..1 Payment
            /*
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PrintContract)
                .WithOne(p => p.Payment)
                .HasForeignKey<PrintContract>(p => p.PaymentId)
                .OnDelete(DeleteBehavior.NoAction);
            */
            /*
            modelBuilder.Entity<PrintContract>()
    .HasOne(pc => pc.Payment)
    .WithOne(p => p.PrintContract)
    .HasForeignKey<Payment>(p => p.PrintContractId)
    .OnDelete(DeleteBehavior.NoAction);
            */
            // no cascading between payment and payment value
            /*
                 modelBuilder.Entity<PaymentValue>()
         .HasOne(pv => pv.Payment)
         .WithMany(p => p.PaymentValues)
         .HasForeignKey(pv => pv.PaymentId)
         .OnDelete(DeleteBehavior.NoAction);
            */
            /*
                 modelBuilder.Entity<Payment>()
           .HasMany(p => p.PaymentValues)
           .WithOne(pv => pv.Payment)
           .HasForeignKey(pv => pv.PaymentId)
           .OnDelete(DeleteBehavior.NoAction);
            */
            /*
            modelBuilder.Entity<PaymentValue>()
    .HasOne(pv => pv.PaymentAttribute)
    .WithMany()
    .HasForeignKey(pv => pv.PaymentAttributeId)
    .OnDelete(DeleteBehavior.NoAction);
           
            */
            // 1:1 Item - CommunityPrintRequest
            modelBuilder.Entity<CommunityPrintRequest>()
                .HasOne(cpr => cpr.Item)
                .WithOne(item => item.CommunityPrintRequest)
                .HasForeignKey<CommunityPrintRequest>(cpr => cpr.ItemId)
                .OnDelete(DeleteBehavior.Cascade); // Korrekt

            

            // Payment → PaymentValue (NoAction to avoid cycles)
            modelBuilder.Entity<Payment>()
                .HasMany(p => p.PaymentValues)
                .WithOne(pv => pv.Payment)
                .HasForeignKey(pv => pv.PaymentId)
                .OnDelete(DeleteBehavior.NoAction); // ❌ keine Cascade hier

            // PrintContract → Payment (NoAction)
            modelBuilder.Entity<PrintContract>()
                .HasOne(pc => pc.Payment)
                .WithOne(p => p.PrintContract)
                .HasForeignKey<Payment>(p => p.PrintContractId)
                .OnDelete(DeleteBehavior.NoAction); // ✅

            modelBuilder.Entity<Address>()
            .HasOne(e => e.User)
            .WithOne(r => r.Address)
            .HasForeignKey<Address>(e => e.UserId)
            .IsRequired(false);

            base.OnModelCreating(modelBuilder);

        }

    }
}
