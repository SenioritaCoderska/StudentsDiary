using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace StudentsDiary
{
    public partial class Main : Form
    {
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>
            (Program.filePath);

        public Main()
        {
            InitializeComponent();
            RefreshDiary();
            var list = new List<Person>() {
                new Student { FirstName = "Koles", LastName = "Kotsiek", Math = "3" },
                new Teacher { FirstName = "Koles", LastName = "Kotsiek" },

            };

            foreach (var item in list)
            {
                MessageBox.Show(item.GetInfo());
            }
        }

       
        public void RefreshDiary()
        {
            var students = _fileHelper.DeserializedFromFile();
            dgvDiary.DataSource = students;
        }
        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();
            addEditStudent.ShowDialog();

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
            RefreshDiary();

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
