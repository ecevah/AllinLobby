using AllinLobby.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DataAccess.Context
{
    public class AllinLobbyContext : IdentityDbContext<Client, AppRole, int>
    {
        public AllinLobbyContext(DbContextOptions<AllinLobbyContext> options) : base(options)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<BookingEvent> BookingEvents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CleaningRoom> CleaningRooms { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Hotel relationships
            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.CleaningRooms)
                .WithOne(cr => cr.Hotel)
                .HasForeignKey(cr => cr.HotelId);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Comments)
                .WithOne(c => c.Hotel)
                .HasForeignKey(c => c.HotelId);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Complaints)
                .WithOne(c => c.Hotel)
                .HasForeignKey(c => c.HotelId);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Events)
                .WithOne(e => e.Hotel)
                .HasForeignKey(e => e.HotelId);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Foods)
                .WithOne(f => f.Hotel)
                .HasForeignKey(f => f.HotelId);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Orders)
                .WithOne(o => o.Hotel)
                .HasForeignKey(o => o.HotelId);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Packages)
                .WithOne(p => p.Hotel)
                .HasForeignKey(p => p.HotelId);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Reservations)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId);

            // Client relationships
            modelBuilder.Entity<Client>()
                .Property(c => c.ClientId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Client>()
                .HasOne(c => c.Subscription)
                .WithMany(s => s.Clients)
                .HasForeignKey(c => c.SubscriptionId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.BookingEvents)
                .WithOne(be => be.Client)
                .HasForeignKey(be => be.ClientId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Comments)
                .WithOne(co => co.Client)
                .HasForeignKey(co => co.ClientId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Complaints)
                .WithOne(co => co.Client)
                .HasForeignKey(co => co.ClientId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Guests)
                .WithOne(g => g.Client)
                .HasForeignKey(g => g.ClientId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Client)
                .HasForeignKey(o => o.ClientId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Reservations)
                .WithOne(r => r.Client)
                .HasForeignKey(r => r.ClientId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Sessions)
                .WithOne(s => s.Client)
                .HasForeignKey(s => s.ClientId);

            // Room relationships
            modelBuilder.Entity<Room>()
               .HasOne(r => r.Hotel)
               .WithMany(h => h.Rooms)
               .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.CleaningRooms)
                .WithOne(cr => cr.Room)
                .HasForeignKey(cr => cr.RoomId);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.Comments)
                .WithOne(c => c.Room)
                .HasForeignKey(c => c.RoomId);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.Complaints)
                .WithOne(c => c.Room)
                .HasForeignKey(c => c.RoomId);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.Reservations)
                .WithOne(res => res.Room)
                .HasForeignKey(res => res.RoomId);

            // Order relationships
            modelBuilder.Entity<Order>()
               .HasOne(o => o.Client)
               .WithMany(c => c.Orders)
               .HasForeignKey(o => o.ClientId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Hotel)
                .WithMany(h => h.Orders)
                .HasForeignKey(o => o.HotelId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Reservation)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.ReservationId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            // Food relationships
            modelBuilder.Entity<Food>()
               .HasOne(f => f.Hotel)
               .WithMany(h => h.Foods)
               .HasForeignKey(f => f.HotelId);

            modelBuilder.Entity<Food>()
                .HasOne(f => f.FoodCategory)
                .WithMany(fc => fc.Foods)
                .HasForeignKey(f => f.FoodCategoryId);

            modelBuilder.Entity<Food>()
                .HasMany(f => f.OrderDetails)
                .WithOne(od => od.Food)
                .HasForeignKey(od => od.FoodId);

            // Event relationships
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Hotel)
                .WithMany(h => h.Events)
                .HasForeignKey(e => e.HotelId);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.BookingEvents)
                .WithOne(be => be.Event)
                .HasForeignKey(be => be.EventId);

            // Reservation relationships
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.ClientId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(ro => ro.Reservations)
                .HasForeignKey(r => r.RoomId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Reservations)
                .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Comments)
                .WithOne(c => c.Reservation)
                .HasForeignKey(c => c.ReservationId);

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Guests)
                .WithOne(g => g.Reservation)
                .HasForeignKey(g => g.ReservationId);

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Orders)
                .WithOne(o => o.Reservation)
                .HasForeignKey(o => o.ReservationId);

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Sessions)
                .WithOne(s => s.Reservation)
                .HasForeignKey(s => s.ReservationId);

            // Session relationships
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Client)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.ClientId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Reservation)
                .WithMany(r => r.Sessions)
                .HasForeignKey(s => s.ReservationId);

            modelBuilder.Entity<Session>()
                .HasMany(s => s.Messages)
                .WithOne(m => m.Session)
                .HasForeignKey(m => m.SessionId);

            // Subscription relationships
            modelBuilder.Entity<Subscription>()
                .HasMany(s => s.Clients)
                .WithOne(c => c.Subscription)
                .HasForeignKey(c => c.SubscriptionId);

            // FoodCategory relationship
            modelBuilder.Entity<FoodCategory>()
                .HasMany(fc => fc.Foods)
                .WithOne(f => f.FoodCategory)
                .HasForeignKey(f => f.FoodCategoryId);
        }
    }
}
