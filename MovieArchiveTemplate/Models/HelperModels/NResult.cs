using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieArchiveTemplate.Models.HelperModels
{
    public class NResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }

    public class NResult<T> : NResult
    {
        public T Data { get; set; }
    }
}