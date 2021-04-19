using Archivist.Core.IO;
using Archivist.Core.Services;
using Archivist.Core.Utilities;
using Autofac;
using System.Net.Http;

namespace Archivist.Core.Configuration
{

	/// <summary>
	/// Autofac module used to quickly register core dependencies.
	/// </summary>
	/// <seealso cref="Autofac.Module" />
	public class CoreModule : Module
	{

		/// <summary>
		/// Override to add registrations to the container.
		/// </summary>
		/// <param name="builder">The builder through which components can be
		/// registered.</param>
		/// <remarks>
		/// Note that the ContainerBuilder parameter is unique to this module.
		/// </remarks>
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<DirectoryUtility>().As<IDirectoryUtility>();
			builder.RegisterType<FileUtility>().As<IFileUtility>();
			builder.RegisterType<PathUtility>().As<IPathUtility>();
			builder.RegisterType<IoUtility>().As<IIoUtility>();
			builder.RegisterType<NewtonsoftSerializer>().As<IJsonSerializer>();
			builder.RegisterType<HttpUtilityWrapper>().As<IHttpUtility>();

			var httpClient = new HttpClient();
			builder.RegisterInstance(httpClient).As<HttpClient>();

			builder.RegisterType<WaybackClient>().As<IWaybackClient>();
		}

	}

}
