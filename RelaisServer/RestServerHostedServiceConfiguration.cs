using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RelaisServer
{
    public class RestServerHostedServiceConfiguration
    {
        public int Port { get; set; }
        public RelayConfiguration RelayConfiguration { get; set; }
    }
}
