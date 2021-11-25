namespace AreaOfEvent.Server.Services.Database.Managment
{
    public interface IDatabaseManagmentService
    {
        string CreateDatabase( string dbName );

        bool DeleteDatabase( string dbName );

        string GetDatabaseConnectionString( string dbName );
    }
}
