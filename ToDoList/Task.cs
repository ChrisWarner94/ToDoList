namespace ToDoList
{
    internal class Task
    {
        public int? id { get; set; }
        public string TextBody { get; set; }
        public bool IsCompleted { get; set; }


        public Task() { }

        public Task(string textBody,bool IsCompleted)
        {    
            TextBody = textBody;
        }
    }
}
