using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoundTools.Common
{

    public class MP3Player
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="path"></param>
        public MP3Player(string path)
        {
            this.FilePath = path;
        }
        /// <summary>   
        /// 文件地址   
        /// </summary>   
        private string FilePath;

        /// <summary>   
        /// 播放   
        /// </summary>   
        public void Play()
        {
            //Console.WriteLine($"开始*************{Thread.CurrentThread.ManagedThreadId.ToString("00")}{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            mciSendString("close all", "", 0, 0);
            mciSendString("open " + FilePath + " alias media", "", 0, 0);
            mciSendString("play media", "", 0, 0);
        }

        /// <summary>   
        /// 暂停   
        /// </summary>   
        public void Pause()
        {
            mciSendString("pause media", "", 0, 0);
        }

        /// <summary>   
        /// 停止   
        /// </summary>   
        public void Stop()
        {
            mciSendString("close media", "", 0, 0);
        }

        /// <summary>   
        /// API函数   
        /// </summary>   
        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        private static extern int mciSendString(
            string lpstrCommand,
            string lpstrReturnString,
            int uReturnLength,
            int hwndCallback
        );
    }
    
}
