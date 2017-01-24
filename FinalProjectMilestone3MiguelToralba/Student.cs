//Miguel Toralba CIS 345 12:00PM
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectMilestone3MiguelToralba
{
    class Student
    {
        // Instance Variables
        private string studentID = "";
        private string firstName = "";
        private string lastName = "";
        private double gpa = 0.00;
        private bool graduateStatus = false;
        private int gmatScore = 0;
        private string courseHistory = "";
        //Student Properties
        public string StudentID 
        {
            get { return this.studentID; }
            set { this.studentID = value;}
        }
        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value;}
        }
        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }
        public double GPA
        {
            get { return this.gpa; }
            set { this.gpa = value; }
        }
        public bool GraduateStatus
        {
            get { return this.graduateStatus; }
            set { this.graduateStatus = value; }
        }
        public int GMATScore
        {
            get { return this.gmatScore; }
            set { this.gmatScore = value; }
        }
        public string CourseHistory
        {
            get { return this.courseHistory; }
            set { this.courseHistory = value; }
        }
        //Default Constructor
        public Student()
        {
        }
        //Student Constructor Method
        public Student(string studentID, string firstName, string lastName, double gpa, bool graduateStatus, int gmatScore, string[] courseHistory)
        {
            this.studentID = studentID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.gpa = gpa;
            this.graduateStatus = graduateStatus;
            this.gmatScore = gmatScore;
            this.courseHistory = this.convertCourseHistoryArrayToString(courseHistory);
        }
        private string convertCourseHistoryArrayToString(string[] courses)
        {
            string courseHistory = "";
            foreach(string courseName in courses)
            {
                courseHistory += courseName + ';';
            }

            return courseHistory;
        }
        
    }
}
