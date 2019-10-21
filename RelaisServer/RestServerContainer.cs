using Jens.InversionOfControl;
using Jens.RestServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelaisServer
{
    /// <summary>
    /// Wrapper for inversion of control container.
    /// </summary>
    public class RestServerContainer : Container, IRestServerDependencyResolver
    {
    }
}
