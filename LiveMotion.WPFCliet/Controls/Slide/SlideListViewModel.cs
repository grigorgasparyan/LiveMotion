using AutoMapper;
using LiveMotion.Core.Services;
using LiveMotion.WPFCliet.Commands;
using LiveMotion.WPFCliet.ViewModels;
using Microsoft.Win32;
using SimpleInjector.Lifestyles;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Threading;

namespace LiveMotion.WPFCliet.Controls.Slide
{
    public class SlideListViewModel : BaseViewModel
    {
        #region Fields
        private readonly IMapper _mapper;
        private ObservableCollection<Models.Slide> _slides;
        private Models.Slide _selectedSlide;
        private DispatcherTimer _sliderTimer;
        private int _presentationId;
        #endregion

        #region Ctor

        public SlideListViewModel(IMapper mapper)
        {
            _mapper = mapper;
            Slides = new ObservableCollection<Models.Slide>
            {
                new Models.Slide { Name = "ADD", IsOpenFileDialogButton = true }
            };
        }

        #endregion

        #region Properties

        public ObservableCollection<Models.Slide> Slides
        {
            get
            {
                return _slides;
            }
            set
            {
                if (_slides == value)
                    return;
                _slides = value;
                OnPropertyChanged();
            }
        }

        public Models.Slide SelectedSlide
        {
            get
            {
                return _selectedSlide;
            }
            set
            {
                if (_selectedSlide == value)
                    return;
                _selectedSlide = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        private ICommand _openFileDialogCommand;

        public ICommand OpenFileDialogCommand => _openFileDialogCommand ?? (_openFileDialogCommand = new RelayCommand(p => CanOpenFileDialog, k => OpenFileDialogExecution()));

        private void OpenFileDialogExecution()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG|*.jpg;*.jpeg|BMP|*.bmp|GIF|*.gif|PNG|*.png|TIFF|*.tif;*.tiff|" + "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";
            var result = dialog.ShowDialog();
            if (result != true)
                return;
            byte[] buffer = File.ReadAllBytes(dialog.FileName);
            int count = Slides.Count;
            Slides.Insert(count - 1, new Models.Slide { Name = dialog.SafeFileName, Stream = buffer, PresentationId = _presentationId });
        }

        public bool CanOpenFileDialog { get { return true; } }


        #endregion

        #region Methods

        public Models.Slide GetNetSlide()
        {
            if (SelectedSlide == null || Slides.Count == 1)
                return null;
            var indexOf = Slides.IndexOf(SelectedSlide);
            if (indexOf != Slides.Count - 2)
            {
                return Slides[indexOf + 1];
            }
            else
            {
                return Slides[0];
            }
        }

        public void StartSliderTimer()
        {
            if (Slides != null && Slides.Count > 1)
            {
                if (_sliderTimer == null)
                {
                    _sliderTimer = new DispatcherTimer();
                    _sliderTimer.Tick += _sliderTimer_Tick;
                    _sliderTimer.Interval = System.TimeSpan.FromSeconds(2);
                }
                _sliderTimer.Start();
            }
        }

        private void _sliderTimer_Tick(object sender, System.EventArgs e)
        {
            var indexOf = Slides.IndexOf(SelectedSlide);
            if (indexOf != Slides.Count - 2)
            {
                SelectedSlide = Slides[indexOf + 1];
            }
            else
            {
                SelectedSlide = Slides[0];
            }
        }

        public void StopSliderTimer()
        {
            if (_sliderTimer != null)
                _sliderTimer.Stop();
        }
        #endregion

        #region Methods

        public void LoadSlides(Models.Presentation presentation)
        {
            Slides.Clear();
            if (presentation == null)
            {
                _presentationId = 0;
                Slides.Add(new Models.Slide { Name = "ADD", IsOpenFileDialogButton = true });
                return;
            }
            _presentationId = presentation.Id;

            using (ThreadScopedLifestyle.BeginScope(Program.Container))
            {
                var service = Program.Container.GetInstance<SlideService>();
                Slides = new ObservableCollection<Models.Slide>(_mapper.Map<List<Core.Entities.Slide>, List<Models.Slide>>(service.GetSlidesByPresentationId(_presentationId)));
            }

            Slides.Add(new Models.Slide { Name = "ADD", IsOpenFileDialogButton = true });
        }

        #endregion
    }
}
