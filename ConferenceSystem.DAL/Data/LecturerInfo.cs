using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConferencySystem.DAL.Data.UserIdentity;

namespace ConferencySystem.DAL.Data
{
    public class LecturerInfo
    {
        public int Id { get; set; }

        [StringLength(4000)]
        public string Introduce { get; set; }

        public byte[] Photo { get; set; }

        [StringLength(300)]
        public string PhotoName { get; set; }

        [StringLength(4000)]
        public string PhotoLink { get; set; }

        public long IN { get; set; }

        [StringLength(300)]
        public string Street { get; set; }

        [StringLength(300)]
        public string Town { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [StringLength(200)]
        public string Accomodation { get; set; }

        [StringLength(400)]
        public string RoomMate { get; set; }

        [StringLength(100)]
        public string Fee { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }

        public LecturerInfo()
        {
            Lectures = new List<Lecture>();
        }
    }
}