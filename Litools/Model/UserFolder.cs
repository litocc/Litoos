using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litools.Model
{
    public class UserFolder : ObservableObject
    {
        private string _folderPath = "文件夹路径";

        public string FolderPath
        {
            get => _folderPath;
            set => Set(ref _folderPath, value);
        }
    }
}
