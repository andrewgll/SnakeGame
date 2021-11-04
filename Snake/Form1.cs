using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        PictureBox fruit = new PictureBox();
        private int _width = 900;
        private int _height = 800;
        private int _sizeOfSides = 20;
        private int dirX, dirY;
        private int rX, rY;
        private Label labelScore;
        private int score = 0;

        private List<PictureBox> snake = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
            Text = "Snake";
            Width = _width;
            Height = _height;

            _generateMap();

            labelScore = new Label();
            labelScore.Text = "Score: 0";
            labelScore.Location = new Point(810, 10);
            Controls.Add(labelScore);

            PictureBox cube = new PictureBox();
            cube.BackColor = Color.DarkOliveGreen;
            cube.Size = new Size( _sizeOfSides, _sizeOfSides);
            cube.Location = new Point(0, 0);
            snake.Add(cube);
            this.Controls.Add(cube);

            dirX = 1;
            dirY = 0;


           
            generateFruit();

            timer.Interval = 200;
            timer.Tick += new EventHandler(update);
            timer.Start();

            KeyDown += new KeyEventHandler(keyDown_pressed);

        }

        private void checkBorders()
        {
            if(snake[0].Location.X < 0)
            {
                Controls.Remove(snake[0]);
                snake[0].Location = new Point(_height - _sizeOfSides, snake[0].Location.Y);
                Controls.Add(snake[0]);
                dirX = -1;
                dirY = 0;
            }
            else if(snake[0].Location.X > _height -  _sizeOfSides)
            {
                Controls.Remove(snake[0]);
                snake[0].Location = new Point(0, snake[0].Location.Y);
                Controls.Add(snake[0]);
                dirX = 1;
                dirY = 0;
            }
            else if (snake[0].Location.Y < 0)
            {
                Controls.Remove(snake[0]);
                snake[0].Location = new Point(snake[0].Location.X, _height - _sizeOfSides);
                Controls.Add(snake[0]);
                dirY = -1;
                dirX = 0;
            }
            else if (snake[0].Location.Y > _height -  _sizeOfSides)
            {
                Controls.Remove(snake[0]);
                snake[0].Location = new Point(snake[0].Location.X, 0);
                Controls.Add(snake[0]);
                dirY = 1;
                dirX = 0;
            }


        }

        private void generateFruit()
        {
            
            fruit.BackColor = Color.Red;
            fruit.Size = new Size(_sizeOfSides, _sizeOfSides);

            Random rnd = new Random();
            rX = rnd.Next(0, _width/2 );
            rY = rnd.Next(0, _height/2);
            int tempX = rX % _sizeOfSides;
            rX -= tempX;
            int tempY = rY % _sizeOfSides;
            rY -= tempY;

            fruit.Location = new Point(rX,rY);
            this.Controls.Add(fruit);
        }

        private void _generateMap()
        {
            for (int i = 0; i < _width/_sizeOfSides - 4; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, _sizeOfSides * i);
                pic.Size = new Size(_width - 100, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= _height / _sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(_sizeOfSides * i,0);
                pic.Size = new Size(1, _width - 100);
                this.Controls.Add(pic);
            }

        }

        private void eatItSelf()
        {
            for (int i = 1; i < score; i++)
            {
                if (snake[0].Location == snake[i].Location)
                {
                  
                        timer.Stop();
                        MessageBox.Show("Game over! Your Score is:" + score);
                     
                }
            }
        }

        private void eatFruit()
        {
            if(snake[0].Location.X == rX && snake[0].Location.Y == rY)
            {
                
                PictureBox cube = new PictureBox();
                cube.Location = new Point(snake[snake.Count - 1].Location.X + _sizeOfSides * dirX, snake[snake.Count - 1].Location.Y + _sizeOfSides * dirY);
                cube.Size = new Size(_sizeOfSides, _sizeOfSides);
                cube.BackColor = Color.DarkOliveGreen;
                snake.Add(cube);
                this.Controls.Add(cube);
                timer.Interval -= 10;
                labelScore.Text = "Score: " + ++score;
                
                generateFruit();

            }
        }


        private void update(object sender, EventArgs e)
        {
            eatItSelf();
            eatFruit();
            checkBorders();
            moveSnake();
        }

        private void moveSnake()
        {
            for (int i = snake.Count - 1; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirX * _sizeOfSides, snake[0].Location.Y + dirY * _sizeOfSides);
        }

        private void keyDown_pressed(object sender, KeyEventArgs e)
        {
            string keyCode = e.KeyCode.ToString();
            if (keyCode == "Right" && dirX != -1)
            {
                dirX = 1;
                dirY = 0;
            }
            else if (keyCode == "Left" && dirX != 1)
            {
                dirX = -1;
                dirY = 0;
            }
            else if (keyCode == "Up" && dirY != 1)
            {
                dirX = 0;
                dirY = -1;
            }
            else if (keyCode == "Down" && dirY != -1)
            {
                dirX = 0;
                dirY = 1;
            }

        }


       
    }
}
