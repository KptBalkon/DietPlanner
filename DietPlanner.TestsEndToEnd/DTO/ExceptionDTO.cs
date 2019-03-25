using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DietPlanner.TestsEndToEnd.DTO
{
    public class ExceptionDTO
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
