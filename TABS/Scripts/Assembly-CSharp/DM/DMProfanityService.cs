using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DM
{
	public class DMProfanityService : ServicePrefab
	{
		private const float MAX_WAIT_TIME = 2f;

		private const int MAX_LENGTH = 1024;

		private const string SEPARATOR = "|w|";

		private float m_sendTimer;

		private List<Action<string>> m_queuedEntires = new List<Action<string>>();

		private string m_uncensoredText = string.Empty;

		private int m_separatorUtf8Length;

		private readonly StringBuilder m_getSubStringBuilder = new StringBuilder();

		public override void OnAwake()
		{
			base.OnAwake();
			if (MustUseUtf8())
			{
				m_separatorUtf8Length = GetBytesLengthUtf8("|w|");
			}
		}

		public async void QueueProfanityMasking(string inText, Action<string> onWordsMasked)
		{
			if (DMProfanityFilter.IsUsingNoProfanityFilter())
			{
				string obj = await DMProfanityFilter.MaskCensoredWordsPlatformAsync(inText);
				onWordsMasked?.Invoke(obj);
				return;
			}
			await Task.Yield();
			if (string.IsNullOrEmpty(inText))
			{
				onWordsMasked?.Invoke(inText);
				return;
			}
			GetLengthsToUse(inText, out var separatorLength, out var uncensoredTextLength, out var inTextLength);
			int num = 1024 - separatorLength;
			if (inTextLength > num)
			{
				inText = GetSubstring(inText, Mathf.Min(inTextLength, num));
			}
			if (uncensoredTextLength + inTextLength + separatorLength <= 1024)
			{
				if (string.IsNullOrEmpty(m_uncensoredText))
				{
					m_uncensoredText = inText;
				}
				else
				{
					m_uncensoredText = string.Join("|w|", m_uncensoredText, inText);
				}
				m_queuedEntires.Add(onWordsMasked);
			}
			else
			{
				SendToProfanityFilter();
				QueueProfanityMasking(inText, onWordsMasked);
			}
		}

		public void SendToProfanityFilter()
		{
			m_sendTimer = 0f;
			List<Action<string>> copiedCallbacks = new List<Action<string>>(m_queuedEntires);
			m_queuedEntires.Clear();
			DMProfanityFilter.MaskCensoredWordsPlatformAsync(m_uncensoredText).ContinueWith(delegate(Task<string> t)
			{
				string[] array = t.Result.Split(new string[1] { "|w|" }, StringSplitOptions.None);
				for (int i = 0; i < copiedCallbacks.Count; i++)
				{
					copiedCallbacks[i]?.Invoke(array[i]);
				}
			});
			m_uncensoredText = string.Empty;
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			if (m_sendTimer >= 2f)
			{
				SendToProfanityFilter();
			}
			else
			{
				m_sendTimer += Time.unscaledDeltaTime;
			}
		}

		private void GetLengthsToUse(string inText, out int separatorLength, out int uncensoredTextLength, out int inTextLength)
		{
			separatorLength = (MustUseUtf8() ? m_separatorUtf8Length : "|w|".Length);
			uncensoredTextLength = (MustUseUtf8() ? GetBytesLengthUtf8(m_uncensoredText) : m_uncensoredText.Length);
			inTextLength = (MustUseUtf8() ? GetBytesLengthUtf8(inText) : inText.Length);
		}

		private string GetSubstring(string input, int length)
		{
			if (length <= 0 || string.IsNullOrEmpty(input))
			{
				return input;
			}
			if (!MustUseUtf8())
			{
				return input.Substring(0, length);
			}
			if (Encoding.UTF8.GetByteCount(input) <= length)
			{
				return input;
			}
			m_getSubStringBuilder.Clear();
			int num = 0;
			TextElementEnumerator textElementEnumerator = StringInfo.GetTextElementEnumerator(input);
			while (textElementEnumerator.MoveNext())
			{
				string textElement = textElementEnumerator.GetTextElement();
				num += Encoding.UTF8.GetByteCount(textElement);
				if (num > length)
				{
					break;
				}
				m_getSubStringBuilder.Append(textElement);
			}
			return m_getSubStringBuilder.ToString();
		}

		private int GetBytesLengthUtf8(string input)
		{
			return Encoding.UTF8.GetByteCount(input);
		}

		private bool MustUseUtf8()
		{
			return false;
		}
	}
}
