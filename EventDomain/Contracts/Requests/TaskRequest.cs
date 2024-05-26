﻿using System.ComponentModel.DataAnnotations;

namespace EventDomain.Contracts.Requests
{
    public class TaskRequest
    {
        [Required]
        public required string Title { get; set; }

        public DateTime? ScheduledTime { get; set; }

        [Required]
        public required string Description { get; set; }
    }
}