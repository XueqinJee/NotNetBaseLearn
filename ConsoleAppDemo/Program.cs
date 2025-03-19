
using ConsoleAppDemo;
using ConsoleAppDemo.Config.Extensions;
using ConsoleAppDemo.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

var host = Host.CreateDefaultBuilder()
    .ConfigureHostConfiguration(options => {
        options.AddJsonFile("config.json", optional: false, reloadOnChange: true);

        // 使用内存配置提供程序
        options.AddEntityCacheConfiguration(db => {
            db.UseInMemoryDatabase("Memory");
        });

        var loggerFactory = LoggerFactory.Create(builder => builder.AddSimpleConsole());
        var logger = loggerFactory.CreateLogger<Program>();

        logger.LogInformation("加载主机配置....");
        // 监听配置文件变化，此监听项是整个文件。
        var configRoot = options.Build();
        ChangeToken.OnChange(() => configRoot.GetSection(BookOption.Book).GetReloadToken(), () => {
            logger.LogInformation($"监听config.json Book.Name：{configRoot["Book:Name"]}");
        });
    })
    .ConfigureLogging(log => {
        log.ClearProviders()
            .AddSimpleConsole();
    })
    .ConfigureServices((context, services) => {

        services.AddHostedService<MyHostService>();

        services.AddOptions<BookOption>()
            .Bind(context.Configuration.GetSection(BookOption.Book))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<BookOption>, BookValidateOption>());
    })
    .Build();

host.Run();