﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{

    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }


        public IEnumerable<string> Roles { get; set;}

       
    }
}
