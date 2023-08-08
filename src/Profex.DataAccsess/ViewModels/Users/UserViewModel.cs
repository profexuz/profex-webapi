﻿using Profex.Domain.Entities;

namespace Profex.DataAccsess.ViewModels.Users
{
    public class UserViewModel : Auditable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool PhoneNumberConfirmed { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}
