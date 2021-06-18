using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litools.Model
{
    public class PageUri : ObservableObject
    {
        private static PageUri instance;
        private PageUri() { }
        /// <summary>
        /// 页面
        /// </summary>
        private string _pageStr = "View/Pages/MenuPage.xaml";

        public string PageStr
        {
            get => _pageStr;
            set => Set(ref _pageStr, value);
        }

        public static PageUri getInstance()
        {
            if (instance == null)
            {
                instance = new PageUri();
            }
            return instance;
        }
    }
}
