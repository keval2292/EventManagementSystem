using EMS.DB.Models;
using EMS.DB.Repository.Interface;
using EventMentorSystem.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EventMentorSystem.Pages.UserM
{
    public partial class UserList
    {
        [CascadingParameter(Name = "cascadeParameters")]
        public GlobalParameter _parameters { get; set; }
        [Inject] private IUserRepository _UserRepository { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        private List<User> userList = new();
        private User userModel = new();
        private string searchString = "";
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        private HubConnection hubConnection;
        private MudTable<User> tableRef;
        private IEnumerable<User> pagedData;
        private int totalItems;
        private IEnumerable<string> Emailaddress(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                yield return "email is required!";
                yield break;
            }
            if (!Regex.IsMatch(pw, @"[a-z0-9]+@[a-z]+\.[a-z]{2,3}"))
                yield return "Invalid email address.";
        }

        protected override async Task OnInitializedAsync()
        {

            hubConnection = new HubConnectionBuilder()
            .WithUrl(NavigationManager.ToAbsoluteUri("/chathub"))
            .Build();
            hubConnection.On<User>("UsersAddUpdate", (user) =>
            {
                GetAllUsers();
                InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();

        }
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                GetAllUsers();
            }

            return base.OnAfterRenderAsync(firstRender);
        }
        private void ShowHideAddForm() {
            IsAdd = !IsAdd;
        }
        private List<User> GetAllUsers()
        {
            tableRef.ReloadServerData();
            userList = _UserRepository.GetAllUser();
            return userList;
        }

        private bool StringValid(string strValue)
        {
            if (!string.IsNullOrEmpty(strValue))
            {
                return true;
            }

            return false;
        }

        private async Task<TableData<User>> ServerReload(TableState state)
        {
            IEnumerable<User> data;
            data = _UserRepository.GetAllUser();
            data = data.Where(selectedModel => { return Search(selectedModel); }).ToArray();
            totalItems = data.Count();

            pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<User>() { TotalItems = totalItems, Items = pagedData };
        }


        private bool Search(User Users)
        {
            if (string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(searchString)
                && StringValid(Users.FullName)
                && StringValid(Users.Email)
                && StringValid(Users.Userrole)
                && StringValid(Users.ContactNo)
                &&
                 Users.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || Users.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || Users.Userrole.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || Users.ContactNo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
        private void OnSearch(string text)
        {
            searchString = text;
            tableRef.ReloadServerData();
        }

        private void Edit(string id)
        {
            try
            {
                userModel = userList.FirstOrDefault(c => c.Id == id);
                IsEdit = true;
                tableRef.ReloadServerData();
            }
            catch (Exception ex)
            {
                _parameters.ShowErrorMessages(ex);
            }
        }
        private async Task Save()
        {
            try
            {

                _UserRepository.Update(userModel);
                await hubConnection.SendAsync("UsersAddUpdate", userModel);
                IsEdit = false;
                tableRef.ReloadServerData();
            }
            catch (Exception ex)
            {
                _parameters.ShowErrorMessages(ex);
            }
        }
        private void Cancel()
        {
            userModel = new User();
            IsEdit = false;
        }
    }
}
