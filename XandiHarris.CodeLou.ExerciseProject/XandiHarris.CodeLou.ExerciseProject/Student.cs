using System;
using System.Collections.Generic;
using System.Text;

namespace XandiHarris.CodeLou.ExerciseProject
{
    class Student
    {
        public int StudentId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string ClassName {get; set;}
        public DateTimeOffset StartDate {get; set;}
        public string LastClassCompleted {get; set;}
        public DateTimeOffset LastClassCompletedOn {get; set;}
        static void Main(string[] _1)
        {
            Console.WriteLine("Would you like to enter a New Student? y/n");
            var newStudent = Console.ReadLine();
            var students = new List<Student>();

            while (newStudent == "y")
            {
                Console.WriteLine("Enter Student Id");
                var studentId = Convert.ToInt32(Console.ReadLine());
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


                Console.WriteLine("Would you like to enter another new student? y/n");
                newStudent = Console.ReadLine();
                Console.WriteLine();
            }
            Console.WriteLine($"Student Id | Name |  Class  Started On | Last Class Finished On |"); ;

            foreach (var studentRecord in students)
            {
                Console.WriteLine($"{studentRecord.StudentId} | {studentRecord.FirstName} {studentRecord.LastName} | {studentRecord.ClassName}  {studentRecord.StartDate}  |  {studentRecord.LastClassCompleted}  {studentRecord.LastClassCompletedOn} "); ;
            }    
        }
    }
}
