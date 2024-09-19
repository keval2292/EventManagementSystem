using EMS.DB.Constant;
using EMS.DB.Models;
using EventMentorSystem.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventMentorSystem.Pages.dashboard
{
    public partial class Index
    {
        [Inject] NavigationManager UriHelper { get; set; }
        private MudDateRangePicker _picker;
        private DateRange _dateRange = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(5).Date);
        private bool _autoClose;
        private string searchString = "";
        private string searchString1 = "";
        private int Totalevent;
        private int TotalWork;
        private int Totaluser;
        private int Totalservice;
        private DateTime? startDate;
        private DateTime? endDate;
        private List<Event> eventList = new();
        private List<User> userlist = new();
        private List<CategoryService> categoryservicelist = new();
        private List<Staff> stafflist = new();
        private IEnumerable<Event> pagedData;
        private IEnumerable<EventStaffWork> EventStaffWorkpagedData;
        private MudTable<Event> tableRef;
        private MudTable<EventStaffWork> tableRefEventStaffWork;
        private int totalItems;
        private int totaleventforOprator;
        public string nowork;
        private List<Event> OpratorWorkPlace;
        private string userId;
        private int totalItemsOperator;
        private List<EventStaffWork> EventStaffWorkList = new();
        private List<EventStaffWork> StaffWorkList = new();
        private HubConnection hubConnection;

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity is { IsAuthenticated: true })
            {
                string nameIdentifier = authState.User.Identity.Name;
                if (!string.IsNullOrEmpty(nameIdentifier))
                {
                    var userIdClaim = authState.User.Claims.Where(c => c.Type == "UserId").FirstOrDefault();
                    if (userIdClaim != null)
                    {
                        userId = userIdClaim.Value;
                    }
                    else
                    {
                        var claim = authState.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").FirstOrDefault();
                        if (claim != null)
                        {
                            userId = claim.Value;
                        }
                    }
                }
            }
            hubConnection = new HubConnectionBuilder()
       .WithUrl(_NavigationManager.ToAbsoluteUri("/chathub"))
       .Build();
            hubConnection.On<Event>("EventAddUpdate", (events) =>
            {
                GetAllevents();
                InvokeAsync(StateHasChanged);
            });
            hubConnection.On<EventStaffWork>("WorkAddUpdate", (EventStaffWorks) =>
            {
                GetstaffWork();
                GetAllTodate();
                InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();

        }
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                GetAllevents();
                GetUserList();
                GetList();
                Getstaff();
                GetAllTodate();
                GetstaffWork();
                GeteventTotal();
                GeteventforOperator();
               
                StateHasChanged();
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        private List<Event> GetAllevents()
        {
            if (tableRef is not null) { 
            tableRef.ReloadServerData();
            }
            eventList = _EventRepository.GetList();
            Totalevent = eventList.Count();
            return eventList;
        }
        
        private void GeteventTotal()
        {
            totaleventforOprator = _EventRepository.GetByOpertorId(userId).Count();
        }
        private List<Event> GeteventforOperator()
        {
            OpratorWorkPlace = _EventRepository.GetListToday(userId);
            if (OpratorWorkPlace.Count() > 0)
            {
                nowork = "No Work";
            }
            return OpratorWorkPlace;
        }
        private List<Staff> Getstaff()
        {
            stafflist = _StaffRepository.GetStaffByUserId(userId);
            return stafflist;
        }
        private List<User> GetUserList()
        {
            userlist = _UserRepository.GetAllUser();
            Totaluser = userlist.Count();
            StateHasChanged();
            return userlist;
        }
        private List<CategoryService> GetList()
        {
            categoryservicelist = _CategoryServiceRepository.GetList();
            Totalservice = categoryservicelist.Count();
            StateHasChanged();
            return categoryservicelist;

        }
        private List<EventStaffWork> GetAllTodate()
        {
            if (tableRefEventStaffWork is not null)
            {
                tableRefEventStaffWork.ReloadServerData();
            }
            EventStaffWorkList = _EventStaffWorkRepository.GetListToday();
            return EventStaffWorkList;
        }
        private List<EventStaffWork> GetstaffWork()
        {
            if (tableRefEventStaffWork is not null)
            {
                tableRefEventStaffWork.ReloadServerData();
            }
            StaffWorkList = _EventStaffWorkRepository.GetListByStaff(userId);
            TotalWork = StaffWorkList.Count();
            return StaffWorkList;
        }

        private bool Search(Event events)
        {
            if (StringValid(events.EventName)
                && StringValid(events.OperatorId.ToString())
                && StringValid(events.EventVenue)
                && StringValid(events.OrganizerName)
                && StringValid(events.OrganizerContact)
                && StringValid(events.FromDate.ToString())

                &&
                events.EventName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || events.OperatorId.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || events.EventVenue.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || events.OrganizerName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || events.OrganizerContact.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || events.FromDate.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
        private void eventlink()
        {
            UriHelper.NavigateTo($"/eventlist");
        }
        private void userlink()
        {
            UriHelper.NavigateTo($"/userlist");
        }
        private void servicelink()
        {
            UriHelper.NavigateTo($"/servicelist");
        }

        private bool SearchforOperatorwork(EventStaffWork EventStaffWorks)
        {
            if (StringValid(EventStaffWorks.Status)
                && StringValid(EventStaffWorks.Status)
                && StringValid(EventStaffWorks.Description)
                && StringValid(EventStaffWorks.Service)
                && StringValid(EventStaffWorks.Event.EventName)
                && StringValid(EventStaffWorks.Event.EventVenue)
                && StringValid(EventStaffWorks.Event.FromDate.ToString())

                &&
                   EventStaffWorks.Status.Contains(searchString1, StringComparison.OrdinalIgnoreCase)
                || EventStaffWorks.Description.Contains(searchString1, StringComparison.OrdinalIgnoreCase)
                || EventStaffWorks.Service.Contains(searchString1, StringComparison.OrdinalIgnoreCase)
                || EventStaffWorks.Event.EventName.Contains(searchString1, StringComparison.OrdinalIgnoreCase)
                || EventStaffWorks.Event.EventVenue.Contains(searchString1, StringComparison.OrdinalIgnoreCase)
                || EventStaffWorks.Event.FromDate.ToString().Contains(searchString1, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private bool StringValid(string strValue)
        {
            if (!string.IsNullOrEmpty(strValue))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<Event>> ServerReload(TableState state)
        {
            IEnumerable<Event> data;
            if (startDate.HasValue && endDate.HasValue)
            {
                data = _EventRepository.GetListFromDashboard(startDate, endDate);
            }
            else
            {
                //get all data of current month
                data = _EventRepository.GetList();
            }

            data = data.Where(selectedModel => { return Search(selectedModel); }).ToArray();
            data = data.OrderByDirection(state.SortDirection, o => o.FromDate.Value);
            totalItems = data.Count();

            pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<Event>() { TotalItems = totalItems, Items = pagedData };
        }
        private async Task<TableData<Event>> ServerReloadForOperator(TableState state)
        {
            IEnumerable<Event> data;
           
                data = _EventRepository.GetByOpertorId(userId);


            data = data.Where(selectedModel => { return Search(selectedModel); }).ToArray();
            data = data.OrderByDirection(state.SortDirection, o => o.FromDate.Value);
            totalItems = data.Count();

            pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<Event>() { TotalItems = totalItems, Items = pagedData };
        }
        private async Task<TableData<EventStaffWork>> ServerReload1(TableState state)
        {
            IEnumerable<EventStaffWork> data;
            if (startDate.HasValue && endDate.HasValue)
            {
                data = _EventStaffWorkRepository.GetListFromWork(startDate, endDate);
            }
            else
            {
                data = _EventStaffWorkRepository.GetEventStaffWorkList();
            }

            data = data.Where(selectedModel => { return SearchforOperatorwork(selectedModel); }).ToArray();
            data = data.OrderByDirection(state.SortDirection, o => o.Event.FromDate.Value);
            totalItemsOperator = data.Count();

            EventStaffWorkpagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<EventStaffWork>() { TotalItems = totalItemsOperator, Items = EventStaffWorkpagedData };
        }
        private async Task<TableData<EventStaffWork>> ServerReloadForStaff(TableState state)
        {
            IEnumerable<EventStaffWork> data;
            
                data = _EventStaffWorkRepository.GetListBystatus(userId);

            data = data.Where(selectedModel => { return SearchforOperatorwork(selectedModel); }).ToArray();
            data = data.OrderByDirection(state.SortDirection, o => o.Event.FromDate.Value);
            totalItemsOperator = data.Count();

            EventStaffWorkpagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<EventStaffWork>() { TotalItems = totalItemsOperator, Items = EventStaffWorkpagedData };
        }
        private async Task<TableData<EventStaffWork>> ServerReloadForstaff(TableState state)
        {
            IEnumerable<EventStaffWork> data;

            data = _EventStaffWorkRepository.GetListBystatusFinish(userId);

            data = data.Where(selectedModel => { return SearchforOperatorwork(selectedModel); }).ToArray();
            data = data.OrderByDirection(state.SortDirection, o => o.Event.FromDate.Value);
            totalItemsOperator = data.Count();

            EventStaffWorkpagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<EventStaffWork>() { TotalItems = totalItemsOperator, Items = EventStaffWorkpagedData };
        }
        private Task UpdateDashboardData()
        {
            if (_dateRange is not null)
            {
                startDate = _dateRange.Start;
                endDate = _dateRange.End;
            }
            else
            {
                startDate = null;
                endDate = null;
            }
            tableRef.ReloadServerData();
            tableRefEventStaffWork.ReloadServerData();
            return Task.CompletedTask;
        }

        private void OnSearch(string text)
        {
            searchString = text;
            tableRef.ReloadServerData();

        }
        private void OnSearchOperator(string text)
        {
            searchString1 = text;
            tableRefEventStaffWork.ReloadServerData();
        }
    }
}
