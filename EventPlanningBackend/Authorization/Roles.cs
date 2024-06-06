namespace EventBackend.Authorization
{
    public enum Roles
    {
        Admin
    }

    public class ApplicationRoles
    {
        public static List<string> GenerateRoles()
        {
            return [Roles.Admin.ToString()];
        }
    }
}
