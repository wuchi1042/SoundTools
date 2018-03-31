using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundTools.Model
{
    public class FileEntity
    {
        //文件名称
        public string Name { get; set; }
        //比特率
        public string Bps { get; set; }
        //时长
        public string Duration { get; set; }
        //文件路径
        public string FilePath { get; set; }

        //快捷键
        public string ShortcutsKey { get; set; }
    }
}
