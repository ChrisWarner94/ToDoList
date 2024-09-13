using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace ToDoList
{
    internal class SqliteDataAccess
    {

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static List<List<Task>> LoadCompletedTasks()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Task>("select * from Task where IsCompleted = true", new DynamicParameters());
                return SplitList(output.ToList());
            }
        }

        public static List<List<Task>> LoadIncompleteTasks()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Task>("select * from Task where IsCompleted = false", new DynamicParameters());
                return SplitList(output.ToList());
            }
        }


        public static void SaveTask(Task task)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Task (TextBody,IsCompleted) values (@TextBody,@IsCompleted)", task);
            }
        }

        public static void UpdateTask(Task task)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Task set TextBody = @TextBody, IsCompleted = @IsCompleted where Id = @Id",
                    new { Id = task.id, TextBody = task.TextBody, IsCompleted = task.IsCompleted});
            }
        }


        public static void DeleteTask(Task task)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from Task where Id = @Id", new { Id = task.id });
            }
        }

        public static List<List<Task>> ReturnTaskSearch(string search)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Task>($"select * from Task where TextBody like '%{search}%'", new DynamicParameters());
                List<List<Task>> searchResults = SplitList(output.ToList());
                return searchResults;
            }
        }

        private static List<List<Task>> SplitList(List<Task> tasks)
        {
            int size = 10;
            var splitList = new List<List<Task>>();
            for (int i = 0; i < tasks.Count; i += size)
            {
                splitList.Add(tasks.GetRange(i, Math.Min(size, tasks.Count - i)));
            }
            return splitList;
        }


    }
}
