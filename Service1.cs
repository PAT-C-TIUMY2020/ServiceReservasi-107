using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;

        }
        string constring = "Data Source = Salis-R;Initial Catalog=WCFReservasi;Persist Security Info=true;User ID=sa;Password=1234";
        SqlConnection connection;
        SqlCommand com; // untuk mengkoneksikan database ke visual studio

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>(); //proses untuk mendeclare nama list yg telah dibuat dengan nama baru
            try
            {
                string sql = "select ID_lokasi, Nama_lokasi, Deskripsi_full, Kuota from dbo.Lokasi"; //declare query
                connection = new SqlConnection(constring); //Fungsi konek ke database
                com = new SqlCommand(sql, connection); //Proses execute query
                connection.Open(); //membuka koneksi
                SqlDataReader reader = com.ExecuteReader(); //Menampilkan data query
                while (reader.Read())
                {
                    /*nama Class*/
                    DetailLokasi data = new DetailLokasi(); //deskripsi data, mengambil 1per1 dari database
                    //bentuk query
                    data.IDLokasi = reader.GetString(0); //0 itu index, ada dikolom keberapa di string sql diatas
                    data.NamaLokasi = reader.GetString(1); //
                    data.DeskripsiFull = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    LokasiFull.Add(data); //mengumpulkan data yang awalnya dari array
                }
                connection.Close(); //untuk menutup akses ke database
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }

        public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>(); // proses untuk mendeclare nama list yg telah dibuat dengan
            try
            {
                string sql = "select ID_reservasi, Nama_costumer, No_telpon, " +
                    "Jumlah_pemesanan, Nama_Lokasi from dbo.Pemesanan p join dbo.Lokasi 1 on p.ID_lokasi = 1.ID_lokasi";
                connection = new SqlConnection(constring); //konek ke database
                com = new SqlCommand(sql, connection); //proses execute query
                connection.Open(); //membuka koneksi
                SqlDataReader reader = com.ExecuteReader(); //menampilkan data query
                while (reader.Read())
                {
                    //nama class
                    Pemesanan data = new Pemesanan(); //deklarasi data, mengambil 1 per 1 dari database
                    // bentuk array
                    data.IDPemesanan = reader.GetString(0); //0 itu index, ada di kolom keberapa string sql diatas
                    data.NamaCostumer = reader.GetString(1);
                    data.NoTelpon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    pemesanans.Add(data); //mengumpulkan data yang awalnya dari array
                }
                connection.Close(); //untuk menutup akses database
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pemesanans;
        }


        public string pemesanan(string IDPemesanan, string NamaCostumer, string NoTelpon, int JumlahPemesanan, string IDLokasi)
        {
            string a = "gagal";
            try
            {
                string sql = "insert into dbo.Pemesanan values ('" + IDPemesanan + "', '" + NamaCostumer + "', '" + NoTelpon + "', '" + JumlahPemesanan + "', '" + IDLokasi + "')"; //petik 1 untuk varchar petik 2 untuk integer
                connection = new SqlConnection(constring); //konek ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                string sql2 = "update dbo.Lokasi set kuota = kuota - " + JumlahPemesanan + " where ID_lokasi = '" + IDLokasi + "'";
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public string editPemesanan(string IDPemesanan, string NamaCostumer, string No_telpon)
        {
            string a = "gagal";
            try
            {
                string sql = "update dbo.Pemesanan set Nama_costumer = '" + NamaCostumer + "', No_telpon = '" + No_telpon + "'" + "where ID_reservasi = '" + IDPemesanan + "' ";
                connection = new SqlConnection(constring); //konek ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public string deletePemesanan(string IDPemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "delete from dbo.Pemesanan where ID_reservasi = '" + IDPemesanan + "' ";
                connection = new SqlConnection(constring); //konek ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();
                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }
    }
}
