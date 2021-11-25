using AreaOfEvent.Server.Models.Chat;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Server.Services.Database.Chat
{
    public class ChatHistoryDbModelBuilder
    {
        public async Task BuildDefaultTables( IDbConnection dbConnection )
        {
            await AddRoleTable( dbConnection );
            await AddUserTable( dbConnection );
            await AddMessageTable( dbConnection, "General" );
        }

        async Task AddRoleTable( IDbConnection dbConnection )
        {
            await dbConnection.ExecuteAsync( Role.TableSchema() );
        }

        async Task AddUserTable( IDbConnection dbConnection )
        {
            await dbConnection.ExecuteAsync( User.TableSchema() );
        }

        public async Task AddMessageTable( IDbConnection connection, string tableName )
        {
            await connection.ExecuteAsync( Message.TableSchema( tableName ) );

        }
    }
}
