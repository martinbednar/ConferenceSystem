using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ConferencySystem.DAL.Data.UserIdentity;

namespace ConferencySystem.DAL.Data
{
    public class Cartering
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime When { get; set; }

        [StringLength(255)]
        public string Category { get; set; }

        public virtual ICollection<AppUser> User { get; set; }

        public Cartering()
        {
            User = new List<AppUser>();
        }
    }

    
}