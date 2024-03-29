﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceMonitor.Models
{
    public class Income
    {
        [Key] public int Id { get; set; }

        [Required] public decimal Value { get; set; }

        public IncomeCategory Category { get; set; }

        [Required] public DateTime OccurredAt { get; set; }

    }
}