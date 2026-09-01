namespace Kamgam.LocalizationForSettings
{
	public interface ITranslation
	{
		string GetTerm();

		bool HasText(int languageIndex);

		string GetText(int languageIndex);

		void SetText(int languageIndex, string text);

		void ClearTexts();
	}
}
