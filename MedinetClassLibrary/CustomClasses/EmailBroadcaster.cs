using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace MedinetClassLibrary.Classes
{
    public class EmailBroadcaster
    {
        public static void SendEmail(string subject, string body, string email)
        {

            MailMessage message = new MailMessage("notificaciones@hrmedinet.com", email, subject, body);//informacion del mensaje que se va a enviar

            /*SmtpClient Permite a las aplicaciones enviar mensajes de correo electrónico mediante el protocolo SMTP (Protocolo simple de transferencia de correo).*/
            SmtpClient client = new SmtpClient("mail.hrmedinet.com", 25);//Inicializa una nueva instancia de la clase SmtpClient que envía correo electrónico mediante el servidor y el puerto SMTP especificados.

            /*NetworkCredential Proporciona credenciales para esquemas de autenticación basados en contraseña como la autenticación básica, implícita, NTLM y Kerberos.*/
            client.Credentials = new System.Net.NetworkCredential("notificaciones@hrmedinet.com", "N0t1.M3d1n3t*.");//Inicializa una nueva instancia de la clase NetworkCredential con el nombre de usuario y la contraseña especificados.

            client.EnableSsl = false;//Especifique si el SmtpClient utiliza Secure Sockets Layer (SSL) para cifrar la conexión.

            try
            {
                client.Send(message);//envio del correo
            }
            catch (Exception ee)//en caso de algun error
            {
                Console.WriteLine(ee.Message);
            }


        }

        public static void SendEmail(string subject, string body, string email, bool isHtml)
        {
            var message = new MailMessage("notificaciones@hrmedinet.com", email)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            NetworkCredential SMTPUserInfo = new NetworkCredential("notificaciones@hrmedinet.com", "N0t1.M3d1n3t*.");
            var client = new SmtpClient("mail.hrmedinet.com");
            client.UseDefaultCredentials = false;
            client.Credentials = SMTPUserInfo;
            //client.EnableSsl = false;
            client.Send(message);
        }
    }
}


