﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TunisBrandCo.Domain.Features.Clients
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public Decimal LoyaltyPoints { get; set; }

        public Client()
        {
         
        }
    }
}
