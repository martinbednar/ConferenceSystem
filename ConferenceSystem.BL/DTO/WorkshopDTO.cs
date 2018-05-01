using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferencySystem.BL.DTO
{
    public class WorkshopDTO
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Presenter { get; set; }

        [StringLength(31)]
        public string Room { get; set; }

        public int Capacity { get; set; }

        public int NumberOfRegistered { get; set; }

        public bool Registered { get; set; }

        public virtual WorkshopsBlockDTO WorkshopsBlock { get; set; }

        public virtual IEnumerable<AppUserDTO> Users { get; set; }
    }
}