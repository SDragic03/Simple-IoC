using System;
using System.Collections.Generic;

namespace IoCWebApp.Classes
{
    static class SingletonStorage
    {

        #region Methods

        public static void  Add( object o)
        {
            _dictionary.Add(o.GetType(), o);
        }

        public static object Get(Type t)
        {
            object temp;

            return _dictionary.TryGetValue(t, out temp) ? temp : null;
        }

        #endregion

        private static readonly Dictionary<Type, object> _dictionary = new Dictionary<Type, object>();
    }
}
