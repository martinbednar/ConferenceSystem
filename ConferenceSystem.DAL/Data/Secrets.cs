namespace ConferencySystem.DAL.Data
{
    public static class Secrets
    {
        public static string GmailZN => System.Configuration.ConfigurationManager.AppSettings["GmailZN"];

        public static string GmailZNPwd => System.Configuration.ConfigurationManager.AppSettings["GmailZNPwd"];

        public static string FirstUserEmail => System.Configuration.ConfigurationManager.AppSettings["FirstUserEmail"];

        public static string FirstUserPwd => System.Configuration.ConfigurationManager.AppSettings["FirstUserPwd"];

        public static string FirstAdminEmail => System.Configuration.ConfigurationManager.AppSettings["FirstAdminEmail"];

        public static string FirstAdminPwd => System.Configuration.ConfigurationManager.AppSettings["FirstAdminPwd"];
    }
}