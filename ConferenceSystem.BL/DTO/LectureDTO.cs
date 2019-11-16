using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConferencySystem.DAL.Data.UserIdentity;

namespace ConferencySystem.BL.DTO
{
    public class LectureDTO
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        [MaxLength(50, ErrorMessage = "Byla překročena maximální délka typu vystoupení - maximální délka pole \"Typ vystuopení (přednáška, workshop, seminář)\" je 50 znaků.")]
        public string Type { get; set; }

        [MaxLength(200, ErrorMessage = "Byla překročena maximální délka názvu - maximální délka pole \"Název\" je 200 znaků.")]
        public string Name { get; set; }

        [MaxLength(4000, ErrorMessage = "Byla překročena maximální délka anotace - maximální délka pole \"Anotace\" je 4000 znaků.")]
        public string Annotation { get; set; }

        public byte[] Presentation { get; set; }

        [MaxLength(200, ErrorMessage = "Byla překročena maximální délka názvu souboru (prezentace) - maximální délka pole \"Název souboru (prezentace)\" je 200 znaků.")]
        public string PresentationName { get; set; }

        [MaxLength(4000, ErrorMessage = "Byla překročena maximální délka odkazu na prezentaci - maximální délka pole \"Odkaz na prezentaci\" je 4000 znaků.")]
        public string PresentationLink { get; set; }

        public bool Microphone { get; set; }

        public bool Flipchart { get; set; }

        [MaxLength(4000, ErrorMessage = "Byla překročena maximální délka požadavků na prostor - maximální délka pole \"Požadavky na prostor\" je 4000 znaků.")]
        public string PlaceRequirements { get; set; }





        [MaxLength(400, ErrorMessage = "Byla překročena maximální délka cíle programu - maximální délka pole \"Cíl programu\" je 400 znaků.")]
        public string Goal { get; set; }

        [MaxLength(400, ErrorMessage = "Byla překročena maximální délka co si účastníci odnesou - maximální délka pole \"Co si účastníci odnesou/dostanou\" je 400 znaků.")]
        public string Given { get; set; }

        public bool Chairs { get; set; }

        public bool Carpet { get; set; }

        public bool Tables { get; set; }

        public bool Notebook { get; set; }

        public bool Dataprojector { get; set; }

        [MaxLength(15, ErrorMessage = "Byla překročena maximální délka názvu grafického portu vlastního notebooku - maximální délka pole \"Grafický port vlastního notebooku (HDMI, VGA, DisplayPort)\" je 15 znaků.")]
        public string NotebookPort { get; set; }

        public bool Speakers { get; set; }

        public bool WorklistsCopies { get; set; }

        public byte[] Worklist { get; set; }

        [StringLength(200)]
        public string WorklistName { get; set; }

        [StringLength(4000)]
        public string WorklistLink { get; set; }

        [StringLength(4000)]
        public string EquipmentRequirements { get; set; }
    }
}