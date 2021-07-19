using Common.Log4;
using System;
using System.Collections.Generic;
using System.IO;

namespace Common.Utils
{
    public class IOHelper
    {
        #region 直接删除指定目录下的所有文件及文件夹(保留目录)

        /// <summary>
        /// 清空文件及文件夹
        /// </summary>
        /// <param name="dic">文件夹路径</param>
        public static void DeleteDir(string dic)
        {
            try
            {
                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(dic);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                //去除文件的只读属性
                System.IO.File.SetAttributes(dic, System.IO.FileAttributes.Normal);

                //判断文件夹是否还存在
                if (Directory.Exists(dic))
                {
                    foreach (string f in Directory.GetFileSystemEntries(dic))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            File.Delete(f);
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteDir(f);
                        }
                    }

                    //删除空文件夹
                    Directory.Delete(dic);
                }

            }
            catch (Exception ex) // 异常处理
            {
                LogService.ILogger.Warn($"(清空文件及文件夹)DeleteDir failed : {ex.Message} ");

            }
        }
        #endregion



        #region 筛选指定目录下的所有文件及文件夹下的文件
        /// <summary>
        /// 筛选文件
        /// </summary>
        /// <param name="dic">文件夹路径</param>
        /// <param name="ext">文件扩展名</param>
        public static void FilterFile(string dic, string ext, ref List<string> list)
        {
            try
            {
                //判断文件夹是否还存在
                if (Directory.Exists(dic))
                {
                    foreach (string f in Directory.GetFileSystemEntries(dic))
                    {
                        if (File.Exists(f))
                        {
                            if (ext.Equals(Path.GetExtension(f)))
                                list.Add(f);
                        }
                        else
                        {
                            //循环递归筛选
                            FilterFile(f, ext, ref list);
                        }
                    }
                }

            }
            catch (Exception ex) // 异常处理
            {
                LogService.ILogger.Warn($"(筛选文件)FilterFile failed : {ex.Message} ");
            }
        }

        #endregion
    }
}
