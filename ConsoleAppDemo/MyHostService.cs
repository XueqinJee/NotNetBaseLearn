using ConsoleAppDemo.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDemo {
    internal class MyHostService : IHostedService {
        private readonly ILogger<MyHostService> _logger;
        private readonly IOptionsMonitor<BookOption> _bookOption;
        private readonly IConfiguration _configuration;

        public MyHostService(ILogger<MyHostService> logger, IOptionsMonitor<BookOption> bookOption, IConfiguration configuration) {
            _logger = logger;
            _bookOption = bookOption;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            var val = _bookOption.CurrentValue;
            _logger.LogInformation($"Name：{val.Name}");
            _logger.LogInformation($"Author：{val.Author}");
            _logger.LogInformation($"Age：{val.Age}");
            _logger.LogInformation($"Name：{val.Name}");

            _logger.LogInformation($"Test: {_configuration["Test:A"]}");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) {

            return Task.CompletedTask;
        }
    }
}
