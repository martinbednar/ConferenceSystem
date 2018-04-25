using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferencySystem.DAL.Data
{
    public class WorkshopsBlock
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public virtual ICollection<Workshop> Workshops { get; set; }

        public WorkshopsBlock ()
        {
            Workshops = new List<Workshop>();
        }
    }
}