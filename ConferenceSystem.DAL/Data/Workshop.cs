using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ConferencySystem.DAL.Data.UserIdentity;

namespace ConferencySystem.DAL.Data
{
    public class Workshop
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Presenter { get; set; }

        [StringLength(31)]
        public string Room { get; set; }

        public int Capacity { get; set; }

        public virtual WorkshopsBlock WorkshopsBlock { get; set; }

        public virtual ICollection<AppUser> People { get; set; }

        public Workshop()
        {
            People = new List<AppUser>();
        }
    }
}