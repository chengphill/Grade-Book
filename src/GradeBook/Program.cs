using System;
using System.Collections.Generic;

namespace GradeBook
{

    class Program
    {
        static void Main(string[] args)
        {
            var book = new DiskBook("Phil's Grade Book");
            book.GradeAdded += OnGradeAdded;

            static void OnGradeAdded(object sender, EventArgs e)
            {
                System.Console.WriteLine("A grade was added");
            }

            EnterGrades(book);

            // book.PrintStatistics();

            var stats = book.GetStatistics();

            PrintStatsMain(book, stats);
        }

        private static void PrintStatsMain(IBook book, Statistics stats)
        {
            System.Console.WriteLine($"\nFor the book named {book.Name}");
            Console.WriteLine($"Program.cs 22\nThe lowest grade is {stats.Low}");
            Console.WriteLine($"The highest grade is {stats.High}");
            System.Console.WriteLine($"Average = {stats.Average:N3}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }

        //this could be in a new class
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