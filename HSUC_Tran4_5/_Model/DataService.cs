using System.Collections.Generic;

namespace HSUC_Tran4_5._Model
{
    public class DataService
    {
        public List<DemoDataModel> GetDemoDataList()
        {
            var list = new List<DemoDataModel>();
            for (var i = 1; i <= 20; i++)
            {
                var dataList = new List<DemoDataModel>();
                for (var j = 0; j < 3; j++)
                {
                    dataList.Add(new DemoDataModel
                    {
                        Index = j,
                        IsSelected = j % 2 == 0,
                        Name = $"SubName{j}",
                        Type = (DemoType)j
                    });
                }
                var model = new DemoDataModel
                {
                    Index = i,
                    IsSelected = i % 2 == 0,
                    Name = $"Name{i}",
                    Type = (DemoType)i,
                    DataList = dataList,
                    //ImgPath = $"/HandyControlDemo;component/Resources/Img/Avatar/avatar{i % 6 + 1}.png",
                    Remark = new string(i.ToString()[0], 10)
                };
                list.Add(model);
            }

            return list;
        }


        public List<LogDataModel> GetLogDataList()
        {
            var list = new List<LogDataModel>();
            for (var i = 1; i <= 20; i++)
            {
                var model = new LogDataModel
                {
                    Index = i,
                    LogType = (LogTypeEnum)i,
                    //ImgPath = $"/HandyControlDemo;component/Resources/Img/Avatar/avatar{i % 6 + 1}.png",
                    Message = new string(i.ToString()[0], 10)
                };
                list.Add(model);
            }

            return list;
        }
    }
}
