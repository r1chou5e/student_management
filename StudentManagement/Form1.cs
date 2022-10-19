using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace StudentManagement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string file_name = "d:\\dssv.txt";
        public void LoadData()
        {
            if (File.Exists(file_name))
            {
                string[] data = File.ReadAllLines(file_name);
                foreach (string line in data)
                {
                    string[] col = line.Split('-');
                    ListViewItem item = new ListViewItem();
                    item.Text = col[0];
                    studentLv.Items.Add(item);

                    ListViewItem.ListViewSubItem subitem1 = new ListViewItem.ListViewSubItem(item, col[1]);
                    item.SubItems.Add(subitem1);

                    ListViewItem.ListViewSubItem subitem2 = new ListViewItem.ListViewSubItem(item, col[2]);
                    item.SubItems.Add(subitem2);

                    ListViewItem.ListViewSubItem subitem3 = new ListViewItem.ListViewSubItem(item, col[3]);
                    item.SubItems.Add(subitem3);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void saveData_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(mssv.Text)) || (string.IsNullOrEmpty(hoten.Text)) || (string.IsNullOrEmpty(malop.Text)) || (string.IsNullOrEmpty(diemtb.Text)))
            {
                MessageBox.Show("Vui lòng điền đủ thông tin");
            }
            else
            {
                string[] data = File.ReadAllLines(file_name);
                foreach (string line in data)
                {
                    string[] col = line.Split('-');
                    if (col[0] == mssv.Text)
                    {
                        MessageBox.Show("Mã số sinh viên đã tồn tại !");
                        return;
                    }
                }

                // Ghi dữ liệu vào file txt
                File.AppendAllText(file_name, mssv.Text + "-");
                File.AppendAllText(file_name, hoten.Text + "-");
                File.AppendAllText(file_name, malop.Text + "-");
                File.AppendAllText(file_name, diemtb.Text + "\n");

                MessageBox.Show("Thông tin sinh viên đã được lưu !!");
                mssv.Clear();
                hoten.Clear();
                malop.Clear();
                diemtb.Clear();
                studentLv.Items.Clear();
                LoadData();
            }
        }

        private void deleteRow_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã xóa thông tin sinh viên!");
            studentLv.Items.Remove(studentLv.SelectedItems[0]);
            List<string> data = new List<string>();
            File.WriteAllText(file_name, "");
            foreach (ListViewItem item in studentLv.Items)
            {
                data.Add(item.Text + "-" + item.SubItems[1].Text + "-" + item.SubItems[2].Text + "-" + item.SubItems[3].Text);
            }
            data.ToArray();
            File.WriteAllLines(file_name, data);
            mssv.Clear();
            hoten.Clear();
            malop.Clear();
            diemtb.Clear();
        }
        private void studentLv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (studentLv.SelectedItems.Count == 0)
            {
                mssv.Text = "";
                hoten.Text = "";
                malop.Text = "";
                diemtb.Text = "";
            }
            else
            {
                ListViewItem item = studentLv.SelectedItems[0];
                mssv.Text = item.Text;
                hoten.Text = item.SubItems[1].Text;
                malop.Text = item.SubItems[2].Text;
                diemtb.Text = item.SubItems[3].Text;
            }
        }

        private void updateData_Click(object sender, EventArgs e)
        {
            if (studentLv.SelectedItems.Count == 0)
            {
                MessageBox.Show("Chưa chọn sinh viên để cập nhật !");
                return;
            }
            MessageBox.Show("Đã cập nhật thông tin sinh viên!");
            studentLv.SelectedItems[0].Text = mssv.Text;
            studentLv.SelectedItems[0].SubItems[1].Text = hoten.Text;
            studentLv.SelectedItems[0].SubItems[2].Text = malop.Text;
            studentLv.SelectedItems[0].SubItems[3].Text = diemtb.Text;
            List<string> data = new List<string>();
            File.WriteAllText(file_name, "");
            foreach (ListViewItem item in studentLv.Items)
            {
                data.Add(item.Text + "-" + item.SubItems[1].Text + "-" + item.SubItems[2].Text + "-" + item.SubItems[3].Text);
            }
            data.ToArray();
            File.WriteAllLines(file_name, data);
            mssv.Clear();
            hoten.Clear();
            malop.Clear();
            diemtb.Clear();
        }
    }
}
