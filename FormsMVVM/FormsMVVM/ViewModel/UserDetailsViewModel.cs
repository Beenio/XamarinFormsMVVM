using FormsMVVM.Model.Entity;
using FormsMVVM.Model.EntityViewModel;
using FormsMVVM.Model.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FormsMVVM.ViewModel
{
    public class UserDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand SaveUser { get; set; }
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _Email;
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                OnPropertyChanged("Email");
            }
        }
        private string _Telephone;
        public string Telephone
        {
            get
            {
                return _Telephone;
            }
            set
            {
                _Telephone = value;
                OnPropertyChanged("Telephone");
            }
        }
        private string _Age;
        public string Age
        {
            get
            {
                return _Age;
            }
            set
            {
                _Age = value;
                OnPropertyChanged("Age");
            }
        }
        private UserViewModel User;

        public UserDetailsViewModel()
        {
            SetupActions();
        }

        public UserDetailsViewModel(UserViewModel User)
        {
            SetupActions();
            SetupPage(User);
        }

        private void SetupActions()
        {
            SaveUser = new Command(Save);
        }

        private void SetupPage(UserViewModel User)
        {
            this.Age = User.Age;
            this.Email = User.Email;
            this.Name = User.Name;
            this.Telephone = User.Telephone;
            this.User = User;
        }

        private async void Save()
        {
            var Repository = new UserRepository();
            if (User == null)
            {
                Repository.Insert(new UserEntity() { Email = Email, Name = Name, Age = Age, Telephone = Telephone });
            }
            else
            {
                var Entity = Repository.Get(w => w.Id.Equals(User.Id));

                Repository.Write(() =>
                {
                    Entity.Name = this.Name;
                    Entity.Email = this.Email;
                    Entity.Age = this.Age;
                    Entity.Telephone = this.Telephone;
                });
            }

            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}
