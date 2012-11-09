using System.Web.Security;
using KendoWcfSample.Filters;
using WebMatrix.WebData;

namespace KendoWcfSample
{
    public static class AppConfig
    {
        public static void Configure()
        {
            System.Data.Entity.Database.SetInitializer(new Models.SampleData());

            CreateAdminUser();
        }

        private static void CreateAdminUser()
        {
            const string username = "Owner";
            const string password = "p@ssword123";
            const string role = "Administrator";

            new InitializeSimpleMembershipAttribute().OnActionExecuting(null);

            if (!WebSecurity.UserExists(username))
                WebSecurity.CreateUserAndAccount(username, password);

            if (!Roles.RoleExists(role))
                Roles.CreateRole(role);

            if (!Roles.IsUserInRole(username, role))
                Roles.AddUserToRole(username, role);
        }
    }
}