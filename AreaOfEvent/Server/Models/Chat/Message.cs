using System;
using UserClass = AreaOfEvent.Server.Models.Chat.User;

namespace AreaOfEvent.Server.Models.Chat
{
    public class Message
    {
        public long ID { get; set; }
        public DateTime SentAt { get; set; }

        public UserClass User { get; set; }

        public Content Content { get; set; }

        public static string TableSchema( string name, string userTableName = "Users", string userTablePKColumnName = nameof( UserClass.ID ) ) => $@"
                CREATE TABLE {name}
                (
                    {nameof( ID )} bigint IDENTITY(0,1) PRIMARY KEY,
                    {nameof( User )}ID int NOT NULL FOREIGN KEY REFERENCES {userTableName}({userTablePKColumnName}),
                    {nameof( SentAt )} datetime NOT NULL,
                    {Content.SQLColumnDefinitions( name )}
                );";
    }

}
