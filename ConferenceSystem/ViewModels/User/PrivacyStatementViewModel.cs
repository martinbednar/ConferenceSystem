using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace ConferencySystem.ViewModels.User
{
    public class PrivacyStatementViewModel : ConferencySystem.ViewModels.MainMasterPageViewModel
    {
        public override Task PreRender()
        {
            RegisterActive = "";
            MainPageActive = "";
            AdminActive = "";
            CarteringActive = "";
            WorkshopsActive = "";
            LecturerInfoActive = "";
            LoginActive = "";
            ProfileActive = "";

            return base.PreRender();
        }
    }
}

