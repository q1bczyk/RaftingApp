namespace Project.Core.Interfaces.IServices.IOtherServices
{
    public interface IMailService
    {
        Task SendConfirmToken(string addressee, string token, string requestUrl);
        Task SendPasswordResetToken(string addressee, string token, string requestUrl);
        Task SendBookingConfirmation(string addressee);
    }
}