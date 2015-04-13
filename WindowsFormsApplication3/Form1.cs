using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            showPicture(panel1, textBox2.Text);
        }



        private void showPicture(Panel P, String url)
        {
            if (P.Controls.Count > 0)
            {
                P.Controls.Clear();
                for (int ix = this.Controls.Count - 1; ix >= 0; ix--)
                {
                    if (this.Controls[ix] is PictureBox) 
                        this.Controls[ix].Dispose();
                }
            }
            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(210, 260);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            panel1.Controls.Add(pictureBox1);       
            StringBuilder sb = new StringBuilder();
            sb.Append("http://");
            sb.Append(url);
            String link = sb.ToString();
            try
            {

                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(link);
                request.Timeout = 5000;   //set time out
                System.Net.HttpWebResponse response;
                request.AllowWriteStreamBuffering = true;
                response = (System.Net.HttpWebResponse)request.GetResponse();
                textBox3.Clear();
                textBox3.AppendText(((int)response.StatusCode) + ":" + response.StatusCode);
                textBox4.Clear();
                for (int i = 0; i < response.Headers.Count; ++i)
                {
                    //Console.WriteLine("\nHeader Name:{0}, Value :{1}", request.Headers.Keys[i], request.Headers[i]);
                    textBox4.AppendText(response.Headers.Keys[i] + ":" + response.Headers[i] + Environment.NewLine);

                }
                using (var stream = response.GetResponseStream())
                {
                    pictureBox1.Image = Bitmap.FromStream(stream);
                }

            }
            catch(Exception)
            {
                System.Windows.Forms.MessageBox.Show("Invalid URL, please re-enter");
            }
        }


    }
}
