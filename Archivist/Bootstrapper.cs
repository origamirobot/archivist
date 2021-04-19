using Archivist.Core.Configuration;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics;
using System.IO;

namespace Archivist
{

	/// <summary>
	/// Registers dependencies needed by this application.
	/// </summary>
	public static class Bootstrapper
	{

		/// <summary>
		/// Kicks off the dependency bootstrapping process.
		/// </summary>
		/// <returns></returns>
		public static IHost Bootstrap()
		{
			IConfigurationRoot root = null;
			var host = Host.CreateDefaultBuilder()
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureAppConfiguration((ctx, config) =>
				{
					using (var processModule = Process.GetCurrentProcess().MainModule)
					{
						var path = Path.GetDirectoryName(processModule?.FileName);
						config
							.SetBasePath(path)
							.AddJsonFile("appsettings.json", optional: true);
						root = config.Build();
					}


				})
				.ConfigureLogging(builder =>
				{
					//builder.AddFilter("Microsoft", LogLevel.Warning).AddFilter("System", LogLevel.Warning);
					Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(root).CreateLogger();
					builder.AddSerilog();
				})
				.ConfigureServices((context, services) =>
				{
					var builder = new ContainerBuilder();
					services.AddOptions();
					builder.Populate(services);
					builder.RegisterModule<CoreModule>();
					var container = builder.Build();
					var csl = new AutofacServiceLocator(container);
					ServiceLocator.SetLocatorProvider(() => csl);
				})
				.Build();
			return host;
		}

	}

}
