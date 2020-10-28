using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperWebSocket;

namespace SKServer
{
    class Program
    {
        private static WebSocketServer server;//objeto, nos permitira utilizar las clases de web server
        static void Main(string[] args)
        {
            server = new WebSocketServer();
            int puerto = 8088;//si hay error lo checas con las aplicaciones que usen puertos
            server.Setup(puerto);//la más comun se inicializa con el puerto

            server.NewSessionConnected += server_NewsessionConect; //agregar o ir acumulando a server, como no sabemos cuanntos clientes se van a conectar, se van a ir agregando
            server.NewMessageReceived += server_NuevoMensajeRecibido;// acumulador de mensajes, en caso de haber muchos mensajes los va a ir acumulando
            server.NewDataReceived += server_DatosRecibidos;
            server.SessionClosed += serve_CerrarSesion;

            server.Start();
            Console.WriteLine("El servidor esta en el puerto " + Convert.ToString(puerto)+ " Presiona una tecla para continuar para continuar");
            
            Console.ReadKey();
            server.Stop();

        }
        //Metodo para cerrar sesion
        private static void serve_CerrarSesion(WebSocketSession session, CloseReason value)// Razon para cerrar la sesion valor espera un valor
        {
            Console.WriteLine("Se cerro la sesión");
        }

        //metodo de datos recibidos
        private static void server_DatosRecibidos(WebSocketSession session, byte[] value)//CRea un arreglo de bytes, representa un entero de 8bits sin signo
        {
            Console.WriteLine("Se recibieron los datos");
        }

        //Metodo de  Nuevo Mensaje Recibido
        private static void server_NuevoMensajeRecibido(WebSocketSession session, string value)
        {
            Console.WriteLine("Se recibio el siguiente mensaje "+ value);

            if(value=="Hola servidor")
            {
                session.Send("Hola cliente :D ");   
            }
        }

        //metodo de nueva sesion conectada
        private static void server_NewsessionConect(WebSocketSession session)
        {
            Console.WriteLine("Nueva Sesión iniciada");
        }
    }
}
