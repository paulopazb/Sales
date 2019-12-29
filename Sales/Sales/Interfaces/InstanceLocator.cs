namespace Sales.Interfaces
{
    using ViewModels;
    public class InstanceLocator  // Instancia una sola instancia de la MainViewModel
    {
        #region Properties
        public MainViewModel Main { get; set; }
        #endregion
        #region Constructors
        public InstanceLocator()
        {
            this.Main = new MainViewModel(); 
        }
        #endregion
    }
}
