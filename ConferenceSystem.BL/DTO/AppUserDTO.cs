using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using PdfSharp.Pdf;

namespace ConferencySystem.BL.DTO
{
    public class AppUserDTO
    {
        public int Id { get; set; }

        public int SequenceNumber { get; set; }

        public DateTime RegisterTimestamp { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Vyplňte své křestní jméno - pole \"Jméno\" je povinné.")]
        [MaxLength(200, ErrorMessage = "Byla překročena maximální délka křestního jméno - maximální délka pole \"Jméno\" je 200 znaků.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Vyplňte své příjmení - pole \"Příjmení\" je povinné.")]
        [MaxLength(200, ErrorMessage = "Byla překročena maximální délka příjmení - maximální délka pole \"Příjmení\" je 200 znaků.")]
        public string LastName { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Bylo zadáno neplatné datum.")]
        public DateTime? BirthDate { get; set; }

        [MaxLength(200, ErrorMessage = "Byla překročena maximální délka místa narození - maximální délka pole \"Místo narození\" je 200 znaků.")]
        public string BirthPlace { get; set; }

        [MaxLength(30, ErrorMessage = "Byla překročena maximální délka titulu před jménem - maximální délka pole \"Titul před jménem\" je 30 znaků.")]
        public string TitleBefore { get; set; } = "";

        [MaxLength(30, ErrorMessage = "Byla překročena maximální délka titulu za jménem - maximální délka pole \"Titul za jménem\" je 30 znaků.")]
        public string TitleAfter { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte svůj email - pole \"Email\" je povinné.")]
        [EmailAddress(ErrorMessage = "Zadaná emailová adresa je ve špatném formátu. Zadejte emailovou adresu ve formátu uzivatelskejmeno@domena.koncovka")]
        [MaxLength(255, ErrorMessage = "Byla překročena maximální délka emailu - maximální délka pole \"Email\" je 255 znaků.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vyplňte své telefonní číslo - pole \"Telefonní číslo\" je povinné.")]
        [MaxLength(30, ErrorMessage = "Byla překročena maximální délka telefonního čísla - maximální délka pole \"Telefonní číslo\" je 30 znaků.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vyplňte svoji pracovní pozici - pole \"Pracovní pozice\" je povinné.")]
        [MaxLength(200, ErrorMessage = "Byla překročena maximální délka pracovní pozice - maximální délka pole \"Pracovní pozice\" je 200 znaků.")]
        public string Position { get; set; } = "Ředitel";

        [Range(typeof(bool), "true", "true", ErrorMessage = "Zaškrtněte souhlas se zpracováném osobních údajů - pole \"Souhlas se zpracovnáním osobních údajů\" je povinné.")]
        public bool Agreement { get; set; } = false;

        public DateTime? PaidDate { get; set; }

        public int VariableSymbol { get; set; }

        [MaxLength(1000, ErrorMessage = "Byla překročena maximální délka poznámky - maximální délka pole \"Poznámka\" je 1000 znaků.")]
        public string NoteUser { get; set; }

        [MaxLength(1000, ErrorMessage = "Byla překročena maximální délka poznámky - maximální délka pole \"Poznámka\" je 1000 znaků.")]
        public string NoteAdmin { get; set; }
        
        public bool WantCert { get; set; }

        public bool IsAlternate { get; set; }

        public string InvoiceNumber { get; set; }

        public string PasswordHash { get; set; }

        public virtual OrganizationDTO Organization { get; set; }

        public virtual IEnumerable<CarteringDTO> Cartering { get; set; }

        public virtual IEnumerable<WorkshopDTO> Workshops { get; set; }

        public virtual InvoiceDTO Invoice { get; set; }
    }
}