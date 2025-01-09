using Microsoft.AspNetCore.SignalR;
using Project.Core.DTO.ReservationsDTO;

namespace Project.Core.SignalR
{
    public class NotificationHub : Hub
    {
        public async Task JoinGroup(string connectionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connectionId);
        }
        public async Task NewReservationNotification(string connectionId, GetReservationDTO getReservationDTO)
        {
            await Clients.Group(connectionId).SendAsync("NewOrderNotification", getReservationDTO);       
        }
    }
}