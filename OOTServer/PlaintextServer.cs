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
            writer.WriteLine("Hiya :D please input your command. You can choose between:");
            writer.WriteLine("\tRandom");
            writer.WriteLine("\tAdd");
            writer.WriteLine("\tSubtract");
            writer.Flush();
            string command = reader.ReadLine();
            switch (command)
            {
                case "Random":
                    writer.WriteLine("Random ain't implemented yet, but I did recognize it");
                    writer.Flush();
                    break;
                case "Add":
                    writer.WriteLine("Add ain't implemented yet, but I did recognize it");
                    writer.Flush();
                    break;
                case "Subtract":
                    writer.WriteLine("Subtract ain't implemented yet, but I did recognize it");
                    writer.Flush();
                    break;
                default:
                    writer.WriteLine($"Sorry, didn't recognize that. What I got was: {command}");
                    writer.Flush();
                    break;
            }
        }
    }
}
