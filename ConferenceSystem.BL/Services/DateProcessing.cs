namespace ConferencySystem.BL.Services
{
    public class DateProcessing
    {
        public string[] BirthDay { get; set; } = { "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };

        public string[] BirthMonth { get; set; } = { "", "Leden", "Únor", "Březen", "Duben", "Květen", "Červen", "Červenec", "Srpen", "Září", "Říjen", "Listopad", "Prosinec" };

        public string[] BirthYear { get; set; } = { "", "1920", "1921", "1922", "1923", "1924", "1925", "1926", "1927", "1928", "1929", "1930", "1931", "1932", "1933", "1934", "1935", "1936", "1937", "1938", "1939", "1940", "1941", "1942", "1943", "1944", "1945", "1946", "1947", "1948", "1949", "1950", "1951", "1952", "1953", "1954", "1955", "1956", "1957", "1958", "1959", "1960", "1961", "1962", "1963", "1964", "1965", "1966", "1967", "1968", "1969", "1970", "1971", "1972", "1973", "1974", "1975", "1976", "1977", "1978", "1979", "1980", "1981", "1982", "1983", "1984", "1985", "1985", "1986", "1987", "1988", "1989", "1990", "1991", "1992", "1993", "1994", "1995", "1996", "1997", "1998", "1999", "2000", "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017" };

        public string DayToDb (string day)
        {
            if (day.Length < 2)
            {
                return ("0" + day);
            }
            else
            {
                return day;
            }
        }

        public string DayFromDb(string day)
        {
            if (day.Substring(0,1) == "0")
            {
                return (day.Substring(1, 1));
            }
            else
            {
                return day;
            }
        }

        public string MonthToDb(string month)
        {
            switch (month)
            {
                case "Leden":
                    month = "01";
                    break;
                case "Únor":
                    month = "02";
                    break;
                case "Březen":
                    month = "03";
                    break;
                case "Duben":
                    month = "04";
                    break;
                case "Květen":
                    month = "05";
                    break;
                case "Červen":
                    month = "06";
                    break;
                case "Červenec":
                    month = "07";
                    break;
                case "Srpen":
                    month = "08";
                    break;
                case "Září":
                    month = "09";
                    break;
                case "Říjen":
                    month = "10";
                    break;
                case "Listopad":
                    month = "11";
                    break;
                case "Prosinec":
                    month = "12";
                    break;
                default:
                    month = "";
                    break;
            }
            return month;
        }

        public string MonthFromDb(string month)
        {
            switch (month)
            {
                case "01":
                    month = "Leden";
                    break;
                case "02":
                    month = "Únor";
                    break;
                case "03":
                    month = "Březen";
                    break;
                case "04":
                    month = "Duben";
                    break;
                case "05":
                    month = "Květen";
                    break;
                case "06":
                    month = "Červen";
                    break;
                case "07":
                    month = "Červenec";
                    break;
                case "08":
                    month = "Srpen";
                    break;
                case "09":
                    month = "Září";
                    break;
                case "10":
                    month = "Říjen";
                    break;
                case "11":
                    month = "Listopad";
                    break;
                case "12":
                    month = "Prosinec";
                    break;
                default:
                    month = "";
                    break;
            }
            return month;
        }
    }
}