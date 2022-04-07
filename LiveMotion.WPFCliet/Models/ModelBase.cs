using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiveMotion.WPFCliet.Models
{
    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public int Id { get; set; }
    }
}
