using System;
using System.Windows;
using System.Windows.Media;

namespace Common.Resources.CommonResources
{
    public class CommonResources
    {
        public static ResourceDictionary CommonResourcesDictionary = new ResourceDictionary
        {
            Source = new Uri("/Common;component/Resources/CommonResources/CommonResources.xaml", UriKind.RelativeOrAbsolute) // 指定样式文件的路径
        };


        //常用窗体圆角01
        public static CornerRadius CornerRadius01 = (CornerRadius)CommonResourcesDictionary["Common_CornerRadius01"];

        //常用暗色01
        public static SolidColorBrush Common_Dark01 = CommonResourcesDictionary["Common_Dark01"] as SolidColorBrush;
        //常用暗色02
        public static SolidColorBrush Common_Dark02 = CommonResourcesDictionary["Common_Dark02"] as SolidColorBrush;
        //常用暗色03
        public static SolidColorBrush Common_Dark03 = CommonResourcesDictionary["Common_Dark03"] as SolidColorBrush;
        //常用红色
        public static SolidColorBrush Common_Red = CommonResourcesDictionary["Common_Red"] as SolidColorBrush;
        //常用绿色
        public static SolidColorBrush Common_Green = CommonResourcesDictionary["Common_Green"] as SolidColorBrush;
        //常用白色
        public static SolidColorBrush Common_White = CommonResourcesDictionary["Common_White"] as SolidColorBrush;
        //常用金色01
        public static SolidColorBrush Common_Golden01 = CommonResourcesDictionary["Common_Golden01"] as SolidColorBrush;
        //常用金色02
        public static SolidColorBrush Common_Golden02 = CommonResourcesDictionary["Common_Golden02"] as SolidColorBrush;
        //常用灰白
        public static SolidColorBrush Common_GrayWhite = CommonResourcesDictionary["Common_GrayWhite"] as SolidColorBrush;
        //常用灰色
        public static SolidColorBrush Common_Gray = CommonResourcesDictionary["Common_Gray"] as SolidColorBrush;
        //常用蓝色01
        public static SolidColorBrush Common_Blue01 = CommonResourcesDictionary["Common_Blue01"] as SolidColorBrush;


        //置顶
        public static StreamGeometry FixGeometry = CommonResourcesDictionary["FixGeometry"] as StreamGeometry;
        //取消置顶
        public static StreamGeometry FixedGeometry = CommonResourcesDictionary["FixedGeometry"] as StreamGeometry;
        //最大化
        public static StreamGeometry MaxGeometry = CommonResourcesDictionary["MaxGeometry"] as StreamGeometry;
        //Normal
        public static StreamGeometry NormalGeometry = CommonResourcesDictionary["NormalGeometry"] as StreamGeometry;

        //暂停
        public static StreamGeometry PauseGeometry = CommonResourcesDictionary["PauseGeometry"] as StreamGeometry;
        //播放
        public static StreamGeometry PlayGeometry = CommonResourcesDictionary["PlayGeometry"] as StreamGeometry;



        //音量无
        public static StreamGeometry NoVolumeGeometry = CommonResourcesDictionary["NoVolumeGeometry"] as StreamGeometry;
        //音量小
        public static StreamGeometry MinVolumeGeometry = CommonResourcesDictionary["MinVolumeGeometry"] as StreamGeometry;
        //音量中
        public static StreamGeometry NormalVolumeGeometry = CommonResourcesDictionary["NormalVolumeGeometry"] as StreamGeometry;
        //音量大
        public static StreamGeometry MaxVolumeGeometry = CommonResourcesDictionary["MaxVolumeGeometry"] as StreamGeometry;

        //全屏
        public static StreamGeometry FullScreenGeometry = CommonResourcesDictionary["FullScreenGeometry"] as StreamGeometry;
        //非全屏
        public static StreamGeometry RestoreScreenGeometry = CommonResourcesDictionary["RestoreScreenGeometry"] as StreamGeometry;


    }
}
