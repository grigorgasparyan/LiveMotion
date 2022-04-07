using LiveMotion.WPFCliet.ViewModels;

namespace LiveMotion.WPFCliet.Controls.Slide
{
    public class SlideViewerViewModel : BaseViewModel
    {
        #region Fields

        private string _title;

        private Models.Slide _slide;

        #endregion

        #region Ctor

        public SlideViewerViewModel()
        {

        }

        #endregion

        #region Properties

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title == value)
                    return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public Models.Slide Slide
        {
            get
            {
                return _slide;
            }
            set
            {
                if (_slide == value)
                    return;
                _slide = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
