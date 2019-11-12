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
                FirstName = user.FirstName,
                LastName = user.LastName,
                HasSundayCoffeeBreak = (user.Cartering.Where(c => c.Id == 1).Count() == 0) ? false : true,
                HasSundaySoup = (user.Cartering.Where(c => c.Id == 2).Count() == 0) ? false : true,
                SundayDinner = (user.Cartering.Where(c => ((c.Id == 3) || (c.Id == 4))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 3) || (c.Id == 4))).First().Name,
                HasMondayAMCoffeeBreak = (user.Cartering.Where(c => c.Id == 5).Count() == 0) ? false : true,
                HasMondaySoup = (user.Cartering.Where(c => c.Id == 6).Count() == 0) ? false : true,
                MondayLunch = (user.Cartering.Where(c => ((c.Id == 7) || (c.Id == 8))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 7) || (c.Id == 8))).First().Name,
                HasMondayPMCoffeeBreak = (user.Cartering.Where(c => c.Id == 9).Count() == 0) ? false : true,
                HasMondayRaut = (user.Cartering.Where(c => c.Id == 10).Count() == 0) ? false : true,
                HasTuesdayCoffeeBreak = (user.Cartering.Where(c => c.Id == 11).Count() == 0) ? false : true,
                HasTuesdayLunch = (user.Cartering.Where(c => c.Id == 15).Count() == 0) ? false : true
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