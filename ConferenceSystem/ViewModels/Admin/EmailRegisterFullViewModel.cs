using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class EmailRegisterFullViewModel : MainMasterPageViewModel
    {
        public List<TextDTO> Emails { get; set; }

        public ConstantDTO MaxParticipants { get; set; }

        public int ActualParticipantsNumber { get; set; }

        public override Task PreRender()
        {
            var textService = new TextService();
            Emails = textService.GetTexts();

            var constantService = new ConstantService();
            MaxParticipants = constantService.GetConstant(1);

            var registerService = new RegisterService();
            ActualParticipantsNumber = registerService.GetUsers().Count;

            return base.PreRender();
        }

        public void Save()
        {
            var textService = new TextService();
            textService.SaveTexts(Emails);
            
            var constantService = new ConstantService();
            constantService.SaveConstant(MaxParticipants);
        }
    }
}

