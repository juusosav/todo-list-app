// See https://aka.ms/new-console-template for more information

using System.Text.Json;

class Program
{
    static void Main()
    {

        var tasks = new List<string>();

        string filePath = "tasks.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            tasks = JsonSerializer.Deserialize<List<string>>(json) ?? [];
        }


        while (true)
        {
            Console.Clear();

            Console.WriteLine("To-Do List Application");
            Console.WriteLine("----------------------");

            Console.WriteLine("Select an operation (1, 2, 3 or 4 and press Enter): ");

            Console.WriteLine("\n1. Add Task");
            Console.WriteLine("2. Remove Task");
            Console.WriteLine("3. Display All Tasks");
            Console.WriteLine("4. Clear Task List");
            Console.WriteLine("5. Exit Application");
            Console.Write("\nChoice: ");

            string? rawChoice = Console.ReadLine();
            if (!int.TryParse(rawChoice, out int initialChoice))
            {
                Console.WriteLine("\nInvalid choice. Please enter a number between 1 and 4.");
                Console.WriteLine("Press any key to return to menu.");
                Console.ReadKey();
                Todos.ClearBelowMenu(8);
                continue;
            }

            switch (initialChoice)
            {
                case 1:
                    Console.WriteLine("\nPlease enter a task");
                    Console.Write("Name: ");
                    string? userAddChoice = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(userAddChoice))
                    {
                        tasks.Add(userAddChoice);
                        File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
                        Console.WriteLine("\nTask added.");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("\nEmpty task was not added.");
                        Thread.Sleep(1000);
                    }
                    break;

                case 2:
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("\nNo tasks to remove, list is empty.");
                        Thread.Sleep(1000);
                        break;
                    }
                    Console.WriteLine("\nCurrent tasks: ");
                    Todos.DisplayAll(tasks);

                    Console.WriteLine("\nEnter the number of the task to remove:");
                    Console.Write("Choice: ");
                    string? userRemoveString = Console.ReadLine();

                    if (!int.TryParse(userRemoveString, out int userRemoveChoice))
                    {
                        Console.WriteLine("\nInvalid input. Please enter a valid number.");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        int indexToRemove = userRemoveChoice - 1; // convert to 0-based index

                        if (indexToRemove < 0 || indexToRemove >= tasks.Count)
                        {
                            Console.WriteLine("Sorry, that task number does not exist.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            string removed = tasks[indexToRemove];
                            tasks.RemoveAt(indexToRemove);
                            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
                            Console.WriteLine($"Removed task: {removed}");
                            Thread.Sleep(1000);
                        }
                    }
                    break;

                case 3:
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("\nNo tasks in the list.");
                    }
                    else
                    {
                        Console.WriteLine("\nTask list: ");
                        Todos.DisplayAll(tasks);
                        Thread.Sleep(1000);
                    }
                    break;

                case 4:

                    Console.WriteLine("Are you sure you want to clear the task list? (y/n)");

                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        if (tasks.Count > 0)
                        {
                            tasks.Clear();
                            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
                            Console.WriteLine("\nTask list cleared!");
                            Thread.Sleep(100);
                        }
                        else
                        {
                            Console.WriteLine("\nTask list empty, no clearing required.");
                            Thread.Sleep(1000);
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.N)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please choose 'y' or 'n' key");
                    }
                    break;

                case 5:
                    Console.WriteLine("\nExit application? (y/n)");

                    while (true)
                    {
                        ConsoleKeyInfo keyInfo1 = Console.ReadKey(intercept: true);

                        if (keyInfo1.Key == ConsoleKey.Y)
                        {
                            Console.WriteLine("\nExiting...");
                            Thread.Sleep(1000);
                            Environment.Exit(0);
                        }

                        else if (keyInfo1.Key == ConsoleKey.N)
                        {
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Please choose 'y' or 'n' key");
                        }
                    }
                    break;
            }

            Console.WriteLine("\nReturn to main selection? (y/n)");

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Y)
                {
                    Todos.ClearBelowMenu(8);
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    Console.WriteLine("Exiting...");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Please choose 'y' or 'n' key");
                }
            }

        }
    }
}

