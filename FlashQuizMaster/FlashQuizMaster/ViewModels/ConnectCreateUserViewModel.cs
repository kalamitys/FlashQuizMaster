using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizMaster.ViewModels
{
    public class ConnectCreateUserViewModel: BindableBase
    {
        private DbRepository Repository = new DbRepository();
        private int _userId;
        private User user;
        //public event PropertyChangedEventHandler PropertyChanged;

        private string loginImage = string.Empty; //SDI: the image is local to the viewmodel for simplification

        public ConnectCreateUserViewModel()
        {
            user = new User();
        }


        public int UserId
        {
            set
            {
                _userId = value;
            }
            get
            {
                return _userId;
            }
        }
        public string LoginName
        {
            set
            {
                if (!value.Equals(user.LoginName, StringComparison.OrdinalIgnoreCase))
                {
                    user.LoginName = value;
                    OnPropertyChanged("LoginName");
                }
            }
            get
            {
                return user.LoginName;
            }
        }

        public string LoginImage
        {
            set
            {
                this.loginImage = value;
            }
            get
            {
                return this.loginImage;
            }
        }

        /// <summary>
        /// Save one user
        /// </summary>
        public void SaveUserVM()
        {
            user.LoginName = this.LoginName;            
            Repository.SaveUser(user);
        }

        /// <summary>
        /// Get all users from the DB
        /// </summary>
        /// <returns></returns>
        public List<ConnectCreateUserViewModel> GetUsersVM()
        {
            List<User> AllUsers = Repository.GetUsers().ToList();
            List<ConnectCreateUserViewModel> AllUsersVM = new List<ConnectCreateUserViewModel>();
            if (AllUsers != null)
            {
                foreach (User user in AllUsers)
                {
                    ConnectCreateUserViewModel uvm = new ConnectCreateUserViewModel();
                    uvm.UserId = user.ID;
                    uvm.LoginName = user.LoginName;
                    uvm.loginImage = user.LoginName + ".png";
                    AllUsersVM.Add(uvm);
                }

            }

            return AllUsersVM;
        }

        //void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
