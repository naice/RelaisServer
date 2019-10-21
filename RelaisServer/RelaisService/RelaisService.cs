using Jens.RestServer;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;

namespace RelaisServer.RelaisService
{
    [RestServerServiceInstance(RestServerServiceInstanceType.SingletonStrict)]
    internal class RelaisService : RestServerService
    {
        private readonly RestServerHostedServiceConfiguration _config;
        private readonly GpioController _gpio;

        public RelaisService(RestServerHostedServiceConfiguration configuration)
        {
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _gpio = new GpioController(PinNumberingScheme.Logical, new System.Device.Gpio.Drivers.RaspberryPi3Driver());
            foreach (var pin in _config.RelayConfiguration.Pins)
            {
                _gpio.OpenPin(pin.Pin, PinMode.Output);
                SetPinOutput(pin);
            }
        }

        [RestServerServiceCall("/getconfiguration")]
        public RestServerHostedServiceConfiguration GetConfiguration()
        {
            return _config;
        }

        [RestServerServiceCall("/relais")]
        public RelaisResponse SetOrToggleRelais(RelaisRequest request)
        {
            return SetOrToggleRelaisName(request);   
        }

        [RestServerServiceCall("/relais/{Name}")]
        public RelaisResponse SetOrToggleRelaisName(RelaisRequest request)
        {
            var pinConfig = _config?.RelayConfiguration?.Pins?.FirstOrDefault(pin => pin?.Name?.ToLowerInvariant() == request.Name?.ToLowerInvariant());
            if (pinConfig == null)
                return MakeError($"No relais configuration found for {request.Name ?? "NULL"}");

            pinConfig.State = request.State ?? !pinConfig.State;
            SetPinOutput(pinConfig);

            return MakeSuccess($"New state is {pinConfig.State}.");
        }

        private void SetPinOutput(PinConfiguration pinConfig)
        {
            _gpio.Write(pinConfig.Pin, pinConfig.State ? PinValue.Low : PinValue.High);
        }

        private static RelaisResponse MakeError(string message) => new RelaisResponse() { Message = message, Success = false };
        private static RelaisResponse MakeSuccess(string message = null) => new RelaisResponse() { Message = message, Success = true };
    }
}
