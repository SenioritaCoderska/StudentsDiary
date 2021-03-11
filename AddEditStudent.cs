using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>> (Program.filePath);
        private Student _student;
        private int _studentId;

        public delegate void MySimpleDelegate();
        public event MySimpleDelegate StudentAdded;
        public AddEditStudent(int id = 0)
        {
            InitializeComponent();

            if (id != 0)
            {
                var students = _fileHelper.DeserializedFromFile();
                _student = students.FirstOrDefault(x => x.Id == id);

                if (_student == null)
                    throw new Exception("No student under provided Id");

                FillTextBoxes();

                this.Show();
            }
        }

        private void OnStudentAdded()
        {
            StudentAdded?.Invoke();
        }
        private void FillTextBoxes()
        {
                tbId.Text = _student.Id.ToString();
                tbFirstName.Text = _student.FirstName;
                tbLastName.Text = _student.LastName;
                rtbComments.Text = _student.Comments;
                tbMath.Text = _student.Math;
                tbPhysics.Text = _student.Math;
                tbPolish.Text = _student.Polish;
                tbEnglish.Text = _student.English;
                tbTech.Text = _student.Technology;
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {

            GetStudentData();
            this.Close();

        }

        private void GetStudentData()
        {
            bool ifStudentAvailable = int.TryParse(tbId.Text, out _studentId);

            var students = _fileHelper.DeserializedFromFile();

            if (ifStudentAvailable)
            {
                UpdateExistingUser(students);
            }
            else
            {
                AddNewUserToList(students);
            }

            _fileHelper.SerializeToFile(students);
            OnStudentAdded();
            Close();
        }

        private void UpdateExistingUser(List<Student> students)
        {
            Student selectStudentBasedOnId = students
                    .OrderByDescending(x => x.Id == _studentId).FirstOrDefault();

            selectStudentBasedOnId.Id = _studentId;
            selectStudentBasedOnId.FirstName = tbFirstName.Text;
            selectStudentBasedOnId.LastName = tbLastName.Text;
            selectStudentBasedOnId.Comments = rtbComments.Text;
            selectStudentBasedOnId.Math = tbMath.Text;
            selectStudentBasedOnId.Physics = tbPhysics.Text;
            selectStudentBasedOnId.Polish = tbPolish.Text;
            selectStudentBasedOnId.English = tbEnglish.Text;
        }
        private void AddNewUserToList(List<Student> students)
        {
            AssignIdToNewStudent(students);

            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Comments = rtbComments.Text,
                Math = tbMath.Text,
                Physics = tbPhysics.Text,
                Polish = tbPolish.Text,
                English = tbEnglish.Text,
                Technology = tbTech.Text
            };
            students.Add(student);

            
        }

        private void AssignIdToNewStudent(List<Student> students)
        {
            var studentWithHighestId = students
                   .OrderByDescending(x => x.Id).FirstOrDefault();

            _studentId = studentWithHighestId == null ?
            1 : studentWithHighestId.Id + 1;
        }
    }
}


