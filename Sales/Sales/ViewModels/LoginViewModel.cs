using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using GalaSoft.MvvmLight.Views;
using Sales.Common.Models;
using Sales.Services;
using Sales.Views;

using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Attributes 
        private string email;
        private string password;
        private bool isRunning;

     
        private ApiService apiService;
        #endregion

       #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        #endregion
    

    #region Constructor
    public LoginViewModel()
        {
            this.apiService = new ApiService();
         
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        { get
            {
                return new RelayCommand(Login);
            }
                
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error ", "You must enter an email ", " Accept");
                    return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error ", "You must enter a password ", " Accept");
                return;
            }
            if (this.Email != "paulopaz03" || this.Password != "1234")
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error ", "Email or password incorrect ", " Accept");
                this.Password = string.Empty;
                return;


            }
            this.IsRunning = false;

            this.Email = string.Empty;
            this.Password = string.Empty;
            MainViewModel.GetInstance().Products = new ProductsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());
        }
        #endregion






    }
}

