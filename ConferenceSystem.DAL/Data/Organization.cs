using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConferencySystem.DAL.Data.UserIdentity;

namespace ConferencySystem.DAL.Data
{
    public class Organization
    {
        public int Id { get; set; }

        public long IN { get; set; }

        public string VATID { get; set; }

        [Index]
        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(300)]
        public string BillStreet { get; set; }

        [StringLength(300)]
        public string Town { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        public virtual ICollection<AppUser> People { get; set; }

        public Organization()
        {
            People = new List<AppUser>();
        }
    }
}