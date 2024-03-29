﻿using Jens.RestServer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelaisServer
{
    public class ILoggerAdapter : Jens.RestServer.ILogger
    {
        private readonly ILogger<RestServerHostedService> _logger;
        public ILoggerAdapter(ILogger<RestServerHostedService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void Error(string message)
        {
            _logger.LogError(message);
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }

        public void Warn(string message)
        {
            _logger.LogWarning(message);
        }
    }
}
