namespace EventBackend.Authorization
{
    static class Permissions
    {
        public static class Event
        {
            private const string Base = "event";

            public const string Create = Base + ".create";
            public const string Read = Base + ".read";
            public const string Update = Base + ".update";
            public const string Delete = Base + ".delete";
        }

        public static class Task
        {
            private const string Base = "task";

            public const string Create = Base + ".create";
            public const string Read = Base + ".read";
            public const string Update = Base + ".update";
            public const string Delete = Base + ".delete";
        }
    }
}
