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
        public int? id { get; set; }
        public string TextBody { get; set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? CompletedDate { get; private set; }
        public bool IsCompleted { get; set; }


        public Task() { }

        public Task(string textBody,bool IsCompleted, DateTime CreateDate, DateTime? CompletedDate )
        {
            CreateDate = DateTime.Now;
            CompletedDate = null;      
            TextBody = textBody;
        }
    }
}
