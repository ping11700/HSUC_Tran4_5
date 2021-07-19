using Common.Log4;
using System;
using System.IO;

namespace Common.Utils
{
    public class DirectoryFileHelper
    {
        /// <summary>
        /// 删除文件夹及文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isDelResource"></param>
        public static void DeleteFolderFile(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path)) return;

                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(path);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                System.IO.File.SetAttributes(path, System.IO.FileAttributes.Normal);

                if (Directory.Exists(path))
                {
                    foreach (string f in Directory.GetFileSystemEntries(path))
                    {
                        //如果有子文件删除文件
                        if (File.Exists(f))
                        {
                            string[] strArr = f.Split('\\');

                            File.Delete(f);
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteFolderFile(f);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogService.ILogger.Error($"删除文件失败:{ex.Message}");
            }
        }
    }
}
