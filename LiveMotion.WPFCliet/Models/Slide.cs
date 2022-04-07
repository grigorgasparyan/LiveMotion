using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveMotion.WPFCliet.Models
{
    public class Slide : ModelBase
    {
        #region Field

        private string _name;
        private byte[] _stream;
        private bool _isOpenFileDialogButton;

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

        public byte[] Stream
        {
            get { return _stream; }
            set
            {
                if (_stream == value) return;
                _stream = value;
                OnPropertyChanged();
            }
        }

        public bool IsOpenFileDialogButton
        {
            get { return _isOpenFileDialogButton; }
            set
            {
                if (_isOpenFileDialogButton == value) return;
                _isOpenFileDialogButton = value;
                OnPropertyChanged();
            }
        }

        public int PresentationId { get; set; }

        #endregion       
    }
}
