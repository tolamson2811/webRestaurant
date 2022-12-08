using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Shared.Contract
{
    /// <summary>
    /// Trả về
    /// </summary>
    public class CustomResponseMessage
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
