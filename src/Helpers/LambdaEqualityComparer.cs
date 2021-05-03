using System;
using System.Collections.Generic;

namespace Helpers
{
    public class LambdaEqualityComparer<T>: IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _areEqual;
        private readonly Func<T, int> _getHashCode;

        public LambdaEqualityComparer(Func<T, T, bool> areEqual, Func<T, int> getHashCode)
        {
            _areEqual = areEqual;
            _getHashCode = getHashCode;
        }
        
        public bool Equals(T x, T y)
        {
            return _areEqual(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _getHashCode(obj);
        }
    }
}
