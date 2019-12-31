using Sales.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.ViewModels
{
    public class MainViewModel
    {
        #region Properties
        public List<Product> ProductsList { get; set; }

        public TokenResponse Token { get; set; }
        #endregion

        #region ViewModels
        public LoginViewModel Login { get; set; }
        public ProductsViewModel Products { get; set; }
        #endregion
     
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
