using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool weighted) : base(name, weighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            int gradeBreakPoint = Students.Count / 5;
            var studentsAverageGrades = new List<double>();
            int gradePosition = 1;

            if (Students.Count < 5)
            {
                throw new InvalidOperationException();
            }

            foreach (Student student in Students)
            {
                studentsAverageGrades.Add(student.AverageGrade);
            }

            studentsAverageGrades.Sort();
            studentsAverageGrades.Reverse();

            foreach(var grade in studentsAverageGrades)
            {
                if(averageGrade < grade)
                {
                    gradePosition++;
                }
                else
                {
                    break;
                }
            }

            switch (gradePosition / gradeBreakPoint)
            {
                case 1:
                    return 'A';

                case 2:
                    return 'B';

                case 3:
                    return 'C';

                case 4:
                    return 'D';

                default:
                    return 'F';
            }
        }

        public override void CalculateStatistics()
        {
            if(Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order" +
                    " to properly calculate a student's overall grade.");

                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if(Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order" +
                    " to properly calculate a student's overall grade.");

                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
