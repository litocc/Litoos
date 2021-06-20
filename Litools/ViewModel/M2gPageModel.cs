using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Litools.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Litools.ViewModel
{
    public class M2gPageModel : ViewModelBase
    {
        public M2gPageModel()
        {

        }
        // ffmpeg路径
        public static string ffmpegtool = Environment.CurrentDirectory + @"\ffmpeg.exe";

        //输出路径
        public static string gifFile = "";

        private UserVideo _uv = new UserVideo();
        public UserVideo Uv
        {
            get => _uv;
            set
            {
                if (value != _uv)
                {
                    _uv = value;
                    RaisePropertyChanged(nameof(Uv));
                }
            }
        }

        private M2gLog _ml = new M2gLog();
        public M2gLog Ml
        {
            get => _ml;
            set
            {
                if (value != _ml)
                {
                    _ml = value;
                    RaisePropertyChanged(nameof(Ml));
                }
            }
        }

        /// <summary>
        /// 拖入文件获取路径
        /// </summary>
        public RelayCommand<DragEventArgs> getVideoPath => new RelayCommand<DragEventArgs>((e) =>
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    //得到视频路径
                    Uv.VideoPath = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();

                    gifFile = Path.GetDirectoryName(Uv.VideoPath) + @"\output.gif";  //得到视频所在文件夹路径
                    Ml.Pcetime = 0;
                }
            }
            catch
            {
                MessageBox.Show("获取路径失败！！");
            }
        });

        public RelayCommand convertCommand => new RelayCommand(async () =>
        {
            await VideoToGif();
        });

        public RelayCommand SelectFilePath => new RelayCommand(() =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择数据源文件";
            openFileDialog.Filter = "视频文件|*.mp4|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "mp4";
            if (openFileDialog.ShowDialog() == false)
            {
                return;
            }
            Uv.VideoPath = openFileDialog.FileName;  //得到视频路径
            gifFile = Path.GetDirectoryName(Uv.VideoPath) + @"\output.gif";  //得到视频所在文件夹路径
            Ml.Pcetime = 0;
            //MessageBox.Show(txtFile);
        });


        public async Task VideoToGif()
        {
            Process p = new Process();//建立外部调用进程
            //Uv.VideoPath = ffmpegtool;
            //Console.WriteLine(ffmpegtool);
            p.StartInfo.FileName = ffmpegtool;
            //转化gif动画
            //string strArg = "-i " + Uv.VideoPath + " -y -f gif -loglevel quiet " + imgFile;  //-loglevel quiet：黑框框不显示输出信息
            string strArg = "-i " + Uv.VideoPath + " -y -f gif " + gifFile;
            //Console.WriteLine("strArg: " + strArg);
            await Task.Run(() =>
            {
                p.StartInfo.Arguments = strArg;
                p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序启动线程(一定为FALSE,详细的请看MSDN)
                p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,FFMPEG的所有输出信息,都为错误输出流,用StandardOutput是捕获不到任何消息的...)
                p.StartInfo.CreateNoWindow = true;//不创建进程窗口   为true可以不显示调用cmd黑乎乎的窗口
                p.ErrorDataReceived += new DataReceivedEventHandler(Output);//外部程序(这里是FFMPEG)输出流时候产生的事件,这里是把流的处理过程转移到下面的方法中,详细请查阅MSDN
                p.Start();//启动线程
                p.BeginErrorReadLine();//开始异步读取
                p.WaitForExit();//阻塞等待进程结束
                p.Close();//关闭进程
                p.Dispose();//释放资源
            });

            }

        //根据输出获取转换进度
        private void Output(object sendProcess, DataReceivedEventArgs output)
        {
            if (!String.IsNullOrEmpty(output.Data))
            {
                string str = output.Data.ToString();
                //Console.WriteLine(str);
                Regex regex = new Regex(@"\d([0-1]?[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9]\.\d{1,3})");
                if (str.StartsWith("  Duration"))
                {
                    if (regex.IsMatch(str))
                    {
                        Console.WriteLine(regex.Match(str).Value);
                        Console.WriteLine(TimeTODouble(regex.Match(str).Value));
                        Ml.TotalTime = TimeTODouble(regex.Match(str).Value);
                    }
                }
                if (str.StartsWith("frame="))
                {
                    if (regex.IsMatch(str))
                    {
                        Console.WriteLine(regex.Match(str).Value);
                        Console.WriteLine(TimeTODouble(regex.Match(str).Value));
                        Ml.Pcetime = TimeTODouble(regex.Match(str).Value);
                    }
                }
                if (str.StartsWith("video:"))
                {
                    Ml.ShowDia = true;
                }
            }
        }

        public double TimeTODouble(string time)
        {
            //时间格式为xx:xx:xx.xx
            string[] timeStr = time.Split(':');
            int h = Convert.ToInt32(timeStr[0]);
            int m = Convert.ToInt32(timeStr[1]);
            string[] sStr = timeStr[2].Split('.');
            int s = Convert.ToInt32(sStr[0]);
            double ms = new TimeSpan(h, m, s).TotalMilliseconds + Convert.ToInt32(sStr[1]);
            return ms;
        }
    }
}
