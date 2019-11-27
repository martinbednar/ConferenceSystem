using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConferencySystem.DAL.Data.UserIdentity;

namespace ConferencySystem.DAL.Data
{
    public class Lecture
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string Annotation { get; set; }

        public byte[] Presentation { get; set; }

        [StringLength(200)]
        public string PresentationName { get; set; }

        [StringLength(4000)]
        public string PresentationLink { get; set; }

        [StringLength(50)]
        public string Microphone { get; set; }

        public bool Flipchart { get; set; }

        [StringLength(4000)]
        public string PlaceRequirements { get; set; }





        [StringLength(400)]
        public string Goal { get; set; }

        [StringLength(400)]
        public string Given { get; set; }

        public bool Chairs { get; set; }

        public bool Carpet { get; set; }

        public bool Tables { get; set; }

        public bool Nothing { get; set; }

        public bool Notebook { get; set; }

        public bool Dataprojector { get; set; }

        [StringLength(100)]
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