using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web;

namespace Ex3.Models
{
    class Client
    {
            private TcpClient tcpClient;
            private static Client instance = null;
            private NetworkStream stream;
            private BinaryWriter writer;
            private bool isConnected;
            private Client() { }

            public static Client getInstance()
            {

                if (instance == null)
                {
                    instance = new Client();
                  
            }
                return instance;
            }

            public void Connect(string ip, int port)
            {
            
            if (!isConnected)
            {
                tcpClient = new TcpClient();
                Task task = new Task(() =>
                {
                    while (!tcpClient.Connected)
                    {
                        try
                        {
                            //  Console.WriteLine("trying to connect..");
                            tcpClient.Connect(ip, port);
                            isConnected = true;
                        }
                        catch (SocketException)
                        {
                            continue;

                        }
                    }
                    //  Console.WriteLine("new Connection");
                    stream = tcpClient.GetStream();
                    writer = new BinaryWriter(stream);
                });
                task.Start();
                task.Wait();
            }


            }

            public void Disconnect()
            {
                tcpClient.Dispose();
            }


            public void Write(string msg)
            {


            Random r = new Random();
                if (tcpClient != null && tcpClient.Connected)
                {
                    writer.Write(System.Text.Encoding.ASCII.GetBytes(msg));
                }
               string test = Client.getInstance().Read();
               string[] t = test.Split('\'');
               string[] num = t[1].Split('.');

           double num1 = Convert.ToDouble(num[1])%100;
           
            if (msg.Contains("latitude")){
                InfoModel.Instance.Lat = (Convert.ToDouble(t[1]) +num1 +90) * (800 / 180);
            }
            if (msg.Contains("longitude"))
            {
                InfoModel.Instance.Lon = (Convert.ToDouble(t[1]) + num1 + 180) * (800 /360);
            }
            
            }
            public string Read()
            {

                //check the pilot response
                Byte[] data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //     Console.WriteLine("Received: {0}", responseData);
                return responseData;
            }
            public bool isConnect()
            {
                return tcpClient.Connected;
            }
            ~Client()
            {
                tcpClient.Close();
            }
        }
}