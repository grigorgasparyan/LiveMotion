using AutoMapper;
using LiveMotion.Core.Services;
using LiveMotion.WPFCliet.Commands;
using LiveMotion.WPFCliet.Models;
using LiveMotion.WPFCliet.ViewModels;
using SimpleInjector.Lifestyles;
using System;
using System.Windows;
using System.Windows.Input;

namespace LiveMotion.WPFCliet.Dialoges
{
    public class PresentationEditViewModel : BaseViewModel
    {
        #region Field
        private readonly IMapper _mapper;
        private string _name;

        private Presentation _presentation;

        #endregion

        #region Ctor

        public PresentationEditViewModel(IMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion


        #region Properties

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        private ICommand _deleteCommand;

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(p => CanDelete, w => DeleteExecution((Window)w)));

        private void DeleteExecution(Window wnd)
        {
            if (_presentation != null && _presentation.Id > 0)
            {
                using (ThreadScopedLifestyle.BeginScope(Program.Container))
                {
                    var service = Program.Container.GetInstance<PresentationService>();
                    service.Delete(_presentation.Id);
                }
            }
            if (wnd != null)
            {
                wnd.DialogResult = true;
                wnd.Close();
            }
        }

        public bool CanDelete { get { return _presentation != null && _presentation.Id > 0; } }



        private ICommand _saveCommand;

        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(p => CanSave, w => SaveExecution((Window)w)));

        private void SaveExecution(Window wnd)
        {
            using (ThreadScopedLifestyle.BeginScope(Program.Container))
            {
                var service = Program.Container.GetInstance<PresentationService>();
                _presentation.Name = Name;
                service.AddOrUpdate(_mapper.Map<Presentation, Core.Entities.Presentation>(_presentation));
            }
            if (wnd != null)
            {
                wnd.DialogResult = true;
                wnd.Close();
            }
        }

        public bool CanSave { get { return !string.IsNullOrWhiteSpace(Name); } }


        private ICommand _cancelCommand;

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(p => CanCancel, w => CancelExecution((Window)w)));

        private void CancelExecution(Window wnd)
        {
            if (wnd != null)
            {
                wnd.DialogResult = false;
                wnd.Close();
            }
        }

        public bool CanCancel { get { return true; } }

        #endregion

        #region Methods

        public void Set(Presentation presentation)
        {
            _presentation = presentation;
            Name = _presentation.Name;
        }

        #endregion
    }
}
