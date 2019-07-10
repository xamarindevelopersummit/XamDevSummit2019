using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MSC.CM.XaSh.Helpers
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            var retVal = new ObservableCollection<T>();
            foreach (var item in list)
            {
                retVal.Add(item);
            }
            return retVal;
        }
    }
}