using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Common.models
{
    public class Response
    {
        public bool InSucces { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

    }
}
