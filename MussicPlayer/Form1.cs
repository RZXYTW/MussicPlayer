using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MussicPlayer
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (btnPlayOrPause.Text == "播放")
            {
              
                if (musicPlayer.URL == $"")
                {
                    if (listBox1.SelectedIndex == -1)
                    {
                        return;
                    }                 
                    else
                    {
                        musicPlayer.URL = listPath[listBox1.SelectedIndex];
                        musicPlayer.Ctlcontrols.play();
                        this.btnPlayOrPause.Text = "暂停";
                    }
                   
                }
                else if(musicPlayer.URL!= listPath[listBox1.SelectedIndex])
                {
                    musicPlayer.URL = listPath[listBox1.SelectedIndex];
                    musicPlayer.Ctlcontrols.play();
                    this.btnPlayOrPause.Text = "暂停";
                }
                else
                {
                    musicPlayer.Ctlcontrols.play();
                    this.btnPlayOrPause.Text = "暂停";
                }                                  
            }
            else if (btnPlayOrPause.Text == "暂停")
            {
                musicPlayer.Ctlcontrols.pause();
                this.btnPlayOrPause.Text = "播放";
            }
        }     
        private void Form1_Load(object sender, EventArgs e)
        {
            //程序加载时取消播放器的自动播放功能
            musicPlayer.settings.autoStart = false;          
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            musicPlayer.Ctlcontrols.stop();
        }

        //存储音乐路径
        List<string> listPath = new List<string>();
        private void stnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"E:\浏览器文件夹\NewMusic";
            openFileDialog.Filter = "音乐文件|*.wav|MP3文件|*.mp3文件|所有文件|*.*";
            openFileDialog.Title = "请选择音乐文件";
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();

            //获取在文本框中选择文件的全路径
            string[] path = openFileDialog.FileNames;
            for(int i = 0; i < path.Length; i++)
            {
                //将音乐文件的全路径存储到泛型集合中
                listPath.Add(path[i]);

                //将音乐文件名存储到ListBox中
                listBox1.Items.Add(Path.GetFileName(path[i]));
            }              
        }

        //双击播放对应的音乐
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                musicPlayer.URL = listPath[listBox1.SelectedIndex];
                musicPlayer.Ctlcontrols.play();
                btnPlayOrPause.Text = "暂停";

                //labelInfo.Text = musicPlayer.Ctlcontrols.currentPosition.ToString();
            }
            catch (Exception)
            {

                return;
            }                                   
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
           
            int index = listBox1.SelectedIndex;
            listBox1.SelectedIndices.Clear();

            index++;
            if (index ==listBox1.Items.Count)
            {
                index = 0;
            }

            listBox1.SelectedIndex = index;
            musicPlayer.URL = listPath[index];
            musicPlayer.Ctlcontrols.play();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            listBox1.SelectedIndices.Clear();

            index--;
            if (index < 0)
            {
                index = listBox1.Items.Count - 1;
            }
            listBox1.SelectedIndex = index;
            musicPlayer.URL = listPath[index];
            musicPlayer.Ctlcontrols.play();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = listBox1.SelectedItems.Count;
            for(int i = 0; i < count; i++)
            {
                listPath.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (musicPlayer.playState ==WMPLib.WMPPlayState.wmppsPlaying)
            {
                labelInfo.Text = musicPlayer.currentMedia.duration.ToString() + "\r\n" + musicPlayer.currentMedia.durationString + "\r\n" + musicPlayer.Ctlcontrols.currentPosition.ToString() + "\r\n" + musicPlayer.Ctlcontrols.currentPositionString;
            }
        }
    }
}
