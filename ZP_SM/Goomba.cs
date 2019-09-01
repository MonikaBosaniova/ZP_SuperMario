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
    class Goomba 
    {
        public PictureBox goomba = new PictureBox();
        public int move;
        public bool isGoomba = false;

        public void DrawGoomba(int x, int y)
        {
            Size size = new Size(15, 15);
            goomba.Size = size;
            goomba.Top = y;
            goomba.Left = x;
            goomba.Image = Form1.CropImage(Image.FromFile("goomba1.png"), new Rectangle(0, 0, 64, 64));
            goomba.SizeMode = PictureBoxSizeMode.StretchImage;
            move = 16;

            isGoomba = true;
            goomba.Visible = true;
        }

        public void GoombaWalk(Map map)
        {
            if (isGoomba)
            {
                
                if (map.plan[(goomba.Left + move) / 15 , goomba.Top / 15] != 'X')
                {
                    if (move == 16)
                    {
                        goomba.Left = goomba.Left + 1;
                        goomba.Image = Form1.CropImage(Image.FromFile("goomba1.png"), new Rectangle(64, 0, 64, 64));
                        goomba.SizeMode = PictureBoxSizeMode.StretchImage;

                    }

                    else
                    {
                        goomba.Left = goomba.Left + move;
                        goomba.Image = Form1.CropImage(Image.FromFile("goomba1.png"), new Rectangle(0, 0, 64, 64));
                        goomba.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                        
                }

                else
                {
                    SwitchMovement();
                }

            }

        }

        public void SwitchMovement()
        {
            if (move == 16)
                move = -1;
            else
                move = 16;
        }

        public void Death(ref PictureBox player, ref bool jump, ref bool goombaIsDead, ref bool MarioIsDead, ref int force)
        {
            // goomba's death
            if (player.Left + player.Width > goomba.Left &&
            player.Left + player.Width < goomba.Left + goomba.Width + player.Width &&
            player.Top + player.Height >= goomba.Top &&
            player.Top < goomba.Top)
            {
                jump = false;
                force = 0;
                player.Top = goomba.Location.Y + 3;
                goombaIsDead = true;
                goomba.Visible = false;
            }

            // Mario's death
            if ((player.Right > goomba.Left &&
                player.Left < goomba.Right - player.Width &&
                player.Bottom > goomba.Top) && goombaIsDead == false )
            {
                MarioIsDead = true;
            }

            if ((player.Left < goomba.Right &&
                player.Right > goomba.Left + player.Width &&
                player.Bottom > goomba.Top) && goombaIsDead == false)
            {
                MarioIsDead = true;
            }
            
        }

    }

}
