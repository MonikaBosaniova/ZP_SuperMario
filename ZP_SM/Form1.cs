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
    public partial class Form1 : Form
    {
        public bool inair = false;
        public bool right;              // right arrow
        public bool left;               // left arrow
        public bool jump;               // space
        public int gravity = 15;        // height of jump
        public int force;                
        public int score = 0;
       
        public bool goombaIsDead = false;
        public bool MarioIsDead = false;
       
        public int SM_x = 5 * 15 + 7;       // tested numbers - starting position of Mario
        public int SM_y = 22 * 15 + 3;

        int numberOfMaps = 4;
        bool vykreslenaMapa1 = false;
        bool vykreslenaMapa2 = false;
        bool vykreslenaMapa3 = false;
        bool vykreslenaMapa4 = false;
        bool vykreslenaMapa5 = false;

        int coinX;
        int coinY;
        bool coinDraw = false;

        Mario hero;
        Coins coins;
        Goomba enemyG;
        Map map;

        public static Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        public Form1()
        {
            map = new Map();
            hero = new Mario();
            coins = new Coins();
            enemyG = new Goomba();

            hero.MakeMario(ref SM_x, ref SM_y);

            this.Controls.Add(hero.player);
            this.Paint += new PaintEventHandler(Draw);

            InitializeComponent();

            hero.form = this;
        }


        public void DrawCoin(int x, int y)
        {
            coinX = x;
            coinY = y;
            coinDraw = true;
            map.plan[x , y] = 'B';
        }

        public void Draw(object sender, PaintEventArgs f)
        {
            if (coinDraw)
            {
                coins.DrawCoins(coinX, coinY -1);
                coinDraw = false;
                this.Controls.Add(coins.coin);
                map.plan[coinX, coinY - 1] = 'c';

            }

            if (numberOfMaps == 4 && vykreslenaMapa1 == false)
            {
                map.ReadingMap("mapa1.txt", f, ref enemyG);
                vykreslenaMapa1 = true;
                
            }

            if (numberOfMaps == 3 && vykreslenaMapa2 == false)
            {
                this.Controls.Add(hero.gameOver);
                map.ReadingMap("mapa2.txt", f, ref enemyG);
                vykreslenaMapa2 = true;;
                hero.SetMarioPositionTo(0, 285 - hero.player.Height);
                Controls.Add(hero.player);
                Controls.Add(enemyG.goomba);

            }

            if (numberOfMaps == 2 && vykreslenaMapa3 == false)
            {
                this.Controls.Add(hero.gameOver);
                map.ReadingMap("mapa3.txt", f, ref enemyG);
                vykreslenaMapa3 = true;
                hero.SetMarioPositionTo(0, 90 - hero.player.Height);
                Controls.Add(hero.player);
                Controls.Add(enemyG.goomba);
            }

            if (numberOfMaps == 1 && vykreslenaMapa4 == false)
            {
                this.Controls.Add(hero.gameOver);
                map.ReadingMap("mapa4.txt", f,ref enemyG);
                vykreslenaMapa4 = true;
                hero.SetMarioPositionTo(0, 285 - hero.player.Height);
                Controls.Add(hero.player);
                Controls.Add(enemyG.goomba);
            }

            if (numberOfMaps == 0 && vykreslenaMapa5 == false)
            {
                this.Controls.Add(hero.gameOver);
                map.ReadingMap("mapa5.txt", f,ref enemyG);
                vykreslenaMapa5 = true;
                hero.SetMarioPositionTo(0, 90 - hero.player.Height);
                Controls.Add(hero.player);
                Controls.Add(enemyG.goomba);
            }
        }
       
        private void Mariotmr_Tick(object sender, EventArgs e)
        {
            // testing death at beginnig of every loop
            hero.Death(ref MarioIsDead, ref mariotmr);
            hero.BlockGCollisions(ref right, ref left, map,  ref jump, ref coins, ref score);

            if (inair) { hero.JumpCollisions(ref right, ref left, map, ref jump, ref inair);}

            hero.WalkingJumping(ref right, ref left, ref jump, ref force,map,ref inair, ref MarioIsDead, mariotmr, ref coins, ref score);
            hero.FallCollision(map, ref jump, ref inair);  // falling of blocks

            // testing Goomba's Death.. and walk
            if(enemyG.isGoomba && enemyG.goomba.Visible == true)
            {
                enemyG.Death(ref hero.player, ref jump, ref goombaIsDead, ref MarioIsDead, ref force);
                enemyG.GoombaWalk(map);
            } 

            // Another Level
            if(hero.Finish(map))
            {
                numberOfMaps -= 1;
                this.mariotmr.Stop();
                this.Controls.Clear();
                InitializeComponent();
                enemyG.isGoomba = false;

                // Ending screen
                if (vykreslenaMapa5 == true) 
                {
                    this.Controls.Add(hero.youWin);
                    hero.YouWin(ref mariotmr);
                    this.mariotmr.Stop();

                }
                
            }
  
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) { right = true;  }
            if (e.KeyCode == Keys.Left) { left = true; }
            if (e.KeyCode == Keys.Escape) { this.Close(); }  //escape -> exit
            if (jump != true)
            {
                if (e.KeyCode == Keys.Space)
                {
                    jump = true;
                    force = gravity;
                }

            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) { right = false; }
            if (e.KeyCode == Keys.Left) { left = false; }

        }

    }

}

