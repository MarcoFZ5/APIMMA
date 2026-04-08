using APIMMA.Data;
using APIMMA.Services;
using Microsoft.EntityFrameworkCore;

namespace APIMMA.BackgroundJobs.Emails
{
    public class EmailJobs : IEmailJobs
    {
        private readonly IEmailService _email;

        public EmailJobs(IEmailService email)
        {
            _email = email;
        }

        public async Task sendConfirmationEmail(string to)
        {
            var message = $"Confirmation Email for the MMA COMMUNITY APP";
            var subject = "MMA COMMUNITY APP - Confirmation Email";

            var htmlContext = @"
                    <!DOCTYPE html>
                    <html lang=""es"">
                    <head>
                        <meta charset=""UTF-8"">
                        <title>Confirmación</title>
                    </head>
                    <body style=""margin:0; padding:0; background-color:#f4f4f4; font-family: Arial, sans-serif;"">
    
                        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#f4f4f4; padding:20px 0;"">
                            <tr>
                                <td align=""center"">
                
                                    <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#ffffff; border-radius:8px; overflow:hidden;"">
                    
                                        <tr>
                                            <td style=""background-color:#4CAF50; color:#ffffff; padding:20px; text-align:center; font-size:24px;"">
                                                Confirmación de Cuenta
                                            </td>
                                        </tr>
                    
                                        <tr>
                                            <td style=""padding:30px; color:#333333;"">
                                                <h2 style=""margin-top:0;"">Hola 👋</h2>
                            
                                                <p>
                                                    Gracias por registrarte. Estamos muy contentos de tenerte con nosotros.
                                                </p>
                            
                                                <p>
                                                    Este correo es una prueba de envío utilizando SendGrid para validar que todo funciona correctamente.
                                                </p>
                            
                                                <p>
                                                    Más adelante aquí podrás confirmar tu cuenta y empezar a usar la plataforma.
                                                </p>

                                                <p style=""margin-top:30px;"">
                                                    Si no realizaste esta solicitud, puedes ignorar este mensaje.
                                                </p>
                                            </td>
                                        </tr>
                    
                                        <tr>
                                            <td style=""background-color:#f0f0f0; padding:20px; text-align:center; font-size:12px; color:#777777;"">
                                                © 2026 Tu Proyecto - Todos los derechos reservados
                                            </td>
                                        </tr>
                    
                                    </table>
                
                                </td>
                            </tr>
                        </table>

                    </body>
                    </html>
                    ";

            await _email.SendEmailAsync(to, subject, message, htmlContext);
        }
    }
}
