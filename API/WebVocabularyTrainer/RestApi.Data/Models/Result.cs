using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Data.Models
{
    public class Result<T>
    {
        public T Output { get; set; }
        public Exception Exception { get; set; }
        public bool IsFine { get; set; }

        public Result(T output, Exception exception = null)
        {
            Output = output;
            Exception = exception;
            if(exception != null)
            {
                IsFine = false;
            }
            else
            {
                IsFine = true;
            }
        }
    }

    public class Result
    {
        public Exception Exception { get; set; }
        public bool IsFine { get; set; }

        public Result(Exception exception = null)
        {
            Exception = exception;
            if (exception != null)
            {
                IsFine = false;
            }
            else
            {
                IsFine = true;
            }
        }
    }
}
