﻿using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class Adopter
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public DateTime DOB { get; set; }


        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Parish { get; set; }

    }
}
