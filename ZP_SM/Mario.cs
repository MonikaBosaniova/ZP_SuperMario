using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZP_SM
{
    class Mario 
    {

        public Form1 form;

        public PictureBox player = new PictureBox();
        public PictureBox gameOver = new PictureBox();
        public PictureBox youWin = new PictureBox();

        int pic = 0;

        public void MakeMario(ref int SM_x_pix, ref int SM_y_pix)
        {
            Size size = new Size(15, 15);
            player.Size = size;
            player.Height = 15;
            player.Top = SM_y_pix;
            player.Left = SM_x_pix;
            player.Image = Image.FromFile("sm-mario-right1.png");
            player.BackColor = Color.Transparent;
            player.SizeMode = PictureBoxSizeMode.StretchImage;

            gameOver.Visible = false;
            gameOver.Location = new Point(0, 0);
            gameOver.Size = new Size(450, 300);
            gameOver.Image = Image.FromFile("GameOver.jpg");
            gameOver.SizeMode = PictureBoxSizeMode.StretchImage;

            youWin.Visible = false;
            youWin.Location = new Point(0, 0);
            youWin.Size = new Size(450, 300);
            youWin.Image = Image.FromFile("youWin.png");
            youWin.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        public bool Finish(Map map)
        {
            if (map.plan[player.Left / 15, player.Top / 15] == 'P' || map.plan[player.Right / 15, player.Top / 15] == 'P')
                return true;
            else
                return false;
        }

        public void SetMarioPositionTo(int Left, int Top)
        {
            player.Left = Left;
            player.Top = Top;
        }

        public void WalkingJumping(ref bool right, ref bool left, ref bool jump, ref int force, Map map, ref bool inair, ref bool MarioIsDead , Timer mariotmr, ref Coins coins, ref int score)
        {
            // Walking to right
            if (right == true)
            {
                // changing pictures while Mario is running
                if (pic == 0)
                {
                    player.Image = Form1.CropImage(Image.FromFile("sm-mario-right-run1.png"), new Rectangle(0, 0, 48, 64));
                    player.SizeMode = PictureBoxSizeMode.StretchImage;

                }

                if (pic == 1)
                {
                    player.Image = Form1.CropImage(Image.FromFile("sm-mario-right-run1.png"), new Rectangle(48, 0, 48, 64));
                    player.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                if (pic == 2)
                {
                    player.Image = Form1.CropImage(Image.FromFile("sm-mario-right-run1.png"), new Rectangle(96, 0, 48, 64));
                    player.SizeMode = PictureBoxSizeMode.StretchImage;

                }

                if (pic == 3)
                {
                    player.Image = Form1.CropImage(Image.FromFile("sm-mario-right-run1.png"), new Rectangle(144, 0, 48, 64));
                    player.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic = -1;

                }

                player.Left += 1;       // Mario's move
                BlockGCollisions(ref right, ref left, map, ref jump, ref coins, ref score);
                player.Left += 1;       // another move for better animated feeling
                
                // wall collision 450 = screen width
                if (player.Right > 450)
                {
                    player.Left = 449 - player.Width;
                    right = false;
                }

                pic += 1;
            }

            // To left
            if (left == true)
            {
                
                if (pic == 0)
                {
                    player.Image = Form1.CropImage(Image.FromFile("sm-mario-left-run1.png"), new Rectangle(0, 0, 48, 64));
                    player.SizeMode = PictureBoxSizeMode.StretchImage;

                }

                if (pic == 1)
                {
                    player.Image = Form1.CropImage(Image.FromFile("sm-mario-left-run1.png"), new Rectangle(48, 0, 48, 64));
                    player.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                if (pic == 2)
                {
                    player.Image = Form1.CropImage(Image.FromFile("sm-mario-left-run1.png"), new Rectangle(96, 0, 48, 64));
                    player.SizeMode = PictureBoxSizeMode.StretchImage;

                }

                if (pic == 3)
                {
                    player.Image = Form1.CropImage(Image.FromFile("sm-mario-left-run1.png"), new Rectangle(144, 0, 48, 64));
                    player.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic = -1;

                }

                player.Left -= 1;
                BlockGCollisions(ref right, ref left, map, ref jump, ref coins, ref score);
                player.Left -= 1;

                // Collison with left wall
                if (player.Left < 0)
                {
                    player.Left = 0;
                    left = false;
                }

                pic += 1;

            }

            //Jump
            if (jump == true)
            {
                
                inair = true;
                bool headache = false;     // Head collision with block

                if (force > 0)
                {
                   
                    if (map.plan[player.Left / 15, (player.Top - force) / 15] == 'X' || map.plan[player.Right / 15, (player.Top - force) / 15] == 'X' ||
                        map.plan[player.Left / 15, (player.Top - force) / 15] == 'B' || map.plan[player.Right / 15, (player.Top - force) / 15] == 'B' ||
                        map.plan[player.Left / 15, (player.Top - force) / 15] == 'Q' || map.plan[player.Right / 15, (player.Top - force) / 15] == 'Q' ||
                        map.plan[player.Left / 15, (player.Top - force) / 15] == 'O' || map.plan[player.Right / 15, (player.Top - force) / 15] == 'O')
                    {
                        if (map.plan[player.Left / 15, (player.Top - force) / 15] == 'Q' )
                        {
                            HeadToQuestionBlocksCollisions(player.Left/15, (player.Top - force) / 15);
                        }
                        else if (map.plan[player.Right / 15, (player.Top - force) / 15] == 'Q')
                        {
                            HeadToQuestionBlocksCollisions((player.Right / 15) , (player.Top - force) / 15);
                        }
                        force = (player.Top - ((((player.Top - force) / 15) + 1) * 15));
                        headache = true; 
                    }
                    player.Top -= force;

                    if (headache)
                    {
                        headache = false;
                        force = 0;
                    }

                    if (force > -1)
                        force -= 1;
                }

                else
                {
                    int i = 3;

                    while ( i > 0 && jump )
                    {
                        player.Top += 1;
                        JumpCollisions(ref right, ref left, map, ref jump, ref inair);

                        if (player.Bottom > 285)
                        {
                            MarioIsDead = true;
                            Death(ref MarioIsDead, ref mariotmr);
                        }

                        i--;
                    }

                }
                
            }
 
        }

        public void HeadToQuestionBlocksCollisions(int blockX, int blockY)
        {
            form.DrawCoin(blockX, blockY);
        }

        public void BlockGCollisions(ref bool right, ref bool left, Map map,ref bool jump, ref Coins coin, ref int score)
        {
            //Side Collision for blocks
            int x = ((player.Right +2)  / 15);  
            
            if ((map.plan[x, (player.Top - 1) / 15] == 'X') || (map.plan[x, (player.Top -1) / 15] == 'B') ||
                (map.plan[x, (player.Bottom - 1) / 15] == 'Q') || (map.plan[x, (player.Bottom - 1) / 15] == 'Q') ||
                (map.plan[x, (player.Bottom - 1) / 15] == 'X') || (map.plan[x, (player.Bottom - 1) / 15] == 'B') ||
                (map.plan[x, (player.Bottom - 1) / 15] == 'O') || (map.plan[x, (player.Bottom - 1) / 15] == 'O'))
            {
                right = false;
            }

            coin.Tmp(ref form);
            coin.PickCoins(ref score, map, x, (player.Top - 1) / 15); 
            coin.PickCoins(ref score, map, x, (player.Bottom - 1) / 15);

            x = ((player.Left - 2 ) / 15);
                
            if ((map.plan[x, (player.Top - 1) / 15] == 'X') || (map.plan[x, (player.Top - 1) / 15] == 'B') ||
                (map.plan[x, (player.Top - 1) / 15] == 'Q') || (map.plan[x, (player.Top - 1) / 15] == 'Q') ||
                (map.plan[x, (player.Bottom - 1) / 15] == 'X') || (map.plan[x, (player.Bottom - 1) / 15] == 'B') ||
                (map.plan[x, (player.Bottom - 1) / 15] == 'O') || (map.plan[x, (player.Bottom - 1) / 15] == 'O'))
            {
                left = false;
            }

            coin.PickCoins(ref score, map, x, (player.Top - 1) / 15);
            coin.PickCoins(ref score, map, x, (player.Bottom - 1) / 15);

        }

        public void JumpCollisions(ref bool right, ref bool left, Map map, ref bool jump, ref bool inair)
        {
            if ((map.plan[(player.Left) / 15, player.Bottom / 15] == 'X') || (map.plan[(player.Right) / 15, player.Bottom / 15] == 'X') ||
                (map.plan[(player.Left) / 15, player.Bottom / 15] == 'B') || (map.plan[(player.Right) / 15, player.Bottom / 15] == 'B') ||
                (map.plan[(player.Left) / 15, player.Bottom / 15] == 'Q') || (map.plan[(player.Right) / 15, player.Bottom / 15] == 'Q') ||
                (map.plan[(player.Left) / 15, player.Bottom / 15] == 'O') || (map.plan[(player.Right) / 15, player.Bottom / 15] == 'O'))
            {
                jump = false;
                inair = false;
            }

        
        }

        public void FallCollision(Map map, ref bool jump, ref bool inair)
        {

            if  (!((map.plan[player.Left / 15, player.Bottom / 15] == 'X') || (map.plan[player.Right / 15, player.Bottom / 15] == 'X'))  &&
                  !((map.plan[player.Left / 15, player.Bottom / 15] == 'B') || (map.plan[player.Right / 15, player.Bottom / 15] == 'B')) && !inair &&
                  !((map.plan[player.Left / 15, player.Bottom / 15] == 'Q') || (map.plan[player.Right / 15, player.Bottom / 15] == 'Q')) &&
                  !((map.plan[player.Left / 15, player.Bottom / 15] == 'O') || (map.plan[player.Right / 15, player.Bottom / 15] == 'O')))
            {
                jump = true;
                inair = true;
            }

        }

        public void Death(ref bool MarioIsDead, ref Timer mariotmr)
        {
            if (MarioIsDead == true)
            {
                player.Image = Image.FromFile("mario_death.png");
                player.SizeMode = PictureBoxSizeMode.StretchImage;
                mariotmr.Stop();
                gameOver.Visible = true;
            }

        }

        public void YouWin(ref Timer mariotmr)
        {
                youWin.Visible = true;
                mariotmr.Stop();

        }

    }

}
