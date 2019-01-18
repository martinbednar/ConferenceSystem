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
                SundaySoup = (user.Cartering.Where(c => ((c.Id == 1) || (c.Id == 2))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 1) || (c.Id == 2))).First().Name,
                SundayDinner = (user.Cartering.Where(c => ((c.Id == 3) || (c.Id == 4))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 3) || (c.Id == 4))).First().Name,
                MondaySoup = (user.Cartering.Where(c => ((c.Id == 5) || (c.Id == 6))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 5) || (c.Id == 6))).First().Name,
                MondayLunch = (user.Cartering.Where(c => ((c.Id == 7) || (c.Id == 8))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 7) || (c.Id == 8))).First().Name,
                HasMondayRaut = (user.Cartering.Where(c => c.Id == 9).Count() == 0) ? false : true,
                HasTuesdaySoup = (user.Cartering.Where(c => c.Id == 10).Count() == 0) ? false : true,
                HasTuesdayLunch = (user.Cartering.Where(c => c.Id == 11).Count() == 0) ? false : true
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