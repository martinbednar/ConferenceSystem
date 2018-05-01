namespace ConferencySystem.BL.DTO
{
    public class UserCartering
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool HasSundaySoup { get; set; }

        public string SundayDinner { get; set; }

        public bool HasSundayWine { get; set; }

        public bool HasMondayMorningCoffee { get; set; }

        public string MondaySoup { get; set; }

        public string MondayLunch { get; set; }

        public bool HasMondayAfternoonCoffee { get; set; }

        public bool HasMondayRaut { get; set; }

        public bool HasTuesdayCoffee { get; set; }
    }
}