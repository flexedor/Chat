using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chat
{
    class Program
    {
        static void Main(string[] args)
        {
            
            start();
        }
        static void start()
        {
            Console.WriteLine("inter get port ");
            int getPort =Convert.ToInt32( Console.ReadLine());
            get(getPort);
            Console.WriteLine("inter ip");
            String ip = Console.ReadLine();
            Console.WriteLine("inter port");
            int port = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("inter text");
            String text = Console.ReadLine();
            send(text, port, ip);
            start();
        }
        static void send(String text, int port,String ipAdress)
        {
            byte[] bytCommand = new byte[] { };
            UdpClient udpClient = new UdpClient();
            udpClient.Connect(IPAddress.Parse(ipAdress), port);
            bytCommand = Encoding.ASCII.GetBytes(text);
            udpClient.Send(bytCommand, bytCommand.Length);

        }
        static void get(int port)
        {
          //  CheckForIllegalCrossThreadCalls = false;

            byte[] bytCommand = new byte[] { };
            var receivingUdpClient = new System.Net.Sockets.UdpClient(port);


            Thread th = new Thread(() => {
                IPEndPoint iep = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);
                while (true)
                {
                    byte[] receiveBytes = receivingUdpClient.Receive(ref iep);

                    string str = System.Text.Encoding.ASCII.GetString(receiveBytes);

                    String textOutput = Convert.ToString(str.Replace("#", "").Replace("(", "").Replace(")", "").Split(',')[0]);
                    Console.WriteLine(textOutput);
                    //     int y = Convert.ToInt32(str.Replace("#", "").Replace("(", "").Replace(")", "").Split(',')[1]);

                 //   pictureBox1.CreateGraphics().DrawEllipse(new Pen(Brushes.Black, 4), x - 2, y - 2, 4, 4);

                }

            });
            th.IsBackground = true;
            th.Start();

        }

    }
}
