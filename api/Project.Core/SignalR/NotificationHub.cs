using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Project.Core.DTO.ReservationsDTO;

namespace Project.Core.SignalR
{
     [Authorize(Policy = "Users")] 
    public class NotificationHub : Hub
    {
        public async Task JoinGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "admin");
        }
        public async Task NewReservationNotification(string connectionId, GetReservationDTO getReservationDTO)
        {
            await Clients.Group(connectionId).SendAsync("NewOrderNotification", getReservationDTO);       
        }
    }
}