using System.ComponentModel.DataAnnotations;

namespace ConferencySystem.BL.DTO
{
    public class OrganizationDTO
    {
        public int Id { get; set; }

        public long IN { get; set; }

        public string VATID { get; set; }

        [MaxLength(300, ErrorMessage = "Byla překročena maximální délka názvu Vaší organizace - maximální délka pole \"Název organizace\" je 300 znaků.")]
        public string Name { get; set; }

        [MaxLength(300, ErrorMessage = "Byla překročena maximální délka ulice a č.p. u Vaší organizace - maximální délka pole \"Ulice a číslo popisné\" je 300 znaků.")]
        public string BillStreet { get; set; }
        
        [MaxLength(300, ErrorMessage = "Byla překročena maximální délka jména obce u Vaší organizace - maximální délka pole \"Obec\" je 300 znaků.")]
        public string Town { get; set; }
        
        [MaxLength(10, ErrorMessage = "Byla překročena maximální délka PSČ u Vaší organizace - maximální délka pole \"PSČ\" je 10 znaků.")]
        public string PostalCode { get; set; }
    }
}