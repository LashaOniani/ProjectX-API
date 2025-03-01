﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person
    {
        public int? Id { get; set; }
        public int? R_id { get; set; }
        public string FirstName {get; set;}
        public string LastName { get; set; }
        public int Gender {get; set;}
        public string GenderStr { get; set;}
        public DateTime Birthday {get; set;}
        public string Email {get; set;} 
        public string Phone {get; set;}
        public string Geolocation {get; set;}
        public string Ip {get; set;}
    }
}
