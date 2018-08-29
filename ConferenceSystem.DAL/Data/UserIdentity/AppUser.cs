using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConferencySystem.DAL.Data.UserIdentity
{
    public class AppUser : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public DateTime RegisterTimestamp { get; set; }

        [Index]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Index]
        [StringLength(200)]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(200)]
        public string BirthPlace { get; set; }

        [StringLength(30)]
        public string TitleBefore { get; set; }

        [StringLength(30)]
        public string TitleAfter { get; set; }

        [StringLength(200)]
        public string Position { get; set; }

        public bool Agreement { get; set; }

        public DateTime? PaidDate { get; set; }

        public int VariableSymbol { get; set; }

        [StringLength(1000)]
        public string NoteUser { get; set; }

        [StringLength(1000)]
        public string NoteAdmin { get; set; }

        public bool WantCert { get; set; }

        public bool IsAlternate { get; set; }

        [StringLength(30)]
        public string InvoiceNumber { get; set; }

        public bool WasEmailWorkshopSent { get; set; }

        public bool WasEmailCarteringSent { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual ICollection<Cartering> Cartering { get; set; }

        public virtual ICollection<Workshop> Workshops { get; set; }

        public AppUser()
        {
            Cartering = new List<Cartering>();
            Workshops = new List<Workshop>();
        }
    }
}