using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class UserCredentials
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Username { get; set; }

        public int Id { get; set; }
    }
}