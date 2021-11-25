using System.Collections.Generic;

namespace AreaOfEvent.Server.Models.Chat
{
    public record Role
    {
        public int ID { get; init; }
        public string Name { get; init; }

        public static string TableSchema( string name = "Roles" ) => $@"CREATE TABLE {name}(
{nameof( ID )} int IDENTITY(0,1) PRIMARY KEY,
{nameof( Name )} nvarchar(200) NOT NULL);";

        public static class Defualts
        {
            public static readonly Role Admin = new(){ID = 0, Name  =nameof(Admin)};
            public static readonly Role Moderator = new(){ID = 1, Name =nameof(Moderator)};
            public static readonly Role User = new(){ID = 2, Name =nameof(User)};

            public static readonly IEnumerable<Role> Defaults = new[]{ Admin, Moderator, User};

        }

    }

}
