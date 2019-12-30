using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel Login { get; set; }
        public ProductsViewModel Products { get; set; }

        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return (instance);
        }
        #endregion
    }
}
