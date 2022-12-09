using System;
using System.ComponentModel.DataAnnotations;
using FinanceMonitor.Models;

namespace FinanceMonitor.Dtos
{
    public class UpdateExpenseDto
    {
        [Required] public decimal Value { get; set; }

        public ExpenseCategory Category { get; set; }

        [Required] public DateTime OccurredAt { get; set; }
    }
}