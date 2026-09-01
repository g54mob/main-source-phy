using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

public class Arguments
{
	private readonly Dictionary<string, Collection<string>> _parameters;

	private string _waitingParameter;

	public int Count
	{
		get
		{
			return _parameters.Count;
		}
	}

	public Collection<string> this[string parameter]
	{
		get
		{
			return (!_parameters.ContainsKey(parameter)) ? null : _parameters[parameter];
		}
	}

	public Arguments(IEnumerable<string> arguments)
	{
		_parameters = new Dictionary<string, Collection<string>>();
		Regex regex = new Regex("^-{1,2}(?![0-9]+)(?!=)|^\\+", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Regex regex2 = new Regex("^--(rail[a-zA-Z\\-\\_]+)=(.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		foreach (string argument in arguments)
		{
			bool flag = regex2.IsMatch(argument);
			string[] array;
			if (flag)
			{
				Match match = regex2.Match(argument);
				List<string> list = new List<string>();
				if (match.Groups.Count == 3)
				{
					list.Add(match.Groups[1].Value);
					list.Add(match.Groups[2].Value);
				}
				array = list.ToArray();
			}
			else
			{
				array = regex.Split(argument, 3);
			}
			switch (array.Length)
			{
			case 1:
				AddValueToWaitingArgument(array[0]);
				break;
			case 2:
				if (flag)
				{
					if (_waitingParameter != null)
					{
						AddWaitingArgumentAsFlag();
					}
					_waitingParameter = array[0];
					AddValueToWaitingArgument(array[1]);
				}
				else
				{
					if (IsNumber(argument))
					{
						AddValueToWaitingArgument(argument);
					}
					AddWaitingArgumentAsFlag();
					_waitingParameter = array[1];
				}
				break;
			case 3:
			{
				AddWaitingArgumentAsFlag();
				string text = RemoveMatchingQuotes(array[2]);
				AddListValues(array[1], text.Split(','));
				break;
			}
			}
		}
		AddWaitingArgumentAsFlag();
	}

	public static string[] SplitCommandLine(string commandLine)
	{
		StringBuilder stringBuilder = new StringBuilder(commandLine);
		bool flag = false;
		for (int i = 0; i < stringBuilder.Length; i++)
		{
			if (stringBuilder[i] == '"')
			{
				flag = !flag;
			}
			if (stringBuilder[i] == ' ' && !flag)
			{
				stringBuilder[i] = '\n';
			}
		}
		string[] array = stringBuilder.ToString().Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		for (int j = 0; j < array.Length; j++)
		{
			array[j] = RemoveMatchingQuotes(array[j]);
		}
		return array;
	}

	public static string RemoveMatchingQuotes(string stringToTrim)
	{
		int num = stringToTrim.IndexOf('"');
		int num2 = stringToTrim.LastIndexOf('"');
		while (num != num2)
		{
			stringToTrim = stringToTrim.Remove(num, 1);
			stringToTrim = stringToTrim.Remove(num2 - 1, 1);
			num = stringToTrim.IndexOf('"');
			num2 = stringToTrim.LastIndexOf('"');
		}
		return stringToTrim;
	}

	private bool IsNumber(string s)
	{
		int result;
		decimal result2;
		float result3;
		double result4;
		return int.TryParse(s, out result) || decimal.TryParse(s, out result2) || float.TryParse(s, out result3) || double.TryParse(s, out result4);
	}

	private void AddListValues(string argument, IEnumerable<string> values)
	{
		foreach (string value in values)
		{
			Add(argument, value);
		}
	}

	private void AddWaitingArgumentAsFlag()
	{
		if (_waitingParameter != null)
		{
			AddSingle(_waitingParameter, "true");
			_waitingParameter = null;
		}
	}

	private void AddValueToWaitingArgument(string value)
	{
		if (_waitingParameter != null)
		{
			value = RemoveMatchingQuotes(value);
			Add(_waitingParameter, value);
			_waitingParameter = null;
		}
	}

	public void Add(string argument, string value)
	{
		if (!_parameters.ContainsKey(argument))
		{
			_parameters.Add(argument, new Collection<string>());
		}
		_parameters[argument].Add(value);
	}

	public void AddSingle(string argument, string value)
	{
		if (!_parameters.ContainsKey(argument))
		{
			_parameters.Add(argument, new Collection<string>());
			_parameters[argument].Add(value);
			return;
		}
		throw new ArgumentException(string.Format("Argument {0} has already been defined", argument));
	}

	public void Remove(string argument)
	{
		if (_parameters.ContainsKey(argument))
		{
			_parameters.Remove(argument);
		}
	}

	public bool IsTrue(string argument)
	{
		AssertSingle(argument);
		Collection<string> collection = this[argument];
		return collection != null && collection[0].Equals("true", StringComparison.OrdinalIgnoreCase);
	}

	private void AssertSingle(string argument)
	{
		if (this[argument] != null && this[argument].Count > 1)
		{
			throw new ArgumentException(string.Format("{0} has been specified more than once, expecting single value", argument));
		}
	}

	public string Single(string argument)
	{
		AssertSingle(argument);
		if (this[argument] != null && !IsTrue(argument))
		{
			return this[argument][0];
		}
		return null;
	}

	public bool Exists(string argument)
	{
		return this[argument] != null && this[argument].Count > 0;
	}
}
