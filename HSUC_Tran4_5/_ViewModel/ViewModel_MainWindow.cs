using Common;
using HSUC_Tran4_5._Model;

namespace HSUC_Tran4_5._ViewModel
{
    public partial class ViewModel_MainWindow : ViewModelBase
    {

        public DataService DataSer { get; init; }

        internal ViewModel_MainWindow()
        {
            DataSer = new DataService();
        }


        public override void Init()
        {
            Start_VM01();
        }



        public override void Dispose()
        {

        }



    }
}
