using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.ViewModels.Task;
using TaskManager.Core.ViewModels.User;

namespace TaskManager.WPF.DataContexts
{
    public class MainWindowContext : INotifyPropertyChanged
    {
        private bool hasUser = false;

        public List<TaskViewModel> tasks;
        public UserViewModel user;

        public bool HasUser
        {
            get { return hasUser; }
            set { hasUser = value; NotifyPropertyChanged("HasUser"); }
        }

        public bool ShowLoginBtn
        {
            get { return !hasUser; }
            set { NotifyPropertyChanged("ShowLoginBtn"); }
        }

        public bool NoTasks
        {
            get { return (tasks != null && tasks.Count == 0); }
            set { NotifyPropertyChanged("NoTasks"); }
        }

        public void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
