using System.ComponentModel.DataAnnotations;

namespace ConferencySystem.BL.DTO
{
    public class OrganizationDTO
    {
        public int Id { get; set; }
        
        public long IN { get; set; }

        public string VATID { get; set; }

        [Required(ErrorMessage = "Vyplňte název organizace u fakturačních údajů - pole \"Název organizace\" je povinné.")]
        [MaxLength(300, ErrorMessage = "Byla překročena maximální délka názvu Vaší organizace - maximální délka pole \"Název organizace\" je 300 znaků.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vyplňte ulici a číslo popisné u fakturačních údajů - pole \"Ulice a číslo popisné\" je povinné.")]
        [MaxLength(300, ErrorMessage = "Byla překročena maximální délka ulice a č.p. u Vaší organizace - maximální délka pole \"Ulice a číslo popisné\" je 300 znaků.")]
        public string BillStreet { get; set; }

        [Required(ErrorMessage = "Vyplňte obec u fakturačních údajů - pole \"Obec\" je povinné.")]
        [MaxLength(300, ErrorMessage = "Byla překročena maximální délka jména obce u Vaší organizace - maximální délka pole \"Obec\" je 300 znaků.")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Vyplňte PSČ u fakturačních údajů - pole \"PSČ\" je povinné.")]
        [MaxLength(10, ErrorMessage = "Byla překročena maximální délka PSČ u Vaší organizace - maximální délka pole \"PSČ\" je 10 znaků.")]
        public string PostalCode { get; set; }
    }
}