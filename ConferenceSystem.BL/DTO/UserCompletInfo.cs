using System.Linq;

namespace ConferencySystem.BL.DTO
{
    public class UserCompletInfo
    {
        public AppUserDTO User { get; set; }

        public UserCartering Cartering { get; set; }

        public UserWorkshops Workshops { get; set; }

        public UserCompletInfo() { }

        public UserCompletInfo(AppUserDTO user)
        {
            User = user;

            Cartering = new UserCartering()
            {
                Id = user.Id,
                FirstName = User.FirstName,
                LastName = User.LastName,
                HasSundaySoup = (user.Cartering.Where(c => c.Id == 11).Count() == 0) ? false : true,
                SundayDinner = (user.Cartering.Where(c => ((c.Id == 1) || (c.Id == 2))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 1) || (c.Id == 2))).First().Name,
                HasSundayWine = (user.Cartering.Where(c => c.Id == 3).Count() == 0) ? false : true,
                HasMondayMorningCoffee = (user.Cartering.Where(c => c.Id == 8).Count() == 0) ? false : true,
                MondaySoup = (user.Cartering.Where(c => ((c.Id == 6) || (c.Id == 12))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 6) || (c.Id == 12))).First().Name,
                MondayLunch = (user.Cartering.Where(c => ((c.Id == 4) || (c.Id == 5))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 4) || (c.Id == 5))).First().Name,
                HasMondayAfternoonCoffee = (user.Cartering.Where(c => c.Id == 13).Count() == 0) ? false : true,
                HasMondayRaut = (user.Cartering.Where(c => c.Id == 7).Count() == 0) ? false : true,
                HasTuesdayCoffee = (user.Cartering.Where(c => c.Id == 10).Count() == 0) ? false : true
            };

            Workshops = new UserWorkshops()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Block1 = (user.Workshops.Where(w => w.WorkshopsBlock.Id == 1).Count() == 0) ? "" : user.Workshops.Where(w => w.WorkshopsBlock.Id == 1).First().Name,
                Lecture = (user.Workshops.Where(w => w.WorkshopsBlock.Id == 4).Count() == 0) ? "" : user.Workshops.Where(w => w.WorkshopsBlock.Id == 4).First().Name,
                Block2 = (user.Workshops.Where(w => w.WorkshopsBlock.Id == 2).Count() == 0) ? "" : user.Workshops.Where(w => w.WorkshopsBlock.Id == 2).First().Name,
                Block3 = (user.Workshops.Where(w => w.WorkshopsBlock.Id == 3).Count() == 0) ? "" : user.Workshops.Where(w => w.WorkshopsBlock.Id == 3).First().Name
            };
        }
    }
}