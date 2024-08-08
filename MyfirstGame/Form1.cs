using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace MyfirstGame
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer musicplayer = new WindowsMediaPlayer();
        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed;
        int force;
        int Score = 0;
        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int verticalSpeed = 5;

        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;

        

        public Form1()
        {
            InitializeComponent();
            musicplayer.URL = "soulful-streets-202646.mp3";
            musicplayer.URL = "soulful-streets-202646.mp3";
            musicplayer.URL = "soulful-streets-202646.mp3";
            musicplayer.URL = "soulful-streets-202646.mp3";
            musicplayer.URL = "soulful-streets-202646.mp3";
        }

       

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + Score;

            player.Top += jumpSpeed;

            if(goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if(goRight == true)
            {
               player.Left += playerSpeed;
            }
            if(jumping == true && force < 0)
            {
                jumping = false;
            }
            if(jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }
            foreach (Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    if((string) x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;

                            if((string)x . Name == "horizontalPlatform" && goLeft== false || (string)x.Name == "horizontalPlatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;
                            }
                        }
                    }
                    x.BringToFront();
                }
                if ((string)x.Tag == "Coin")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        Score++;
                    }
                }

                    if ((string) x.Tag == "enemys")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            GameTimer.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score: " + Score + Environment.NewLine + "You have been killed.";
                        }
                    }
                }
            
            horizontalPlatform.Left -= horizontalSpeed;

            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            verticalPlatform.Top += verticalSpeed;

            if (verticalPlatform.Top < 199 || verticalPlatform.Top > 562)
            {
               verticalSpeed = -verticalSpeed;
            }

            enemyOne.Left -= enemyOneSpeed;

            if(enemyOne.Left < pictureBox5.Left || enemyOne.Left + enemyOne.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }
            enemyTwo.Left += enemyTwoSpeed;

            if(enemyTwo.Left < pictureBox2.Left || enemyTwo.Left + enemyTwo.Width >  pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            if(player.Top + player.Height > this.ClientSize.Height + 50)
            {
                GameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + Score + Environment.NewLine + "You fell to your death";
            }
            if(player.Bounds.IntersectsWith(Door.Bounds) && Score == 28)
            {
                GameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + Score + Environment.NewLine + "You have finished Round 1";
            }
            else
            {
                txtScore2.Text = "Score: " + Score + Environment.NewLine + "collect all the coins";

            }
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox36_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            musicplayer.controls.play();
        }

        private void KeyisDown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if(e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyisUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if(jumping == true)
            {
                jumping = false;
            }
            if(e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            Score = 0;

            txtScore.Text = "Score: " + Score;

            foreach (Control x in this.Controls)
            {
                if(x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }
            player.Left = 71;
            player.Top = 624;

            enemyOne.Left = 435;
            enemyTwo.Left = 355;

            horizontalPlatform.Left = 230;
            verticalPlatform.Top = 562;

            GameTimer.Start();



        }
    }
}
