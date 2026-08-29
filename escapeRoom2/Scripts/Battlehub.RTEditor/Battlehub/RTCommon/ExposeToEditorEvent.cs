namespace Battlehub.RTCommon
{
	public delegate void ExposeToEditorEvent(ExposeToEditor obj);
	public delegate void ExposeToEditorEvent<T>(ExposeToEditor obj, T arg);
	public delegate void ExposeToEditorEvent<T, T2>(ExposeToEditor obj, T arg, T2 arg2);
}
