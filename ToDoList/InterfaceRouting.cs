namespace ToDoList
{
    internal class InterfaceRouting
    {

         public static void MenuRoutes(string requiredMenu)
        {
            
            if (requiredMenu == "MainMenu")
            {
                string[] mainMenu = { "Add Task", "Manage Tasks", "Exit" };
                
                int presscount = UserInterface.ReturnMenuSelection(mainMenu);
                
                if (presscount == 0)
                {
                    Task.AddTask();
                }
                else if (presscount == 1)
                {
                    MenuRoutes("TasksMenu");
                }
                else if (presscount == 2)
                {
                    Environment.Exit(0);
                }
            }
            else if (requiredMenu == "TasksMenu")
            {
                string[] taskMenu = { "view completed tasks", "view incomplete tasks", "return to main menu" };
                int presscount = UserInterface.ReturnMenuSelection(taskMenu);

                if (presscount == 0)
                {
                    MenuRoutes("CompletedTasks");
                }
                else if (presscount == 1)
                {
                    MenuRoutes("IncompleteTasks");
                }
                else if (presscount == 2)
                {
                    MenuRoutes("MainMenu");
                }
            }
            else if (requiredMenu == "CompletedTasks")
            {
                List<List<Task>> completedTasks = SqliteDataAccess.LoadCompletedTasks();
                CheckListIsPopulated(completedTasks, "completed");
                List<List<string>> textBodies = ReturnTaskTextBody(completedTasks);
                int[] presscount = UserInterface.ReturnTaskSelection(textBodies);
                Task task = completedTasks[presscount[0]][presscount[1]];
                MenuRoutes(task);

            }
            else if (requiredMenu == "IncompleteTasks")
            {
                List<List<Task>> incompleteTasks = SqliteDataAccess.LoadIncompleteTasks();
                CheckListIsPopulated(incompleteTasks, "incomplete");
                List<List<string>> textBodies = ReturnTaskTextBody(incompleteTasks);
                int[] presscount = UserInterface.ReturnTaskSelection(textBodies);
                Task task = incompleteTasks[presscount[0]][presscount[1]];
                MenuRoutes(task);

            }
         }

        //overload of menu for task instance
        public static void MenuRoutes(Task task)
        {
            string[] taskInstanceMenu = { "Change completion status of task", "Change task", "Delete task" };
            int presscount = UserInterface.ReturnMenuSelection(taskInstanceMenu);
            

            
            if (presscount == 0)
            {
                task.IsCompleted = !task.IsCompleted;
                SqliteDataAccess.UpdateTask(task);
                MenuRoutes("MainMenu");
            }
            else if (presscount == 1)
            {
                string newText = UserInterface.GetText("Enter your updated task:");
                task.TextBody = newText;
                SqliteDataAccess.UpdateTask(task);
                Console.Clear();
                MenuRoutes("MainMenu");
            }
            else if (presscount == 2)
            {
                SqliteDataAccess.DeleteTask(task);
                MenuRoutes("MainMenu");
            }
        }

        private static List<List<string>> ReturnTaskTextBody(List<List<Task>> tasks)
        {
            List<List<string>> taskTextBodyLists = new List<List<string>>();

            foreach (List<Task> taskList in tasks)
            {
                List<string> taskTextBodies = new List<string>();
                foreach (Task task in taskList)
                {
                    taskTextBodies.Add(task.TextBody);
                }

                taskTextBodyLists.Add(taskTextBodies);

            }

            return taskTextBodyLists;
        }
        private static void CheckListIsPopulated(List<List<Task>> tasks, string listType)
        {
            if (tasks.Count == 0)
            {
                UserInterface.PrintNotification("You currently have no " + listType + " tasks");
                Thread.Sleep(2000);
                Console.Clear();
                MenuRoutes("MainMenu");
            }
        }
    }

   
}
    