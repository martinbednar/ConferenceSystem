using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.BusinessPack.Controls;
using DotVVM.BusinessPack.Export.Excel;
using DotVVM.Framework.Runtime.Filters;
using System.Threading.Tasks;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class LecturesViewModel : ConferencySystem.ViewModels.MainMasterPageViewModel
    {
        public GridViewUserSettings UserSettings { get; set; }

        public int TotalLecturesCount { get; set; }

        public bool AllColumns { get; set; } = false;
        public bool RequirementsColumns { get; set; } = false;
        public bool OverviewColumns { get; set; } = true;
        public bool AllColumnsEnabled { get; set; } = true;
        public bool RequirementsColumnsEnabled { get; set; } = true;
        public bool OverviewEnabled { get; set; } = false;

        public BusinessPackDataSet<LectureDTO> Lectures { get; set; } = new BusinessPackDataSet<LectureDTO>();

        public override Task PreRender()
        {
            Lectures.PagingOptions.PageSize = 500;
            Lectures.SortingOptions.SortExpression = nameof(LectureDTO.Id);
            Lectures.SortingOptions.SortDescending = false;

            var lectureService = new LectureService();
            lectureService.GetLectures(Lectures);

            TotalLecturesCount = Lectures.PagingOptions.TotalItemsCount;

            RegisterActive = "";
            MainPageActive = "";
            AdminActive = "active";

            return base.PreRender();
        }

        public void ShowAllColumns()
        {
            AllColumns = true;
            RequirementsColumns = true;
            OverviewColumns = true;

            OverviewEnabled = true;
            RequirementsColumnsEnabled = true;
            AllColumnsEnabled = false;
        }

        public void ShowRequirementsColumns()
        {
            AllColumns = false;
            RequirementsColumns = true;
            OverviewColumns = false;

            OverviewEnabled = true;
            RequirementsColumnsEnabled = false;
            AllColumnsEnabled = true;
        }

        public void ShowOverview()
        {
            AllColumns = false;
            RequirementsColumns = false;
            OverviewColumns = true;

            OverviewEnabled = false;
            RequirementsColumnsEnabled = true;
            AllColumnsEnabled = true;
        }

        public void DeleteLecture(int id)
        {
            var lectureService = new LectureService();
            lectureService.DeactivateProgram(id);
        }

        public void ExportToExcel()
        {
            var exporter = new GridViewExportExcel<LectureDTO>(new GridViewExportExcelSettings<LectureDTO>());
            var gridView = Context.View.FindControlByClientId<GridView>("lectures", true);

            using (var file = exporter.Export(gridView, Lectures))
            {
                Context.ReturnFile(file, "NakopneteSvojiSkolu-Programy.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}

