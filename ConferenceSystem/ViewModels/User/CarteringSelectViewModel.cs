using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.User
{
    [Authorize(Roles = new[] { "user" })]
    public class CarteringSelectViewModel : MainMasterPageViewModel
    {
        public int CurrentUserId
        {
            get { return Int32.Parse(Context.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault());
            }
        }

        public List<Category> OutputCategories { get; set; }

        public CarteringDTO[] DataCartering { get; set; }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var carteringService = new CarteringService();
                DataCartering = carteringService.GetCarterings();

                IEnumerable<string> categories = (from x in DataCartering select x.Category).Distinct();

                List<Category> workingOutputCategories = new List<Category>();

                foreach (string category in categories)
                {
                    workingOutputCategories.Add(new Category(category,
                        DataCartering.Where(c => c.Category == category).First().Id,
                        DataCartering.Where(c => c.Category == category).Where(c => c.People.Any(p => p.Id == CurrentUserId)).Count() != 0,
                        DataCartering.Where(c => c.Category == category).Where(c => c.People.Any(p => p.Id == CurrentUserId)).SingleOrDefault() != null ? DataCartering.Where(c => c.Category == category).Where(c => c.People.Any(p => p.Id == CurrentUserId)).SingleOrDefault()?.Name : null,
                        (DataCartering.Where(c => c.Category == category).Count() > 1) ? DataCartering.Where(c => c.Category == category).Select(c => c.Name) : null));
                }
                OutputCategories = workingOutputCategories;
            }

            return base.PreRender();
        }

        public void CategoryCheckedChanged(Category changedCategory)
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
            foreach (Category category in OutputCategories)
            {
                var carteringService = new CarteringService();
                carteringService.SaveChanges(category.Choices == null,
                    category.FirstId,
                    category.CategoryChecked,
                    (category.ChosenOption != null) ? DataCartering.Where(c => c.Category == category.Name).Where(c => c.Name == category.ChosenOption).First().Id : 0,
                    (category.LastChosenOption != null) ? DataCartering.Where(c => c.Category == category.Name).Where(c => c.Name == category.LastChosenOption).First().Id : 0,
                    CurrentUserId);
            }
            Context.RedirectToRoute("CarteringSelect");
        }



        public class Category
        {
            public int FirstId { get; set; }

            public string Name { get; set; }

            public bool CategoryChecked { get; set; }

            public string ChosenOption { get; set; }

            public string LastChosenOption { get; set; }

            public IEnumerable<string> Choices { get; set; }

            public Category () { }

            public Category(string paramName, int paramFirstId, bool paramCategoryChecked, string paramChosenOption, IEnumerable<string> paramChoices)
            {
                Name = paramName;
                FirstId = paramFirstId;
                Choices = paramChoices;
                ChosenOption = paramChosenOption;
                LastChosenOption = paramChosenOption;
                CategoryChecked = paramCategoryChecked;
            }
        }
    }
}

    