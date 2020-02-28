using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace SvetlanaSurzhan.CodeLou.ExerciseProject
{
    class Program
    {
        static string _studentRepositoryPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\students.json"; // "C:Desktop/ProjectName/student.json"

        static void Save(List<Student> listOfSudents)
        {
            using (var file = File.CreateText(_studentRepositoryPath))
            {
                
                file.WriteAsync(JsonSerializer.Serialize(listOfSudents));

            }

        }

    
        static List<Student> Read(string filePath)
        {
            return JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(filePath));
        }

        static void Main(string[] args)
        {
            List<Student> _allStudents = File.Exists(_studentRepositoryPath) ? Read(_studentRepositoryPath) : new List<Student>();

            _allStudents.Add(new Student(){
                StudentId = 1,
                FirstName = "Test",
                ClassName = "a"
            });            
            
            _allStudents.Add(new Student(){
                StudentId = 2,
                FirstName = "Test2",
                ClassName = "b"
            });

            bool showMenu = true;

            while (showMenu)
            {
                showMenu = ShowMainMenu(ref _allStudents);
            }
        }
        static bool ShowMainMenu(ref List<Student> allStudents)
        {

            CreateMainManue();

            var inputOption = Console.ReadLine();

            switch (inputOption)
            {
                case "1":
                    allStudents = InputStudent(allStudents);
                    return true;
                case "2":
                    ShowListOfStudents(allStudents);
                    return true;
                case "3":
                    ShowStudentsByClassName(allStudents);
                    return true;
                case "4":
                    return false;
                default:
                    return true;
            }
        }

        static void CreateMainManue()
        {
            Console.Clear();
            Console.WriteLine("Menu");
            Console.WriteLine("1) New Student");
            Console.WriteLine("2) List Students");
            Console.WriteLine("3) Find Student By Class");
            Console.WriteLine("4) Exit");
            Console.Write("\r\nSelect an option: ");
        }

        static List<Student> InputStudent(List<Student> students)
        {
            Console.WriteLine("Enter Student Id");
            var studentId = Convert.ToInt32(Console.ReadLine());
            bool isStudentIdExist = students.Any(s => s.StudentId == studentId);
            if (isStudentIdExist == true)
            {
                Console.WriteLine("This Student Id already exists.");
                Console.ReadKey();
                return students;
            }
            Console.WriteLine("Enter First Name");
            var studentFirstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name");
            var studentLastName = Console.ReadLine();
            Console.WriteLine("Enter Class Name");
            var className = Console.ReadLine();
            Console.WriteLine("Enter Last Class Completed");
            var lastClass = Console.ReadLine();
            Console.WriteLine("Enter Last Class Completed Date in format MM/dd/YYYY");
            var lastCompletedOn = DateTimeOffset.Parse(Console.ReadLine());
            Console.WriteLine("Enter Start Date in format MM/dd/YYYY");
            var startDate = DateTimeOffset.Parse(Console.ReadLine());

            var studentRecord = new Student();
            studentRecord.StudentId = studentId;
            studentRecord.FirstName = studentFirstName;
            studentRecord.LastName = studentLastName;
            studentRecord.ClassName = className;
            studentRecord.StartDate = startDate;
            studentRecord.LastClassCompleted = lastClass;
            studentRecord.LastClassCompletedOn = lastCompletedOn;

            students.Add(studentRecord);

            Console.WriteLine($"Student Id | Name |  Class ");
            Console.WriteLine($"{studentRecord.StudentId} | {studentRecord.FirstName} {studentRecord.LastName} | {studentRecord.ClassName} "); ;
            
            ShowListOfStudents(students);
            Save(students);
            Console.ReadKey();
            //add current studet to list


            return students;
        }

        static void ShowListOfStudents(List<Student> students)
        {
            Console.WriteLine($"Student Id | Name |  Class ");

            foreach (var studentRecord in students)
            {
                Console.WriteLine($"{studentRecord.StudentId} | {studentRecord.FirstName} {studentRecord.LastName} | {studentRecord.ClassName} ");
            }

            Console.ReadKey();
        }

        static void ShowStudentsByClassName(List<Student> students)
        {
            Console.WriteLine("Please enter class name.");

            var inputtedClassName = Console.ReadLine().ToLower();
            var fliteredListOfStudents = students.FindAll(student => student.ClassName == inputtedClassName);

            if (fliteredListOfStudents != null)
            {
                ShowListOfStudents(fliteredListOfStudents);
            }
            else
            {
                Console.WriteLine($"There is no student in '{inputtedClassName}' Class");
            }

            Console.ReadKey();
        }
    }

}

