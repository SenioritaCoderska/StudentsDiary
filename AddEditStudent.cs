using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.filePath);
        private Student _student;
        private int _studentId;

        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            cmbClass.Items.AddRange(Program.classesAtSchool);
            

            if (id != 0)
            {
                var students = _fileHelper.DeserializedFromFile();
                _student = students.FirstOrDefault(x => x.Id == id);

                if (_student == null)
                    throw new Exception("No student under provided Id");

                FillUpForm();
            }
        }

        private void FillUpForm()
        {
            tbId.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName;
            tbLastName.Text = _student.LastName;
            cmbClass.Text = _student.GroupId;
            rtbComments.Text = _student.Comments;
            tbMath.Text = _student.Math;
            tbPhysics.Text = _student.Math;
            tbPolish.Text = _student.Polish;
            tbEnglish.Text = _student.English;
            tbTech.Text = _student.Technology;
            cbAdditionalClasses.Checked = _student.AdditionalClasses;
        }
        private async void btnSubmit_Click(object sender, EventArgs e)
        {

            GetStudentData();
            await LongProcessAsync();
            Close();

        }
        private async Task LongProcessAsync()
        {
            await Task.Run(() => { Thread.Sleep(3000); });

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
            selectStudentBasedOnId.AdditionalClasses = cbAdditionalClasses.Checked;
            selectStudentBasedOnId.Math = tbMath.Text;
            selectStudentBasedOnId.Physics = tbPhysics.Text;
            selectStudentBasedOnId.Polish = tbPolish.Text;
            selectStudentBasedOnId.English = tbEnglish.Text;
            selectStudentBasedOnId.GroupId = cmbClass.Text;
        }
        private void AddNewUserToList(List<Student> students)
        {
            AssignIdToNewStudent(students);

            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                AdditionalClasses = cbAdditionalClasses.Checked,
                Comments = rtbComments.Text,
                Math = tbMath.Text,
                Physics = tbPhysics.Text,
                Polish = tbPolish.Text,
                English = tbEnglish.Text,
                Technology = tbTech.Text,
                GroupId = cmbClass.Text
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


