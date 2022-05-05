using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SocialNetwork.Models
{

    public interface ISocialEntitiesContext
    {
        DbSet<Country> Country { get; set; }
        DbSet<Dialog> Dialog { get; set; }
        DbSet<MessageList> MessageList { get; set; }
        DbSet<User_Dialog> User_Dialog { get; set; }
        DbSet<Users> Users { get; set; }
        int SaveChanges();
    }


    class SocialEntitiesContext : DbContext, ISocialEntitiesContext
    {
        public SocialEntitiesContext()
            : base("DBConnection")
        { }
    
        public DbSet<Country> Country { get; set; }
        public DbSet<Dialog> Dialog { get; set; }
        public DbSet<MessageList> MessageList { get; set; }
        public DbSet<User_Dialog> User_Dialog { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
