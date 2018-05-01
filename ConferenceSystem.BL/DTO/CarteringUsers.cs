using System.Collections.Generic;
using System.Linq;

namespace ConferencySystem.BL.DTO
{
    public class CarteringUsers
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public IEnumerable<AppUserDTO> Users { get; set; }

        public CarteringUsers() { }

        public CarteringUsers(string name, IEnumerable<AppUserDTO> users)
        {
            Name = name;
            Count = users.Count();
            Users = users;
        }
    }
}