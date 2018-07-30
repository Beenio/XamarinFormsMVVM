using FormsMVVM.Model.Entity;
using FormsMVVM.Model.EntityViewModel;
using FormsMVVM.Model.Repository;
using FormsMVVM.Views;
using Mapster;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FormsMVVM.ViewModel
{
    public class UserListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private List<UserViewModel> _ItemsList;
        public List<UserViewModel> ItemsList
        {
            get
            {
                return _ItemsList;
            }
            set
            {
                _ItemsList = value;
                IsEmptyCollection = value.Any() ? false : true;
                OnPropertyChanged("ItemsList");
            }
        }
        public ICommand OnAddCommand { get; private set; }
        private bool _EmptyCollection = true;
        public bool IsEmptyCollection
        {
            get
            {
                return _EmptyCollection;
            }
            set
            {
                _EmptyCollection = value;
                OnPropertyChanged("IsEmptyCollection");
                OnPropertyChanged("IsNotEmptyCollection");
            }
        }
        public bool IsNotEmptyCollection
        {
            get
            {
                return !_EmptyCollection;
            }
        }
        private UserViewModel _SelectedItem;
        public UserViewModel SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged("SelectedItem");
                NavigateToDetails(_SelectedItem);
            }
        }


        public UserListViewModel()
        {
            OnAddCommand = new Command(AddItem);
            Setup();
        }

        private async void AddItem()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UserDetails());
        }
        private void Setup()
        {
            SetupAction();
        }
        private void SetupData()
        {
            Task.Run(() => 
            {
                ItemsList = GetUsers();
            });
        }
        private void SetupAction()
        {
            MessagingCenter.Unsubscribe<string>(this, "Reload");
            MessagingCenter.Subscribe<string>(this, "Reload", (s) => 
            {
                SetupData();
            });
        }
        private List<UserViewModel> GetUsers()
        {
            var Repository = new UserRepository();
            var Users = Repository.Get<UserEntity>().Adapt<List<UserViewModel>>();

            return Users;
        }

        public async Task NavigateToDetails(UserViewModel SelectedItem)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UserDetails() {BindingContext = new UserDetailsViewModel(SelectedItem) });
        }
    }
}
