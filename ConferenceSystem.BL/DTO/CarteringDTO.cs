using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferencySystem.BL.DTO
{
    public class CarteringDTO
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime When { get; set; }

        [StringLength(255)]
        public string Category { get; set; }

        public virtual IEnumerable<AppUserDTO> Users { get; set; }
    }
}