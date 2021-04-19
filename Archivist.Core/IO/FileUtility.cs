using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Archivist.Core.IO
{

	/// <summary>
	/// Acts as a wrapper around <see cref="System.IO.File" /> operations to make testing easier.
	/// </summary>
	public class FileUtility : IFileUtility
	{

		/// <summary>
		/// Computes an MD5 hash on the data provided.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public String ComputeMd5Hash(Byte[] data)
		{
			var buffer = MD5.Create().ComputeHash(data);
			var sb = new StringBuilder(buffer.Length);
			for(var i = 0; i < buffer.Length; i++)
				sb.Append(buffer[i].ToString("X2"));
			return sb.ToString();
		}


		/// <summary>
		/// Appends lines to a file, and then closes the file. If the specified file does not exist, this method creates a file,
		/// writes the specified lines to the file, and then closes the file.
		/// </summary>
		/// <param name="path">The file to append the lines to. The file is created if it doesn't already exist.</param>
		/// <param name="contents">The lines to append to the file.</param>
		public void AppendAllLines(String path, IEnumerable<String> contents)
		{
			File.AppendAllLines(path, contents);
		}

		/// <summary>
		/// Appends lines to a file by using a specified encoding, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
		/// </summary>
		/// <param name="path">The file to append the lines to. The file is created if it doesn't already exist.</param>
		/// <param name="contents">The lines to append to the file.</param>
		/// <param name="encoding">The character encoding to use.</param>
		public void AppendAllLines(String path, IEnumerable<String> contents, Encoding encoding)
		{
			File.AppendAllLines(path, contents, encoding);
		}

		/// <summary>
		/// Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.
		/// </summary>
		/// <param name="path">The file to append the specified string to.</param>
		/// <param name="contents">The string to append to the file</param>
		public void AppendAllText(String path, String contents)
		{
			File.AppendAllText(path, contents);
		}

		/// <summary>
		/// Appends the specified string to the file, creating the file if it does not already exist
		/// </summary>
		/// <param name="path">The file to append the specified string to.</param>
		/// <param name="contents">The string to append to the file</param>
		/// <param name="encoding">The character encoding to use</param>
		public void AppendAllText(String path, String contents, Encoding encoding)
		{
			File.AppendAllText(path, contents, encoding);
		}

		/// <summary>
		/// Creates a StreamWriter that appends UTF-8 encoded text to an existing file, or to a new file if the specified file does not exist
		/// </summary>
		/// <param name="path">The path to the file to append to.</param>
		/// <returns>
		/// A stream writer that appends UTF-8 encoded text to the specified file or to a new file.
		/// </returns>
		public StreamWriter AppendText(String path)
		{
			return File.AppendText(path);
		}

		/// <summary>
		/// Copies an existing file to a new file. Overwriting a file of the same name is not allowed.
		/// </summary>
		/// <param name="sourceFileName">The file to copy.</param>
		/// <param name="destFileName">The name of the destination file. This cannot be a directory or an existing file</param>
		public void Copy(String sourceFileName, String destFileName)
		{
			File.Copy(sourceFileName, destFileName);
		}

		/// <summary>
		/// Copies an existing file to a new file. Overwriting a file of the same name is allowed
		/// </summary>
		/// <param name="sourceFileName">The file to copy</param>
		/// <param name="destFileName">The name of the destination file. This cannot be a directory</param>
		/// <param name="overwrite">true if the destination file can be overwritten; otherwise, false</param>
		public void Copy(String sourceFileName, String destFileName, Boolean overwrite)
		{
			File.Copy(sourceFileName, destFileName, overwrite);
		}

		/// <summary>
		/// Creates or overwrites the specified file, specifying a buffer size and a FileOptions value that describes how to create or overwrite the file
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="bufferSize">Size of the buffer.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public FileStream Create(String path, Int32 bufferSize, FileOptions options)
		{
			return File.Create(path, bufferSize, options);
		}

		/// <summary>
		/// Creates or overwrites the specified file
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="bufferSize">Size of the buffer.</param>
		/// <returns></returns>
		public FileStream Create(String path, Int32 bufferSize)
		{
			return File.Create(path, bufferSize);
		}

		/// <summary>
		/// Creates or overwrites a file in the specified path
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public FileStream Create(String path)
		{
			return File.Create(path);
		}

		/// <summary>
		/// Creates or opens a file for writing UTF-8 encoded text
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public StreamWriter CreateText(String path)
		{
			return File.CreateText(path);
		}

		/// <summary>
		/// Returns a unique name for a file.
		/// </summary>
		/// <param name="folder">The folder to get this file name for.</param>
		/// <param name="desiredFileName">The desired file name.</param>
		/// <param name="maxAttempts">The maximum number of attempts.</param>
		/// <returns>Returns a unique name for the specified file.</returns>
		/// <exception cref="System.Exception">Could not create unique filename in  + maxAttempts +  attempts</exception>
		/// <see href="http://stackoverflow.com/a/13696429/1398981"/>
		public String CreateUniqueFileName(String folder, String desiredFileName, Int32 maxAttempts = 1024)
		{
			var fileBase = Path.GetFileNameWithoutExtension(desiredFileName);
			var ext = Path.GetExtension(desiredFileName);

			// BUILD A HASHSET OF FILE NAMES FOR PERFORMANCE
			var files = new HashSet<String>(Directory.GetFiles(folder));

			for (var i = 0; i < maxAttempts; i++)
			{
				// FIRST TRY WITH THE ORIGINAL FILE NAME, THEN TRY WITH INDEXES
				var name = (i == 0)
					? desiredFileName
					: String.Format("{0} ({1}){2}", fileBase, i, ext);

				// CHECK IF THIS FILE EXISTS
				var fullPath = Path.Combine(folder, name);
				if (files.Contains(fullPath))
					continue;

				return fullPath;
			}

			throw new Exception("Could not create unique filename in " + maxAttempts + " attempts");
		}

		/// <summary>
		/// Deletes the specified file
		/// </summary>
		/// <param name="path">The path.</param>
		public void Delete(String path)
		{
			File.Delete(path);
		}

		/// <summary>
		/// Determines whether the specified file exists.
		/// </summary>
		/// <param name="location">The location of the file.</param>
		/// <returns><c>true</c> if the file exists; otherwise <c>false</c></returns>
		public Boolean Exists(String location)
		{
			return File.Exists(location);
		}

		/// <summary>
		/// Gets the FileAttributes of the file on the path
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public FileAttributes GetAttributes(String path)
		{
			return File.GetAttributes(path);
		}

		/// <summary>
		/// Opens a text file, reads all lines of the file, and then closes the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public String[] ReadAllLines(String path)
		{
			return File.ReadAllLines(path);
		}

		/// <summary>
		/// Reads all bytes from tge specified file.
		/// </summary>
		/// <param name="location">The location of the file.</param>
		/// <returns>Returns a byte array for the specified file</returns>
		public Byte[] ReadAllBytes(String location)
		{
			return File.ReadAllBytes(location);
		}

		/// <summary>
		/// Gets the file information.
		/// </summary>
		/// <param name="location">The location of the file.</param>
		/// <returns>Returns a <see cref="FileInfo"/> object for the specified file</returns>
		public FileInfo GetFileInfo(String location)
		{
			return new FileInfo(location);
		}

		/// <summary>
		/// Returns the creation date and time of the specified file or directory.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public DateTime GetCreationTime(String path)
		{
			return File.GetCreationTime(path);
		}

		/// <summary>
		/// Returns the creation date and time, in coordinated universal time (UTC), of the specified file or directory
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public DateTime GetCreationTimeUtc(String path)
		{
			return File.GetCreationTimeUtc(path);
		}

		/// <summary>
		/// Returns the date and time the specified file or directory was last accessed
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public DateTime GetLastAccessTime(String path)
		{
			return File.GetLastAccessTime(path);
		}

		/// <summary>
		/// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last accessed
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public DateTime GetLastAccessTimeUtc(String path)
		{
			return File.GetLastAccessTimeUtc(path);
		}

		/// <summary>
		/// Returns the date and time the specified file or directory was last written to.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public DateTime GetLastWriteTime(String path)
		{
			return File.GetLastWriteTime(path);
		}

		/// <summary>
		/// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last written to
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public DateTime GetLastWriteTimeUtc(String path)
		{
			return File.GetLastWriteTimeUtc(path);
		}

		/// <summary>
		/// Moves a specified file to a new location, providing the option to specify a new file name
		/// </summary>
		/// <param name="sourceFilename">The source filename.</param>
		/// <param name="destFilename">The dest filename.</param>
		public void Move(String sourceFilename, String destFilename)
		{
			File.Move(sourceFilename, destFilename);
		}

		/// <summary>
		/// Opens a FileStream on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="mode">The mode.</param>
		/// <param name="access">The access.</param>
		/// <param name="share">The share.</param>
		/// <returns></returns>
		public FileStream Open(String path, FileMode mode, FileAccess access, FileShare share)
		{
			return File.Open(path, mode, access, share);
		}

		/// <summary>
		/// Opens a FileStream on the specified path, with the specified mode and access
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="mode">The mode.</param>
		/// <param name="access">The access.</param>
		/// <returns></returns>
		public FileStream Open(String path, FileMode mode, FileAccess access)
		{
			return File.Open(path, mode, access);
		}

		/// <summary>
		/// Opens a FileStream on the specified path with read/write access
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="mode">The mode.</param>
		/// <returns></returns>
		public FileStream Open(String path, FileMode mode)
		{
			return File.Open(path, mode);
		}

		/// <summary>
		/// Opens an existing file for reading
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public FileStream OpenRead(String path)
		{
			return File.OpenRead(path);
		}

		/// <summary>
		/// Opens an existing UTF-8 encoded text file for reading
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public StreamReader OpenText(String path)
		{
			return File.OpenText(path);
		}

		/// <summary>
		/// Opens an existing file or creates a new file for writing
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public FileStream OpenWrite(String path)
		{
			return File.OpenWrite(path);
		}

		/// <summary>
		/// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="encoding">The encoding.</param>
		/// <returns></returns>
		public String[] ReadAllLines(String path, Encoding encoding)
		{
			return File.ReadAllLines(path, encoding);
		}

		/// <summary>
		/// Creates a new file, writes the specified string array to the file by using the specified encoding, and then closes the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="contents">The contents.</param>
		public void WriteAllLines(String path, String[] contents)
		{
			File.WriteAllLines(path, contents);
		}

		/// <summary>
		/// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
		/// </summary>
		/// <param name="location">The location of the file to write to.</param>
		/// <param name="textToWrite">The text to write.</param>
		public void WriteAllText(String location, String textToWrite)
		{
			File.WriteAllText(location, textToWrite);
		}

		/// <summary>
		/// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
		/// </summary>
		/// <param name="path">The location of the file to write to.</param>
		/// <param name="contents">The text to write.</param>
		/// <param name="encoding">The encoding to use.</param>
		public void WriteAllText(String path, String contents, Encoding encoding)
		{
			File.WriteAllText(path, contents, encoding);
		}

		/// <summary>
		/// Opens a text file, reads all lines of the file, and then closes the file
		/// </summary>
		/// <param name="location">The file to open for reading.</param>
		/// <returns></returns>
		public String ReadAllText(String location)
		{
			return File.ReadAllText(location);
		}

		/// <summary>
		/// Opens a text file, reads all lines of the file, and then closes the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="encoding">The encoding.</param>
		/// <returns></returns>
		public String ReadAllText(String path, Encoding encoding)
		{
			return File.ReadAllText(path, encoding);
		}

		/// <summary>
		/// Sets the specified FileAttributes of the file on the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="attributes">The attributes.</param>
		public void SetAttributes(String path, FileAttributes attributes)
		{
			File.SetAttributes(path, attributes);
		}

		/// <summary>
		/// Sets the date and time the file was created.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="creationTime">The creation time.</param>
		public void SetCreationTime(String path, DateTime creationTime)
		{
			File.SetCreationTime(path, creationTime);
		}

		/// <summary>
		/// Sets the date and time, in coordinated universal time (UTC), that the file was created
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="creationTime">The creation time.</param>
		public void SetCreationTimeUtc(String path, DateTime creationTime)
		{
			File.SetCreationTimeUtc(path, creationTime);
		}

		/// <summary>
		/// Sets the date and time the specified file was last accessed.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="creationTime">The creation time.</param>
		public void SetLastAccessTime(String path, DateTime creationTime)
		{
			File.SetLastAccessTime(path, creationTime);
		}

		/// <summary>
		/// Sets the date and time, in coordinated universal time (UTC), that the specified file was last accessed.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="creationTime">The creation time.</param>
		public void SetLastAccessTimeUtc(String path, DateTime creationTime)
		{
			File.SetLastAccessTimeUtc(path, creationTime);
		}

		/// <summary>
		/// Sets the date and time that the specified file was last written to.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="creationTime">The creation time.</param>
		public void SetLastWriteTime(String path, DateTime creationTime)
		{
			File.SetLastWriteTime(path, creationTime);
		}

		/// <summary>
		/// Sets the date and time, in coordinated universal time (UTC), that the specified file was last written to.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="creationTime">The creation time.</param>
		public void SetLastWriteTimeUtc(String path, DateTime creationTime)
		{
			File.SetLastWriteTime(path, creationTime);
		}

		/// <summary>
		/// Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="bytes">The bytes.</param>
		public void WriteAllBytes(String path, Byte[] bytes)
		{
			File.WriteAllBytes(path, bytes);
		}

		/// <summary>
		/// Creates a new file by using the specified encoding, writes a collection of strings to the file, and then closes the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="contents">The contents.</param>
		/// <param name="encoding">The encoding.</param>
		public void WriteAllLines(String path, String[] contents, Encoding encoding)
		{
			File.WriteAllLines(path, contents, encoding);
		}

	}

}
