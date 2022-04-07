using AutoMapper;
using LiveMotion.Core.Services;
using LiveMotion.WPFCliet.Commands;
using LiveMotion.WPFCliet.Dialoges;
using LiveMotion.WPFCliet.Models;
using LiveMotion.WPFCliet.ViewModels;
using SimpleInjector.Lifestyles;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LiveMotion.WPFCliet.Controls.PresentationList
{
    public class PresentationListViewModel : BaseViewModel
    {
        #region Field

        private ObservableCollection<Presentation> _presentations;
        private Presentation _selectedPresentation;

        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public PresentationListViewModel(IMapper mapper)
        {
            _mapper = mapper;
            LoadPresentations();
        }

        #endregion

        #region Properties

        public ObservableCollection<Presentation> Presentations
        {
            get { return _presentations; }
            set
            {
                if (_presentations == value) return;
                _presentations = value;
                OnPropertyChanged();
            }
        }

        public Presentation SelectedPresentation
        {
            get { return _selectedPresentation; }
            set
            {
                if (_selectedPresentation == value) return;
                _selectedPresentation = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        private ICommand _addPresentationCommand;

        public ICommand AddPresentationCommand => _addPresentationCommand ?? (_addPresentationCommand = new RelayCommand(p => CanAddPresentation, p => AddPresentationExecution()));

        private void AddPresentationExecution()
        {
            PresentationEditViewModel viewModel = Program.Container.GetInstance<PresentationEditViewModel>();
            viewModel.Set(new Presentation());
            PresentationEditWindow wnd = new PresentationEditWindow { DataContext = viewModel };
            var result = wnd.ShowDialog();
            if (result == true)
            {
                LoadPresentations();
            }
        }

        public bool CanAddPresentation { get { return true; } }


        private ICommand _editPresentationCommand;

        public ICommand EditPresentationCommand => _editPresentationCommand ?? (_editPresentationCommand = new RelayCommand(p => CanEditPresentation, p => EditPresentationExecution()));

        private void EditPresentationExecution()
        {
            if (SelectedPresentation == null)
                return;
            PresentationEditViewModel viewModel = Program.Container.GetInstance<PresentationEditViewModel>();
            viewModel.Set(SelectedPresentation);
            PresentationEditWindow wnd = new PresentationEditWindow { DataContext = viewModel };
            var result = wnd.ShowDialog();
            if(result == true)
            {
                LoadPresentations();
            }
        }

        public bool CanEditPresentation{ get { return SelectedPresentation != null; } }

        #endregion


        public void LoadPresentations()
        {
            using (ThreadScopedLifestyle.BeginScope(Program.Container))
            {
                var service = Program.Container.GetInstance<PresentationService>();
                Presentations = new ObservableCollection<Presentation>(_mapper.Map<List<Core.Entities.Presentation>, List<Presentation>>(service.GetAll()));
            }
        }
    }
}
