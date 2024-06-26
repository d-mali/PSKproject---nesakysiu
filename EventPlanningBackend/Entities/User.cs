﻿using EventDomain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventBackend.Entities
{
    public class User
    {
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public UserPrivilegeType Privilege { get; set; }

        [Timestamp]
        public byte[]? Version { get; set; }
    }
}
