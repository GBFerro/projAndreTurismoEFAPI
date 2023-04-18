﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Address From { get; set; }
        public Address To { get; set; }
        public Client Client { get; set; }
        public decimal Value { get; set; }
        public DateTime DateTrip { get; set; }
    }
}