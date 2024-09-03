using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Reflection.Metadata.Ecma335;

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
                cnn.Execute("insert into Task (TextBody,IsCompleted, CreateDate, CompletedDate) values (@TextBody,@IsCompleted, @CreateDate, @CompletedDate)", task);
            }
        }

        public static void UpdateTask(Task task)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Task set TextBody = @TextBody, IsCompleted = @IsCompleted, CreateDate = @CreateDate, CompletedDate = @CompletedDate where Id = @Id",
                    new { Id = task.id, TextBody = task.TextBody, IsCompleted = task.IsCompleted, CreateDate = task.CreateDate, CompletedDate = task.CompletedDate });
            }
        }


        public static void DeleteTask(Task task)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from Task where Id = @Id", new { Id = task.id });
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
