namespace Project.Core.Interfaces.IServices.IOtherServices
{
    public interface IMailService
    {
        Task SendConfirmToken(string addressee, string token, string userId);
        Task SendPasswordResetToken(string addressee, string token, string userId);
        Task SendBookingConfirmation(string addressee);
    }
}