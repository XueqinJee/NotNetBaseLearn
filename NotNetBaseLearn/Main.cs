using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotNetBaseLearn.Options;

namespace NotNetBaseLearn;

public class Main : IHostedService {
    private readonly ILogger<Main> _logger;
    private readonly IOptionsMonitor<BookOptions> _options;

    public Main(ILogger<Main> logger, IOptionsMonitor<BookOptions> options, IOptions<SettingOption> settingOption) {
        _options = options;
        _logger = logger;

        Console.WriteLine(settingOption.Value.Key);
    }

    public Task StartAsync(CancellationToken cancellationToken) {
        _logger.LogInformation("Starting Main");
        Task.Run(() => {
            while (true) {

                _logger.LogInformation($"{DateTime.Now:hh:mm:ss:fff tt} 值：{_options.CurrentValue.Name}");
                Thread.Sleep(100000);
            }
        });
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }
}