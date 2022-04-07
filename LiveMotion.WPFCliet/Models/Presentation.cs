namespace LiveMotion.WPFCliet.Models
{
    public class Presentation : ModelBase
    {
        #region Field

        private string _name;

        #endregion

        #region Property

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        #endregion       
    }
}
