using EMS.DB.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BlazorServerSignalRApp.Server.SignalRHubs
{
    public class ChatHub : Hub
    {
        public async Task EventAddUpdate(Event events)
        {
            await Clients.All.SendAsync("EventAddUpdate", events);
        }
        public async Task InquiryAddUpdate(Inquiry inquirys)
        {
            await Clients.All.SendAsync("InquiryAddUpdate", inquirys);
        }
        public async Task PaymentAddUpdate(Payment Payments)
        {
            await Clients.All.SendAsync("PaymentAddUpdate", Payments);
        }
        public async Task WorkAddUpdate(EventStaffWork EventStaffWorks)
        {
            await Clients.All.SendAsync("WorkAddUpdate", EventStaffWorks);
        }
        public async Task CategoruAddUpdate(EventCategory eventCategory)
        {
            await Clients.All.SendAsync("CategoruAddUpdate", eventCategory);
        }
        public async Task ServiceAddUpdate(CategoryService categoryService)
        {
            await Clients.All.SendAsync("ServiceAddUpdate", categoryService);
        }
        public async Task staffAddUpdate(Staff staffs)
        {
            await Clients.All.SendAsync("staffAddUpdate", staffs);
        }
        public async Task UsersAddUpdate(User user)
        {
            await Clients.All.SendAsync("UsersAddUpdate", user);
        }
        public async Task SendMessage(NotificationMessages notificationMessages)
        {
            await Clients.All.SendAsync("SendMessage", notificationMessages);
        }
    }
}