using Backend_Platform.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<CommunityPrintRequest> CommunityPrintRequests { get; set; }
        public DbSet<ConstructionFile> ConstructionFiles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<User> Users { get; set; }



    }
}
