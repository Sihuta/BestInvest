﻿using System.ComponentModel.DataAnnotations;

namespace BestInvest.API.Entities
{
    public class Deal
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public int ProjectId { get; set; }

        [Required]
        public decimal MoneyCapital { get; set; }

        [Required]
        public string State { get; set; }

        //
        public Account Account { get; set; }
        public Project Project { get; set; }
    }
}
