using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Crypto
{
    public partial class Caesar : Form
    {
        private int key = 0;
        private string str = "";
        private bool type = true;
        private OpenFileDialog ofd = new OpenFileDialog();
        private SaveFileDialog sfd = new SaveFileDialog();
        public Caesar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("请输入明文！");
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入K值！");
            }
            else
            {
                string Cae = "";
                string tar = "";
                key = Convert.ToInt32(textBox1.Text.ToString());
                char[] ch = str.ToArray();
                if (radioButton1.Checked == true)
                {
                    type = true;
                }
                if (radioButton2.Checked == true)
                {
                    type = false;
                }
                for (int i = 0; i < str.Length; i++)
                {
                    string temp = ch[i].ToString();
                    bool isChar = "abcdefghijklmnopqrstuvwxyz".Contains(temp.ToLower());
                    bool isToUpperChar = isChar && (temp.ToUpper() == temp);
                    temp = temp.ToLower();
                    if (isChar)
                    {
                        if (type)
                        {
                            int offset = (AscII(temp) + key - AscII("a")) % (AscII("z") - AscII("a") + 1);
                            tar = Convert.ToChar(offset + AscII("a")).ToString();
                        }
                        else
                        {
                            int offset = (AscII("z") + key - AscII(temp)) % (AscII("z") - AscII("a") + 1);
                            tar = Convert.ToChar(AscII("z") - offset).ToString();
                        }
                        if (isToUpperChar)
                        {
                            tar = tar.ToUpper();
                        }
                    }
                    else
                    {
                        tar = temp;
                    }
                    Cae += tar;
                }
                richTextBox2.Text = Cae;
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private int AscII(string str)
        {
            byte[] array = new byte[1];
            array = Encoding.ASCII.GetBytes(str);
            int asciicode = array[0];
            return asciicode;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (string s in richTextBox1.Lines) ;
            str = richTextBox1.Text;
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            ofd.Title = "打开(Open)";
            ofd.FileName = "";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.Filter = "文本文件(*.txt)|*.txt";
            ofd.ValidateNames = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(ofd.FileName, System.Text.Encoding.Default);
                    richTextBox1.Text = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Simple Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            sfd.Title = "保存(Save)";
            sfd.FileName = "";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfd.Filter = "文本文件(*.txt)|*.txt";
            sfd.ValidateNames = true;
            sfd.CheckPathExists = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {       
                    richTextBox2.SaveFile(sfd.FileName, RichTextBoxStreamType.PlainText);
                    MessageBox.Show("文件保存成功!");
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Simple Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 关于AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Date：2016/4/30\n"
                              + "Algorithm：Caesar\n"
                              + "Function：Crypto\n"
                              + "Author:DengGerry\n");
        }
    }
}
