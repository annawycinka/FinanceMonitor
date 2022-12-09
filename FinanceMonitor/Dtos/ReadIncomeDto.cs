using System;
using System.ComponentModel.DataAnnotations;
using FinanceMonitor.Models;

namespace FinanceMonitor.Dtos
{
    public class ReadIncomeDto
    {
        [Key] public int Id { get; set; }

        [Required] public decimal Value { get; set; }

        public IncomeCategory Category { get; set; }

        [Required] public DateTime OccurredAt { get; set; }
    }
}