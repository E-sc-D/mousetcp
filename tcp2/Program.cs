using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;

namespace tcp2
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        static void Main(string[] args)
        {

            double x = 0, y = 0;
            double xi = 0,yi = 0;
            try
            {

                IPAddress ipAd = IPAddress.Parse("192.168.43.63");
                // use local m/c IP address, and 
                // use the same in the client
                bool broke = true;
                /* Initializes the Listener */
                TcpListener myList = new TcpListener(ipAd, 8001);
                string bypass = "";
                /* Start Listeneting at the specified port */

                myList.Start();
                Console.WriteLine("The server is running at port 8001...");
                Console.WriteLine("The local End point is  :" + myList.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");
                int k;
                byte[] b = new byte[50];
                Socket s = myList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
                while (broke == true)
                {
                    s = myList.AcceptSocket();
                    k = s.Receive(b);
                    for (int i = 0; i < k; i++)
                    {
                        bypass += Convert.ToChar(b[i]);
                    }
                    filter(bypass);
                    Console.WriteLine(bypass);                  
                    SetCursorPos(Convert.ToInt32(Math.Round(xi)), Convert.ToInt32(Math.Round(yi)));                  
                    bypass = "";
                }


                /*ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes("The string was recieved by the server."));
                Console.WriteLine("\nSent Acknowledgement");
                /* clean up */
                s.Close();
                myList.Stop();
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }

            void filter(string r)
            {
                string[] val = r.Split(' ');
                xi += Convert.ToDouble(val[0])/-43;
                yi +=Convert.ToDouble(val[1])/43;

                if (Convert.ToInt32(val[2]) == 1)
                {
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
                }


                if (Convert.ToInt32(val[3]) == 1)
                {
                    mouse_event((int)(MouseEventFlags.RIGHTDOWN), 0, 0, 0, 0);
                    mouse_event((int)(MouseEventFlags.RIGHTUP), 0, 0, 0, 0);
                }
            }


            /*double swicher(double ader,double actual)
            {
                      if ((Math.Sign(ader)!=Math.Sign(actual))&&(ader<-0.5||ader>0.5)&&(actual>2||actual<-2))           
                return -actual;
                return Math.Round(ader);
            }*/
    }        

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        public void leftClick()
        {           
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }

        public void rightClick()
        {            
            mouse_event((int)(MouseEventFlags.RIGHTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.RIGHTUP), 0, 0, 0, 0);
        }


    }
}
