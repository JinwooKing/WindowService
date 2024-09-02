
#### Windows 서비스 만들기

#### 1. NuGet 패키지 설치    Microsoft.Extensions.Hosting
    Microsoft.Extensions.Hosting.WindowsServices
#### 2. .csproj 파일 아래 문구 추가
    <RootNamespace>App.WindowsService</RootNamespace>
    <OutputType>exe</OutputType>
    <PublishSingleFile Condition="'$(Configuration)' == 'Release'">true</PublishSingleFile>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
    <PlatformTarget>x64</PlatformTarget>
#### 3. Program.cs 파일 아래와 같이 수정
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
#### 4. 서비스 등록 및 실행
    sc <서버>create [서비스 이름] [binPath= ] <옵션1> <옵션2>...
    sc <서버> start [서비스 이름] <인수1> <인수2> ...