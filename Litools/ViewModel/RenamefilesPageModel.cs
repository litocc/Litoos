using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Litools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Litools.ViewModel
{
    public class RenamefilesPageModel : ViewModelBase
    {
        public RenamefilesPageModel()
        {

        }

        private UserFolder _uf = new UserFolder();
        public UserFolder Uf
        {
            get => _uf;
            set
            {
                if (value != _uf)
                {
                    _uf = value;
                    RaisePropertyChanged(nameof(Uf));
                }
            }
        }

        public RelayCommand<DragEventArgs> getVideoPath => new RelayCommand<DragEventArgs>((e) =>
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    //得到文件夹路径
                    Uf.FolderPath = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                }
            }
            catch
            {
                MessageBox.Show("获取路径失败！！");
            }
        });
    }
}
