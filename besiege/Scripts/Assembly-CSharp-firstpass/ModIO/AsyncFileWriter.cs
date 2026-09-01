using System;
using System.IO;
using System.Threading;
using UnityEngine;

namespace ModIO
{
	public static class AsyncFileWriter
	{
		public static void WriteBufferAsync(string filePath, byte[] buffer, Action<bool> onComplete)
		{
			new Thread((ThreadStart)delegate
			{
				bool isSuccessful = false;
				try
				{
					File.WriteAllBytes(filePath, buffer);
					isSuccessful = true;
				}
				catch (Exception exception)
				{
					Debug.LogError("Failed to write file to: " + filePath);
					Debug.LogException(exception);
				}
				finally
				{
					WebRequestDispatcher.Dispatch(delegate
					{
						if (onComplete != null)
						{
							onComplete(isSuccessful);
						}
					});
				}
			}).Start();
		}
	}
}
