//Miguel Toralba CIS 345 12:00PM
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectMilestone3MiguelToralba
{
    class Course
    {
        //Instance Variable
        private string subject = "";
        private string courseNumber = "";
        private string courseTitle = "";
        private int units = 0;
        private string startTime = "";
        private string days = "";
        private int seats = 0;
        private string prerequisites;
        private string[] prerequisitesArray;
        //Course Properties
        public string Subject
        {
            get { return this.subject; }
            set { this.subject = value; }
        }
        public string CourseNumber
        {
            get { return this.courseNumber; }
            set { this.courseNumber = value; }
        }
        public string CourseTitle
        {
            get { return this.courseTitle; }
            set { this.courseTitle = value; }
        }
        public int Units
        {
            get { return this.units; }
            set { this.units = value; }
        }
        public string StartTime
        {
            get { return this.startTime; }
            set { this.startTime = value; }
        }
        public string Days
        {
            get { return this.days; }
            set { this.days = value; }
        }
        public int Seats
        {
            get { return this.seats; }
            set { this.seats = value; }
        }
        public string Prerequisites
        {
            get { return this.prerequisites; }
            set { this.prerequisites = value; }
        }
        public string[] PrerequisitesArray
        {
            get { return this.prerequisitesArray; }
            set { this.prerequisitesArray = value; }
        }
        //Default Constructor
        public Course()
        {
        }
        //Course Constructor Method
        public Course(string subject, string courseNumber, string courseTitle, int units, string startTime, string days, int seats, string[] prerequisites)
        {
            this.subject = subject;
            this.courseNumber = courseNumber;
            this.courseTitle = courseTitle;
            this.units = units;
            this.startTime = startTime;
            this.days = days;
            this.seats = seats;
            this.prerequisites = this.convertPrerequisitesArrayToString(prerequisites);
            this.prerequisitesArray = prerequisites;
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} {6}",
                Subject,
                CourseNumber,
                CourseTitle,
                Units,
                StartTime,
                Days,
                Seats);
        }
        private string convertPrerequisitesArrayToString(string[] prereqs)
        {
            string prerequisites = "";
            if (prereqs == null)
            {
                return prerequisites;
            }
            foreach (string prerequisiteName in prereqs)
            {
                prerequisites += prerequisiteName + ';';
            }

            return prerequisites;
        }
    }
}
