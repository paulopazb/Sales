
namespace Sales.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.Models;
    using Sales.Services;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;

        private ObservableCollection<Product> products;
        #endregion
        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }

        }
   

        #region Constructor 
        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }
        #endregion

        #region Methods
        private async void LoadProducts()
        {
            var connection = await this.apiService.CheckConnection(); //Comprobando si hay conexion con internet
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                return;
            }
            var response = await this.apiService.GetList<Product>("https://salesapiservice.azurewebsites.net", "/api", "/Products");
            if (!response.IsSuccess)
            {
             
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }
            var list = (List<Product>)response.Result; // El servicio devuelve listas
            this.Products = new ObservableCollection<Product>(list);  // Transforma la lista en una Observable collection
        

        }
      
        #endregion
    }


}
      
    
    

