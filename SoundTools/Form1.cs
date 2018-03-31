using SoundTools.Common;
using SoundTools.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SoundTools
{
    public partial class Frm_main : DSkin.Forms.DSkinForm
    {
        public Frm_main()
        {
            InitializeComponent();
        }

        private void add_sound_Click(object sender, EventArgs e)
        {
            FileHandle filehandle = new FileHandle();
            FileEntity fileEntity = null;
           
            string StrConfig = string.Empty;
            if (filehandle.File_open())
            {
                fileEntity  = filehandle.LoadFileContextInfo();
                InsertTable(fileEntity);
                StrConfig = filehandle.FileRead();
                if (StrConfig == "")
                {
                    //如果不存在内容直接保存
                    List<FileEntity> Lfe = new List<FileEntity>();
                    Lfe.Add(fileEntity);
                    filehandle.FileSave(Lfe);
                }
                else
                {
                    //如果存在内容先转换对象后再保存
                    List<FileEntity> Lfe = Json.ToList<FileEntity>(StrConfig);
                    Lfe.Add(fileEntity);
                    filehandle.FileSave(Lfe);
                }
            }

        }

        public void InsertTable(FileEntity fileEntity)
        {
            #region DGV增加一行
            int rowid = dSkinDataGridView1.Rows.Add();
            //DataGridViewRow row  = dSkinDataGridView1.Rows[rowid];
            dSkinDataGridView1.Rows[rowid].Cells[0].Value = fileEntity.Name;
            dSkinDataGridView1.Rows[rowid].Cells[1].Value = fileEntity.Duration;
            dSkinDataGridView1.Rows[rowid].Cells[2].Value = "";
            dSkinDataGridView1.Rows[rowid].Cells[3].Value = fileEntity.FilePath;
            #endregion
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Console.WriteLine(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void tsb_play_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            int row  = dSkinDataGridView1.CurrentRow.Index;

            path = dSkinDataGridView1.Rows[row].Cells[3].Value.ToString();
            if (path != "")
            {
                FileHandle fileHandle = new FileHandle();
                fileHandle.FilePlay(path);
            }

        }
    }
}
