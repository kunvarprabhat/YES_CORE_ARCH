﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace YES.Dtos
{
#nullable disable
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PasswordHash { get; set; }
        public bool PhoneNumberConfirmed { get; set; } 
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
