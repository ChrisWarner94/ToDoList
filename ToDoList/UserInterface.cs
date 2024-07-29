using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace ToDoList
{
    internal class UserInterface
    {
        
        



        public static void MenuManager()
        {
            int keyPressCount = 0;
            Task[] tasks = Task.GetAllTasks().ToArray();
            string[] mainMenu = { "Add Task", "View Tasks", "Exit" };
            string[] taskMenu = { "Change Completed Status", "Update Task", "Delete Task", "Return to Main Menu" };
            PrintMenu(mainMenu, 0);

            ConsoleKeyInfo keyPress = Console.ReadKey();
            while (keyPress.Key != ConsoleKey.Enter)
            {
                switch (keyPress.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (keyPressCount > 0)
                        {
                            keyPressCount--;
                        }
                        Console.Clear();
                        PrintMenu(mainMenu, keyPressCount);
                        keyPress = Console.ReadKey(true);
                        break;

                    case ConsoleKey.DownArrow:
                        if (keyPressCount < mainMenu.Length - 1)
                        {
                            keyPressCount++;
                        }
                        Console.Clear();
                        PrintMenu(mainMenu, keyPressCount);
                        keyPress = Console.ReadKey(true);
                        break;
                }
            }

            if (keyPressCount == 0)
            {
                Console.Clear();
                string taskToAdd = GetText("Enter the task you would like to add:");
                Task newTask = new Task(taskToAdd);
                Console.Clear();
                PrintNotification($"Task Added: {newTask.TextBody}");
                Thread.Sleep(1000);
                Console.Clear();
                MenuManager();
            }
            else if (keyPressCount == 1)
            {
                Console.Clear();
                PrintNotification("Your Tasks");
                tasks = Task.GetAllTasks().ToArray();
                PrintMenu(tasks.Select(t => t.TextBody).ToArray(), 0);    
                Console.WriteLine("Press any key to return to the main menu"); //insert here
                Console.ReadKey();
                Console.Clear();
                MenuManager();
            }
            else if (keyPressCount == 2)
            {
                Environment.Exit(0);
            }
        }


        private static void PrintMenu<T>(IEnumerable<T> menuToPrint, int keyPressCount)
        {
            // Converts the IEnumerable<T> to a list to allow for indexing
            var menuList = menuToPrint.ToList();

            for (int i = 0; i < menuList.Count; i++)
            {
                if (keyPressCount == i)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                // converts list object to string to allow for centering
                string menuListConverted = menuList[i].ToString();
                CentreText(menuListConverted);
                Console.ResetColor();
            }

          

        }

        private static void PrintNotification(string notification)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            CentreText(notification);
            Console.ResetColor();
        }
        private static void CentreText(string text)
        {
            int topBuffer = 1;
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop + topBuffer);
            Console.WriteLine(text);
        }

        private static string GetText(string prompt)
        {
            Console.WriteLine(prompt);
            string toReturn = Console.ReadLine();
            if (String.IsNullOrEmpty(toReturn))
            {
                Console.WriteLine("Please enter a valid task.");
                GetText(prompt);
            }

            return toReturn;

        }
    }

  
}
