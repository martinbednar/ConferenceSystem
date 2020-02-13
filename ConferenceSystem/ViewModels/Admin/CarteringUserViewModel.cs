using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using ConferencySystem.ViewModels.User;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { /*"admin",*/ "super" })]
    public class CarteringUserViewModel : MainMasterPageViewModel
    {
        public int UserId
        {
            get { return Convert.ToInt32(Context.Parameters["UserId"]); }
        }

        public List<CarteringSelectViewModel.Category> OutputCategories { get; set; }

        public CarteringDTO[] DataCartering { get; set; }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var carteringService = new CarteringService();
                DataCartering = carteringService.GetCarterings();

                IEnumerable<string> categories = (from x in DataCartering select x.Category).Distinct();

                List<CarteringSelectViewModel.Category> workingOutputCategories = new List<CarteringSelectViewModel.Category>();

                foreach (string category in categories)
                {
                    workingOutputCategories.Add(new CarteringSelectViewModel.Category(category,
                        DataCartering.Where(c => c.Category == category).First().Id,
                        DataCartering.Where(c => c.Category == category).Where(c => c.Users.Any(p => p.Id == UserId)).Count() != 0,
                        DataCartering.Where(c => c.Category == category).Where(c => c.Users.Any(p => p.Id == UserId)).SingleOrDefault() != null ? DataCartering.Where(c => c.Category == category).Where(c => c.Users.Any(p => p.Id == UserId)).SingleOrDefault()?.Name : null,
                        (DataCartering.Where(c => c.Category == category).Count() > 1) ? DataCartering.Where(c => c.Category == category).Select(c => c.Name) : null));
                }
                OutputCategories = workingOutputCategories;
            }

            return base.PreRender();
        }

        public void CategoryCheckedChanged(CarteringSelectViewModel.Category changedCategory)
        {
            if (changedCategory != null)
            {
                if (!changedCategory.CategoryChecked)
                {
                    changedCategory.ChosenOption = null;
                }
                else
                {
                    if (changedCategory.Choices != null)
                    {
                        changedCategory.ChosenOption = changedCategory.Choices.FirstOrDefault();
                    }
                }
            }
        }

        public void SaveCartering()
        {
            foreach (CarteringSelectViewModel.Category category in OutputCategories)
            {
                var carteringService = new CarteringService();
                carteringService.SaveChanges(category.Choices == null,
                    category.FirstId,
                    category.CategoryChecked,
                    (category.ChosenOption != null) ? DataCartering.Where(c => c.Category == category.Name).Where(c => c.Name == category.ChosenOption).First().Id : 0,
                    (category.LastChosenOption != null) ? DataCartering.Where(c => c.Category == category.Name).Where(c => c.Name == category.LastChosenOption).First().Id : 0,
                    UserId);
            }
            Context.RedirectToRoute("CarteringOverviewUsers");
        }
    }
}

