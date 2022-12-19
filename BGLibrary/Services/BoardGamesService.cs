using BGLibrary.Models;
using System.Data.SQLite;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace BGLibrary.Services
{
    public class GetService : DBClientService, IGetInterface
    {
        private string Request = "";
        private List<SelectViewModel> SelectedColumns;
        public List<BoardGame> GetBoardGames()
        {
            List<SelectViewModel> columns = new List<SelectViewModel>() {
                new SelectViewModel() { Column = "Id", ColumnType = "string" },
                new SelectViewModel() { Column = "MaxPlayers", ColumnType = "int" },
                new SelectViewModel() { Column = "MaxPlayTime", ColumnType = "int" },
                new SelectViewModel() { Column = "MinPlayers", ColumnType = "int" },
                new SelectViewModel() { Column = "MinPlayTime", ColumnType = "int" },
                new SelectViewModel() { Column = "Name", ColumnType = "string" },
                new SelectViewModel() { Column = "YearPublished", ColumnType = "int" }
            };
            var request = 
                @"SELECT ""Id"", ""MaxPlayers"", ""MaxPlayTime"",""MinPlayers"", ""MinPlayTime"", ""Name"", ""YearPublished"" FROM BoardGames LIMIT 30";

            this.OpenConnection();
            var Reader = this.Execute(request);
            var serialisedData = this.Serialization(Reader, columns);
            SerializeToJson(serialisedData);
            this.CloseConnection();

            return serialisedData;
        }

        public BoardGamePage GetBoardGame(string Id)
        {
            var request =
                @"SELECT ""Name"", ""description"", ""YearPublished"", ""BoardGameArtist"", ""BoardGameDesigner"" FROM BoardGames WHERE Id = '" + Id + "' LIMIT 1";

            this.OpenConnection();
            var Reader = this.Execute(request);
            var serialisedData = this.Serialization(Reader);
            this.CloseConnection();
            return serialisedData;
        }

        public List<BoardGame> Get()
        {
            this.OpenConnection();
            var Reader = this.Execute(Request + " LIMIT 1000");
            var serialisedData = this.Serialization(Reader, SelectedColumns);
            this.CloseConnection();
            SerializeToJson(serialisedData);
            return serialisedData;
        }

        [Obsolete("Obsolete")]
        private void SerializeToJson(List<BoardGame> boardGames)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = true
            };
            using FileStream fs = new FileStream("wwwroot/Table.json", FileMode.Create);
            JsonSerializer.Serialize(fs, boardGames, options);

            Console.WriteLine("Data has been saved to file");
        }

        public List<BoardGame> Serialization(SQLiteDataReader reader, List<SelectViewModel> columns)
        {
            List<BoardGame> boardGames = new List<BoardGame>();
            while (reader.Read())
            {
                var game = new BoardGame();
                for (int i = 0; i < columns.Count(); ++i)
                {
                    var property = game.GetType().GetProperty(columns[i].Column);
                    var propertyType = property.PropertyType;
                    if (propertyType == typeof(int?))
                        if (reader[i] == null || string.IsNullOrEmpty(reader[i].ToString()))
                            property.SetValue(game, null);
                        else
                        {
                            var v = reader[i].ToString();
                            property.SetValue(game, int.Parse(reader[i].ToString()));
                        }
                    else
                        property.SetValue(game, reader[i].ToString());
                }
                boardGames.Add(game);
            }
            return boardGames;
        }

        private BoardGamePage Serialization(SQLiteDataReader reader)
        {
            BoardGamePage v = new BoardGamePage();
            while (reader.Read())
            {
                v.Name = reader[0].ToString();
                v.Description = reader[1].ToString();
                v.YearPublished = reader[2].ToString();
                v.BoardGameArtist = reader[3].ToString();
                v.BoardGameDesigner = reader[4].ToString();
            }
            return v;
        }

        public GetService Select(List<SelectViewModel> columns, string tableName = "BoardGames")
        {
            SelectedColumns = columns;
            Request += " SELECT";
            foreach (var column in columns)
            {
                Request += $" {column.Column},";
            }
            Request = Request.Remove(Request.Length - 1, 1);
            Request += $" FROM {tableName}";
            return this;
        }

        public GetService OrderBy(List<OrderByViewModel> column_order)
        {
            if (IsValidAny(column_order))
            {
                var order = " ORDER BY";
                foreach (var i in column_order)
                {
                    if (i.IsValid())
                        order += $" {i.Column} {i.Operator},";
                }
                Request += order.Remove(order.Length - 1, 1);
            }
            return this;
        }

        public GetService Where(List<WhereViewModel> Where)
        {
            if (IsValidAny(Where))
            {
                var order = " WHERE";
                foreach (var i in Where)
                {
                    if (i.IsValid())
                    {
                        if(i.Operator == "LIKE")
                        {
                            order += $" {i.Column} {i.Operator} '%{i.Value}%' AND";
                        }
                        else
                        {
                            if (i.Type == "string")
                                order += $" {i.Column} {i.Operator} '{i.Value}' AND";
                            else
                                order += $" {i.Column} {i.Operator} {i.Value} AND";
                        }
                    }
                }
                Request += order.Remove(order.Length - 3, 3);
            }
            return this;
        }

        private bool IsValidAny(List<OrderByViewModel> Orderby)
        {
            bool isValid = false;
            foreach (var i in Orderby)
                if (i.IsValid()) { isValid = true; break; }
            return isValid;
        }

        private bool IsValidAny(List<WhereViewModel> Where)
        {
            bool isValid = false;
            foreach (var i in Where)
                if (i.IsValid()) { isValid = true; break; }
            return isValid;
        }

        public override string ToString()
        {
            return Request + " LIMIT 30";
        }
    }
}