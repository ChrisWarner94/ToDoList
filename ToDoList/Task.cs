using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    internal class Task
    {
        public DateTime CreateDate { get; private set; }
        public DateTime? CompletedDate { get; private set; }
        public string TextBody { get; set; }

        private static List<Task> allTasks = new List<Task>();

            public Task(string textBody)
            {
                CreateDate = DateTime.Now;
                CompletedDate = null;      
                TextBody = textBody;
                allTasks.Add(this);
        }

        public static List<Task> GetAllTasks()
        {
            return new List<Task>(allTasks);
        }

        public static void ToggleCompleted(Task task) 
        {
            if (task.CompletedDate == null)
            {
                task.CompletedDate = DateTime.Now;
            }
            else
            {
                task.CompletedDate = null;
            }
        }

        public static Task UpdateTextBody(Task task, string textBody) 
        {
            task.TextBody = textBody;
            return task;
        }

    }
}
