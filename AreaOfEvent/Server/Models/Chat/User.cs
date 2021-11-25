using System;

namespace AreaOfEvent.Server.Models.Chat
{
    public class User
    {
        public int ID { get; set; }
        public Guid GlobalUserID { get; set; }
        public string UserName { get; set; }

        public Role UserRole { get; set; }

        public static string TableSchema( string name = "Users", string roleTableName = "Roles", string roleTablePKColumnName = nameof( Role.ID ) ) => @$"CREATE TABLE {name}(
{nameof( ID )} int IDENTITY(0,1) PRIMARY KEY,
{nameof( GlobalUserID )} uniqueidentifier NOT NULL,
{nameof( UserName )} nvarchar(200) NOT NULL,
{nameof( UserRole )}ID int NOT NULL FOREIGN KEY REFERENCES {roleTableName}({roleTablePKColumnName}));";
    }
}
