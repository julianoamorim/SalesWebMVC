using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class ResponseWrapper<T>
    {
        public bool Sucess { get; set; } = true;
        public int Status { get; set; }
        public T Data {get;set;}
    }
}