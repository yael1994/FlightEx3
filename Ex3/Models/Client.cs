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

                tcpClient = new TcpClient();
                Task task = new Task(() =>
                {
                    while (!tcpClient.Connected)
                    {
                        try
                        {
                            //  Console.WriteLine("trying to connect..");
                            tcpClient.Connect(ip, port);

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

            public void Disconnect()
            {
                tcpClient.Dispose();
            }


            public void Write()
            {

                string send = "get /position/longitude-deg\r\n";
                if (tcpClient != null && tcpClient.Connected)
                {
                    writer.Write(System.Text.Encoding.ASCII.GetBytes(send));
                }
                string test = Client.getInstance().Read();
                string[] t = test.Split('\'');
                FlightModel.Instance.Lon = Convert.ToDouble(t[1]) + 100;
                string send2 = "get /position/latitude-deg\r\n";
                // Console.WriteLine("Sends: " + send);
                //send the message to the server
                if (tcpClient != null && tcpClient.Connected)
                {
                    writer.Write(System.Text.Encoding.ASCII.GetBytes(send2));

                }
                string test1 = Client.getInstance().Read();
                string[] t1 = test1.Split('\'');
                FlightModel.Instance.Lon = Convert.ToDouble(t1[1]) + 50;



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