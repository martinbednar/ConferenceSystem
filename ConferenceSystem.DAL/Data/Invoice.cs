using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConferencySystem.DAL.Data.UserIdentity;

namespace ConferencySystem.DAL.Data
{
    public class Invoice
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string FileName { get; set; }

        public byte[] FileBytes { get; set; }
    }
}