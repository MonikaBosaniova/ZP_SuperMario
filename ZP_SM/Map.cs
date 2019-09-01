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

    class Map
    {
        public char[,] plan;
        
        public void ReadingMap(string s, PaintEventArgs e,ref Goomba gomba)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(s);

            int height = 20;        // height and width of game screen
            int width = 30;
            
            plan = new char[width, height];
            
            for (int i = 0; i < width; i++)
            {
                string line = sr.ReadLine();
                for (int j = 0; j < height; j++)        //making an array of text file
                {
                    char ch = line[j];
                    plan[i, j] = ch;
                    
                    
                    switch (ch)                         // drawing a map
                    {
                        case 'X':
                            e.Graphics.DrawImage(new Bitmap(Image.FromFile("rock1.png"), new Size(15,15)), i * 15, j * 15 );
                            break;

                        case 'B':
                            e.Graphics.DrawImage(new Bitmap(Image.FromFile("brick1.png"), new Size(15, 15)), i * 15, j * 15);
                            break;

                        case 'O':
                            e.Graphics.DrawImage(new Bitmap(Image.FromFile("block1.png"), new Size(15, 15)), i * 15, j * 15);
                            break;

                        case 'W':
                            break;

                        case '.':
                            break;

                        case 'Q':
                            e.Graphics.DrawImage(new Bitmap(Form1.CropImage(Image.FromFile("coinbox1.png"), new Rectangle(0,0,64,64)), new Size(15, 15)), i * 15, j * 15);
                            break;

                        case 'C':
                            e.Graphics.DrawImage(new Bitmap(Form1.CropImage(Image.FromFile("coin.png"), new Rectangle(0,0,64,64)), new Size(15, 15)), i * 15, j * 15);
                            break;

                        case 'G':
                            gomba.DrawGoomba(i * 15, j * 15);
                            break;

                        case 'P':
                            e.Graphics.DrawImage(new Bitmap(Image.FromFile("pole1.png"), new Size(15, 15)), i * 15, j * 15);
                            break;

                        case 'F':
                            e.Graphics.DrawImage(new Bitmap(Form1.CropImage(Image.FromFile("flag.png"), new Rectangle(28, 0, 70, 64)), new Size(15, 15)), i * 15 - 6, j * 15);
                            break;

                        case 'T':
                            e.Graphics.DrawImage(new Bitmap(Image.FromFile("poletop.png"), new Size(15, 15)), i * 15, j * 15);
                            break;

                        default:
                            break;
                    }

                }

            }
            sr.Close();

        } 

    }

}

