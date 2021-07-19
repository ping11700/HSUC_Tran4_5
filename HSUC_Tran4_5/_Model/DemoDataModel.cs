using System.Collections.Generic;

namespace HSUC_Tran4_5._Model
{
    public class DemoDataModel
    {
        public int Index { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public string Remark { get; set; }

        public DemoType Type { get; set; }

        public string ImgPath { get; set; }

        public List<DemoDataModel> DataList { get; set; }
    }


    public class LogDataModel
    {
        public int Index { get; set; }

        public LogTypeEnum LogType { get; set; }


        public string Message { get; set; }


    }
}
