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


                    case ConsoleKey.Backspace:
                        Console.Clear();
                        InterfaceRouting.MenuRoutes("MainMenu");
                        break;

                    default:
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
            int pageNumber = horizontalPressCount + 1;

            List<string> listToPrint = taskLists[horizontalPressCount];
            PrintNotification($"Page {pageNumber} of {taskLists.Count}");
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
                        PrintNotification($"Page {pageNumber} of {taskLists.Count}");
                        PrintMenu(listToPrint, verticalPressCount);
                        keyPress = Console.ReadKey(true);
                        break;

                    case ConsoleKey.DownArrow:
                        if (verticalPressCount < listToPrint.Count() - 1)
                        {
                            verticalPressCount++;
                        }
                        Console.Clear();
                        PrintNotification($"Page {pageNumber} of {taskLists.Count}");
                        PrintMenu(listToPrint, verticalPressCount);
                        keyPress = Console.ReadKey(true);
                        break;

                    case ConsoleKey.LeftArrow:
                        if (horizontalPressCount > 0)
                        {
                            horizontalPressCount--;
                            pageNumber--;
                        }
                        listToPrint = taskLists[horizontalPressCount];
                        Console.Clear();
                        verticalPressCount = 0;
                        PrintNotification($"Page {pageNumber} of {taskLists.Count}");
                        PrintMenu(listToPrint, verticalPressCount);

                        keyPress = Console.ReadKey(true);
                        break;

                    case ConsoleKey.RightArrow:
                        if (horizontalPressCount < taskLists.Count - 1)
                        {
                            horizontalPressCount++;
                            pageNumber++;
                        }
                        listToPrint = taskLists[horizontalPressCount];
                        Console.Clear();
                        verticalPressCount = 0;
                        PrintNotification($"Page {pageNumber} of {taskLists.Count}");
                        PrintMenu(listToPrint, verticalPressCount);

                        keyPress = Console.ReadKey(true);
                        break;
                    
                     case ConsoleKey.Backspace:
                        Console.Clear();
                        InterfaceRouting.MenuRoutes("MainMenu");
                        break;

                    default:
                        keyPress = Console.ReadKey(true);
                        break;
                }
            }

            Console.Clear();
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

            PrintNavigationInstructions();
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
            // stops an error being thrown if the text is longer than the console width
            if (text.Length > Console.WindowWidth)
            {
                Console.WriteLine(text);
            }
            else
            {
                int topBuffer = 1;
                Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop + topBuffer);
                Console.WriteLine(text);
            }

       
        }

        private static void LeftIndentCursor()
        {
            int consoleWidth = Console.WindowWidth;
            int leftIndent = consoleWidth / 8;
            Console.SetCursorPosition(leftIndent, 2);
        }

        public static string GetText(string prompt)
        {
            PrintNotification(prompt);
            LeftIndentCursor();
            string toReturn = Console.ReadLine();
            if (String.IsNullOrEmpty(toReturn))
            {
                CentreText("Please enter a valid value.");
                GetText(prompt);
            }
            else if (toReturn.Length > 90)
            {
                CentreText("Please enter a value less than 90 characters.");
                GetText(prompt);
            }
            Console.Clear();
            return toReturn;
        }

        private static void PrintNavigationInstructions()
        {
            Console.WriteLine();
            PrintNotification("-------------------------------------------------------------------------------------------------------");
            PrintNotification("Navigate: Arrow keys - Confirm: Enter - Return to Main Menu: Backspace");
        }
    }
}