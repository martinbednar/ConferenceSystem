namespace ConferencySystem.BL.DTO
{
    public class UserCartering
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SundaySoup { get; set; }

        public string SundayDinner { get; set; }

        public string MondaySoup { get; set; }

        public string MondayLunch { get; set; }

        public bool HasMondayRaut { get; set; }

        public bool HasTuesdaySoup { get; set; }

        public bool HasTuesdayLunch { get; set; }
    }
}