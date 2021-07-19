using Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Common
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        private Dictionary<string, Action> _propertyActionDic = new Dictionary<string, Action>();  //属性变化通知字典，值为此属性的变更通知事件列表

        //注册属性变更通知
        public void RegisterPropertyNotification(string propertyName, Action action)
        {
            UAssert.Assert(GetType().GetProperty(propertyName) != null);

            if (_propertyActionDic.ContainsKey(propertyName))
            {
                if (!_propertyActionDic[propertyName].Equals(action))
                    _propertyActionDic[propertyName] += action;
            }
            else
            {
                _propertyActionDic.Add(propertyName, action);
            }
        }
        //取消属性变更通知
        public void DeregisterPropertyNotification(string propertyName, Action action)
        {
            UAssert.Assert(GetType().GetProperty(propertyName) != null);

            if (_propertyActionDic.ContainsKey(propertyName))
            {
                _propertyActionDic[propertyName] -= action;

                if (_propertyActionDic[propertyName] == null)
                {
                    _propertyActionDic.Remove(propertyName);
                }
            }
        }

        public void ClearPropertyChangedActions()
        {
            _propertyActionDic.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (_propertyActionDic.ContainsKey(propertyName))
            {
                _propertyActionDic[propertyName].Invoke();
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NotifyPropertyChanged<T>(Expression<Func<T>> expr)
        {
            var propertyName = PropertyNameHelper.GetPropertyName(expr);
            this.NotifyPropertyChanged(propertyName);
        }
    }
}
