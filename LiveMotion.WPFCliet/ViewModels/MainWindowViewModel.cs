using LiveMotion.WPFCliet.Commands;
using LiveMotion.WPFCliet.Controls.PresentationList;
using LiveMotion.WPFCliet.Controls.Slide;
using System.Windows.Input;

namespace LiveMotion.WPFCliet.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Field

        private PresentationListViewModel _presentationListViewModel;
        private SlidePlayerViewModel _slidePlayerViewModel;       

        #endregion

        #region Ctor

        public MainWindowViewModel(PresentationListViewModel presentationListViewModel, SlidePlayerViewModel slidePlayerViewModel)
        {
            _presentationListViewModel = presentationListViewModel;
            _slidePlayerViewModel = slidePlayerViewModel;
            _presentationListViewModel.PropertyChanged += _presentationListViewModel_PropertyChanged;
        }

        private void _presentationListViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(_presentationListViewModel.SelectedPresentation):
                    _slidePlayerViewModel.SetSlides(_presentationListViewModel.SelectedPresentation);
                    break;
            }
        }

        #endregion

        #region Properties


        public PresentationListViewModel PresentationListViewModel
        {
            get
            {
                return _presentationListViewModel;
            }
            set
            {
                if (_presentationListViewModel == value)
                    return;
                _presentationListViewModel = value;
                OnPropertyChanged();
            }
        }

        public SlidePlayerViewModel SlidePlayerViewModel
        {
            get
            {
                return _slidePlayerViewModel;
            }
            set
            {
                if (_slidePlayerViewModel == value)
                    return;
                _slidePlayerViewModel = value;
                OnPropertyChanged();
            }
        }      

        #endregion

        #region Commands

        #endregion

    }
}
