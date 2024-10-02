using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOTServer
{
    public class JsonServer
    {
        private const int PORT = 12000;

        public void Start()
        {
            TcpListener server = new TcpListener(PORT);
            server.Start();
            Console.WriteLine($"Server started on port {PORT}");
            Console.WriteLine($"Accepting JSON responses");

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
            string command = reader.ReadLine();
            try
            {
                CommandPackage package = JsonSerializer.Deserialize<CommandPackage>(command);
                switch(package.Method)
                {
                    case "Random":
                        package.Result = Random(package.FirstNumber,package.SecondNumber);
                        break;
                    case "Add":
                        package.Result = Add(package.FirstNumber, package.SecondNumber);
                        break;
                    case "Subtract":
                        package.Result = Subtract(package.FirstNumber, package.SecondNumber);
                        break;

                }
                package.ErrorMessage = "All good";
                string response = JsonSerializer.Serialize(package);
                writer.WriteLine(response);
                writer.Flush();
            }
            catch (Exception ex)
            {
                CommandPackage package = new CommandPackage() { ErrorMessage = "Protocol was not upheld"};
                writer.WriteLine(JsonSerializer.Serialize(package));
                writer.Flush();
            }

        }
        private int Random(int n1, int n2)
        {
            Random random = new Random();
            return random.Next(n1, n2+1);
        }

        private int Add(int n1, int n2)
        {
            return n1 + n2;
        }

        private int Subtract(int n1, int n2)
        { 
            return n1 - n2; 
        }
    }
}
