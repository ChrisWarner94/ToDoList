using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
                    UserInterface.AddTask();
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
                List<Task> completedTasks = SqliteDataAccess.LoadCompletedTasks();
                CheckListIsPopulated(completedTasks, "completed");
                List<string> textBodies = ReturnTaskTextBody(completedTasks);
                int presscount = UserInterface.ReturnMenuSelection(textBodies);
                Task task = completedTasks[presscount];
                MenuRoutes(task);

            }
            else if (requiredMenu == "IncompleteTasks")
            {
                List<Task> incompleteTasks = SqliteDataAccess.LoadIncompleteTasks();
                CheckListIsPopulated(incompleteTasks, "incomplete");
                List<string> textBodies = ReturnTaskTextBody(incompleteTasks);
                int presscount = UserInterface.ReturnMenuSelection(textBodies);
                Task task = incompleteTasks[presscount];
                Console.WriteLine(textBodies.Count);
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

        private static List<string> ReturnTaskTextBody(List<Task> tasks)
        {
            List<string> taskTextBody = new List<string>();
            foreach (var task in tasks)
            {
                taskTextBody.Add(task.TextBody);
            }
            return taskTextBody;
        }

        private static void CheckListIsPopulated(List<Task> tasks, string listType)
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
    