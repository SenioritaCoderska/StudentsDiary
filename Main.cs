using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace StudentsDiary
{
    public partial class Main : Form
    {
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>
            (Program.filePath);

        //private delegate void DisplayMessage(string message);

        public Main()
        {
            InitializeComponent();
            RefreshDiary();

            //var messages = new Action<string>(DisplayMessage1);
            //messages += DisplayMessage2;
            //messages("test");
            //var list = new List<Person>() {
            //    new Student { FirstName = "Koles", LastName = "Kotsiek", Math = "3" },
            //    new Teacher { FirstName = "Koles", LastName = "Kotsiek" },

            //};

            //foreach (var item in list)
            //{
            //    MessageBox.Show(item.GetInfo());
            //}
        }

       public void DisplayMessage1(string message)
        {
            MessageBox.Show($"Method 1 - {message}");
        }
        public void DisplayMessage2(string message)
        {
            MessageBox.Show($"Method 2 - {message}");
        }

        public void RefreshDiary()
        {
            var students = _fileHelper.DeserializedFromFile();
            dgvDiary.DataSource = students;
        }
        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();
            addEditStudent.StudentAdded += AddEditStudent_StudentAdded;
            addEditStudent.ShowDialog();
            addEditStudent.StudentAdded -= AddEditStudent_StudentAdded;

        }

        private void AddEditStudent_StudentAdded()
        {
            RefreshDiary();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select the student you want to edit");
                return;
            }
            var addEditStudent = new AddEditStudent((Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value)));
            addEditStudent.ShowDialog();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chose student wich you want to delete.");
            }
            else
            {


                var selectedStudent = dgvDiary.SelectedRows[0];

                try
                {
                    var studentFirstName = selectedStudent.Cells[1].Value.ToString().Trim();
                    var studentLastName = selectedStudent.Cells?[2]?.Value?.ToString().Trim();
                    var confirmDelete = MessageBox.Show($"Do you really wish to remove student: " +
                   $" {studentFirstName}  {studentLastName}", "Delete", MessageBoxButtons.OKCancel);

                    //
                    if (confirmDelete == DialogResult.OK)
                    {
                        DeleteStudent(Convert.ToInt32(selectedStudent.Cells[0].Value));
                        RefreshDiary();

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

            private void DeleteStudent(int id)
            {
                var students = _fileHelper.DeserializedFromFile();
                students.RemoveAll(x => x.Id == id);
                _fileHelper.SerializeToFile(students);
               
            }

        
    }
    }
