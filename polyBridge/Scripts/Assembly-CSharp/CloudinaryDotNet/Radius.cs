using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CloudinaryDotNet.Core;

namespace CloudinaryDotNet
{
	public class Radius : CloudinaryDotNet.Core.ICloneable
	{
		private string m_radius;

		public Radius(object value)
		{
			SetRadius(Normalize(value));
		}

		public Radius(string value)
		{
			SetRadius(value);
		}

		public Radius(int value)
		{
			SetRadius(value);
		}

		public Radius(float value)
		{
			SetRadius(value);
		}

		public Radius(object topLeftAndBottomRight, object topRightAndBottomLeft)
		{
			if (topLeftAndBottomRight == null)
			{
				throw new ArgumentNullException("topLeftAndBottomRight");
			}
			if (topRightAndBottomLeft == null)
			{
				throw new ArgumentNullException("topRightAndBottomLeft");
			}
			m_radius = $"{topLeftAndBottomRight}:{topRightAndBottomLeft}";
		}

		public Radius(object topLeft, object topRightAndBottomLeft, object bottomRight)
		{
			if (topLeft == null)
			{
				throw new ArgumentNullException("topLeft");
			}
			if (topRightAndBottomLeft == null)
			{
				throw new ArgumentNullException("topRightAndBottomLeft");
			}
			if (bottomRight == null)
			{
				throw new ArgumentNullException("bottomRight");
			}
			m_radius = $"{topLeft}:{topRightAndBottomLeft}:{bottomRight}";
		}

		public Radius(object topLeft, object topRight, object bottomRight, object bottomLeft)
		{
			if (topLeft == null)
			{
				throw new ArgumentNullException("topLeft");
			}
			if (topRight == null)
			{
				throw new ArgumentNullException("topRight");
			}
			if (bottomRight == null)
			{
				throw new ArgumentNullException("bottomRight");
			}
			if (bottomLeft == null)
			{
				throw new ArgumentNullException("bottomLeft");
			}
			m_radius = $"{topLeft}:{topRight}:{bottomRight}:{bottomLeft}";
		}

		public Radius Clone()
		{
			return (Radius)MemberwiseClone();
		}

		object CloudinaryDotNet.Core.ICloneable.Clone()
		{
			return Clone();
		}

		public override string ToString()
		{
			return m_radius;
		}

		private static string Normalize(object value)
		{
			if (value is ICollection collection)
			{
				if (collection.Count == 0 || collection.Count > 4)
				{
					throw new ArgumentException("Radius array should contain between 1 and 4 values");
				}
				IEnumerable<string> values = from object item in collection
					select item.ToString();
				return string.Join(":", values);
			}
			return value.ToString();
		}

		private void SetRadius(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			m_radius = value.ToString();
		}
	}
}
