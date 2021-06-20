using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Litools.Model;

namespace Litools.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {

        }

        private PageUri _pu = PageUri.getInstance();
        public PageUri Pu
        {
            get => _pu;
            set
            {
                if (value != _pu)
                {
                    _pu = value;
                    RaisePropertyChanged(nameof(Pu));
                }
            }
        }

        public RelayCommand<string> MenuItemSelectCommand => new RelayCommand<string>(x => {
            Pu.PageStr = x;
        });
    }
}