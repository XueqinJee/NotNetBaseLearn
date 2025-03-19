using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using NotNetBaseLearn;
using NotNetBaseLearn.Options;

var host = Host.CreateDefaultBuilder()
    .ConfigureHostConfiguration(options => {

        options.SetBasePath(Directory.GetCurrentDirectory());
        options.AddJsonFile("appsettings.json", true, reloadOnChange: true);

        options.AddEntityConfiguration(opt => {
            opt.UseInMemoryDatabase("_MemoryDatabase");
        });

        var configRoot = options.Build();
        // 配置文件变化监听
        ChangeToken.OnChange(() => configRoot.GetReloadToken(), () => {
            Console.WriteLine("================监听=============================");
            Console.WriteLine($"Name：{configRoot["Book:Name"]}");
        });
    })
    .ConfigureServices((hostContext, services) => {

        services.AddHostedService<Main>();
        services.AddOptions<BookOptions>()
            .Bind(hostContext.Configuration.GetSection(BookOptions.Book))
            .ValidateDataAnnotations()          // 注解校验
            .Validate(x => {                    // 自定义校验
                if(x.Name == "著名") {
                    
                    return false;
                }
                return true;
            })                                  // 更为复杂的校验方式
            .ValidateOnStart();

        services.AddOptions<SettingOption>()
            .Bind(hostContext.Configuration.GetSection("Setting"));

        // 启用复杂验证
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<BookOptions>, BookOptionsValidated>());

    }).Build();


host.Run();