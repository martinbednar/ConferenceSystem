using System.Collections.Generic;
using System.Linq;

namespace ConferencySystem.BL.DTO
{
    public class CarteringPeople
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public IEnumerable<AppUserDTO> People { get; set; }

        public CarteringPeople() { }

        public CarteringPeople(string name, IEnumerable<AppUserDTO> people)
        {
            Name = name;
            Count = people.Count();
            People = people;
        }
    }
}