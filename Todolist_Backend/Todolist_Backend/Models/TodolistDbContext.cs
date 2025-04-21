using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models.Entities;

namespace Todolist_Backend.Models
{
    public class TodolistDbContext : DbContext
    {
        // Code First Migration 資料庫 context
        public TodolistDbContext(DbContextOptions<TodolistDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<ResetToken> ResetTokens { get; set; }
    }
}
