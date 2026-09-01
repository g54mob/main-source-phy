using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleTables
{
	public class ConsoleTable
	{
		public IList<object> Columns { get; set; }

		public IList<object[]> Rows { get; protected set; }

		public ConsoleTableOptions Options { get; protected set; }

		public ConsoleTable(params string[] columns)
			: this(new ConsoleTableOptions
			{
				Columns = new List<string>(columns)
			})
		{
		}

		public ConsoleTable(ConsoleTableOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			Options = options;
			Rows = new List<object[]>();
			Columns = new List<object>(options.Columns.OfType<object>().ToList());
		}

		public ConsoleTable AddColumn(IEnumerable<string> names)
		{
			foreach (string name in names)
			{
				Columns.Add(name);
			}
			return this;
		}

		public ConsoleTable AddRow(params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (!Columns.Any())
			{
				throw new Exception("Please set the columns first");
			}
			if (Columns.Count != values.Length)
			{
				throw new Exception("The number columns in the row (" + Columns.Count + ") does not match the values (" + values.Length + ")");
			}
			Rows.Add(values);
			return this;
		}

		public static ConsoleTable From<T>(IEnumerable<T> values)
		{
			ConsoleTable consoleTable = new ConsoleTable();
			IEnumerable<string> columns = GetColumns<T>();
			consoleTable.AddColumn(columns);
			foreach (IEnumerable<object> item in values.Select((T value) => columns.Select((string column) => GetColumnValue<T>(value, column))))
			{
				consoleTable.AddRow(item.ToArray());
			}
			return consoleTable;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<int> columnLengths = ColumnLengths();
			string format = (from i in Enumerable.Range(0, Columns.Count)
				select " | {" + i + ",-" + columnLengths[i] + "}").Aggregate((string s, string a) => s + a) + " |";
			int val = Math.Max(0, Rows.Any() ? Rows.Max((object[] row) => string.Format(format, row).Length) : 0);
			string text = string.Format(format, Columns.ToArray());
			int num = Math.Max(val, text.Length);
			List<string> list = Rows.Select((object[] row) => string.Format(format, row)).ToList();
			string value = " " + string.Join(string.Empty, Enumerable.Repeat("-", num - 1).ToArray()) + " ";
			stringBuilder.AppendLine(value);
			stringBuilder.AppendLine(text);
			foreach (string item in list)
			{
				stringBuilder.AppendLine(value);
				stringBuilder.AppendLine(item);
			}
			stringBuilder.AppendLine(value);
			if (Options.EnableCount)
			{
				stringBuilder.AppendLine(string.Empty);
				stringBuilder.AppendFormat(" Count: {0}", Rows.Count);
			}
			return stringBuilder.ToString();
		}

		public string ToMarkDownString()
		{
			StringBuilder builder = new StringBuilder();
			List<int> columnLengths = ColumnLengths();
			string format = Format(columnLengths);
			string text = string.Format(format, Columns.ToArray());
			List<string> list = Rows.Select((object[] row) => string.Format(format, row)).ToList();
			string value = Regex.Replace(text, "[^|]", "-");
			builder.AppendLine(text);
			builder.AppendLine(value);
			list.ForEach(delegate(string row)
			{
				builder.AppendLine(row);
			});
			return builder.ToString();
		}

		public string ToStringAlternative()
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<int> columnLengths = ColumnLengths();
			string format = Format(columnLengths);
			string text = string.Format(format, Columns.ToArray());
			List<string> list = Rows.Select((object[] row) => string.Format(format, row)).ToList();
			string text2 = Regex.Replace(text, "[^|]", "-");
			string value = text2.Replace("|", "+");
			stringBuilder.AppendLine(value);
			stringBuilder.AppendLine(text);
			foreach (string item in list)
			{
				stringBuilder.AppendLine(value);
				stringBuilder.AppendLine(item);
			}
			stringBuilder.AppendLine(value);
			return stringBuilder.ToString();
		}

		private string Format(List<int> columnLengths)
		{
			return ((from i in Enumerable.Range(0, Columns.Count)
				select " | {" + i + ",-" + columnLengths[i] + "}").Aggregate((string s, string a) => s + a) + " |").Trim();
		}

		private List<int> ColumnLengths()
		{
			return Columns.Select((object t, int i) => (from x in Rows.Select((object[] x) => x[i]).Union(Columns)
				where x != null
				select x.ToString().Length).Max()).ToList();
		}

		public void Write(Format format = ConsoleTables.Format.Default)
		{
			switch (format)
			{
			case ConsoleTables.Format.Default:
				Console.WriteLine(ToString());
				break;
			case ConsoleTables.Format.MarkDown:
				Console.WriteLine(ToMarkDownString());
				break;
			case ConsoleTables.Format.Alternative:
				Console.WriteLine(ToStringAlternative());
				break;
			default:
				throw new ArgumentOutOfRangeException("format", format, null);
			}
		}

		private static IEnumerable<string> GetColumns<T>()
		{
			return (from x in typeof(T).GetProperties()
				select x.Name).ToArray();
		}

		private static object GetColumnValue<T>(object target, string column)
		{
			return typeof(T).GetProperty(column).GetValue(target, null);
		}
	}
}
