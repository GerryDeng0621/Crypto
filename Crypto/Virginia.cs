using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Crypto
{
    public partial class Virginia : Form
    {
        private string[,] matrix = new string[26, 26];
        private ASCIIEncoding ascii = new ASCIIEncoding();
        private string key;
        private string code;
        private string text;
        private bool type;
        private OpenFileDialog ofd = new OpenFileDialog();
        private SaveFileDialog sfd = new SaveFileDialog();
        public Virginia()
        {
            InitializeComponent();
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    int number = 65 + i + j;
                    if (number > 90)
                    {
                        number -= 26;
                    }
                    byte[] bt = new byte[] { (byte)number };
                    matrix[i, j] = ascii.GetString(bt);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("请输入文本！");
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入密钥！");
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    type = true;
                }

                if (radioButton2.Checked == true)
                {
                    type = false;
                }

                key = textBox1.Text.ToString().ToUpper();
                code = "";
                text = richTextBox1.Text.ToString().ToUpper();
                List<int> keyNum = new List<int>();
                for (int i = 0; i < key.Length; i++)
                {
                    string str = key.Substring(i, 1);
                    keyNum.Add((int)ascii.GetBytes(str)[0] - 65);
                }

                int index = -1;
                for (int i = 0; i < this.text.Length; i++)
                {
                    if (this.text.Substring(i, 1).ToString() == " ")
                    {
                        code += " ";
                        continue;
                    }
                    index++;
                    if (type)
                    {
                        code += matrix[keyNum[index % key.Length], ascii.GetBytes(text.Substring(i, 1))[0] - 65];
                    }
                    else
                    {
                        for (int j = 0; j < 26; j++)
                        {
                            if (text.Substring(i, 1).ToString() == matrix[keyNum[index % key.Length], j])
                            {
                                byte[] bt = new byte[] { (byte)(j + 65) };
                                code += ascii.GetString(bt);
                            }
                        }
                    }
                }
                richTextBox2.Text = code.ToString();
            }
            
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
                                          + "Algorithm：Virginia\n"
                                          + "Function：Crypto\n"
                                          + "Author:DengGerry\n");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
    }
}
