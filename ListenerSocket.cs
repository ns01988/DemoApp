
using DemoApp.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;


namespace DemoApp
{
   static class  ListenerSocket
    {
       static IPEndPoint ep = new IPEndPoint(IPAddress.Loopback, 1234);
        static TcpListener listener = new TcpListener(ep);

        static public Person startservice()
        {
        

                listener.Start();


                // Run the loop continuously; this is the server.  
                while (true)
                {
                    const int bytesize = 1024 * 1024;

                    string message = null;
                    byte[] buffer = new byte[bytesize];

                    var sender = listener.AcceptTcpClient();
                    sender.GetStream().Read(buffer, 0, bytesize);

                    // Read the message and perform different actions  
                    message = cleanMessage(buffer);

                    // Save the data sent by the client;  
                    Person msg = JsonConvert.DeserializeObject<Person>(message); // Deserialize  

                    byte[] bytes = System.Text.Encoding.Unicode.GetBytes("Thank you for your message, " + msg.FName);
                    sender.GetStream().Write(bytes, 0, bytes.Length); // Send the response  
                    


                return msg;
           
            }

    }


        private static string cleanMessage(byte[] bytes)
        {
            string message = System.Text.Encoding.Unicode.GetString(bytes);

            string messageToPrint = null;
            foreach (var nullChar in message)
            {
                if (nullChar != '\0')
                {
                    messageToPrint += nullChar;
                }
            }
            return messageToPrint;
        }

        // Sends the message string using the bytes provided.  
        private static void sendMessage(byte[] bytes, TcpClient client)
        {
            client.GetStream()
                .Write(bytes, 0,
                bytes.Length); // Send the stream  
        }
    }

  
}  
