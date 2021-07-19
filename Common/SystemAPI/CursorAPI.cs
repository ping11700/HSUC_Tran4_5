using System.Windows;

namespace Common.SystemAPI
{
    public class CursorAPI
    {
        public static Point GetCursorPos()
        {
            var result = default(Point);
            if (Win32API.GetCursorPos(out POINT point))
            {
                result.X = point.x;
                result.Y = point.y;
            }
            return result;
        }
    }
}
