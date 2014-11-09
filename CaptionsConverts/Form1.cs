using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace CaptionsConverts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var of = new OpenFileDialog();
            if (of.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = of.OpenFile())
                {
                    string vvt = StreamToString(stream);


                    string file = of.FileName;


                    string newfile = Path.ChangeExtension(file, "srt");


                    SavaCaptionFile(vvt, newfile, txtEncoding.Text);
                    MessageBox.Show("Convert OK");
                }
            }
        }


        private static void SavaCaptionFile(string content, string savePath, string encoding = "utf-8")
        {
            string srt = SubtitleHelper.ConvertWebvttToSrt(content);

            File.WriteAllText(savePath, srt, Encoding.GetEncoding(encoding));
        }

        public static string StreamToString(Stream stream)
        {
            if (stream.CanSeek)
                stream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static Stream StringToStream(string src)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var webclient = new WebClient();

            string vvt = "";

            using (Stream stream = webclient.OpenRead(txtUrl.Text))
            {
                vvt = StreamToString(stream);
            }

            if (!string.IsNullOrEmpty(vvt))
            {
                var sfd = new SaveFileDialog();

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var file = sfd.FileName;
                    file = Path.ChangeExtension(file, "srt");
                    SavaCaptionFile(vvt, file, txtEncoding.Text);

                    MessageBox.Show("完成");
                }
            }
        }
    }
}