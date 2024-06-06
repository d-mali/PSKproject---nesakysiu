namespace EventBackend.Authorization
{
    //interface to return permissions array
    public interface IPermissionsInterface
    {
        public static abstract List<string> GeneratePermissions();
    }

    public class Permissions : IPermissionsInterface
    {
        public class Event : IPermissionsInterface
        {
            private const string Base = "event";

            public const string Create = Base + ".create";
            public const string Read = Base + ".read";
            public const string Update = Base + ".update";
            public const string Delete = Base + ".delete";

            public static List<string> GeneratePermissions()
            {
                return [Create, Read, Update, Delete];
            }
        }

        public class Task : IPermissionsInterface
        {
            private const string Base = "task";

            public const string Create = Base + ".create";
            public const string Read = Base + ".read";
            public const string Update = Base + ".update";
            public const string Delete = Base + ".delete";

            public static List<string> GeneratePermissions()
            {
                return [Create, Read, Update, Delete];
            }
        }

        public static List<string> GeneratePermissions()
        {
            return [.. Event.GeneratePermissions(), .. Task.GeneratePermissions()];
        }
    }
}
