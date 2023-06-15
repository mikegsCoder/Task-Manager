using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.WPF.DataContexts
{
    public class MainWindowContext : INotifyPropertyChanged
    {
        private bool hasUser = false;

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
