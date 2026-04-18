public static class Permissions
{
    public static class Events
    {
        public const string Create = "events.create";
        public const string View = "events.view";
        public const string Edit = "events.edit";
        public const string Delete = "events.delete";
    }

    public static class Orders
    {
        public const string Create = "orders.create";
        public const string View = "orders.view";
    }

    public static class Users
    {
        public const string Create = "users.create";
        public const string View = "users.view";
        public const string Edit = "users.edit";
        public const string Delete = "users.delete";
        public const string Ban = "users.ban";
    }

    public static class TicketTypes
    {
        public const string Create = "tickettypes.create";
        public const string View = "tickettypes.view";
        public const string Edit = "tickettypes.edit";
        public const string Delete = "tickettypes.delete";
    }

    public static class Organizers
    {
        public const string Create = "organizers.create";
        public const string View = "organizers.view";
        public const string Edit = "organizers.edit";
        public const string Delete = "organizers.delete";
    }



    public static readonly Dictionary<string, string> Descriptions = new()
    {
        [Events.Create] = "Can create events",
        [Events.View] = "Can view events",
        [Events.Edit] = "Can edit events",
        [Events.Delete] = "Can delete events",

        [Orders.Create] = "Can create orders",
        [Orders.View] = "Can view orders",

        [Users.Create] = "Can create users",
        [Users.View] = "Can view users",
        [Users.Edit] = "Can edit users",
        [Users.Delete] = "Can delete users",
        [Users.Ban] = "Can ban users",

        [TicketTypes.Create] = "Can create ticket types",
        [TicketTypes.View] = "Can view ticket types",
        [TicketTypes.Edit] = "Can edit ticket types",
        [TicketTypes.Delete] = "Can delete ticket types",

        [Organizers.Create] = "Can create organizers",
        [Organizers.View] = "Can view organizers",
        [Organizers.Edit] = "Can edit organizers",
        [Organizers.Delete] = "Can delete organizers",
    };
}