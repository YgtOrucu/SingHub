using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using SignHub.Application.Contract.Persistence;
using SignHub.Application.MailSetting;

namespace SignHub.Persistence.Concreate;

public class MailService(IOptions<MailSettings> options) : IMailService
{
    private readonly MailSettings _mailSettings = options.Value;

    public async Task SendMail(string Name, string Surname, string toEmail)
    {
        var email = new MimeMessage();

        email.Sender = MailboxAddress.Parse(_mailSettings.SenderEmail);
        email.From.Add(new MailboxAddress("SignHub Music", _mailSettings.SenderEmail));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = "SignHub Dünyasına Hoş Geldin! 🎵";

        var builder = new BodyBuilder
        {
            HtmlBody = GetWelcomeEmailTemplate(Name, Surname)
        };

        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_mailSettings.SenderEmail, _mailSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

    private string GetWelcomeEmailTemplate(string name, string surname)
    {
        return $@"
        <!DOCTYPE html>
        <html lang=""tr"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>SignHub'a Hoş Geldin</title>
        </head>
        <body style=""margin: 0; padding: 0; background-color: #0b0914; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #ffffff;"">
            <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""background-color: #0b0914; padding: 40px 10px;"">
                <tr>
                    <td align=""center"">
                        <!-- Ana Kart Container -->
                        <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""max-width: 600px; background-color: #131124; border-radius: 16px; border: 1px solid #231f3d; overflow: hidden; box-shadow: 0 10px 30px rgba(0,0,0,0.5);"">
                            
                            <!-- Header / Logo -->
                            <tr>
                                <td align=""center"" style=""padding: 40px 0 20px 0; background: linear-gradient(135deg, #18152e 0%, #0b0914 100%);"">
                                    <h1 style=""margin: 0; font-size: 32px; font-weight: 800; letter-spacing: -1px; background: linear-gradient(90deg, #8b5cf6, #3b82f6); -webkit-background-clip: text; -webkit-text-fill-color: transparent; color: #ccb9f8;"">
                                        SignHub<span style=""color: #ccb9f8;"">.</span>
                                    </h1>
                                    <p style=""margin: 5px 0 0 0; font-size: 12px; color: #94a3b8; letter-spacing: 2px; text-transform: uppercase;"">Music Platform</p>
                                </td>
                            </tr>

                            <!-- Hero Banner / Müzik İkonu -->
                            <tr>
                                <td align=""center"" style=""padding: 10px 40px;"">
                                    <div style=""width: 80px; height: 80px; background: linear-gradient(135deg, #7c3aed 0%, #2563eb 100%); border-radius: 50%; display: table; text-align: center; box-shadow: 0 0 25px rgba(124, 58, 237, 0.4);"">
                                        <span style=""display: table-cell; vertical-align: middle; font-size: 36px; color: #ffffff;"">🎧</span>
                                    </div>
                                </td>
                            </tr>

                            <!-- İçerik Alanı -->
                            <tr>
                                <td style=""padding: 20px 40px 30px 40px; text-align: center;"">
                                    <h2 style=""margin: 0 0 15px 0; font-size: 24px; font-weight: 700; color: #ffffff;"">
                                        Aramıza Hoş Geldin, <span style=""color: #a78bfa;"">{name} {surname}</span>!
                                    </h2>
                                    <p style=""margin: 0 0 25px 0; font-size: 15px; line-height: 1.6; color: #cbd5e1;"">
                                        Müziğin ritmini hissetmeye hazır mısın? SignHub dünyasına adım atarak milyonlarca şarkıya, sana özel oluşturulmuş çalma listelerine ve yüksek kaliteli ses deneyimine erişim kazandın.
                                    </p>

                                    <!-- Öne Çıkan Özellikler Kutusu -->
                                    <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""background-color: #1c1836; border-radius: 12px; padding: 20px; margin-bottom: 30px; text-align: left;"">
                                        <tr>
                                            <td style=""padding: 8px 0; font-size: 14px; color: #e2e8f0;"">✨ <strong>Kişiselleştirilmiş Listeler:</strong> Tarzına uygun dinamik öneriler.</td>
                                        </tr>
                                        <tr>
                                            <td style=""padding: 8px 0; font-size: 14px; color: #e2e8f0;"">🚀 <strong>Kesintisiz Yayın:</strong> Yüksek çözünürlüklü ses kalitesi.</td>
                                        </tr>
                                        <tr>
                                            <td style=""padding: 8px 0; font-size: 14px; color: #e2e8f0;"">🔥 <strong>Trendler:</strong> En yeni albümler ve popüler parçalar anında yanında.</td>
                                        </tr>
                                    </table>

                                    <!-- CTA Butonu -->
                                    <a href=""https://signhub.com"" target=""_blank"" style=""display: inline-block; padding: 14px 36px; background: linear-gradient(90deg, #7c3aed 0%, #2563eb 100%); color: #ffffff; text-decoration: none; font-weight: 600; font-size: 16px; border-radius: 30px; box-shadow: 0 4px 15px rgba(37, 99, 235, 0.3);"">
                                        Müziği Keşfetmeye Başla
                                    </a>
                                </td>
                            </tr>

                            <!-- Footer -->
                            <tr>
                                <td style=""padding: 25px 40px; background-color: #0e0c1b; border-top: 1px solid #1f1b36; text-align: center;"">
                                    <p style=""margin: 0 0 10px 0; font-size: 13px; color: #64748b;"">
                                        Bu e-posta, SignHub üyeliğinize istinaden gönderilmiştir.
                                    </p>
                                    <p style=""margin: 0; font-size: 12px; color: #475569;"">
                                        &copy; 2026 SignHub Music Inc. Tüm hakları saklıdır.
                                    </p>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
        </body>
        </html>";
    }
}