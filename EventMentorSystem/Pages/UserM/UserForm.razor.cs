using EMS.DB.Constant;
using EMS.DB.Models;
using EMS.DB.Repository.Interface;
using EventMentorSystem.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EventMentorSystem.Pages.UserM
{
    public partial class UserForm
    {

        [CascadingParameter(Name = "cascadeParameters")]
        public GlobalParameter _parameters { get; set; }
        [Inject] SignInManager<User> SignInManager { get; set; }
        [Inject] ISnackbar _snackbar { get; set; }

        [Inject] UserManager<User> UserManager { get; set; }


        [Parameter]
        public EventCallback  OnCancelEvent { get; set; }
       
        private User UserModel = new();
        private Operator OperatorModel = new();
        private Admin AdminModel = new();
        private Staff StaffModel = new();
        private HubConnection hubConnection;
        protected override async Task OnInitializedAsync()
        {

            hubConnection = new HubConnectionBuilder()
            .WithUrl(NavigationManager.ToAbsoluteUri("/chathub"))
            .Build();
            hubConnection.On<User>("EventAddUpdate", (events) =>
            {
               
                InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();

        }


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
        private void Cancel() {
            OnCancelEvent.InvokeAsync();
        }
        private async Task Save()
        {
            try
            {

                var user = new User
                {

                    FullName = UserModel.FullName,
                    UserName = UserModel.Email,
                    Password = UserModel.Password,
                    Email = UserModel.Email,
                    ContactNo = UserModel.ContactNo,
                    Userrole = UserModel.Userrole,
                    Useraddress = UserModel.Useraddress,
                };

                user.IsActive = true;
                if (user is not null && !string.IsNullOrEmpty(UserModel.Password))
                {
                    var result = await UserManager.CreateAsync(user, UserModel.Password);
                    if (result.Succeeded)
                    {

                        if (UserModel.Userrole == Userrole.Operator.ToString())
                        {
                            OperatorModel.UserId = user.Id;
                            _OperatorRepository.Insert(OperatorModel);
                            //UserModel = new User();
                        }
                        else if (UserModel.Userrole == Userrole.Admin.ToString())
                        {
                            AdminModel.UserId = user.Id;
                            _AdminRepository.Insert(AdminModel);
                        }
                        else if (UserModel.Userrole == Userrole.Staff.ToString())
                        {
                            StaffModel.UserId = user.Id;
                            _StaffRepository.Insert(StaffModel);

                        }
                        if (!string.IsNullOrWhiteSpace(UserModel.Userrole))
                        {
                            var roles = UserModel.Userrole.Split(',').Select(a => a.Trim()).ToArray();
                            Console.WriteLine($"{roles.Length}");
                            foreach (var role in roles)
                            {
                                await UserManager.AddToRoleAsync(user, role);
                            }
                        }
                        
                        _snackbar.Add("Added successfully", Severity.Success);
                        OnCancelEvent.InvokeAsync();
                        StateHasChanged();
                    }

                }
                await hubConnection.SendAsync("UsersAddUpdate", UserModel);
                UserModel = new User();
            }
            catch (Exception ex)
            {
                _parameters.ShowErrorMessages(ex);
            }

        }
    }
}
