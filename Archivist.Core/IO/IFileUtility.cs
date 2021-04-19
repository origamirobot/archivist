using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Archivist.Core.IO
{

	/// <summary>
	/// Defines a contact to use in place of <see cref="System.IO.File"/> for testibility.
	/// </summary>
	public interface IFileUtility
	{

		/// <summary>
		/// Computes an MD5 hash on the data provided.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		String ComputeMd5Hash(Byte[] data);

		/// <summary>
		/// Appends lines to a file, and then closes the file. If the specified file does not exist, this method creates a file,
		/// writes the specified lines to the file, and then closes the file.
		/// </summary>
		/// <param name="path">The file to append the lines to. The file is created if it doesn't already exist.</param>
		/// <param name="contents">The lines to append to the file.</param>
		void AppendAllLines(string path, IEnumerable<string> contents);

		/// <summary>
		/// Appends lines to a file by using a specified encoding, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
		/// </summary>
		/// <param name="path">The file to append the lines to. The file is created if it doesn't already exist.</param>
		/// <param name="contents">The lines to append to the file.</param>
		/// <param name="encoding">The character encoding to use.</param>
		void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding);

		/// <summary>
		/// Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.
		/// </summary>
		/// <param name="path">The file to append the specified string to.</param>
		/// <param name="contents">The string to append to the file</param>
		void AppendAllText(string path, string contents);

		/// <summary>
		/// Appends the specified string to the file, creating the file if it does not already exist
		/// </summary>
		/// <param name="path">The file to append the specified string to.</param>
		/// <param name="contents">The string to append to the file</param>
		/// <param name="encoding">The character encoding to use</param>
		void AppendAllText(string path, string contents, Encoding encoding);

		/// <summary>
		/// Creates a StreamWriter that appends UTF-8 encoded text to an existing file, or to a new file if the specified file does not exist
		/// </summary>
		/// <param name="path">The path to the file to append to.</param>
		/// <returns>A stream writer that appends UTF-8 encoded text to the specified file or to a new file.</returns>
		StreamWriter AppendText(string path);

		/// <summary>
		/// Copies an existing file to a new file. Overwriting a file of the same name is not allowed.
		/// </summary>
		/// <param name="sourceFileName">The file to copy.</param>
		/// <param name="destFileName">The name of the destination file. This cannot be a directory or an existing file</param>
		void Copy(string sourceFileName, string destFileName);

		/// <summary>
		/// Copies an existing file to a new file. Overwriting a file of the same name is allowed
		/// </summary>
		/// <param name="sourceFileName">The file to copy</param>
		/// <param name="destFileName">The name of the destination file. This cannot be a directory</param>
		/// <param name="overwrite">true if the destination file can be overwritten; otherwise, false</param>
		void Copy(string sourceFileName, string destFileName, bool overwrite);

		/// <summary>
		/// Creates or overwrites the specified file, specifying a buffer size and a FileOptions value that describes how to create or overwrite the file
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="bufferSize">Size of the buffer.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		FileStream Create(string path, int bufferSize, FileOptions options);

		/// <summary>
		/// Creates or overwrites the specified file
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="bufferSize">Size of the buffer.</param>
		/// <returns></returns>
		FileStream Create(string path, int bufferSize);

		/// <summary>
		/// Creates or overwrites a file in the specified path
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		FileStream Create(string path);

		/// <summary>
		/// Creates or opens a file for writing UTF-8 encoded text
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		StreamWriter CreateText(string path);

		/// <summary>
		/// Returns a unique name for a file.
		/// </summary>
		/// <param name="folder">The folder to get this file name for.</param>
		/// <param name="desiredFileName">The desired file name.</param>
		/// <param name="maxAttempts">The maximum number of attempts.</param>
		/// <returns>Returns a unique name for the specified file.</returns>
		/// <exception cref="System.Exception">Could not create unique filename in  + maxAttempts +  attempts</exception>
		String CreateUniqueFileName(String folder, String desiredFileName, Int32 maxAttempts = 1024);

		/// <summary>
		/// Deletes the specified file
		/// </summary>
		/// <param name="path">The path.</param>
		void Delete(string path);

		/// <summary>
		/// Determines whether the specified file exists
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		bool Exists(string path);

		/// <summary>
		/// Gets the FileAttributes of the file on the path
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		FileAttributes GetAttributes(string path);

		/// <summary>
		/// Gets the file information.
		/// </summary>
		/// <param name="location">The location of the file.</param>
		/// <returns>Returns a <see cref="FileInfo"/> object for the specified file</returns>
		FileInfo GetFileInfo(String location);

		/// <summary>
		/// Returns the creation date and time of the specified file or directory.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		DateTime GetCreationTime(string path);

		/// <summary>
		/// Returns the creation date and time, in coordinated universal time (UTC), of the specified file or directory
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		DateTime GetCreationTimeUtc(string path);

		/// <summary>
		/// Returns the date and time the specified file or directory was last accessed
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		DateTime GetLastAccessTime(string path);

		/// <summary>
		/// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last accessed
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		DateTime GetLastAccessTimeUtc(string path);

		/// <summary>
		/// Returns the date and time the specified file or directory was last written to.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		DateTime GetLastWriteTime(string path);

		/// <summary>
		/// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last written to
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		DateTime GetLastWriteTimeUtc(string path);

		/// <summary>
		/// Moves a specified file to a new location, providing the option to specify a new file name
		/// </summary>
		/// <param name="sourceFilename">The source filename.</param>
		/// <param name="destFilename">The dest filename.</param>
		void Move(string sourceFilename, string destFilename);

		/// <summary>
		/// Opens a FileStream on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="mode">The mode.</param>
		/// <param name="access">The access.</param>
		/// <param name="share">The share.</param>
		/// <returns></returns>
		FileStream Open(string path, FileMode mode, FileAccess access, FileShare share);

		/// <summary>
		/// Opens a FileStream on the specified path, with the specified mode and access
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="mode">The mode.</param>
		/// <param name="access">The access.</param>
		/// <returns></returns>
		FileStream Open(string path, FileMode mode, FileAccess access);

		/// <summary>
		/// Opens a FileStream on the specified path with read/write access
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="mode">The mode.</param>
		/// <returns></returns>
		FileStream Open(string path, FileMode mode);

		/// <summary>
		/// Opens an existing file for reading
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		FileStream OpenRead(string path);

		/// <summary>
		/// Opens an existing UTF-8 encoded text file for reading
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		StreamReader OpenText(string path);

		/// <summary>
		/// Opens an existing file or creates a new file for writing
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		FileStream OpenWrite(string path);

		/// <summary>
		/// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="encoding">The encoding.</param>
		/// <returns></returns>
		string[] ReadAllLines(string path, Encoding encoding);

		/// <summary>
		/// Opens a text file, reads all lines of the file, and then closes the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		string[] ReadAllLines(string path);

		/// <summary>
		/// Reads all bytes from tge specified file.
		/// </summary>
		/// <param name="location">The location of the file.</param>
		/// <returns>Returns a byte array for the specified file</returns>
		byte[] ReadAllBytes(String location);

		/// <summary>
		/// Opens a text file, reads all lines of the file, and then closes the file
		/// </summary>
		/// <param name="location">The file to open for reading.</param>
		/// <returns></returns>
		String ReadAllText(String location);

		/// <summary>
		/// Opens a text file, reads all lines of the file, and then closes the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="encoding">The encoding.</param>
		/// <returns></returns>
		string ReadAllText(string path, Encoding encoding);

		/// <summary>
		/// Sets the specified FileAttributes of the file on the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="attributes">The attributes.</param>
		void SetAttributes(string path, FileAttributes attributes);

		/// <summary>
		/// Sets the date and time the file was created.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="creationTime">The creation time.</param>
		void SetCreationTime(string path, DateTime creationTime);

		/// <summary>
		/// Sets the date and time, in coordinated universal time (UTC), that the file was created
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="creationTime">The creation time.</param>
		void SetCreationTimeUtc(string path, DateTime creationTime);

		/// <summary>
		/// Sets the date and time the specified file was last accessed.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="creationTime">The creation time.</param>
		void SetLastAccessTime(string path, DateTime creationTime);

		/// <summary>
		/// Sets the date and time, in coordinated universal time (UTC), that the specified file was last accessed.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="creationTime">The creation time.</param>
		void SetLastAccessTimeUtc(string path, DateTime creationTime);

		/// <summary>
		/// Sets the date and time that the specified file was last written to.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="creationTime">The creation time.</param>
		void SetLastWriteTime(string path, DateTime creationTime);

		/// <summary>
		/// Sets the date and time, in coordinated universal time (UTC), that the specified file was last written to.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="creationTime">The creation time.</param>
		void SetLastWriteTimeUtc(string path, DateTime creationTime);

		/// <summary>
		/// Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="bytes">The bytes.</param>
		void WriteAllBytes(string path, byte[] bytes);

		/// <summary>
		/// Creates a new file by using the specified encoding, writes a collection of strings to the file, and then closes the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="contents">The contents.</param>
		/// <param name="encoding">The encoding.</param>
		void WriteAllLines(string path, string[] contents, Encoding encoding);

		/// <summary>
		/// Creates a new file, writes the specified string array to the file by using the specified encoding, and then closes the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="contents">The contents.</param>
		void WriteAllLines(string path, string[] contents);

		/// <summary>
		/// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
		/// </summary>
		/// <param name="path">The location of the file to write to.</param>
		/// <param name="contents">The text to write.</param>
		void WriteAllText(String path, String contents);

		/// <summary>
		/// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
		/// </summary>
		/// <param name="path">The location of the file to write to.</param>
		/// <param name="contents">The text to write.</param>
		/// <param name="encoding">The encoding to use.</param>
		void WriteAllText(string path, string contents, Encoding encoding);

	}

}
