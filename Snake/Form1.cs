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
        private int _sizeOfSides = 10;
        private int dirX, dirY;
        private int rX, rY;
        private List<PictureBox> snake = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
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
                generateFruit();
            }
        }


        private void update(object sender, EventArgs e)
        {
            eatFruit();
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
            if (keyCode == "Right")
            {
                dirX = 1;
                dirY = 0;
            }
            else if (keyCode == "Left")
            {
                dirX = -1;
                dirY = 0;
            }
            else if (keyCode == "Up")
            {
                dirX = 0;
                dirY = -1;
            }
            else if (keyCode == "Down")
            {
                dirX = 0;
                dirY = 1;
            }

        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            button.Text = button.Tag + " HELLO";

        }

       
    }
}
