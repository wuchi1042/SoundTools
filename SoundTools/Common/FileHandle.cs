
using Shell32;
using SoundTools.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundTools.Common
{
    public class FileHandle
    {
        private string path = string.Empty;
        public bool File_open()
        {
            string fName = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "mp3文件|*.mp3|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fName = openFileDialog.FileName;
                path = fName;
            }
            if (fName == "")
            {
                return false;
            }
            return true;
        }
        /// <summary>
        ///  读取音频文件内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public FileEntity LoadFileContextInfo()
        {
            ShellClass shellClass = new ShellClass();
            FileEntity fileEntity = new FileEntity();

            Folder dir = shellClass.NameSpace(Path.GetDirectoryName(path));
            FolderItem item = dir.ParseName(Path.GetFileName(path));
            fileEntity.Name = dir.GetDetailsOf(item, 0); // 获取歌曲名称
            fileEntity.Duration = dir.GetDetailsOf(item, 27); //时长
            fileEntity.Bps = dir.GetDetailsOf(item, 28);     //比特率
            fileEntity.FilePath = path;                     //绝对路径
            return fileEntity;
        }

        public void FilePlay(string filepath)
        {
            MP3Player player = new MP3Player(filepath);
            player.Play();
            //Action action = player.Play;
            //action.BeginInvoke(null, null);

            // Console.WriteLine($"结束*************{Thread.CurrentThread.ManagedThreadId.ToString("00")}{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
        }
        /// <summary>
        /// 保存json文本
        /// </summary>
        /// <param name="listfileEntity"></param>
        public void FileSave(List<FileEntity> listfileEntity)
        {
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            string JsonfilePath = System.IO.Directory.GetCurrentDirectory() + "\\Config\\default.Json";
            if (!File.Exists(JsonfilePath))
            {
                FileStream fileStream = new FileStream(JsonfilePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            else
            {
                try
                {
                    File.WriteAllText(JsonfilePath, listfileEntity.ToJson().ToString());
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                
            }
        }

        /// <summary>
        /// 读取默认配置
        /// </summary>
        /// <returns></returns>
        public string FileRead()
        {
            string content = string.Empty;
            string JsonfilePath = System.IO.Directory.GetCurrentDirectory() + "\\Config\\default.Json";
            if (!File.Exists(JsonfilePath))
            {
                FileStream fileStream = new FileStream(JsonfilePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            else
            {
                try
                {
                    content = File.ReadAllText(JsonfilePath);
                }
                catch (Exception ex)
                {
                   
                    throw new Exception(ex.Message);
                }

            }

            return content;
        }
    }
}
