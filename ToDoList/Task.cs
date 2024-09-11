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

        public static void AddTask()
        {
            string taskToAdd = UserInterface.GetText("Enter the task you would like to add:");
            Task newTask = new Task(taskToAdd, false);
            SqliteDataAccess.SaveTask(newTask);
            Console.Clear();
            UserInterface.PrintNotification($"Task Added: {newTask.TextBody}");
            Thread.Sleep(1000);
            Console.Clear();
            InterfaceRouting.MenuRoutes("MainMenu");
        }
    }
}
