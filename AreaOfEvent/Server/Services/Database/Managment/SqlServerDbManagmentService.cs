using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SmoDB = Microsoft.SqlServer.Management.Smo.Database;
using SQLServer = Microsoft.SqlServer.Management.Smo.Server;

namespace AreaOfEvent.Server.Services.Database.Managment
{


    public class SqlServerDbManagmentService : IDatabaseManagmentService
    {
        private const string ConfigDBServerName = "DatabaseServerName";
        private readonly IConfiguration config;
        private string DBServerName => config[ConfigDBServerName];
        //"DESKTOP-4J509JC";

        public SqlServerDbManagmentService( IConfiguration config )
        {
            this.config = config;
        }

        //DESKTOP-4J509JC
        public string CreateDatabase( string name )
        {
            SQLServer serv  = new(DBServerName);


            if (!serv.Databases.Contains( name ))
            {
                var newDB = new SmoDB(serv,name);
                newDB.Create();
            }
            return GetDatabaseConnectionString( name );
        }

        public bool DeleteDatabase( string dbName )
        {
            SQLServer serv = new(DBServerName);


            if (serv.Databases.Contains( dbName ))
            {
                var db = serv.Databases[dbName];
                db.Drop();
                return true;
            }
            return false;
        }

        public string GetDatabaseConnectionString( string dbName )
        {
            return $"Data Source={DBServerName};Initial Catalog={dbName};Integrated Security=True;";
        }

    }
}
