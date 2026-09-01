using System.Text;

namespace mattmc3.dotmore.Extensions
{
	public static class StringBuilderExtensions
	{
		public static StringBuilder AppendLine(this StringBuilder that, object value)
		{
			if (value == null)
			{
				return that.AppendLine();
			}
			return that.AppendLine(value.ToString());
		}
	}
}
