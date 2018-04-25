using System.Linq;

namespace ConferencySystem.BL.DTO
{
    public class PersonCompletInfo
    {
        public AppUserDTO Person { get; set; }

        public PersonCartering Cartering { get; set; }

        public PersonWorkshops Workshops { get; set; }

        public PersonCompletInfo() { }

        public PersonCompletInfo(AppUserDTO person)
        {
            Person = person;

            Cartering = new PersonCartering()
            {
                Id = person.Id,
                FirstName = Person.FirstName,
                LastName = Person.LastName,
                HasSundaySoup = (person.Cartering.Where(c => c.Id == 15).Count() == 0) ? false : true,
                SundayDinner = (person.Cartering.Where(c => ((c.Id == 1) || (c.Id == 4))).Count() == 0) ? "" : person.Cartering.Where(c => ((c.Id == 1) || (c.Id == 4))).First().Name,
                HasSundayWine = (person.Cartering.Where(c => c.Id == 5).Count() == 0) ? false : true,
                HasMondayMorningCoffee = (person.Cartering.Where(c => c.Id == 12).Count() == 0) ? false : true,
                MondaySoup = (person.Cartering.Where(c => ((c.Id == 10) || (c.Id == 16))).Count() == 0) ? "" : person.Cartering.Where(c => ((c.Id == 10) || (c.Id == 16))).First().Name,
                MondayLunch = (person.Cartering.Where(c => ((c.Id == 8) || (c.Id == 9))).Count() == 0) ? "" : person.Cartering.Where(c => ((c.Id == 8) || (c.Id == 9))).First().Name,
                HasMondayAfternoonCoffee = (person.Cartering.Where(c => c.Id == 13).Count() == 0) ? false : true,
                HasMondayRaut = (person.Cartering.Where(c => c.Id == 11).Count() == 0) ? false : true,
                HasTuesdayCoffee = (person.Cartering.Where(c => c.Id == 14).Count() == 0) ? false : true
            };

            Workshops = new PersonWorkshops()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Block1 = (person.Workshops.Where(w => w.WorkshopsBlock.Id == 1).Count() == 0) ? "" : person.Workshops.Where(w => w.WorkshopsBlock.Id == 1).First().Name,
                Lecture = (person.Workshops.Where(w => w.WorkshopsBlock.Id == 4).Count() == 0) ? "" : person.Workshops.Where(w => w.WorkshopsBlock.Id == 4).First().Name,
                Block2 = (person.Workshops.Where(w => w.WorkshopsBlock.Id == 2).Count() == 0) ? "" : person.Workshops.Where(w => w.WorkshopsBlock.Id == 2).First().Name,
                Block3 = (person.Workshops.Where(w => w.WorkshopsBlock.Id == 3).Count() == 0) ? "" : person.Workshops.Where(w => w.WorkshopsBlock.Id == 3).First().Name
            };
        }
    }
}