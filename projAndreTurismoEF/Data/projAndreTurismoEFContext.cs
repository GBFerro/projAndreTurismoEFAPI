using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projAndreTurismoEF.Models;

namespace projAndreTurismoEF.Data
{
    public class projAndreTurismoEFContext : DbContext
    {
        public projAndreTurismoEFContext (DbContextOptions<projAndreTurismoEFContext> options)
            : base(options)
        {
        }

        public DbSet<projAndreTurismoEF.Models.City> City { get; set; } = default!;

        public DbSet<projAndreTurismoEF.Models.Address>? Address { get; set; }

        public DbSet<projAndreTurismoEF.Models.Client>? Client { get; set; }

        public DbSet<projAndreTurismoEF.Models.Hotel>? Hotel { get; set; }

        public DbSet<projAndreTurismoEF.Models.Ticket>? Ticket { get; set; }

        public DbSet<projAndreTurismoEF.Models.Package>? Package { get; set; }
    }
}
