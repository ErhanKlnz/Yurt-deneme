﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace denemee
{
    public partial class FrmKullanicidüzenle : Form
    {
        public FrmKullanicidüzenle()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl = new SqlBaglantim();
        private void sifreislemleri_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'yurtSistemiDataSet13.KullaniciPersonel' table. You can move, or remove it, as needed.
            this.kullaniciPersonelTableAdapter.Fill(this.yurtSistemiDataSet13.KullaniciPersonel);
           
            SqlCommand komut = new SqlCommand("Select PerTc From Personel", bgl.baglanti());
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbyöneticiTC.Items.Add(oku[0].ToString());

            }
            bgl.baglanti().Close();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into KullaniciPersonel (KullaniciAdi,Sifre,PerTC) values (@a1,@a2,@a3)", bgl.baglanti());
            komut.Parameters.AddWithValue("@a1", txtkullanıcıadi.Text);
            komut.Parameters.AddWithValue("@a2", txtsifre.Text);
            komut.Parameters.AddWithValue("@a3",cmbyöneticiTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kullanıcı Personel Eklendi");
            this.kullaniciPersonelTableAdapter.Fill(this.yurtSistemiDataSet13.KullaniciPersonel);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen;
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            string ad, sifre,id;
          
            ad = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            sifre = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            id = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtkullanıcıadi.Text = ad;
            txtsifre.Text = sifre;
            cmbyöneticiTC.Text= id;
           
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from KullaniciPersonel where PerTC=@a1", bgl.baglanti());
            komut.Parameters.AddWithValue("@a1", cmbyöneticiTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kullanıcı silinmiştir");
            this.kullaniciPersonelTableAdapter.Fill(this.yurtSistemiDataSet13.KullaniciPersonel);
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update  KullaniciPersonel set KullaniciAdi=@a1, Sifre=@a2 where PerTC=@a3", bgl.baglanti());
            komut.Parameters.AddWithValue("@a1", txtkullanıcıadi.Text);
            komut.Parameters.AddWithValue("@a2", txtsifre.Text);
            komut.Parameters.AddWithValue("@a3", cmbyöneticiTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Güncelleme gerçekleşti");
            this.kullaniciPersonelTableAdapter.Fill(this.yurtSistemiDataSet13.KullaniciPersonel);
        }


    }
}
