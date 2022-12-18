using BGLibrary.Models;
using System.Data.SQLite;

namespace BGLibrary.Services
{
    public interface IDBClientInterface
    {
        public void OpenConnection();
        public void CloseConnection();
        public SQLiteDataReader Execute (string request);
    }
    public interface IGetInterface
    {
        public List<BoardGame> GetBoardGames();
        public List<BoardGame> Serialization(SQLiteDataReader reader, List<SelectViewModel> columns);
        public IGetService Select(List<SelectViewModel> columns, string tableName = "BoardGames");
        public IGetService OrderBy(List<OrderByViewModel> columnOrder);
        public IGetService Where(List<WhereViewModel> where);
        public List<BoardGame> Get();
        public BoardGamePage GetBoardGame(string id);
    }
    public interface IPlotInterface
    {
        public void GetPlot(Count plot);
        public void GetPlot(TimeBased plot);
        public void GetPlot(AVG plot);
    }

}