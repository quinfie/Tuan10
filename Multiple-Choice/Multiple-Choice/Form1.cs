using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Multiple_Choice
{
    public partial class Form1 : Form
    {

        private const string ConnectionString = @"Data Source=LAPTOP-77AKUU80\SQLEXPRESS;Initial Catalog=Multiple_Choice;Integrated Security=True";
        private int currentQuestionIndex = 0;
        private List<CauHoi> danhSachCauHoi = new List<CauHoi>();

        private class CauHoi
        {
            public string TenCauHoi { get; set; }
            public string LuaChon1 { get; set; }
            public string LuaChon2 { get; set; }
            public string LuaChon3 { get; set; }
            public string LuaChon4 { get; set; }

            public string DapAn { get; set; }
            public CauHoi(string tenCauHoi, string luaChon1, string luaChon2, string luaChon3, string luaChon4, string dapAn)
            {
                TenCauHoi = tenCauHoi;
                LuaChon1 = luaChon1;
                LuaChon2 = luaChon2;
                LuaChon3 = luaChon3;
                LuaChon4 = luaChon4;
                DapAn = dapAn;
            }
        }

        public Form1()
        {
            InitializeComponent();
            LoadDataFromDatabase();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                danhSachCauHoi.Clear();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT CauHoi AS 'Tên Câu Hỏi', LuaChon1, LuaChon2, LuaChon3, LuaChon4, DapAn FROM Cau_Hoi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string cauHoi = reader["Tên Câu Hỏi"].ToString();
                                string luaChon1 = reader["LuaChon1"].ToString();
                                string luaChon2 = reader["LuaChon2"].ToString();
                                string luaChon3 = reader["LuaChon3"].ToString();
                                string luaChon4 = reader["LuaChon4"].ToString();
                                string dapAn = reader["DapAn"].ToString();

                                danhSachCauHoi.Add(new CauHoi(cauHoi, luaChon1, luaChon2, luaChon3, luaChon4, dapAn));
                            }
                        }
                    }
                }

                if (danhSachCauHoi.Count > 0)
                {
                    HienThiCauHoi();
                }
                else
                {
                    MessageBox.Show("Không có câu hỏi trong cơ sở dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void HienThiCauHoi()
        {
            if (currentQuestionIndex >= 0 && currentQuestionIndex < danhSachCauHoi.Count)
            {
                CauHoi cauHoiHienTai = danhSachCauHoi[currentQuestionIndex];

                label2.Text = cauHoiHienTai.TenCauHoi;
                radioButton1.Text = cauHoiHienTai.LuaChon1;
                radioButton2.Text = cauHoiHienTai.LuaChon2;
                radioButton3.Text = cauHoiHienTai.LuaChon3;
                radioButton4.Text = cauHoiHienTai.LuaChon4;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            currentQuestionIndex++;
            HienThiCauHoi();
            ResetRadioButtonStates();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                HienThiCauHoi();
            }
            else
            {
                MessageBox.Show("Bạn đã đến câu hỏi đầu tiên.");
            }
            ResetRadioButtonStates();
        }
        private bool daChonLaiDapAn = false;
        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked)
            {
                CauHoi cauHoiHienTai = danhSachCauHoi[currentQuestionIndex];
                string dapAnChon = LayDapAnRadioButtonDuocChon();
                if (dapAnChon == cauHoiHienTai.DapAn)
                {
                    MessageBox.Show("Chính xác! Đáp án là " + cauHoiHienTai.DapAn, "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button1.Enabled = true;
                    button2.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Sai, Vui lòng chọn lại đáp án.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = false;
                    button2.Enabled = false;
                    daChonLaiDapAn = true;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đáp án trước khi nhấn Submit.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }

        private string LayDapAnRadioButtonDuocChon()
        {
            if (radioButton1.Checked)
                return radioButton1.Text;
            else if (radioButton2.Checked)
                return radioButton2.Text;
            else if (radioButton3.Checked)
                return radioButton3.Text;
            else if (radioButton4.Checked)
                return radioButton4.Text;
            else
                return string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ResetRadioButtonStates()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }
    }
}
