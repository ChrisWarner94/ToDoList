using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ToDoList
{
    internal class UserInterface
    {
        
        public static int ReturnMenuSelection<T>(IEnumerable<T> menuToPrint)
        {
           
            int verticalPressCount = 0;
            PrintMenu(menuToPrint, verticalPressCount); 
            ConsoleKeyInfo keyPress = Console.ReadKey();
            while (keyPress.Key != ConsoleKey.Enter)
            {
                    switch (keyPress.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (verticalPressCount > 0)
                            {
                                verticalPressCount--;
                            }
                            Console.Clear();
                            PrintMenu(menuToPrint, verticalPressCount);
                            keyPress = Console.ReadKey(true);
                            break;

                        case ConsoleKey.DownArrow:
                            if (verticalPressCount < menuToPrint.Count() - 1)
                            {
                                verticalPressCount++;
                            }
                            Console.Clear();
                            PrintMenu(menuToPrint, verticalPressCount);
                            keyPress = Console.ReadKey(true);
                            break;
                    }
            }

            Console.Clear();
            return verticalPressCount;
        }

        public static int[] ReturnTaskSelection(List<List<string>> taskLists)
        {
            int verticalPressCount = 0;
            int horizontalPressCount = 0;

            List<string> listToPrint = taskLists[horizontalPressCount];
            PrintMenu(listToPrint, verticalPressCount);


            ConsoleKeyInfo keyPress = Console.ReadKey();
            while (keyPress.Key != ConsoleKey.Enter)
            {
                switch (keyPress.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (verticalPressCount > 0)
                        {
                            verticalPressCount--;
                        }
                        Console.Clear();
                        PrintMenu(listToPrint, verticalPressCount);
                        keyPress = Console.ReadKey(true);
                        break;

                    case ConsoleKey.DownArrow:
                        if (verticalPressCount < listToPrint.Count() - 1)
                        {
                            verticalPressCount++;
                        }
                        Console.Clear();
                        PrintMenu(listToPrint, verticalPressCount);
                        keyPress = Console.ReadKey(true);
                        break;

                    case ConsoleKey.LeftArrow:
                        if (horizontalPressCount > 0)
                        {
                            horizontalPressCount--;
                        }
                        listToPrint = taskLists[horizontalPressCount];
                        Console.Clear();
                        verticalPressCount = 0;
                        PrintMenu(listToPrint, verticalPressCount);
                        keyPress = Console.ReadKey(true);
                        break;

                     case ConsoleKey.RightArrow:
                        if (horizontalPressCount < taskLists.Count - 1)
                        {
                            horizontalPressCount++;
                        }
                        listToPrint = taskLists[horizontalPressCount];
                        Console.Clear();
                        verticalPressCount = 0;
                        PrintMenu(listToPrint, verticalPressCount);
                        keyPress = Console.ReadKey(true);
                        break;
                }
            }

            return new int[] { horizontalPressCount, verticalPressCount };
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

        public static void AddTask()
        {
            string taskToAdd = GetText("Enter the task you would like to add:");
            Task newTask = new Task(taskToAdd, false, DateTime.Now, null);
            SqliteDataAccess.SaveTask(newTask);
            Console.Clear();
            PrintNotification($"Task Added: {newTask.TextBody}");
            Thread.Sleep(1000);
            Console.Clear();
            InterfaceRouting.MenuRoutes("MainMenu");
        }

        public static void PrintNotification(string notification)
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

        public static string GetText(string prompt)
        {
            PrintNotification(prompt);
            string toReturn = Console.ReadLine();
            if (String.IsNullOrEmpty(toReturn))
            {
                CentreText("Please enter a valid task.");
                GetText(prompt);
            }
            return toReturn;
        }
    }

  
}
