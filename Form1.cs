using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;


namespace SnakeGame
{
    public partial class Form1 : Form
    {
        int x, y;
        int startX, startY;
        string direction = "";
        string directionKeyDown = "";
        int snakeLenght;
        int startSnakeLenght = 25;
        int foodNumber = 5;
        int sizeX, sizeY;
        int width, height; //of array
        int[,] snakeArr; //snakes
        int[,] blockArr; //food/block
        List<Point> foodPoint = new List<Point>();
        Queue<Point> snakePointQueue = new Queue<Point>();
        Point failPos = new Point();
        bool gameover = false;
        Font font = new Font("Consolas", 25.0f);

        Random random;
        Timer timer;
        int interval = 60; //snakespeed (ms)

        public Form1()
        {
            InitializeComponent();
            random = new Random();
            width = 120; height = 60;
            sizeX = gamepanel.Width / width;
            sizeY = gamepanel.Height / height;
            snakeArr = new int[width, height];
            blockArr = new int[width, height];
            startX = width / 2; startY = height / 2;
            x = startX; y = startY;
            timer = new Timer();
            timer.Interval = interval;
            timer.Tick += new EventHandler(tick);
            this.DoubleBuffered = true;
            timer.Enabled = true;
        }
        private void gamepanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            gfx.DrawRectangle(Pens.Black, 0, 0, gamepanel.Width - 1, gamepanel.Height - 1); //border
            foreach (Point p in snakePointQueue.ToList()) //snakes
            {
                gfx.FillRectangle(Brushes.Black, p.X * sizeX, p.Y * sizeY, sizeX, sizeY);
            }
            foreach (Point p in foodPoint.ToList()) //foods
            {
                gfx.FillRectangle(Brushes.DarkRed, p.X * sizeX, p.Y * sizeY, sizeX, sizeY);
            }
            if (gameover) //gameover
            {
                gfx.FillRectangle(Brushes.PaleVioletRed, failPos.X * sizeX, failPos.Y * sizeY, sizeX, sizeY);
                gfx.DrawString($"GameOver! - SnakeLenght : {snakeLenght}", font, Brushes.Black, width / 2 - 50, height / 2);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            if (key == Keys.W || key == Keys.Up && (direction != "down" || snakeLenght == 0)) { directionKeyDown = "up"; }
            if (key == Keys.S || key == Keys.Down && (direction != "up" || snakeLenght == 0)) { directionKeyDown = "down"; }
            if (key == Keys.A || key == Keys.Left && (direction != "right" || snakeLenght == 0)) { directionKeyDown = "left"; }
            if (key == Keys.D || key == Keys.Right && (direction != "left" || snakeLenght == 0)) { directionKeyDown = "right"; }
            if (key == Keys.R) { newgame(); }

        }

        
        private void newgame()
        {
            resetGame();
            snakeArr[x, y] = 1;
            snakePointQueue.Enqueue(new Point(x, y));
            for (int i = 0; i < foodNumber; i++)
            {
            spawnNewFood:
                Point fPoint = new Point(random.Next(width), random.Next(height));
                if (snakeArr[fPoint.X, fPoint.Y] > 0 || blockArr[fPoint.X, fPoint.Y] != 0) //food in snake || food in block/food
                { goto spawnNewFood; }
                foodPoint.Add(fPoint);
                blockArr[fPoint.X, fPoint.Y] = 1; //food
            }
            timer.Enabled = true;
            gameover = false;
        }

        private void resetGame()
        {
            direction = "";
            Array.Clear(snakeArr, 0, snakeArr.Length);
            Array.Clear(blockArr, 0, blockArr.Length);
            foodPoint.Clear();
            snakePointQueue.Clear();
            x = startX; y = startY;
            snakeLenght = 0;
        }

        private void gameOver()
        {
            timer.Enabled = false;
            failPos = new Point(x, y);
            gameover = true;
            Refresh();
        }


        /// <summary>
        /// timer
        /// </summary>
        private void tick(object sender, EventArgs e)
        {
            direction = directionKeyDown;
            if (direction != "")
            {
                switch (direction)
                {
                    case "left":
                        {
                            if (x != 0) { x--; } else { x = width - 1; }
                            break;
                        }
                    case "right":
                        {
                            if (x != width - 1) { x++; } else { x = 0; } //border-across
                            break;
                        }
                    case "up":
                        {
                            if (y != 0) { y--; } else { y = height - 1; }
                            break;
                        }
                    case "down":
                        {
                            if (y != height - 1) { y++; } else { y = 0; }
                            break;
                        }
                    default: break;
                }
                if (snakeArr[x, y] == 0) //snake movement
                {
                    snakeArr[x, y] = 1;
                    snakePointQueue.Enqueue(new Point(x, y)); //queue for snake tail
                }
                else { gameOver(); } //snake collision
                if (blockArr[x, y] != 1 && snakeLenght >= startSnakeLenght) //food not eaten
                {
                    Point deq = snakePointQueue.Dequeue();
                    snakeArr[deq.X, deq.Y] = 0;
                }
                else if (blockArr[x, y] != 1 && snakeLenght < startSnakeLenght) //snake growth
                { snakeLenght++; }
                else //food eaten
                {
                    snakeLenght++;
                    blockArr[x, y] = 0;
                    //new-food:
                    int i = foodPoint.IndexOf(new Point(x, y));
                newFoodPoint:
                    Point fPoint = new Point(random.Next(width - 1), random.Next(height - 1));
                    if (snakeArr[fPoint.X, fPoint.Y] > 0 || blockArr[fPoint.X, fPoint.Y] != 0) //food in snake || food in block/food
                    { goto newFoodPoint; }
                    foodPoint[i] = fPoint;
                    blockArr[fPoint.X, fPoint.Y] = 1;
                }
            }
            Refresh();
        }
        
        protected override CreateParams CreateParams
        {
            get
            {
                var parm = base.CreateParams;
                parm.ExStyle &= ~0x02000000;  //Turn off WS_CLIPCHILDREN 
                parm.ExStyle |= 0x02000000; //Turn on WS_EX_COMPOSITED
                return parm;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
