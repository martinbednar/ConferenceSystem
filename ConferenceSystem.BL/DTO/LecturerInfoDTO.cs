
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferencySystem.BL.DTO
{
    public class LecturerInfoDTO
    {
        public int Id { get; set; }

        [MaxLength(4000, ErrorMessage = "Byla překročena maximální délka medailonku - maximální délka pole \"Medailonek\" je 4000 znaků.")]
        public string Introduce { get; set; }

        public byte[] Photo { get; set; }

        [MaxLength(4000, ErrorMessage = "Byla překročena maximální délka odkazu na fotku - maximální délka pole \"Odkaz na fotku\" je 4000 znaků.")]
        public string PhotoLink { get; set; }

        public long IN { get; set; }

        [MaxLength(300, ErrorMessage = "Byla překročena maximální délka ulice a č.p. Vašeho bydliště - maximální délka pole \"Ulice a číslo popisné\" je 300 znaků.")]
        public string Street { get; set; }

        [MaxLength(300, ErrorMessage = "Byla překročena maximální délka jména obce Vašeho bydliště - maximální délka pole \"Obec\" je 300 znaků.")]
        public string Town { get; set; }

        [MaxLength(10, ErrorMessage = "Byla překročena maximální délka PSČ Vašeho bydliště - maximální délka pole \"PSČ\" je 10 znaků.")]
        public string PostalCode { get; set; }

        [MaxLength(50, ErrorMessage = "Byla překročena maximální délka čísla Vašeho bankovního účtu - maximální délka pole \"Číslo účtu\" je 50 znaků.")]
        public string AccountNumber { get; set; }

        [MaxLength(200, ErrorMessage = "Chyba při zadávání ubytování v délce výsledného řetězce ukládaného do databáze. Maximální délka řetězce je 200 znaků.")]
        public string Accomodation { get; set; }

        [MaxLength(400, ErrorMessage = "Byla překročena maximální délka jména Vašeho spolubydlícího - maximální délka pole \"Spolubydlící\" je 400 znaků.")]
        public string RoomMate { get; set; }
    }
}