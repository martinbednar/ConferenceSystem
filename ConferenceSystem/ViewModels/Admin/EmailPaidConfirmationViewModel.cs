using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Runtime.Filters;
using DotVVM.Framework.ViewModel;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] {"admin", "super"})]
    public class EmailPaidConfirmationViewModel : ConferencySystem.ViewModels.MainMasterPageViewModel
    {
        public List<TextDTO> Emails { get; set; }

        public override Task PreRender()
        {
            var textService = new TextService();
            Emails = textService.GetTexts();

            return base.PreRender();
        }

        public void Save()
        {
            var textService = new TextService();

            textService.SaveTexts(Emails);
        }
    }
}

