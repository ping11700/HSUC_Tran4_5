using System.Windows;

namespace Common
{
    /// <summary>
    /// Int+Int类型数据组成的数据结构
    /// </summary>
    public struct TwoInt
    {
        public int V1 { get; set; }
        public int V2 { get; set; }
    }

    /// <summary>
    /// Int+Int类型数据组成的数据结构
    /// </summary>
    public struct ThreeInt
    {
        public int V1 { get; set; }
        public int V2 { get; set; }
        public int V3 { get; set; }
    }

    /// <summary>
    /// double+double类型数据组成的数据结构
    /// </summary>
    public struct FourDouble
    {
        public double V1 { get; set; }
        public double V2 { get; set; }
        public double V3 { get; set; }
        public double V4 { get; set; }
    }

    /// <summary>
    /// String+String类型数据组成的数据结构
    /// </summary>
    public struct TwoString
    {
        public string V1 { get; set; }
        public string V2 { get; set; }
    }

    /// String+String + String类型数据组成的数据结构
    /// </summary>
    public struct ThreeString
    {
        public string V1 { get; set; }
        public string V2 { get; set; }
        public string V3 { get; set; }
    }

    /// <summary>
    /// Int+String类型数据组成的数据结构
    /// </summary>
    public struct IntString
    {
        public int V1 { get; set; }
        public string V2 { get; set; }
    }

    public class FunctionEventArgs<T> : RoutedEventArgs
    {
        public FunctionEventArgs(T info)
        {
            Info = info;
        }

        public FunctionEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }

        public T Info { get; set; }
    }


}
