using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Mp3_Player
{
    public partial class Form1 : Form
    {
        private string _command;
        private bool isOpen;

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder stringReturn, int iReturnLength, IntPtr hwndCallback);

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.SpecialFolder.MyMusic.ToString();
            openFileDialog.Filter = " mp3 files|*.mp3|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog.FileName.ToString();
            }
        }

            public void Play(bool loop)
            {
                if (isOpen)
                {
                    _command = "Play Mediafile";
                    if (loop)
                        _command += "REPEAT";
                    mciSendString(_command, null, 0, IntPtr.Zero);
                }
            }
            
            public void OpenPlayer(string sFilename)
            {
                _command = "open \"" + sFilename + "\" type mpegvideo alias MediaFile";
                mciSendString(_command, null, 0, IntPtr.Zero);
                isOpen = true;
            }

            public void ClosePlayer()
            {
                _command = "close Mediafile";
                mciSendString(_command, null, 0, IntPtr.Zero);
                isOpen = false;
            }

            private void button1_Click(object sender, EventArgs e)
            {
                try
                {
                    this.OpenPlayer(this.textBox1.Text);
                    this.Play(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            private void button2_Click(object sender, EventArgs e)
            {
                try
                {
                    this.ClosePlayer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            }
        }

