namespace SingHub.Application.Contract.Persistence;

public interface IMailService
{
    Task SendMail(string Name, string Surname, string toEmail);
    Task SendForgotPasswordCodeAsync(string Email, string resetCode);
    Task SuccessPasswordChangedMessageAsync(string Name, string Surname, string Email);
}
