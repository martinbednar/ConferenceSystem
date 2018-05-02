using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ConferencySystem.BL;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Controls;
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

            Assert.IsTrue(dateProcessing.BirthMonth.Length == 33);
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
    }
}
