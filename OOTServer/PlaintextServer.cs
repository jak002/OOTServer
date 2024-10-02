using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OOTServer
{
    public class PlaintextServer
    {
        private const int PORT = 7;

        public void Start()
        {
            TcpListener server = new TcpListener(PORT);
            server.Start();
            Console.WriteLine($"Server started on port {PORT}");
            Console.WriteLine($"Accepting plaintext responses");

            while (true)
            {
                TcpClient socket = server.AcceptTcpClient();
                Task.Run(() =>
                {
                    TcpClient tempsocket = socket;
                    ParseCommand(tempsocket);
                }
                );
            }
        }

        public void ParseCommand(TcpClient socket)
        {
            Console.WriteLine($"New client: {socket.Client.RemoteEndPoint}");
            StreamReader reader = new(socket.GetStream());
            StreamWriter writer = new(socket.GetStream());
            //writer.WriteLine("Hiya :D please input your command. You can choose between:");
            //writer.WriteLine("\tRandom");
            //writer.WriteLine("\tAdd");
            //writer.WriteLine("\tSubtract");
            //writer.Flush();
            string command = reader.ReadLine();
            switch (command)
            {
                case "Random":
                    writer.WriteLine("Random recognized");
                    writer.Flush();
                    reader.DiscardBufferedData();
                    break;
                case "Add":
                    writer.WriteLine("Add recognized");
                    writer.Flush();
                    reader.DiscardBufferedData();
                    break;
                case "Subtract":
                    writer.WriteLine("Subtract recognized");
                    writer.Flush();
                    reader.DiscardBufferedData();
                    break;
                default:
                    writer.WriteLine($"Not recognized");
                    writer.Flush();
                    reader.DiscardBufferedData();
                    break;
            }
            //reader = new(socket.GetStream());
            //writer = new(socket.GetStream());
            string numbers = reader.ReadLine();
            Console.WriteLine($"Numbers received: {numbers}");
        }
    }
}
