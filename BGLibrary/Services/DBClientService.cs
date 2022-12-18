using BGLibrary.Models;
using System.Data.SQLite;

namespace BGLibrary.Services
{
    public class IDBClientService : IDBClientInterface
    {
        private static string dbConnectionString = @"Data Source= \boardgames.db";
        private static SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);

        public void OpenConnection() { sqliteCon.Open(); }

        public SQLiteDataReader Execute(string request)
        {
            using (SQLiteTransaction sqlTransaction = sqliteCon.BeginTransaction())
            {
                SQLiteCommand createCommand = new SQLiteCommand(request, sqliteCon);
                var reader = createCommand.ExecuteReader();
                return reader;
            }
        }
        public void CloseConnection() { sqliteCon.Close(); }
    }
}