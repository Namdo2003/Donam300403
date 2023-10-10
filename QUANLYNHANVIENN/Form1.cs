using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QUANLYNHANVIENN
{
    public partial class Form1 : Form
    {
        SqlConnection connect;
        string connectionString = "Data Source=DESKTOP-N39PB8F\\THANHDEATH;Initial Catalog=QLNV;Integrated Security=True";
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command;
        DataTable table = new DataTable();
        public Form1()
        {
            InitializeComponent();
           connect = new SqlConnection(connectionString);
            connect.Open();
            load(); 
        }

        private void Form1_Load(object sender, EventArgs e)
            {
              
             
               
                string sqlDSNV = "select * from DSNV";
                string sqlDMP = "select * from DMPHONG";
                string sqlCV = "select * from CHUCVU";
                da = new SqlDataAdapter(sqlDSNV, connectionString);
                da.Fill(ds, "DSNV");
                da = new SqlDataAdapter(sqlDMP, connectionString);
                da.Fill(ds, "DMPHONG");
                da = new SqlDataAdapter(sqlCV, connectionString);
                da.Fill(ds, "CHUCVU");
                gridDSNV.DataSource = ds.Tables["DSNV"];
                cbbPhong.DataSource = ds.Tables["DMPHONG"];
                cbbPhong.DisplayMember = "TenPhong";
                cbbPhong.ValueMember = "MaPhong";

               cbbChucVu.DataSource = ds.Tables["CHUCVU"];
               cbbChucVu.DisplayMember = "TenChucVu";
               cbbChucVu.ValueMember = "MaChucVu";

           }
        void load()
        {
            command = connect.CreateCommand();
            command.CommandText = "select * from DSNV";
            da.SelectCommand = command;
            table.Clear();
            da.Fill(table);
            gridDSNV.DataSource = table;
        }



        private void btnThemNV_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-N39PB8F\\THANHDEATH;Initial Catalog=QLNV;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            
            try
            {
                connection.Open();
                string sqlinsert = "INSERT INTO DSNV(MaNV, HoTen, MaPhong, HeSoLuong, MaChucVu, NgaySinh, GioiTinh)" +
                    " VALUES(@MaNV, @HoTen, @MaPhong, @HeSoLuong, @MaChucVU, @NgaySinh, @GioiTinh)";
                SqlCommand command = new SqlCommand(sqlinsert, connection);
               //// command = connection.CreateCommand();
                command.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                command.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                command.Parameters.AddWithValue("@MaPhong", cbbPhong.SelectedValue.ToString());
                command.Parameters.AddWithValue("@HeSoLuong", hsluong.Text);
                command.Parameters.AddWithValue("@MaChucVu", cbbChucVu.SelectedValue.ToString());
                command.Parameters.AddWithValue("@NgaySinh", dtpNS.Value);
                command.Parameters.AddWithValue("GioiTinh", cbbGioiTinh.Text);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm mới nhân viên thành công!");
                    ds.Clear();
                    
                    load();
                   
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại");
                }
            }
            finally { connection.Close(); }
        }

       

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-N39PB8F\\THANHDEATH;Initial Catalog=QLNV;Integrated Security=True"; ;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
               
                connection.Open();

                
                string query = "DELETE FROM DSNV WHERE MaNV = @MaNV";

                
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaNV", txtMaNV.Text);

                
                int result = command.ExecuteNonQuery();

                
                if (result > 0)
                {
                    MessageBox.Show("Xóa nhân viên thành công!");
                    ds.Clear();
                    load();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên có mã " + txtMaNV.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                
                connection.Close();
            }
        }


        
        //SqlConnection connection = new SqlConnection(connectionString);
        //try
        //{
        //    connection.Open();
        //    string query = "delete from DSNV where MaNV = @MaNV";
        //    SqlCommand command = new SqlCommand(query, connection);
        //    command.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
        //    SqlDataReader reader = command.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        string sqlxoa = "delete * from DSNV where MaNV = N'" + reader["MaNV"].ToString() + "'";
        //        da = new SqlDataAdapter(sqlxoa, connectionString);
        //        da.Fill(ds, "DSNV1");
        //        gridDSNV.DataSource = ds.Tables["DSNV1"];

        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.Message);

        //}
    

    private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-N39PB8F\\THANHDEATH;Initial Catalog=QLNV;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string sqlupdate = "UPDATE DSNV SET HoTen = @HoTen, MaPhong = @MaPhong, HeSoLuong = @HeSoLuong, MaChucVu = @MaChucVu, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh WHERE MaNV = @MaNV";
                SqlCommand command = new SqlCommand(sqlupdate, connection);
               // command = connection.CreateCommand();
                command.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                command.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                command.Parameters.AddWithValue("@MaPhong", cbbPhong.SelectedValue.ToString());
                command.Parameters.AddWithValue("@HeSoLuong", hsluong.Text);
                command.Parameters.AddWithValue("@MaChucVu", cbbChucVu.SelectedValue.ToString());
                command.Parameters.AddWithValue("@NgaySinh", dtpNS.Value);
                command.Parameters.AddWithValue("GioiTinh", cbbGioiTinh.Text);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật nhân viên thành công!");
                    txtMaNV.Clear();
                    txtHoTen.Clear();
                    hsluong.Clear();
                    gridDSNV.DataSource = ds.Tables["DSNV"];
                }
                else
                {
                    MessageBox.Show("Cập nhật nhân viên thất bại");
                }
            }
            finally { connection.Close(); }
        }
        private void btnKhoiTao_Click(object sender, EventArgs e)
            {
                txtMaNV.Text = "";
                txtHoTen.Text = "";
                dtpNS.Text = "01/01/1900";
                cbbPhong.Text = "";
                hsluong.Text = "";
                cbbGioiTinh.Text = "Nam";
                cbbChucVu.Text = "";
            }

        private void btnThoat_Click(object sender, EventArgs e)
            {
                this.Close();
            }

        //private void cbbPhong_SelectedIndexChanged(object sender, EventArgs e)
        //    {
        //        string sqlDSNV = "select * from DSNV where MaPhong = N'" + cbbPhong.SelectedValue.ToString() + "'";
        //        da = new SqlDataAdapter(sqlDSNV, connectionString);
        //        da.Fill(ds, "DSNV1");
        //        gridDSNV.DataSource = ds.Tables["DSNV1"];
        //    }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=DESKTOP-N39PB8F\\THANHDEATH; Database=QLNSS; Trusted_Connection =true";
                SqlConnection connection = new SqlConnection(connectionString);
               try
               {

                  connection.Open();
                   string query = "select * from DSNV where Hoten = @Hoten";
                  SqlCommand command = new SqlCommand(query, connection);
                  command.Parameters.AddWithValue("@Hoten", txtHoTen.Text);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                      string sqltimkiem = "select * from DSNV where Hoten = N'" + reader["Hoten"].ToString() + "'";
                        da = new SqlDataAdapter(sqltimkiem, connectionString);
                        da.Fill(ds, "DSNV1");
                       gridDSNV.DataSource = ds.Tables["DSNV1"];

                    }
                    else
                   {
                       MessageBox.Show("Không tìm thấy tên nhân viên bạn đang cần", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
                }
               catch (Exception ex)
               {
                    MessageBox.Show(ex.Message);
               }
                finally { connection.Close();

                }
        }

        private void gridDSNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNV.ReadOnly = true;
            int i;
            i = gridDSNV.CurrentRow.Index;
            txtMaNV.Text = gridDSNV.Rows[i].Cells[0].Value.ToString();
            txtHoTen.Text = gridDSNV.Rows[i].Cells[1].Value.ToString();
            dtpNS.Text = gridDSNV.Rows[i].Cells[5].Value.ToString();
            cbbPhong.Text = gridDSNV.Rows[i].Cells[2].Value.ToString();
            hsluong.Text = gridDSNV.Rows[i].Cells[3].Value.ToString();
            cbbGioiTinh.Text = gridDSNV.Rows[i].Cells[6].Value.ToString();
            cbbChucVu.Text = gridDSNV.Rows[i].Cells[4].Value.ToString();
        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }
    }
}























