using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.IO;

namespace XandiHarris.CodeLou.ExerciseProject
{
    class Program
    {
        public static List<Student> students = new List<Student>();
        public static Dictionary<string, Student> studentDictionary = new Dictionary<string, Student>();

        static void Main()
        {
            bool showMenu = true;
            LoadStudents();
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
        public static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Menu");
            Console.WriteLine("1. New Student");
            Console.WriteLine("2. List Students");
            Console.WriteLine("3. Find Students By Name");
            Console.WriteLine("4. Find Students By Class");
            Console.WriteLine("5. Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    NewStudentEntry();
                    return true;
                case "2":
                    ListStudents();
                    return true;
                case "3":
                    SearchStudents();
                    return true;
                case "4":
                    SearchClasses();
                    return true;
                case "5":
                    return false;
                default:
                    return true;
            }
        }
        public static void NewStudentEntry()
        {
            Console.WriteLine("Would you like to enter a New Student? y/n");
            string enterStudent = Console.ReadLine();

            while (enterStudent == "y")
            {
                bool validated = false;
                int studentId;
                do
                {
                    Console.WriteLine("Enter Student ID Number");
                    validated = int.TryParse(Console.ReadLine(), out studentId);
                    if (validated)
                    {
                        if (students.Exists(s => s.StudentId == studentId))
                        {
                            Console.WriteLine("Student already exists in registry with this ID number.");
                            validated = false;
                        }
                    }
                } while (!validated);
                Console.WriteLine("Enter First Name");
                var studentFirstName = Console.ReadLine();
                Console.WriteLine("Enter Last Name");
                var studentLastName = Console.ReadLine();
                Console.WriteLine("Enter Class Name");
                var className = Console.ReadLine();
                validated = false;
                DateTimeOffset startDate;
                do
                {
                    Console.WriteLine("Enter Class Start Date");
                    validated = DateTimeOffset.TryParse(Console.ReadLine(), out startDate);
                } while (!validated);
                Console.WriteLine("Enter Last Course Completed");
                var lastClass = Console.ReadLine();
                validated = false;
                DateTimeOffset lastCompletedOn;
                do
                {
                    Console.WriteLine("Enter Last Course Completion Date");
                    validated = DateTimeOffset.TryParse(Console.ReadLine(), out lastCompletedOn);
                    if (validated)
                    {
                        if (lastCompletedOn >= startDate)
                        {
                            Console.WriteLine("Course must have been completed before the current course start date!");
                            validated = false;
                        }
                    }
                } while (!validated);

                var newStudent = new Student
                {
                    StudentId = studentId,
                    FirstName = studentFirstName,
                    LastName = studentLastName,
                    ClassName = className,
                    StartDate = startDate,
                    LastClassCompleted = lastClass,
                    LastClassCompletedOn = lastCompletedOn
                };

                students.Add(newStudent);
                studentDictionary.Add(newStudent.FirstName + " " + newStudent.LastName, newStudent);

                Console.WriteLine("Would you like to enter another new student? y/n");
                enterStudent = Console.ReadLine();
                Console.WriteLine();
            }
            SaveStudents();
        }
        public static void ListStudents()
        {
            foreach (var student in students)
            {
                Console.WriteLine("");
                Console.WriteLine($"Student ID: {student.StudentId}");
                Console.WriteLine($"Student name: {student.FirstName} {student.LastName}");
                Console.WriteLine($"Current class: {student.ClassName} started {student.StartDate}");
                Console.WriteLine($"Previous class: {student.LastClassCompleted} finished {student.LastClassCompletedOn}");
                Console.WriteLine("");
            }
            Console.WriteLine("Press any Key to return to menu");
            Console.ReadKey();
        }
        public static void ListStudents(Student student)
        {
            Console.WriteLine("");
            Console.WriteLine($"Student ID: {student.StudentId}");
            Console.WriteLine($"Student name: {student.FirstName} {student.LastName}");
            Console.WriteLine($"Current class: {student.ClassName} started {student.StartDate}");
            Console.WriteLine($"Previous class: {student.LastClassCompleted} finished {student.LastClassCompletedOn}");
            Console.WriteLine("");
            Console.WriteLine("Press any Key to return to menu");
            Console.ReadKey();
        }
        public static void ListStudents(IEnumerable<Student> selectedStudents)
        {
            foreach (Student student in selectedStudents)
            {
                ListStudents(student);
            }
        }

        public static void SearchStudents()
        {
            Console.WriteLine("Please enter a Student's FIRST or LAST NAME to search for.");
            var searchName = Console.ReadLine();
            var selectedStudents = students.Where(s => s.FirstName.IndexOf(searchName, StringComparison.CurrentCultureIgnoreCase) >= 0 || s.LastName.IndexOf(searchName, StringComparison.CurrentCultureIgnoreCase) >= 0);
            if (selectedStudents.Any())
            {
                ListStudents(selectedStudents);
            }
            else
            {
                Console.WriteLine($"{searchName} Is not found in student records. Please check spelling of Name.");
            }
        }
        public static void SearchClasses()
        {
            Console.WriteLine("Please enter a CLASS to search for.");
            var searchName = Console.ReadLine();
            var selectedStudents = students.Where(s => s.ClassName.IndexOf(searchName, StringComparison.CurrentCultureIgnoreCase) >= 0);
            if (selectedStudents.Any())
            {
                ListStudents(selectedStudents);
            }
            else
            {
                Console.WriteLine($"{searchName} not found in class records. Please check spelling of Class.");
            }
        }
        public static void SaveStudents()
        {
            var serializer = new JsonSerializer();
            string fileName = "Students.JSON";
            using (var writer = new StreamWriter(fileName))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, students);
            }
        }
        public static void LoadStudents()
        {
            string fileName = "Students.JSON";
            if (File.Exists(fileName))
            {
                var serializer = new JsonSerializer();
                using (var reader = new StreamReader(fileName))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    students = serializer.Deserialize<List<Student>>(jsonReader);
                }
            }
            else
            {
                Console.WriteLine("Students database not found on disk.  Will be created upon adding students.");
            }
        }
    }
}
