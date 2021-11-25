using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Server.Models.Chat
{
    public abstract partial class Content
    {
        public static string SQLColumnDefinitions( string tableName, string tableNamePKColumnName = nameof( Message.ID ) ) => @$"Discriminator nvarchar(512) NOT NULL,
{nameof( Text.Data )} ntext,
{nameof( Multimedia.ContentType )} tinyint,
{nameof( Multimedia.FilePath )} nvarchar(max),
{nameof( MessageReference.RefrencedMessageID )} bigint FOREIGN KEY REFERENCES {tableName}({tableNamePKColumnName})";
    }
}
