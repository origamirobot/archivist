using System;
using System.Collections.Generic;
using System.Text;

namespace Archivist.Core.IO
{

	/// <summary>
	/// Wraps up the <see cref="IDirectoryUtility"/>, <see cref="IFileUtility"/>, and <see cref="IPathUtility"/> into one class.
	/// </summary>
	/// <seealso cref="Archivist.Core.IO.IIoUtility" />
	public class IoUtility : IIoUtility
	{

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets the directory.
		/// </summary>
		public IDirectoryUtility Directory { get; private set; }

		/// <summary>
		/// Gets the file.
		/// </summary>
		public IFileUtility File { get; private set; }

		/// <summary>
		/// Gets the path.
		/// </summary>
		public IPathUtility Path { get; private set; }


		#endregion PUBLIC ACCESSORS

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="IoUtility"/> class.
		/// </summary>
		public IoUtility(
			IDirectoryUtility directoryUtility,
			IFileUtility fileUtility,
			IPathUtility pathUtility)
		{
			Directory = directoryUtility;
			Path = pathUtility;
			File = fileUtility;
		}


		#endregion CONSTRUCTORS

	}

}
