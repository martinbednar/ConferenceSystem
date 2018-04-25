using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferencySystem.BL.DTO
{
    public class WorkshopsBlockDTO
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool AnyWorkshopRegistered { get; set; }

        public virtual IEnumerable<WorkshopDTO> Workshops { get; set; }
    }
}