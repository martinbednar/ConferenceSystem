namespace ConferencySystem.BL.DTO
{
    public class UserCartering
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool HasSundayCoffeeBreak { get; set; }

        public bool HasSundaySoup { get; set; }

        public string SundayDinner { get; set; }

        public bool HasMondayAMCoffeeBreak { get; set; }

        public bool HasMondaySoup { get; set; }

        public string MondayLunch { get; set; }

        public bool HasMondayPMCoffeeBreak { get; set; }

        public bool HasMondayRaut { get; set; }

        public bool HasTuesdayCoffeeBreak { get; set; }

        public bool HasTuesdaySoup { get; set; }

        public bool HasTuesdayLunch { get; set; }
    }
}