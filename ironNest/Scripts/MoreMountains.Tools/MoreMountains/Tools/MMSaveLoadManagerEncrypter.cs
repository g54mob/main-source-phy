using System.IO;

namespace MoreMountains.Tools
{
	public abstract class MMSaveLoadManagerEncrypter
	{
		protected string _saltText;

		public virtual string Key { get; set; }

		protected virtual void Encrypt(Stream inputStream, Stream outputStream, string sKey)
		{
		}

		protected virtual void Decrypt(Stream inputStream, Stream outputStream, string sKey)
		{
		}
	}
}
