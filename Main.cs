using StudentsDiary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace StudentsDiary
{
    public partial class Main : Form
    {
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>
            (Program.filePath);
        private List<Student> _students = new List<Student>();
        public bool IsMaximized
        {
            get
            {
                return Settings.Default.IsMaximized;
            }
            set
            {
                Settings.Default.IsMaximized = value;
            }
        }

        //private delegate void DisplayMessage(string message);

        public Main()
        {
            InitializeComponent();

            if (IsMaximized)
                WindowState = FormWindowState.Minimized;

            cmbFilter.Items.AddRange(Program.classesAtSchool);
            cmbFilter.SelectedIndex = 0;

            RefreshDiary();
        }

        public void RefreshDiary()
        {
          _students = _fileHelper.DeserializedFromFile();
            dgvDiary.DataSource = _students;
            dgvDiary.ReadOnly = true;

            //you could refresh filter ith that
            //PutFilterOnDGV();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();
            addEditStudent.FormClosing -= AddEditStudent_FormClosing;

        }

        private void AddEditStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshDiary();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select the student you want to edit");
                return;
            }
            var addEditStudent = new AddEditStudent((Convert.ToInt32(dgvDiary.SelectedRows[0].Cells["Id"].Value)));
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();
            addEditStudent.FormClosing -= AddEditStudent_FormClosing;

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

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)

                IsMaximized = true;
            else
                IsMaximized = false;

            Settings.Default.Save();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            PutFilterOnDGV();

        }

        private void PutFilterOnDGV()
        {
            string groupId = cmbFilter.SelectedItem.ToString();
            if (groupId == "Show all")
            {
                dgvDiary.DataSource = _students;
                dgvDiary.Refresh();
            }
            else
            {
                var chosenList = _students.Where(x => x.GroupId == groupId).ToList();
                dgvDiary.DataSource = chosenList;
                dgvDiary.Refresh();
            }
        }
    }
}
