﻿using System.ComponentModel.DataAnnotations;

namespace FahasaStoreAPI.Models
{
    public class SignInModel
    {
        public SignInModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
