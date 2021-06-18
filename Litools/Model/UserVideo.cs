using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litools.Model
{
    public class UserVideo : ObservableObject
    {
        private string _videoPath = "视频路径";

        public string VideoPath
        {
            get => _videoPath;
            set => Set(ref _videoPath, value);
        }
    }
}
