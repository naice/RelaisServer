using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RelaisServer
{
    public class RestServerHostedServiceConfiguration
    {
        public int Port { get; set; }
        public RelaisConfiguration RelayConfiguration { get; set; }
    }
}
