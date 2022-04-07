using AutoMapper;
using LiveMotion.Core.Services;
using LiveMotion.WPFCliet.Commands;
using LiveMotion.WPFCliet.Models;
using LiveMotion.WPFCliet.ViewModels;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace LiveMotion.WPFCliet.Controls.Slide
{
    public class SlidePlayerViewModel : BaseViewModel
    {
        #region Fields
        private readonly IMapper _mapper;
        private SlideViewerViewModel _nextSlideViewerViewModel;
        private SlideViewerViewModel _currentSlideViewerViewModel;
        private SlideListViewModel _slideListViewModel;

        private bool _isPlayed;

        #endregion

        #region Ctor

        public SlidePlayerViewModel(SlideListViewModel slideListViewModel, IMapper mapper)
        {
            _mapper = mapper;
            _nextSlideViewerViewModel = new SlideViewerViewModel { Title = "PREVIEW - NEXT SLIDE TO LOAD" };
            _currentSlideViewerViewModel = new SlideViewerViewModel { Title = "CURRENT SLIDE" };
            _slideListViewModel = slideListViewModel;
            _slideListViewModel.PropertyChanged += _slideListViewModel_PropertyChanged;
        }

        private void _slideListViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_slideListViewModel.SelectedSlide):
                    if (_slideListViewModel.SelectedSlide == null)
                    {
                        _currentSlideViewerViewModel.Slide = null;
                        return;
                    }
                    if (!_slideListViewModel.SelectedSlide.IsOpenFileDialogButton)
                        _currentSlideViewerViewModel.Slide = _slideListViewModel.SelectedSlide;
                    break;
            }
        }

        public void SetSlides(Presentation selectedPresentation)
        {
            _slideListViewModel.LoadSlides(selectedPresentation);
        }

        #endregion

        #region Properties

        public SlideViewerViewModel NextSlideViewerViewModel
        {
            get
            {
                return _nextSlideViewerViewModel;
            }
            set
            {
                if (_nextSlideViewerViewModel == value)
                    return;
                _nextSlideViewerViewModel = value;
                OnPropertyChanged();
            }
        }

        public SlideViewerViewModel CurrentSlideViewerViewModel
        {
            get
            {
                return _currentSlideViewerViewModel;
            }
            set
            {
                if (_currentSlideViewerViewModel == value)
                    return;
                _currentSlideViewerViewModel = value;
                OnPropertyChanged();
            }
        }

        public SlideListViewModel SlideListViewModel
        {

            get
            {
                return _slideListViewModel;
            }
            set
            {
                if (_slideListViewModel == value)
                    return;
                _slideListViewModel = value;
                OnPropertyChanged();
            }

        }

        public bool IsPlayed
        {

            get
            {
                return _isPlayed;
            }
            set
            {
                if (_isPlayed == value)
                    return;
                _isPlayed = value;
                OnPropertyChanged();
            }

        }


        #endregion

        #region Commands

        private ICommand _playAndPauseSliderCommand;

        public ICommand PlayAndPauseSliderCommand => _playAndPauseSliderCommand ?? (_playAndPauseSliderCommand = new RelayCommand(p => CanPlayAndPauseSlider, p => PlayAndPauseSliderExecution()));

        public bool CanPlayAndPauseSlider { get { return true; } }

        private void PlayAndPauseSliderExecution()
        {
            if (!IsPlayed)
            {
                _slideListViewModel.StartSliderTimer();
            }
            else
            {
                _slideListViewModel.StopSliderTimer();
            }
            IsPlayed = !IsPlayed;
        }


        private ICommand _saveSlidersCommand;

        public ICommand SaveSlidersCommand => _saveSlidersCommand ?? (_saveSlidersCommand = new RelayCommand(p => CanSaveSliders, p => SaveSlidersExecution()));

        private void SaveSlidersExecution()
        {
            var newSlides = _slideListViewModel.Slides.Where(x => x.Id <= 0 && !x.IsOpenFileDialogButton).ToList();
            if (newSlides.Any())
            {
                using (ThreadScopedLifestyle.BeginScope(Program.Container))
                {
                    var service = Program.Container.GetInstance<SlideService>();
                    service.AddOrUpdateRange(_mapper.Map<List<Models.Slide>, List<Core.Entities.Slide>>(newSlides));
                }
            }
        }
        public bool CanSaveSliders { get { return true; } }



        private ICommand _previewNextSliderCommand;

        public ICommand PreviewNextSliderCommand => _previewNextSliderCommand ?? (_previewNextSliderCommand = new RelayCommand(p => CanPreviewNextSlider, p => PreviewNextSliderExecution()));

        private void PreviewNextSliderExecution()
        {
            _nextSlideViewerViewModel.Slide = _slideListViewModel.GetNetSlide();
        }

        public bool CanPreviewNextSlider { get { return true; } }


        private ICommand _closeApplicationCommand;

        public ICommand CloseApplicationCommand => _closeApplicationCommand ?? (_closeApplicationCommand = new RelayCommand(p => CanCloseApplication, p => CloseApplicationExecution()));

        private void CloseApplicationExecution()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public bool CanCloseApplication { get { return true; } }

        #endregion
    }
}
