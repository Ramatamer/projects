using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace package
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox1.Text);
            int y1 = Convert.ToInt32(textBox2.Text);

            int x2 = Convert.ToInt32(textBox3.Text);
            int y2 = Convert.ToInt32(textBox4.Text);

            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);

            ddaline(p1, p2);
        }


        int round(float a) { return Convert.ToInt32(a + 0.5); }
        private void ddaline(Point p1, Point p2)
        {
            var aBrush = Brushes.Navy;
            var g = panel1.CreateGraphics();
            int k = 0;
            float xincrement, yincrement, x, y, steps;
            int deltax = p2.X - p1.X;
            int deltay = p2.Y - p1.Y;
            if (Math.Abs(deltax) > Math.Abs(deltay))
                steps = Math.Abs(deltax);
            else
                steps = Math.Abs(deltay);
            xincrement = (float)deltax / (float)steps;
            yincrement = (float)deltay / (float)steps;
            x = p1.X;
            y = p1.Y;
            output.AppendText("  X        Y       (X,Y)" + Environment.NewLine);
            drawPoint(round(x),round(y));
            for (k = 0; k < steps; k++)
            {
                x = x + xincrement;         
                y = y + yincrement;
                drawPoint(round(x),round(y));
                output.AppendText(x.ToString("0.00") + "    " + y.ToString("0.00") + "    " + "(" + Math.Round(x) + "," + Math.Round(y) + ")" + Environment.NewLine);
            }
        }

        private void drawPoint(int x, int y)
        {
            var brush = Brushes.Black;
            var P = panel1.CreateGraphics();

            P.FillRectangle(brush, center.X + x, center.Y - y, 2, 2);

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox5.Text);
            int y1 = Convert.ToInt32(textBox6.Text);

            int x2 = Convert.ToInt32(textBox7.Text);
            int y2 = Convert.ToInt32(textBox8.Text);

            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            panel1.Refresh();
            Bresenhamline(x1, y1, x2, y2, "Def");

        }
        private void swap(ref int x0, ref int y0, ref int xEnd, ref int yEnd)
        {
            int temp1, temp2;
            temp1 = x0;
            x0 = y0;
            y0 = temp1;

            temp2 = xEnd;
            xEnd = yEnd;
            yEnd = temp2;
        }
        private void Bresenhamline(int x0, int y0, int xEnd, int yEnd, string Color)
        {
            float deltaX, deltaY, p;
            deltaX = (xEnd - x0);
            deltaY = (yEnd - y0);
            var brush = Brushes.Navy;
            if (Color == "Yellow") { brush = Brushes.Yellow; }

            var g = panel1.CreateGraphics();

            float slope = deltaY / deltaX;
            if (deltaX == 0) { slope = 99999; }
            // First Ocstant
            if (x0 < xEnd && slope >= 0 && slope <= 1)
            {
                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = x0; i < xEnd; i++)
                {
                    output.AppendText("  X        Y       (X,Y)" + Environment.NewLine);
                    if (p < 0)
                    {
                        
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (++X), center.Y - (Y), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (++X), center.Y - (++Y), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

            // Second Ocstant
            else if (y0 < yEnd && slope > 1 && slope < 999999)
            {
                swap(ref x0, ref y0, ref xEnd, ref yEnd);
                deltaX = xEnd - x0;

                deltaY = yEnd - y0;

                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = x0; i < xEnd; i++)
                {

                    if (p < 0)
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (Y), center.Y - (++X), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (++Y), center.Y - (++X), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

            // Third Ocstant
            else if (y0 < yEnd && slope < -1 && slope > -999999)
            {
                swap(ref x0, ref y0, ref xEnd, ref yEnd);
                deltaX = xEnd - x0;
                deltaY = yEnd - y0;
                deltaY = -deltaY;
                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = x0; i < xEnd; i++)
                {

                    if (p < 0)
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (Y), center.Y - (++X), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (--Y), center.Y - (++X), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

            // Fourth Ocstant
            else if (x0 > xEnd && slope <= 0 && slope >= -1)
            {
                deltaX = -deltaX;
                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = xEnd; i < x0; i++)
                {

                    if (p < 0)
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (--X), center.Y - (Y), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (--X), center.Y - (++Y), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }

                }
            }

            // Fifth Ocstant
            else if (x0 > xEnd && slope > 0 && slope <= 1)
            {
                deltaX = -deltaX; deltaY = -deltaY;
                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = xEnd; i < x0; i++)
                {

                    if (p < 0)
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (--X), center.Y - (Y), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (--X), center.Y - (--Y), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

            // Sixth Ocstant
            else if (y0 > yEnd && slope > 1 && slope < 999999)
            {
                // Swap x1 and y1 ,,, Swap x2 and y2
                swap(ref x0, ref y0, ref xEnd, ref yEnd);

                deltaX = xEnd - x0;
                deltaY = yEnd - y0;
                deltaX = -deltaX; deltaY = -deltaY;

                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = xEnd; i < x0; i++)
                {

                    if (p < 0)
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (Y), center.Y - (--X), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (--Y), center.Y - (--X), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

            // Seventh Ocstant
            else if (y0 > yEnd && slope < -1 && slope < 999999)
            {
                swap(ref x0, ref y0, ref xEnd, ref yEnd);

                deltaX = xEnd - x0;
                deltaY = yEnd - y0;
                deltaX = -deltaX;
                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = xEnd; i < x0; i++)
                {

                    if (p < 0)
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (Y), center.Y - (--X), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (++Y), center.Y - (--X), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

            // Eighth Ocstant
            else if (x0 < xEnd && slope <= 0 && slope >= -1)
            {
                deltaY = -deltaY;
                p = (2 * deltaY) - deltaX;
                int X = x0, Y = y0;

                for (int i = x0; i < xEnd; i++)
                {

                    if (p < 0)
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (++X), center.Y - (Y), 2, 2);
                        p += (2 * deltaY);
                    }
                    else
                    {
                        output.AppendText(X.ToString("0.00") + "    " + Y.ToString("0.00") + "    " + "(" + X + "," + Y + ")" + Environment.NewLine);
                        g.FillRectangle(brush, center.X + (++X), center.Y - (--Y), 2, 2);
                        p += (2 * deltaY) - (2 * deltaX);
                    }
                }
            }

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int xc = Convert.ToInt32(textBox9.Text);
            int yx = Convert.ToInt32(textBox10.Text);
            double r = Convert.ToDouble(textBox11.Text);
            Point center = new Point(xc, yx);
            Circle(center, r);
        }
        private void Circle(Point center, double r)
        {
            var aBrush = Brushes.Navy;
            var g = panel1.CreateGraphics();
            for (int theta = 0; theta <= 360; theta++)
            {
                double x, y;
                x = center.X + r * Math.Cos(theta);
                y = center.Y + r * Math.Sin(theta);
                output.AppendText(x.ToString("0.00") + "    " + y.ToString("0.00") + "    " + "(" + Math.Round(x) + "," + Math.Round(y) + ")" + Environment.NewLine);
                drawPoint((int)x, (int)y);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            int xc = Convert.ToInt32(textBox12.Text);
            int yx = Convert.ToInt32(textBox13.Text);
            double rx = Convert.ToDouble(textBox14.Text);
            double ry = Convert.ToDouble(textBox15.Text);
            Point center = new Point(xc, yx);
            Ellipse(center, rx,ry);
        }
        private void Ellipse(Point center, double rx, double ry)
        { 
            double Rx2 = rx * ry;
            double Ry2 = rx * ry;
            double c1 = 2 * Rx2;
            double c2 = 2 * Ry2;
            double p;
            double x = 0;
            double y = ry;
            double px = 0;
            double py = c1 * y;
            ellipsePoints(center, x, y);
            p = Math.Round(Ry2 - (Rx2 * ry) + (0.25 * Rx2));
            while (px < py)
            {
                x++;
                px += c2;
                if (p < 0)
                    p += Ry2 + px;
                else
                {
                    y--;
                    py -= c1;
                    p += Ry2 + px - py;
                }
                ellipsePoints(center, x, y);
            }
            p = Math.Round(Ry2 * (x + 0.5) * (x + 0.5) + Rx2 * (y - 1) * (y - 1) - Rx2 * Ry2);
            while (y > 0)
            {
                y--;
                py -= c1;
                if (p > 0)
                    p += Rx2 - py;
                else
                {
                    x++;
                    px += c2;
                    p += Rx2 - py + px;
                }
                ellipsePoints(center, x, y);
            }
        }

        private void ellipsePoints(Point center, double x, double y)
        {
            var aBrush = Brushes.Navy;
            var g = panel1.CreateGraphics();
            output.AppendText("  X        Y       (X,Y)" + Environment.NewLine);
            output.AppendText(x.ToString("0.00") + "    " + y.ToString("0.00") + "    " + "(" + Math.Round(x) + "," + Math.Round(y) + ")" + Environment.NewLine);
            drawPoint((int)(center.X + x), (int)(center.Y + y));
            output.AppendText(x.ToString("0.00") + "    " + y.ToString("0.00") + "    " + "(" + Math.Round(x) + "," + Math.Round(y) + ")" + Environment.NewLine);
            drawPoint((int)(center.X - x), (int)(center.Y + y));
            output.AppendText(x.ToString("0.00") + "    " + y.ToString("0.00") + "    " + "(" + Math.Round(x) + "," + Math.Round(y) + ")" + Environment.NewLine);
            drawPoint((int)(center.X + x), (int)(center.Y - y));
            output.AppendText(x.ToString("0.00") + "    " + y.ToString("0.00") + "    " + "(" + Math.Round(x) + "," + Math.Round(y) + ")" + Environment.NewLine);
            drawPoint((int)(center.X - x), (int)(center.Y - y));
        }

        public Point center = new Point(230, 230);
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
                Pen pen = new Pen(Color.Black);
                var g = panel1.CreateGraphics();

                Point p1 = new Point(center.X + 400, center.Y);
                Point p2 = new Point(center.X - 400, center.Y);
                Point p3 = new Point(center.X, center.Y + 400);
                Point p4 = new Point(center.X, center.Y - 400);
            
                g.DrawLine(pen, p1, p2);
                g.DrawLine(pen, p3, p4);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = new Point(x3, y3);
            Point p4 = new Point(x4, y4);

            panel1.Refresh();

            ddaline(p1, p2);
            ddaline(p2, p3);
            ddaline(p3, p4);
            ddaline(p4, p1);
        }

        public void Transiation(ref int X, ref int Y, int X_Translation, int Y_Translation)
        {
            X += X_Translation;
            Y += Y_Translation;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            int X_Translation = Convert.ToInt32(textBox32.Text);
            int Y_Translation = Convert.ToInt32(textBox33.Text);

            Transiation(ref x1, ref y1, X_Translation, Y_Translation);
            Transiation(ref x2, ref y2, X_Translation, Y_Translation);
            Transiation(ref x3, ref y3, X_Translation, Y_Translation);
            Transiation(ref x4, ref y4, X_Translation, Y_Translation);

            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = new Point(x3, y3);
            Point p4 = new Point(x4, y4);

            ddaline(p1, p2);
            ddaline(p2, p3);
            ddaline(p3, p4);
            ddaline(p4, p1);
        }

        public double Cos(double Angel)
        {
            double angel = Convert.ToInt32(Math.Cos(Math.PI * Angel / 180) * 100);
            angel = Convert.ToDouble(angel / 100);
            return angel;
        }

        public void Rotation(ref int X, ref int Y, double Angel)
        {

            double x, y, con, sin;
            con = Cos(Angel);
            sin = Math.Sin(Math.PI * Convert.ToDouble(Angel / 180));

            x = (X * con) + (Y * sin);
            y = (X * sin) - (Y * con);

            X = Convert.ToInt32(Math.Round(x));
            Y = Convert.ToInt32(Math.Round(y));

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            int Angel = Convert.ToInt32(textBox35.Text);

            Rotation(ref x1, ref y1, Angel);
            Rotation(ref x2, ref y2, Angel);
            Rotation(ref x3, ref y3, Angel);
            Rotation(ref x4, ref y4, Angel);

            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = new Point(x3, y3);
            Point p4 = new Point(x4, y4);

            ddaline(p1, p2);
            ddaline(p2, p3);
            ddaline(p3, p4);
            ddaline(p4, p1);
        }

        public void Shearing_X(ref int X, ref int Y, int Y_Shearing)
        {
            int x, y;
            x = X + Y_Shearing * Y;
            y = Y;
            X = x;
            Y = y;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            int Shearing_shx = Convert.ToInt32(textBox39.Text);

            Shearing_X(ref x1, ref y1, Shearing_shx);
            Shearing_X(ref x2, ref y2, Shearing_shx);
            Shearing_X(ref x3, ref y3, Shearing_shx);
            Shearing_X(ref x4, ref y4, Shearing_shx);

            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = new Point(x3, y3);
            Point p4 = new Point(x4, y4);

            ddaline(p1, p2);
            ddaline(p2, p3);
            ddaline(p3, p4);
            ddaline(p4, p1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            Point p1 = new Point(-x1, y1);
            Point p2 = new Point(-x2, y2);
            Point p3 = new Point(-x3, y3);
            Point p4 = new Point(-x4, y4);

            ddaline(p1, p2);
            ddaline(p2, p3);
            ddaline(p3, p4);
            ddaline(p4, p1);
        }

        public void Shearing_Y(ref int X, ref int Y, int X_Shearing)
        {
            int x, y;
            x = X * 1;
            y = X * X_Shearing + Y;
            X = x;
            Y = y;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = Convert.ToInt32(textBox30.Text);

            int Shearing_shy = Convert.ToInt32(textBox40.Text);

            Shearing_Y(ref x1, ref y1, Shearing_shy);
            Shearing_Y(ref x2, ref y2, Shearing_shy);
            Shearing_Y(ref x3, ref y3, Shearing_shy);
            Shearing_Y(ref x4, ref y4, Shearing_shy);

            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = new Point(x3, y3);
            Point p4 = new Point(x4, y4);

            ddaline(p1, p2);
            ddaline(p2, p3);
            ddaline(p3, p4);
            ddaline(p4, p1);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = -Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = -Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = -Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = -Convert.ToInt32(textBox30.Text);

            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = new Point(x3, y3);
            Point p4 = new Point(x4, y4);

            ddaline(p1, p2);
            ddaline(p2, p3);
            ddaline(p3, p4);
            ddaline(p4, p1);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = -Convert.ToInt32(textBox21.Text);

            int x2 = Convert.ToInt32(textBox23.Text);
            int y2 = -Convert.ToInt32(textBox24.Text);

            int x3 = Convert.ToInt32(textBox26.Text);
            int y3 = -Convert.ToInt32(textBox27.Text);

            int x4 = Convert.ToInt32(textBox29.Text);
            int y4 = -Convert.ToInt32(textBox30.Text);

            Point p1 = new Point(-x1, y1);
            Point p2 = new Point(-x2, y2);
            Point p3 = new Point(-x3, y3);
            Point p4 = new Point(-x4, y4);

            ddaline(p1, p2);
            ddaline(p2, p3);
            ddaline(p3, p4);
            ddaline(p4, p1);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            output.Clear();
            panel1.Refresh();
        }

        private void output_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
