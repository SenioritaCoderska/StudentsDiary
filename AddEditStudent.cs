using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>
         (Program.filePath);
        public AddEditStudent(int id = 0)
        {
            InitializeComponent();

            if (id != 0)
            {
                var students = _fileHelper.DeserializedFromFile();
                var student = students.FirstOrDefault(x => x.Id == id);

                if (student == null)
                    throw new Exception("No student under provided Id");

                tbId.Text = student.Id.ToString();
                tbFirstName.Text = student.FirstName;
                tbLastName.Text = student.LastName;
                rtbComments.Text = student.Comments;
                tbMath.Text = student.Math;
                tbPhysics.Text = student.Math;
                tbPolish.Text = student.Polish;
                tbEnglish.Text = student.English;
                tbTech.Text = student.Technology;

                this.Show();
            }
        }
        
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
            var students = _fileHelper.DeserializedFromFile();
            int studentId;


            if (tbId.Text != null)
            {
                var ifStudentAvailable = int.TryParse(tbId.Text, out studentId);
                Student selectStudentBasedOnId = students
                .OrderByDescending(x => x.Id == studentId).FirstOrDefault();

                selectStudentBasedOnId.Id = studentId;
                selectStudentBasedOnId.FirstName = tbFirstName.Text;
                selectStudentBasedOnId.LastName = tbLastName.Text;
                selectStudentBasedOnId.Comments = rtbComments.Text;
                selectStudentBasedOnId.Math = tbMath.Text;
                selectStudentBasedOnId.Physics = tbPhysics.Text;
                selectStudentBasedOnId.Polish = tbPolish.Text;
                selectStudentBasedOnId.English = tbEnglish.Text;

                _fileHelper.SerializeToFile(students);
            }
            else
            {
                var studentWithHighestId = students
                    .OrderByDescending(x => x.Id).FirstOrDefault();

                studentId = studentWithHighestId == null ?
                1 : studentWithHighestId.Id + 1;

                var student = new Student
                {
                    Id = studentId,
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
                _fileHelper.SerializeToFile(students);
            };
             
            
            this.Close();


        }
    }
}
