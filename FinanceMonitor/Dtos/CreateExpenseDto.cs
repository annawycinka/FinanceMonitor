using System;
using System.ComponentModel.DataAnnotations;
using FinanceMonitor.Models;

namespace FinanceMonitor.Dtos
{
    public class CreateExpenseDto
    {
        [Required] public decimal Value { get; set; }

        public ExpenseCategory Category { get; set; }

        [Required] public DateTime OccurredAt { get; set; }
    }
}