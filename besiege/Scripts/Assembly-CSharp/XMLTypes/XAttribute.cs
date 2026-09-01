using System;

namespace XMLTypes
{
	public class XAttribute
	{
		public string Name { get; private set; }

		public string Value { get; private set; }

		public XAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Name = name;
			Value = value;
		}

		private XAttribute(string value)
		{
			Name = null;
			Value = value;
		}

		public static XAttribute[] Single(string value)
		{
			return new XAttribute[1]
			{
				new XAttribute(value)
			};
		}
	}
}
