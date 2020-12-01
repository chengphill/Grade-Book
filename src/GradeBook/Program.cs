using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GradeBook
{

    class Program
    {
        static void Main(string[] args)
        {
            var name = GetBookName();
            Console.WriteLine($"Opening: {name}\n");

            var book = new DiskBook(name);
            book.GradeAdded += OnGradeAdded;

            static void OnGradeAdded(object sender, EventArgs e)
            {
                System.Console.WriteLine("A grade was added");
            }

            EnterGrades(book);

            var stats = book.GetStatistics();

            PrintStatsMain(book, stats);
            book.PrintStatistics();
        }


        //everythin below could be in a seperate classes
        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or 'q' to quit");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);//format error
                    book.AddGrade(grade);//arguement error
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                finally
                {

                }
            }
        }

        private static string GetBookName()
        {
            while (true)
            {
                Console.WriteLine("Press '1' for new gradebook or '2' to load a previous gradebook");
                var input = Console.ReadLine();

                if (input == "1")
                {
                    //Takes user input for new book name, will load existing names too
                    Console.WriteLine("Enter a new name for gradebook");
                    var name = Console.ReadLine();
                    if (File.Exists(name + ".txt"))
                    {
                        System.Console.WriteLine("\nFile exists and will now load");
                    }
                    if (name == "" || name == null)
                    {
                        while (true)
                        {
                            System.Console.WriteLine("Invalid name, please enter a new name");
                            var newName = Console.ReadLine();
                            if (!String.IsNullOrWhiteSpace(newName))
                            {
                                return newName;
                            }

                        }
                    }
                    else
                    {
                        return name;
                    }
                }
                else if (input == "2")
                {
                    //Finds all book names by searching for txt files and displays them to user for them to choose from
                    Console.WriteLine("Select existing gradebook by pressing the number corresponding to the name on the left");
                    int count = 0;

                    var sourceDirectory = Directory.GetCurrentDirectory();
                    var txtFiles = Directory.EnumerateFiles(sourceDirectory, "*.txt", SearchOption.TopDirectoryOnly).Select(x => Path.GetFileName(x)).Select(x => Path.ChangeExtension(x, null)).ToList();
                    foreach (string file in txtFiles)
                    {
                        count++;
                        Path.ChangeExtension(file, null);
                        Console.WriteLine($"{count}: {file}");
                    }
                    var name = 0;
                    do
                    {
                        count = 0;
                        var loadName = Console.ReadLine();
                        name = int.Parse(loadName);
                        if (name > 0 && name <= txtFiles.Count())
                        {
                            return txtFiles[name - 1];
                        }
                        else
                        {
                            System.Console.WriteLine("Invalid input, try again from list");
                            foreach (string file in txtFiles)
                            {
                                count++;
                                Path.ChangeExtension(file, null);
                                Console.WriteLine($"{count}: {file}");
                            }
                        }
                        count = 0;
                    } while (true);
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again.\n");
                }
            }
        }

        private static void PrintStatsMain(IBook book, Statistics stats)
        {
            System.Console.WriteLine($"\nFor the book named {book.Name}");
            Console.WriteLine($"Program.cs 23->30\nThe lowest grade is {stats.Low}");
            Console.WriteLine($"The highest grade is {stats.High}");
            System.Console.WriteLine($"Average = {stats.Average:N3}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }

    }
}



/* Unused code, why here? To archive older code as progress            

             //array practice

            var numbers = new double[] { 12.70, 13.10, 12.10, 4.18};
            var result1 = -1.0;
            var result2 = 0.00;
            var result3 = 0.00;
            var averageList = 0.0;
            var highGrade = double.MinValue;
            var lowGrade = double.MaxValue;

            foreach(var number in numbers)
            {
                result2 += number;
            }

            for (int x = 0; x < numbers.Length; x++)
            {
                result1 += numbers[x]; 
            }
            System.Console.WriteLine($"\nLINE 35\nResult 1 = {result1} \nResult 2 = {result2}");

            //list practice
            var grades = new List<double>() { 12.70, 13.10, 12.10, 5.37};
            grades.Add(56.1);

            foreach(var number in grades)
            {
                highGrade = Math.Max(number, highGrade);
                lowGrade = Math.Min(number, lowGrade);
                result3 += number;
            }
            averageList = result3 / grades.Count;
            //N3 -> 3 decimal points after
            Console.WriteLine($"\nLINE 49\nThe lowest grade is {lowGrade}");
            Console.WriteLine($"The highest grade is {highGrade}");
            System.Console.WriteLine($"Result 3 = {result3} Average = {averageList:N3}");

            //args practice
            if(args.Length > 0)
            {
                Console.WriteLine($"Hello {args[0]}!");
            }
            else
            {
                Console.WriteLine("Hello!");
            }


*/