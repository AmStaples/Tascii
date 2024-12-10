using Microsoft.EntityFrameworkCore;
namespace Tascii.Server.Models
{
    public class TasciiDBContext(DbContextOptions<TasciiDBContext> options) : DbContext(options)
    {
        public required DbSet<User> Users {  get; set; }
        
        public required DbSet<Note> Notes { get; set; }

        public required DbSet<Boards> Boards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "user1",
                    Password = "password1"
                },
                new User
                {
                    Id =2,
                    Name = "user2",
                    Password = "password2"
                }
                );

            modelBuilder.Entity<Note>().HasData(
                new Note
                {
                    Id =  1,
                    content = "Note1 for User1 Board1",
                    xCoord = 10,
                    yCoord = 10,
                    BoardId = 1
                },
                new Note
                {
                    Id = 2,
                    content = "Note2 for User1 Board1",
                    xCoord = -10,
                    yCoord = -10,
                    BoardId = 1
                },
                new Note
                {
                    Id = 3,
                    content = "Note1 for User1 Board2",
                    xCoord = -10,
                    yCoord = -10,
                    BoardId = 2
                },
                new Note
                {
                    Id = 4,
                    content = "Note2 for User1 Board2",
                    xCoord = 10,
                    yCoord = 10,
                    BoardId = 2
                },
                new Note
                {
                    Id = 5,
                    content = "Note2 for User2 Board1",
                    xCoord = -10,
                    yCoord = -10,
                    BoardId = 3
                }, new Note
                {
                    Id = 6,
                    content = "Note1 for User2 Board1",
                    xCoord = 10,
                    yCoord = 10,
                    BoardId = 3
                }, new Note
                {
                    Id = 7,
                    content = "Note2 for User2 Board2",
                    xCoord = -10,
                    yCoord = -10,
                    BoardId = 4
                }, new Note
                {
                    Id = 8,
                    content = "Note1 for User2 Board2",
                    xCoord = 10,
                    yCoord = 10,
                    BoardId = 4
                }
                );
            modelBuilder.Entity<Boards>().HasData(
                new Boards
                {
                    Id = 1,
                    Name = "Board1",
                    OwnerId = 1,
                },
                new Boards
                {
                    Id = 2,
                    Name = "Board2",
                    OwnerId = 1,
                },
                new Boards
                {
                    Id = 3,
                    Name = "Board1",
                    OwnerId = 2,
                },
                new Boards
                {
                    Id = 4,
                    Name = "Board1",
                    OwnerId = 2,
                }
                );
        }

    }
}
