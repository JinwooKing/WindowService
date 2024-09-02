
#### Windows ���� �����

#### 1. NuGet ��Ű�� ��ġ    Microsoft.Extensions.Hosting
    Microsoft.Extensions.Hosting.WindowsServices
#### 2. .csproj ���� �Ʒ� ���� �߰�
    <RootNamespace>App.WindowsService</RootNamespace>
    <OutputType>exe</OutputType>
    <PublishSingleFile Condition="'$(Configuration)' == 'Release'">true</PublishSingleFile>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
    <PlatformTarget>x64</PlatformTarget>
#### 3. Program.cs ���� �Ʒ��� ���� ����
    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService(options =>
        {
            options.ServiceName = "WorkerService";
        })
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
        })
        .Build();

    //var builder = Host.CreateApplicationBuilder(args);
    //builder.Services.AddHostedService<Worker>();

    //var host = builder.Build();
#### 4. ���� ��� �� ����
    sc <����>create [���� �̸�] [binPath= ] <�ɼ�1> <�ɼ�2>...
    sc <����> start [���� �̸�] <�μ�1> <�μ�2> ...