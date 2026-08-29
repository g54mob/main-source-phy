using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace GLTFast.Schema
{
	internal class JsonWriter
	{
		private StreamWriter m_Stream;

		private bool m_Separation;

		public JsonWriter(StreamWriter stream)
		{
			m_Stream = stream;
			OpenBrackets();
		}

		public void OpenBrackets()
		{
			m_Stream.Write('{');
			m_Separation = false;
		}

		public void AddProperty(string name)
		{
			Separate();
			m_Stream.Write('"');
			m_Stream.Write(name);
			m_Stream.Write("\":");
			m_Separation = false;
		}

		public void AddObject()
		{
			Separate();
			m_Stream.Write('{');
			m_Separation = false;
		}

		public void AddArray(string name)
		{
			Separate();
			m_Stream.Write('"');
			m_Stream.Write(name);
			m_Stream.Write("\":[");
			m_Separation = false;
		}

		public void CloseArray()
		{
			m_Stream.Write(']');
			m_Separation = true;
		}

		public void AddArrayProperty<T>(string name, IEnumerable<T> values)
		{
			AddArray(name);
			foreach (T value in values)
			{
				Separate();
				m_Stream.Write(value.ToString());
			}
			CloseArray();
		}

		public void AddArrayProperty(string name, IEnumerable<float> values)
		{
			AddArray(name);
			foreach (float value in values)
			{
				Separate();
				m_Stream.Write(value.ToString("R", CultureInfo.InvariantCulture));
			}
			CloseArray();
		}

		public void AddArrayProperty(string name, IEnumerable<string> values)
		{
			AddArray(name);
			foreach (string value in values)
			{
				Separate();
				m_Stream.Write('"');
				m_Stream.Write(value);
				m_Stream.Write('"');
			}
			CloseArray();
		}

		public void AddArrayPropertySafe(string name, IEnumerable<string> values)
		{
			AddArray(name);
			foreach (string value in values)
			{
				Separate();
				m_Stream.Write('"');
				WriteStringValueSafe(value);
				m_Stream.Write('"');
			}
			CloseArray();
		}

		public void AddProperty<T>(string name, T value)
		{
			Separate();
			m_Stream.Write('"');
			m_Stream.Write(name);
			m_Stream.Write("\":");
			m_Stream.Write(value.ToString());
		}

		public void AddProperty(string name, float value)
		{
			Separate();
			m_Stream.Write('"');
			m_Stream.Write(name);
			m_Stream.Write("\":");
			m_Stream.Write(value.ToString("R", CultureInfo.InvariantCulture));
		}

		public void AddProperty(string name, string value)
		{
			Separate();
			m_Stream.Write('"');
			m_Stream.Write(name);
			m_Stream.Write("\":\"");
			m_Stream.Write(value);
			m_Stream.Write('"');
		}

		public void AddPropertySafe(string name, string value)
		{
			Separate();
			m_Stream.Write('"');
			m_Stream.Write(name);
			m_Stream.Write("\":\"");
			WriteStringValueSafe(value);
			m_Stream.Write('"');
		}

		public void AddProperty(string name, bool value)
		{
			Separate();
			m_Stream.Write('"');
			m_Stream.Write(name);
			m_Stream.Write("\":");
			m_Stream.Write(value ? "true" : "false");
		}

		private void Separate()
		{
			if (m_Separation)
			{
				m_Stream.Write(',');
			}
			m_Separation = true;
		}

		public void Close()
		{
			m_Stream.Write('}');
			m_Separation = true;
		}

		private void WriteStringValueSafe(string value)
		{
			foreach (char c in value)
			{
				switch (c)
				{
				case '\\':
					m_Stream.Write("\\\\");
					break;
				case '\f':
					m_Stream.Write("\\f");
					break;
				case '\n':
					m_Stream.Write("\\n");
					break;
				case '\r':
					m_Stream.Write("\\r");
					break;
				case '\t':
					m_Stream.Write("\\t");
					break;
				case '"':
					m_Stream.Write("\\\"");
					break;
				default:
					m_Stream.Write(c);
					break;
				}
			}
		}

		[Conditional("DEBUG")]
		private static void CertifyValidJsonString(string value)
		{
		}
	}
}
