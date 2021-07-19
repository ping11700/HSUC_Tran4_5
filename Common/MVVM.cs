using System;
using System.Collections.Generic;

namespace Common
{

    public interface IModel
    {
        void AddViewModel(IViewModel vm);
        void RemoveViewModel(IViewModel vm);

        void ClearViewModels();

        void NotifyViewModels();

        void Init();

        void Dispose();

        //获取该Model的所有ViewModel
        List<IViewModel> ViewModels { get; }

    }

    public interface IViewModel
    {
        void SetView(IView v);
        void RemoveView();

        void NotifyView();

        void Refresh();

        //获取该ViewModel对应的View
        IView View { get; }

        void Init();

        void Dispose();
    }

    public interface IView
    {
        void Refresh();

        void Init();

        void Dispose();
    }

    public class ModelBase : NotifyPropertyChangedBase, IModel
    {
        protected List<IViewModel> _vms = new List<IViewModel>();

        public List<IViewModel> ViewModels
        {
            get
            {
                return _vms;
            }
        }

        public virtual void AddViewModel(IViewModel vm)
        {
            UAssert.Assert(!_vms.Contains(vm));

            _vms.Add(vm);
        }

        public virtual void RemoveViewModel(IViewModel vm)
        {
            UAssert.Assert(_vms.Contains(vm));

            _vms.Remove(vm);
        }

        public virtual void NotifyViewModels()
        {
            foreach (var vm in ViewModels)
            {
                vm.Refresh();
            }
        }

        public void ClearViewModels()
        {
            _vms.Clear();
        }

        public virtual void Init()
        {
        }

        public virtual void Dispose()
        {
        }
    }

    public class ViewModelBase : NotifyPropertyChangedBase, IViewModel, IDisposable
    {
        protected IView _view = null;

        public IView View
        {
            get
            {
                return _view;
            }
        }

        public virtual void SetView(IView v)
        {
            if (_view != null)
                RemoveView();

            UAssert.Assert(v != null);

            _view = v;
        }

        public virtual void RemoveView()
        {
            _view = null;
        }

        public virtual void NotifyView()
        {
            if (_view != null)
            {
                _view.Refresh();
            }
        }

        public virtual void Refresh()
        {
            NotifyView();
        }

        public virtual void Init()
        {
        }

        public virtual void Dispose()
        {
        }
    }

    public class ViewBase : NotifyPropertyChangedBase, IView
    {
        public virtual void Refresh()
        {

        }

        public virtual void Init()
        {
        }

        public virtual void Dispose()
        {
        }
    }
}
