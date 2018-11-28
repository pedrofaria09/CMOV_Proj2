using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WeatherApp.Services
{
    class State
    {
        public HttpStatusCode Status { get; set; }
        public string Response { get; set; }
        public WebRequest Request { get; set; }
    }
}
