using System;
using System.Collections.Generic;
using System.Text;

namespace Truelayer.Utils
{
   public class Generic
    {

        public class Wrapper<T>
            where T : class
        {
            public string Status { get; set; }

            public string Message { get; set; }
            /// <summary>
            /// Stores a single object
            /// </summary>
            public T SingleObject { get; set; }
            /// <summary>
            /// Stores a collection of objects
            /// </summary>
            public IList<T> ObjectCollection { get; set; }

            public Wrapper()
            {
                SingleObject = null;
                ObjectCollection = new List<T>();
                Status = Consts.FAIL;
                Message = Consts.FAIL;
            }
        }
    }
}
