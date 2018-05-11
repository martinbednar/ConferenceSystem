using ConferencySystem.BL;
using ConferencySystem.BL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConferencySystemTests
{
    [TestClass]
    public class DateprocessingTests
    {
        public void InitializeMapper()
        {
            if (!MapperInitializer.IsInicialized)
            {
                MapperInitializer.Initialize();
                MapperInitializer.IsInicialized = true;
            }
        }

        [TestMethod]
        public void OptionsForDays_GetNumberOfDays_IsEqual32()
        {
            InitializeMapper();
            DateProcessing dateProcessing = new DateProcessing();

            Assert.IsTrue(dateProcessing.BirthDay.Length == 32);
        }

        [TestMethod]
        public void OptionsForMonth_GetNumberOfMonths_IsEqual13()
        {
            InitializeMapper();
            DateProcessing dateProcessing = new DateProcessing();

            Assert.IsTrue(dateProcessing.BirthMonth.Length == 13);
        }

        [TestMethod]
        public void OptionsForYears_GetNumberOfYears_IsBiggerThan70()
        {
            InitializeMapper();
            DateProcessing dateProcessing = new DateProcessing();

            Assert.IsTrue(dateProcessing.BirthYear.Length > 33);
        }

        [TestMethod]
        public void ConvertDayToDbFormat_DayToDb_IsEqualToExpected()
        {
            InitializeMapper();
            DateProcessing dateProcessing = new DateProcessing();

            for (int day = 1; day < 10; day++)
            {
                Assert.IsTrue(dateProcessing.DayToDb(day.ToString()) == "0" + day);
            }

            for (int day = 10; day <= 31; day++)
            {
                Assert.IsTrue(dateProcessing.DayToDb(day.ToString()) == day.ToString());
            }
        }

        [TestMethod]
        public void ConvertDayFromDbFormat_DayFromDb_IsEqualToExpected()
        {
            InitializeMapper();
            DateProcessing dateProcessing = new DateProcessing();

            for (int day = 1; day < 10; day++)
            {
                Assert.IsTrue(dateProcessing.DayFromDb("0" + day) == day.ToString());
            }

            for (int day = 10; day <= 31; day++)
            {
                Assert.IsTrue(dateProcessing.DayFromDb(day.ToString()) == day.ToString());
            }
        }

        [TestMethod]
        public void ConvertMonthToDbFormat_MonthToDb_IsEqualToExpected()
        {
            InitializeMapper();
            DateProcessing dateProcessing = new DateProcessing();

            for (int month = 1; month <= 9; month++)
            {
                Assert.IsTrue(dateProcessing.MonthToDb(dateProcessing.BirthMonth[month]) == "0" + month);
            }
            for (int month = 10; month <= 12; month++)
            {
                Assert.IsTrue(dateProcessing.MonthToDb(dateProcessing.BirthMonth[month]) == month.ToString());
            }
        }

        [TestMethod]
        public void ConvertMonthFromDbFormat_MonthFromDb_IsEqualToExpected()
        {
            InitializeMapper();
            DateProcessing dateProcessing = new DateProcessing();

            for (int month = 1; month <= 9; month++)
            {
                Assert.IsTrue(dateProcessing.MonthFromDb("0" + month) == dateProcessing.BirthMonth[month]);
            }
            for (int month = 10; month <= 12; month++)
            {
                Assert.IsTrue(dateProcessing.MonthFromDb(month.ToString()) == dateProcessing.BirthMonth[month]);
            }
        }
    }
}
