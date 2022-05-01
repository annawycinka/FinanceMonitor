using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceMonitor.Data
{
    public class FinanceContext : DbContext
    {
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options)
        {
        }


        public DbSet<Income> Incomes { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Expense>()
                .Property(e => e.Category)
                .HasConversion(
                    v => v.ToString(),
                    v => (ExpenseCategory)Enum.Parse(typeof(ExpenseCategory), v));

            modelBuilder
                .Entity<Income>()
                .Property(e => e.Category)
                .HasConversion(
                    v => v.ToString(),
                    v => (IncomeCategory)Enum.Parse(typeof(IncomeCategory), v));
        }
    }
}