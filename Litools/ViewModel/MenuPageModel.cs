using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Litools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litools.ViewModel
{
    public class MenuPageModel : ViewModelBase
    {
        public MenuPageModel()
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
        /// <summary>
        /// 菜单命令
        /// </summary>
        public RelayCommand<string> MenuItemSelectCommand => new RelayCommand<string>(x => {
            Pu.PageStr = x;
            Console.WriteLine(x);
        });
    }
}
