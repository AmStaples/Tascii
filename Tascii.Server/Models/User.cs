﻿using System.ComponentModel.DataAnnotations;

namespace Tascii.Server.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }

    }
}
