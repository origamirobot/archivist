namespace Archivist.Core.IO
{

	/// <summary>
	/// Wraps up the <see cref="IDirectoryUtility"/>, <see cref="IFileUtility"/>, and <see cref="IPathUtility"/> into one class.
	/// </summary>
	public interface IIoUtility
	{

		/// <summary>
		/// Gets the directory.
		/// </summary>
		IDirectoryUtility Directory { get; }

		/// <summary>
		/// Gets the file.
		/// </summary>
		IFileUtility File { get;  }

		/// <summary>
		/// Gets the path.
		/// </summary>
		IPathUtility Path { get;  }

	}

}
