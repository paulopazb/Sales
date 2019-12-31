using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using GalaSoft.MvvmLight.Views;
using Sales.Common.Models;
using Sales.Services;
using Sales.Views;
using Xamarin.Forms;
using Sales.Helpers;

namespace Sales.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
        #region Attributes 
        private string email;
        private string password;
        private bool isRunning;
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
            this.IsRunning = true;
          
            var connection = await this.apiService.CheckConnection();
            this.IsRunning = false;

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error ",connection.Message, " Accept");
                return;
            }
            var token = await this.apiService.GetToken("https://salesapiservice.azurewebsites.net/",this.Email,this.Password);
          
            if (token == null)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error ","Something went wrong, please try later.", " Accept");

            }
          
            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error ",token.ErrorDescription, " Accept");
                this.Password = string.Empty;
            }
            Settings.TokenType = token.TokenType;
            Settings.AccessToken = token.AccessToken;
          
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Products = new ProductsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage()); //Nos permite pasar del login al ListView

            this.IsRunning = false;

            this.Email = string.Empty;
            this.Password = string.Empty;//Blanquea el password
            
        }
        #endregion






    }
}

