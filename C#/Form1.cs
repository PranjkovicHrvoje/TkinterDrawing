using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace LV5_C
{
    public partial class Form1 : Form
    {
        int port;
        string message;
        TcpClient client;
        NetworkStream stream;
        int byteCount;
        byte[] sendData;
        Random rnd = new Random();
        string[] colors =
        {
            "black",
            "blue",
            "white",
            "red",
            "green",
            "yellow",
            "orange",
            "purple"
        };
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if(!int.TryParse(HostportTextbox.Text, out port))
            {
                MessageBox.Show("Port number not valid");
            }
            try
            {
                client = new TcpClient(HostnameTextbox.Text, port);
                //MessageBox.Show("Connection made with server");
                StatusPanel.BackColor = Color.Green;
            }
            catch(System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Connection failed");
            }


        }

        private void CloseConnectButton_Click(object sender, EventArgs e)
        {
            //stream.Close();
            client.Close();
            StatusPanel.BackColor = Color.Red;

        }

        private void LRandomButton_Click(object sender, EventArgs e)
        {
            L1X.Text = (rnd.Next(0, 800)).ToString();
            L1Y.Text = (rnd.Next(0, 600)).ToString();
            L2X.Text = (rnd.Next(0, 800)).ToString();
            L2Y.Text = (rnd.Next(0, 600)).ToString();
            LBojaTextbox.Text = colors[rnd.Next(0, colors.Length)];
        }
        private void LinijaSend_Click(object sender, EventArgs e)
        {
            try
            {
                message = "Line " + LBojaTextbox.Text + " " + L1X.Text + " " + L1Y.Text + " " + L2X.Text + " " + L2Y.Text;
                byteCount = Encoding.ASCII.GetByteCount(message);
                sendData = new byte[byteCount];
                sendData = Encoding.ASCII.GetBytes(message);
                stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
            }
            catch(System.NullReferenceException)
            {
                MessageBox.Show("Error while sending a message");
            }
        }

        private void TRandomButton_Click(object sender, EventArgs e)
        {
            T1X.Text = (rnd.Next(0, 800)).ToString();
            T1Y.Text = (rnd.Next(0, 600)).ToString();
            T2X.Text = (rnd.Next(0, 800)).ToString();
            T2Y.Text = (rnd.Next(0, 600)).ToString();
            T3X.Text = (rnd.Next(0, 800)).ToString();
            T3Y.Text = (rnd.Next(0, 600)).ToString();
            TBojaTextbox.Text = colors[rnd.Next(0, colors.Length)];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                message = "Triangle " + TBojaTextbox.Text + " " + T1X.Text + " " + T1Y.Text + " " + T2X.Text + " " + T2Y.Text + " " + T3X.Text + " " + T3Y.Text;
                byteCount = Encoding.ASCII.GetByteCount(message);
                sendData = new byte[byteCount];
                sendData = Encoding.ASCII.GetBytes(message);
                stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Error while sending a message");
            }
        }

        private void PravokutnikRandomButton_Click(object sender, EventArgs e)
        {
            int pX = (rnd.Next(0, 800));
            PravokutnikX.Text = pX.ToString();
            int pY = (rnd.Next(0, 600));
            PravokutnikY.Text = pY.ToString();
            Visina.Text = (rnd.Next(0, (800 - pX))).ToString();
            Sirina.Text = (rnd.Next(0, (600 - pY))).ToString();
            PravokutnikBoja.Text = colors[rnd.Next(0, colors.Length)];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                message = "Rectangle " + PravokutnikBoja.Text + " " + PravokutnikX.Text + " " + PravokutnikY.Text + " " + Sirina.Text + " " + Visina.Text;
                byteCount = Encoding.ASCII.GetByteCount(message);
                sendData = new byte[byteCount];
                sendData = Encoding.ASCII.GetBytes(message);
                stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Error while sending a message");
            }
        }

        private void KruznicaRandom_Click(object sender, EventArgs e)
        {
            int kX = (rnd.Next(0, 800));
            KruznicaX.Text = kX.ToString();
            int kY = (rnd.Next(0, 600));
            KruznicaY.Text = kY.ToString();
            int kR = rnd.Next(0, Math.Min(Math.Min(kX, kY), Math.Min(800 - kX, 600 - kY)));
            KruznicaRadius.Text = kR.ToString();
            KruznicaBoja.Text = colors[rnd.Next(0, colors.Length)];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                message = "Circle " + KruznicaBoja.Text + " " + KruznicaX.Text + " " + KruznicaY.Text + " " + KruznicaRadius.Text;
                byteCount = Encoding.ASCII.GetByteCount(message);
                sendData = new byte[byteCount];
                sendData = Encoding.ASCII.GetBytes(message);
                stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Error while sending a message");
            }
        }

        private void ElypseRandom_Click(object sender, EventArgs e)
        {
            int eX = (rnd.Next(0, 800));
            EllipseX.Text = eX.ToString();
            int eY = (rnd.Next(0, 600));
            EllipseY.Text = eY.ToString();
            int eR1 = rnd.Next(0, Math.Min(Math.Min(eX, eY), Math.Min(800 - eX, 600 - eY)));
            EllipseR1.Text = eR1.ToString();
            int eR2 = rnd.Next(0, Math.Min(Math.Min(eX, eY), Math.Min(800 - eX, 600 - eY)));
            EllipseR2.Text = eR2.ToString();
            EllipseColor.Text = colors[rnd.Next(0, colors.Length)];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                message = "Ellipse " + EllipseColor.Text + " " + EllipseX.Text + " " + EllipseY.Text + " " + EllipseR1.Text + " " + EllipseR2.Text;
                byteCount = Encoding.ASCII.GetByteCount(message);
                sendData = new byte[byteCount];
                sendData = Encoding.ASCII.GetBytes(message);
                stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Error while sending a message");
            }
        }

        private void polygonRandomButton_Click(object sender, EventArgs e)
        {
            int n = rnd.Next(4, 10);
            int avgX = 0;
            int avgY = 0;
            List<Point> polyDots = new List<Point>();
            
            double CalculateAngle(Point p1, Point p2)
            {
                double dx = p1.X - p2.X;
                double dy = p1.Y - p2.Y;
                return Math.Atan2(dy, dx);
            }
            
            polygonList.Items.Clear();
            for(int i = 0; i < n; i++)
            {
                Point p = new Point();
                int x = rnd.Next(0, 800);
                p.X = x;
                avgX += x;
                int y = rnd.Next(0, 600);
                p.Y = y;
                avgY += y;
                polyDots.Add(p);
            }
            avgX /= n;
            avgY /= n;
            Point refPoint = new Point();
            refPoint.X = avgX;
            refPoint.Y = avgY;
            polyDots.Sort((p1, p2) => CalculateAngle(p1, refPoint).CompareTo(CalculateAngle(p2, refPoint)));
            for (int i = 0; i < n; i++)
            {
                polygonList.Items.Add($"{polyDots[i].X} {polyDots[i].Y}");
            }
            polygonColor.Text = colors[rnd.Next(0, colors.Length)];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            polygonList.Items.Add($"{polygonX.Text} {polygonY.Text}");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                message = "Polygon " + polygonColor.Text;
                for (int i = 0; i < polygonList.Items.Count; i++)
                {
                    message = message + " " + polygonList.Items[i].ToString();
                }
                byteCount = Encoding.ASCII.GetByteCount(message);
                sendData = new byte[byteCount];
                sendData = Encoding.ASCII.GetBytes(message);
                stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Error while sending a message");
            }
        }
        class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private void ConnectionTab_Click(object sender, EventArgs e)
        {

        }
        private void HostportTextbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
