namespace ConferencySystem.BL.DTO
{
    public class InvoiceDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string FileName { get; set; }

        public byte[] FileBytes { get; set; }
    }
}