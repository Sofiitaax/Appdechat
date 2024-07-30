using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace App_de_chat
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Uso: dotnet run <puerto>");
                return;
            }

            if (!int.TryParse(args[0], out int port))
            {
                Console.WriteLine("Puerto inválido.");
                return;
            }

            // Inicia y empieza el servidor
            Server server = new Server(port);
            server.Start();

            // Bucle para enviar mensajes
            while (true)
            {
                try
                {
                    Console.Write("Ingrese puerto de destino: ");
                    string portInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(portInput)) continue;

                    if (!int.TryParse(portInput, out int destinationPort))
                    {
                        Console.WriteLine("Puerto inválido.");
                        continue;
                    }

                    Console.Write("Escriba mensaje: ");
                    string message = Console.ReadLine();
                    if (string.IsNullOrEmpty(message)) continue;

                    // Envia mensaje al puerto destino
                    Client client = new Client(destinationPort);
                    client.SendMessage(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }

    class Server
    {
        private TcpListener Listener;
        private bool Running;

        public Server(int port)
        {
            // Inicia el listener del servidor
            Listener = new TcpListener(IPAddress.Any, port);
            Running = true;
        }

        public void Start()
        {
            // Empieza la escucha
            Listener.Start();
            Console.WriteLine($"Servidor iniciado en el puerto {Listener.LocalEndpoint}");

            Thread listenerThread = new Thread(() =>
            {
                while (Running)
                {
                    try
                    {
                        // Acepta nuevas conexiones
                        TcpClient client = Listener.AcceptTcpClient();
                        NetworkStream stream = client.GetStream();
                        byte[] buffer = new byte[1024];
                        int byteCount = stream.Read(buffer, 0, buffer.Length);
                        if (byteCount > 0)
                        {
                            // Lee y muestra el mensaje recibido
                            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, byteCount);
                            Console.WriteLine($"\nNuevo mensaje recibido: {receivedMessage}");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (Running)
                        {
                            Console.WriteLine("Error en el servidor");
                        }
                    }
                }
            })
            {
                IsBackground = true
            };

            // Inicia el hilo del listener
            listenerThread.Start();
        }
    }

    class Client
    {
        private int _port;

        public Client(int port)
        {
            _port = port;
        }

        public void SendMessage(string message)
        {
            try
            {
                // Conecta al servidor y envia el mensaje
                using (TcpClient clientSocket = new TcpClient())
                {
                    clientSocket.Connect(IPAddress.Loopback, _port);
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    clientSocket.GetStream().Write(buffer, 0, buffer.Length);
                    Console.WriteLine($"Mensaje enviado");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el mensaje: " + ex.Message);
            }
        }
    }
}
