using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Todolist_Backend.Models
{
    public class TodolistDbContext : DbContext
    {
        public TodolistDbContext(DbContextOptions<TodolistDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<ResetToken> ResetTokens { get; set; }
    }
}
