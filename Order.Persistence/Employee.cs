using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace Order.Persistence
{
    public class Employee:IdentityUser
    {
        [Required]
        public string Name { get; set; }
    }
}
