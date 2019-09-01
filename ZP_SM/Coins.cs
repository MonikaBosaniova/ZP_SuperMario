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
    class Coins
    {
        public Form1 Form;
        public PictureBox coin = new PictureBox();

        public void DrawCoins(int x, int y)
        {
            Size size = new Size(15, 15);
            coin.Size = size;
            coin.Top = y *15;
            coin.Left = x *15;
            coin.Image = Form1.CropImage(Image.FromFile("coin.png"), new Rectangle(0, 0, 64, 64));
            coin.SizeMode = PictureBoxSizeMode.StretchImage;
            coin.Visible = true;

        }

        public void Tmp(ref Form1 form)
        {
            this.Form = form;
        }

        public void Echo(int score)
        {
            this.Form.Text = "Your score: " + score;
        }

        public void PickCoins(ref int score, Map map, int x, int y)
        {
            if (map.plan[x, y] == 'C' || map.plan[x, y] == 'c')
            {
                if (map.plan[x, y] == 'c')
                {
                    coin.Visible = false;
                }
                map.plan[x, y] = '.';
                score += 1;
                Echo(score);

            }

        }

    }

}
