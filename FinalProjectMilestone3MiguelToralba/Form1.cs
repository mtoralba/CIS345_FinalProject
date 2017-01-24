//Miguel Toralba CIS 345 12:00PM
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FinalProjectMilestone3MiguelToralba
{
    public partial class universityRegistrationApp : Form
    {
        bool editMode = false;
        List<Student> studentList = new List<Student>();
        List<Course> courseList = new List<Course>();
        Course selectedCourse;      
        Schedule schedule;
        Student foundStudent;
        public universityRegistrationApp()
        {
            InitializeComponent();

            scheduleCourseListBox1.Enabled = false;
            addCourseToScheduleButton.Enabled = false;

            //Load Courses
            Course CIS105 = new Course("CIS", "105", "Computer Appls & Info Technology", 10, "9:00am", "T TH", 1, null);
            Course CIS235 = new Course("CIS", "235", "Intro to Information Systems", 6, "10:30am", "T TH", 2, null);
            Course CIS340 = new Course("CIS", "340", "Bus Info Systems Devlopment I", 3, "12:00pm", "T TH", 3, new string[] { "CIS 105", "CIS 235"});
            Course CIS345 = new Course("CIS", "345", "Bus Info Systems Devlopment II", 3, "4:30am", "T TH", 4, new string[] { "CIS 105", "CIS 235", "CIS 340"});
            Course CIS365 = new Course("CIS", "365", "Business Database Systems", 3, "9:00am", "T TH", 5, new string[] { "CIS 105", "CIS 235" });
            Course CIS425 = new Course("CIS", "425", "Electronic Commerce Strategy", 4, "9:00am", "M W F", 6, new string[] { "CIS 105", "CIS 235", "CIS 340", "CIS 345" });
            Course CIS435 = new Course("CIS", "430", "Networks/Distributed Systems", 4, "10:30am", "M W F", 7, new string[] { "CIS 105", "CIS 235", "CIS 340", "CIS 345", "CIS425" });
            Course CIS440 = new Course("CIS", "440", "Systems Design/Electronic Commerce", 4, "12:00pm", "M W F", 8, new string[] { "CIS 105", "CIS 235", "CIS 340", "CIS 345", "CIS425", "CIS 430" });
            Course WPC101 = new Course("WPC", "101", "Student Success in Business", 1, "9:00am", "M W F", 9, null);
            Course WPC301 = new Course("WPC", "301", "Business Forum", 3, "10:30am", "M W F", 10, new string[] { "WPC 101", "ENG 102" });
            Course ENG102 = new Course("ENG", "102", "First-Year Composition", 1, "9:00am", "M W", 11, null);
            Course ECN211 = new Course("ECN", "211", "Macroeconomic Principles", 2, "6:00pm", "M W", 12, null);
            Course ECN212 = new Course("ECN", "212", "Microeconomic Principles", 2, "6:00pm", "M W", 13, null);
            Course FIN300 = new Course("FIN", "300", "Fundamentals of Finance", 3, "9:00am", "M W", 14, new string[] { "WPC 101", "ENG 102" });
            Course MKT300 = new Course("MKT", "300", "Org & Mgt Leadership", 3, "9:00am", "M W", 0, new string[] { "WPC 101", "ENG 102" });

            //Add each course to courseList
            courseList.Add(CIS105);
            courseList.Add(CIS235);
            courseList.Add(CIS340);
            courseList.Add(CIS345);
            courseList.Add(CIS365);
            courseList.Add(CIS425);
            courseList.Add(CIS435);
            courseList.Add(CIS440);
            courseList.Add(WPC101);
            courseList.Add(WPC301);
            courseList.Add(ENG102);
            courseList.Add(ECN211);
            courseList.Add(ECN212);
            courseList.Add(FIN300);
            courseList.Add(MKT300);

            //Load courseList into courseComboBox for student form
            //studentCourseHistoryComboBox.DataSource = courseList;
            //Load courseList into courseComboBox for course form
            //courseFormCourseHistoryComboBox.DataSource = courseList;

            //courseListBox.DataSource = courseList;
            foreach (Course c in courseList)
            {
                studentCourseHistoryComboBox.Items.Add(c);
                courseFormCourseHistoryComboBox.Items.Add(c);
                courseListBox.Items.Add(c);
                scheduleCourseListBox1.Items.Add(c);
            }

            // Pupulate courseListBox
            //PopulateCourseData();

            // Load students from csv file
            string[] data;
            // if file exist then read the data
            if (File.Exists("studentFile.csv"))
            {
                FileStream fs = new FileStream("studentFile.csv", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string line = sr.ReadLine();

                while (line != null)
                {
                    data = line.Split(',');

                    Student tempStudent = new Student(data[0], data[1], data[2], Convert.ToDouble(data[3]), Convert.ToBoolean(data[4]), Convert.ToInt32(data[5]), data[6].Split(';'));

                    studentList.Add(tempStudent);
                    line = sr.ReadLine();
                }
                sr.Close();
                fs.Close();
                //Populate studentListBox
                populateStudentData();
            }

        }
        // addNewStudentButton student button
        private void button1_Click(object sender, EventArgs e)
        {
            //Validate student ID
            if (studentIDTbx.Text.Length != 10 )
            {
                MessageBox.Show("Please enter a valid 10 digit student ID number!");
                return;
            }
            else if (Convert.ToInt32(studentIDTbx.Text) < 0)
            {
                MessageBox.Show("Please enter a valid positive student ID number!");
                return;
            }
            //Validate GPA
            if(!(Convert.ToDouble(studentGPATbx.Text) >= 0 && Convert.ToDouble(studentGPATbx.Text) <= 4.0))
            {
                MessageBox.Show("Please enter a valid GPA between 0.00 and 4.00!");
                return;
            }
            //GMAT score validation
            if (!(Convert.ToDouble(gmatScoreTbx.Text) >= 200 && Convert.ToDouble(gmatScoreTbx.Text) <= 800))
            {
                MessageBox.Show("Please enter a valid GMAT score between 200 and 800!");
                return;
            }
            //if not graduate, set gmatScore to 0
            if(graduateStatusCheckBox.Checked == false)
            {
                gmatScoreTbx.Text = "0";
            }
            //Load studentCourseHistoryComboBox.Text into courseHistoryListBox.Items
            courseHistoryListBox.Items.Add(string.Format(studentCourseHistoryComboBox.Text));

            //store selected courseHistoryListbox items into courseHistory array
            string[] courseHistoryArray = new string[courseHistoryListBox.Items.Count -1];
            int itemCount = courseHistoryListBox.Items.Count;
            for (int i = 0; i < itemCount -1 ; i++)
            {
                courseHistoryArray[i] = courseHistoryListBox.Items[i].ToString();
            }

            //Create newStudent object
            Student newStudent = new Student(studentIDTbx.Text, studentFirstNameTbx.Text, studentLastNameTbx.Text, 
                Convert.ToDouble(studentGPATbx.Text), graduateStatusCheckBox.Checked, Convert.ToInt32(gmatScoreTbx.Text), courseHistoryArray);

            studentList.Add(newStudent);

            // Write to file upon saving a new employee
            // if the file does not exist create the file  
            if (!File.Exists("studentFile.csv"))
            {
                FileStream fs = new FileStream("studentFile.csv", FileMode.Create, FileAccess.Write);
                fs.Close();
            }

            if (!editMode)
            {
                StreamWriter sw = File.AppendText("studentFile.csv"); // without append data will be overwritten
                sw.WriteLine("{0},{1},{2},{3},{4},{5},{6}", newStudent.StudentID, newStudent.FirstName, newStudent.LastName, newStudent.GPA, newStudent.GraduateStatus, newStudent.GMATScore, newStudent.CourseHistory);
                sw.Close();
            }
            else
            {
                //Delete selected student from studentFile
                studentList.RemoveAt(studentListBox.SelectedIndex);
                //Delete selected student from studentListBox
                studentListBox.Items.RemoveAt(studentListBox.SelectedIndex);

                overwriteCSVFile();
            }

            //Populate student data into studentListBox
            populateStudentData();

            //Clear fields
            clearStudentFormFields();
            editMode = false;
        }
        //Populate student data method
        private void populateStudentData()
        {
            //Clear studentListBox
            studentListBox.Items.Clear();
            //Display student data to studentListBox 
            foreach (Student s in studentList)
            {
                if (s != null)
                {
                    this.studentListBox.Items.Add(string.Format("{0},{1},{2},{3:0.00},{4},{5},{6}",
                        s.StudentID, s.FirstName, s.LastName, s.GPA, s.GraduateStatus, s.GMATScore, s.CourseHistory));
                }
            }
        }        
        //Delete student from studentList
        private void button3_Click(object sender, EventArgs e)
        {
            editMode = false;

            if(studentListBox.SelectedIndex != -1)
            {
                //Delete selected student from studentFile
                studentList.RemoveAt(studentListBox.SelectedIndex);
                //Delete selected student from studentListBox
                studentListBox.Items.RemoveAt(studentListBox.SelectedIndex);

                //Rewrite studentFile excluding deleted student
                overwriteCSVFile();
            }
            clearStudentFormFields();
            
        }
        // studentCourseHistoryComboBox
        private void courseComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13 && studentCourseHistoryComboBox.Text != "")
            {
                courseHistoryListBox.Items.Add(studentCourseHistoryComboBox.Text);
                //studentCourseHistoryComboBox.ResetText();
            }
        }
        // Clear student fields
        private void clearStudentFormFields()
        {
            studentIDTbx.Text = "";
            studentFirstNameTbx.Text = "";
            studentLastNameTbx.Text = "";
            studentGPATbx.Text = "";
            graduateStatusCheckBox.Checked = false;
            gmatScoreTbx.Text = "";
            courseHistoryListBox.Items.Clear();
            gmatScoreTbx.Enabled = false;
        }
        //toggle graduateStatusCheckBox
        private void graduateStatusCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(graduateStatusCheckBox.Checked)
            {
                gmatScoreTbx.Enabled = true;
            }
            else if(graduateStatusCheckBox.Checked == false)
            {
                gmatScoreTbx.Enabled = false;
            }
        }        
        //editExistingStudentButton
        private void editExistingStudentButton_Click(object sender, EventArgs e)
        {
            editMode = true;
            string line = studentListBox.SelectedItem.ToString();
            string[] loadSelectedStudent = line.Split(',',';');

            studentIDTbx.Text = loadSelectedStudent[0];
            studentFirstNameTbx.Text = loadSelectedStudent[1];
            studentLastNameTbx.Text = loadSelectedStudent[2];
            studentGPATbx.Text = loadSelectedStudent[3];
            graduateStatusCheckBox.Checked = Convert.ToBoolean(loadSelectedStudent[4]);
            gmatScoreTbx.Text = loadSelectedStudent[5];

            for (int i = 6; i < loadSelectedStudent.Length; i++)
            {
                if (loadSelectedStudent[i] != "")
                {
                    courseHistoryListBox.Items.Add(loadSelectedStudent[i]);
                }
            }

        }
        //Overwrite CSV file
        private void overwriteCSVFile()
        {
            StreamWriter sw = new StreamWriter("studentFile.csv"); // overwrite file
            foreach (Student s in studentList)
            {
                sw.WriteLine("{0},{1},{2},{3},{4},{5},{6}",
                    s.StudentID, s.FirstName, s.LastName, s.GPA, s.GraduateStatus, s.GMATScore, s.CourseHistory);
            }
            sw.Close();
        }
        //use delete key to remove selected courseHistory item
        private void courseHistoryListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 8 && courseHistoryListBox.SelectedIndex != -1)
            {
                courseHistoryListBox.Items.RemoveAt(courseHistoryListBox.SelectedIndex);
                overwriteCSVFile();
            }
        }
        //courseListBox selected index change
        private void courseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           selectedCourse = courseListBox.SelectedItem as Course;
        }
        //editExistingCourseButton
        private void editExistingCourseButton_Click(object sender, EventArgs e)
        {
            clearCourseFormFields();
            //Populate course fields from selected pre-existing course
            courseSubjectComboBox.Text = selectedCourse.Subject;
            courseNumberTbx.Text = selectedCourse.CourseNumber;
            courseTitleTbx.Text = selectedCourse.CourseTitle;
            courseUnitsTbx.Text = selectedCourse.Units.ToString();
            courseTimesComboBox.Text = selectedCourse.StartTime;
            courseDaysComboBox.Text = selectedCourse.Days;
            courseSeatsTbx.Text = selectedCourse.Seats.ToString();

            //Populate courseHistoryListBox2
            string[] tempData = selectedCourse.Prerequisites.Split(';');
            foreach(string course in tempData)
            {
                if (course != "")
                {
                    courseHistoryListBox2.Items.Add(course);
                }
            }
        }
        private void addNewCourseButton_Click(object sender, EventArgs e)
        {
            //Validate subject
            if (courseSubjectComboBox.Text.Length != 3)
            {
                MessageBox.Show("Please enter a valid 3 letter acronym for your subject!");
                return;
            }
            //Validate courseNumber
            if (!(Convert.ToInt32(courseNumberTbx.Text) >= 100 && Convert.ToInt32(courseNumberTbx.Text) <= 999))
            {
                MessageBox.Show("Please enter a valid 3 digit number identifying a specific course in a subject area");
                return;
            }

            //store selected courseHistoryListbox2 items into prerequisites array
            string[] prerequisitesArray = new string[courseHistoryListBox2.Items.Count];
            int itemCount = courseHistoryListBox2.Items.Count;
            for (int i = 0; i < itemCount; i++)
            {
                prerequisitesArray[i] = courseHistoryListBox2.Items[i].ToString();
            }

            //Create newCourse object
            Course newCourse = new Course(courseSubjectComboBox.Text, courseNumberTbx.Text, courseTitleTbx.Text, Convert.ToInt32(courseUnitsTbx.Text),
                courseTimesComboBox.Text, courseDaysComboBox.Text, Convert.ToInt32(courseSeatsTbx.Text), prerequisitesArray);

            courseList.Add(newCourse);

            courseFormCourseHistoryComboBox.Items.Add(newCourse);

            populateCourseData();

            clearCourseFormFields();
          
        }
        //add courseFormCourseHistoryComboBox to courseHistoryListBox2 items
        private void courseFormCourseHistoryComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13 && courseFormCourseHistoryComboBox.Text != "")
            {
                courseHistoryListBox2.Items.Add(courseFormCourseHistoryComboBox.Text);
                //studentCourseHistoryComboBox.ResetText();
            }     
        }
        private void populateCourseData()
        {
            //Remove old course object from courseList
            courseList.Remove(selectedCourse);
            //Clear the courseListBox
            courseListBox.Items.Clear();
            courseFormCourseHistoryComboBox.Items.Clear();
  
            //Reload and display student data to studentListBox 
            foreach (Course c in courseList)
            {
                if (c != null)
                {
                    this.courseListBox.Items.Add(c);
                    courseFormCourseHistoryComboBox.Items.Add(c);
                    //this.courseListBox.Items.Add(string.Format("{0} {1} {2} {3} {4} {5} {6}",
                    //    c.Subject, c.CourseNumber, c.CourseTitle, c.Units, c.StartTime, c.Days, c.Seats, c.Prerequisites));
                }
            }

        }
        //Clear course form fields
        private void clearCourseFormFields()
        {
            courseSubjectComboBox.Text = "";
            courseNumberTbx.Text = "";
            courseTitleTbx.Text = "";
            courseUnitsTbx.Text = "";
            courseTimesComboBox.Text = "";
            courseDaysComboBox.Text = "";
            courseSeatsTbx.Text = "";
            courseHistoryListBox2.Items.Clear();
        }
        //Delete course button
        private void button4_Click(object sender, EventArgs e)
        {
            if (courseListBox.SelectedIndex != -1)
            {
                //Delete selected course from studentFile
                courseList.Remove(selectedCourse);
                //Delete selected course from studentListBox
                courseListBox.Items.RemoveAt(courseListBox.SelectedIndex);
                //Delete and reload updated combobox
                courseFormCourseHistoryComboBox.Items.Clear();
                studentCourseHistoryComboBox.Items.Clear();
                scheduleCourseListBox1.Items.Clear();
                foreach (Course c in courseList)
                {
                    studentCourseHistoryComboBox.Items.Add(c);
                    courseFormCourseHistoryComboBox.Items.Add(c);
                    scheduleCourseListBox1.Items.Add(c);
                }
            }
            clearCourseFormFields();
        }
        //delete course prereqs from selected course
        private void courseHistoryListBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 8 && courseHistoryListBox2.SelectedIndex != -1)
            {
                courseHistoryListBox2.Items.RemoveAt(courseHistoryListBox2.SelectedIndex);
            }
        }
        private void scheduleCourseListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCourse = scheduleCourseListBox1.SelectedItem as Course;
        }
        //scheduleStudentIDTbx_TextChanged
        private void scheduleStudentIDTbx_TextChanged(object sender, EventArgs e)
        {
            //disable addCourseToScheduleButton until 10 digit ID is typed
            addCourseToScheduleButton.Enabled = false;
            scheduleCourseListBox1.Enabled = false;
            if (scheduleStudentIDTbx.Text.Length == 10)
            {
                schedule = new Schedule();
                foundStudent = null;

                foreach (Student searchedStudent in studentList)
                {
                    if (searchedStudent.StudentID == scheduleStudentIDTbx.Text)
                    {
                        foundStudent = searchedStudent;
                        break;
                    }
                }
                //if entered studentID does not match existing studentID then show message box to try again
                if (foundStudent == null)
                {
                    MessageBox.Show("Student Not Found, Try Again!");
                }
                else
                {
                    addCourseToScheduleButton.Enabled = true;
                    scheduleCourseListBox1.Enabled = true;
                    scheduleStudentFNameTbx.Text = foundStudent.FirstName;
                    scheduleStudentLNameTbx.Text = foundStudent.LastName;
                }

            }
        }
        //add course button to schedule
        private void addCourseToScheduleButton_Click(object sender, EventArgs e)
        {
            //Students cannot add a course to their schedule if no seats are available
            if (selectedCourse.Seats == 0)
            {
                MessageBox.Show("There are not seats left for this class!");
                return;
            }
            //Students cannot enroll in a course if the prerequisites have not been met.
            if (!PreRequisitsMet())
            {
                MessageBox.Show("You have not met the prerequisites for this course!");
                return;
            }

            scheduleCourseListBox2.Items.Add(selectedCourse);

            selectedCourse.Seats--;

            if (schedule != null) //if no schedule object has been created
            {
                schedule.TotalUnits += selectedCourse.Units;
                scheduleTotalUnitsTbx.Text = schedule.TotalUnits.ToString();

                scheduleCourseListBox1.Items.Remove(selectedCourse);
                if (scheduleCourseListBox1.Items.Count == 0 || schedule.TotalUnits >= 15)
                {
                    addCourseToScheduleButton.Enabled = false;
                }

            }
        }
        //Check if students cannot enroll in a course if the prerequisites have not been met.
        private bool PreRequisitsMet()
        {
            //if selectedCourse has no prerequisites(null) then proceed
            if (selectedCourse.PrerequisitesArray == null)
            {
                return true;
            }
            //if foundStudent.CourseHistory contains a match in preReqTitle
            foreach(string preReqTitle in selectedCourse.PrerequisitesArray)
            {
                if (!foundStudent.CourseHistory.Contains(preReqTitle))
                {
                    return false;
                }
            }
            return true;
        }
        //removeCourseFromScheduleButton
        private void removeCourseFromScheduleButton_Click(object sender, EventArgs e)
        {
            selectedCourse = scheduleCourseListBox2.SelectedItem as Course;
            scheduleCourseListBox1.Items.Add(selectedCourse);
            scheduleCourseListBox2.Items.Remove(selectedCourse);

            schedule.TotalUnits -= selectedCourse.Units;
            scheduleTotalUnitsTbx.Text = schedule.TotalUnits.ToString();

            if (schedule.TotalUnits < 15)
            {
                addCourseToScheduleButton.Enabled = true;
            }
        }
        //registerScheduleButton
        private void registerScheduleButton_Click(object sender, EventArgs e)
        {
            //Validate registration
            if (scheduleTotalUnitsTbx.Text == "" || scheduleTotalUnitsTbx.Text == "0")
            {
                MessageBox.Show("Please add courses to your schedule in order to register!");
                return;
            }
            if (foundStudent != null)
	        {
                foreach (Course course in scheduleCourseListBox2.Items)
                {
                    if (course.ToString() != "")
                    {
                        foundStudent.CourseHistory += course.ToString() + ";";
                    }
                    else
                    {
                        foundStudent.CourseHistory = course.ToString() + ";";
                    }
                }
                //Update studentFile with correct coureHistory
                overwriteCSVFile();
                //Refresh studentListBox
                populateStudentData();
                //Confirmation popup
                MessageBox.Show("Thank you for registering!");
                //Reset scheduleStudentIDTbx, scheduleStudentFNameTbx, scheduleStudentLNameTbx, scheduleTotalUnitsTbx, scheduleCourseListBox1, scheduleCourseListBox2 
                scheduleStudentIDTbx.Text = "";
                scheduleStudentFNameTbx.Text = "";
                scheduleStudentLNameTbx.Text = "";
                scheduleTotalUnitsTbx.Text = "0";
                scheduleCourseListBox1.Items.Clear();
                scheduleCourseListBox2.Items.Clear();
                foreach (Course c in courseList)
                {
                    scheduleCourseListBox1.Items.Add(c);
                }
                
	        }

        }
        //Exit Application
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Validate max allowed total units of 15
        private void scheduleTotalUnitsTbx_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(scheduleTotalUnitsTbx.Text) > 15)
            {
                registerScheduleButton.Enabled = false;
            }
            else
            {
                registerScheduleButton.Enabled = true;
            }
        }
    }
}
