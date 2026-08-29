namespace Battlehub.RTCommon
{
	public delegate void ObjectEvent(ExposeToEditor obj);
	public delegate void ObjectEvent<T>(ExposeToEditor obj, T arg);
	public delegate void ObjectEvent<T, T2>(ExposeToEditor obj, T arg, T2 arg2);
}
