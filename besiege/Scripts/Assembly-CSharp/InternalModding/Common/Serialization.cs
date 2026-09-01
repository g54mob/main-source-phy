using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using InternalModding.Misc;
using Modding.Serialization;
using UnityEngine;

namespace InternalModding.Common
{
	public static class Serialization
	{
		private static bool IsXmlAttribute(MemberInfo info)
		{
			return info.IsDefined(typeof(XmlAttributeAttribute), false);
		}

		private static bool IsXmlElement(MemberInfo info)
		{
			return info.IsDefined(typeof(XmlElementAttribute), false);
		}

		private static bool IsXmlText(MemberInfo info)
		{
			return info.IsDefined(typeof(XmlTextAttribute), false);
		}

		private static bool IsXmlList(MemberInfo info)
		{
			return info.IsDefined(typeof(XmlArrayAttribute), false) || (IsXmlElement(info) && (GetMemberType(info).IsArray || typeof(List<>).IsAssignableFrom(GetMemberType(info))));
		}

		private static string GetXmlName(MemberInfo info)
		{
			string text;
			if (IsXmlList(info))
			{
				XmlArrayAttribute[] array = (XmlArrayAttribute[])info.GetCustomAttributes(typeof(XmlArrayAttribute), false);
				text = ((array.Length <= 0) ? string.Empty : array[0].ElementName);
			}
			else if (IsXmlAttribute(info))
			{
				XmlAttributeAttribute xmlAttributeAttribute = ((XmlAttributeAttribute[])info.GetCustomAttributes(typeof(XmlAttributeAttribute), false))[0];
				text = xmlAttributeAttribute.AttributeName;
			}
			else if (IsXmlElement(info))
			{
				XmlElementAttribute xmlElementAttribute = ((XmlElementAttribute[])info.GetCustomAttributes(typeof(XmlElementAttribute), false))[0];
				text = xmlElementAttribute.ElementName;
			}
			else
			{
				if (!info.IsDefined(typeof(XmlTextAttribute), false))
				{
					throw new ArgumentException("GetXmlName called on member that does not have Xml declarations!");
				}
				text = string.Empty;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = info.Name;
			}
			return text;
		}

		private static IEnumerable<string> GetAllXmlNames(MemberInfo info)
		{
			if (info.GetCustomAttributes(typeof(XmlArrayAttribute), false).Length > 0)
			{
				return new string[1] { GetXmlName(info) };
			}
			return from XmlElementAttribute a in info.GetCustomAttributes(typeof(XmlElementAttribute), false)
				select a.ElementName;
		}

		private static object GetMemberValue(MemberInfo member, object obj)
		{
			if (member is PropertyInfo)
			{
				return ((PropertyInfo)member).GetValue(obj, null);
			}
			if (member is FieldInfo)
			{
				return ((FieldInfo)member).GetValue(obj);
			}
			throw new ArgumentException("GetMemberValue called with member that is not a Field or Property!");
		}

		private static void SetMemberValue(MemberInfo member, object obj, object value)
		{
			if (member is PropertyInfo)
			{
				((PropertyInfo)member).SetValue(obj, value, null);
				return;
			}
			if (member is FieldInfo)
			{
				((FieldInfo)member).SetValue(obj, value);
				return;
			}
			throw new ArgumentException("SetMemberValue called with a member that is not a Field or Property!");
		}

		private static object[] GetListValue(MemberInfo member, object obj)
		{
			object memberValue = GetMemberValue(member, obj);
			if (memberValue == null)
			{
				return new object[0];
			}
			Type memberType = GetMemberType(member);
			if (memberType.IsArray)
			{
				return (object[])memberValue;
			}
			if (memberType.GetGenericArguments().Length > 0)
			{
				Type type = typeof(List<>).MakeGenericType(memberType.GetGenericArguments()[0]);
				if (type.IsAssignableFrom(memberType))
				{
					return ((IList)memberValue).Cast<object>().ToArray();
				}
			}
			throw new ArgumentException("GetListValue called with member that is not an array or list!");
		}

		private static Type GetMemberType(MemberInfo member)
		{
			if (member is PropertyInfo)
			{
				return ((PropertyInfo)member).PropertyType;
			}
			if (member is FieldInfo)
			{
				return ((FieldInfo)member).FieldType;
			}
			throw new ArgumentException("GetMemberType called with member that is not a Field or Property!");
		}

		public static bool Validate(string elementName, IValidatable toValidate)
		{
			Type type = toValidate.GetType();
			IEnumerable<MemberInfo> enumerable = from p in type.GetMembers()
				where (IsXmlAttribute(p) || IsXmlElement(p) || IsXmlText(p)) && !IsXmlList(p)
				select p;
			foreach (MemberInfo item in enumerable)
			{
				if (GetMemberType(item) == typeof(UnityEngine.Vector3))
				{
					MLog.Warn(item.DeclaringType.FullName + "." + item.Name + ": UnityEngine.Vector3 does not deserialize correctly. Consider using Modding.Serialization.Vector3 instead.");
				}
			}
			IEnumerable<MemberInfo> enumerable2 = enumerable.Where((MemberInfo m) => !m.IsDefined(typeof(DefaultValueAttribute), false));
			string[] array = toValidate.ElementsUsed.Split('|');
			string[] array2 = toValidate.AttributesUsed.Split('|');
			foreach (MemberInfo item2 in enumerable2)
			{
				string xmlName = GetXmlName(item2);
				Element element = toValidate as Element;
				if (element != null && Element.SpecialAttributeNames.Contains(xmlName))
				{
					continue;
				}
				if (IsXmlElement(item2))
				{
					if (!array.Contains(xmlName))
					{
						return MissingElement(elementName, xmlName, toValidate);
					}
				}
				else if (IsXmlAttribute(item2) && !array2.Contains(xmlName))
				{
					return MissingAttribute(elementName, xmlName, toValidate);
				}
			}
			IEnumerable<MemberInfo> enumerable3 = enumerable.Where((MemberInfo m) => m.IsDefined(typeof(RequireToValidateAttribute), false));
			foreach (MemberInfo item3 in enumerable3)
			{
				if (!item3.IsDefined(typeof(DefaultValueAttribute), false) || GetMemberValue(item3, toValidate) != null)
				{
					Element element2 = (Element)GetMemberValue(item3, toValidate);
					if (!element2.InvokeValidate(GetXmlName(item3)))
					{
						return false;
					}
				}
			}
			IEnumerable<MemberInfo> enumerable4 = type.GetMembers().Where(IsXmlList);
			foreach (MemberInfo item4 in enumerable4)
			{
				object[] listValue = GetListValue(item4, toValidate);
				if (!item4.IsDefined(typeof(CanBeEmptyAttribute), false) && (listValue == null || listValue.Length == 0))
				{
					return InvalidData(elementName, "Must contain elements for the " + GetXmlName(item4) + " list!", toValidate);
				}
				if (!item4.IsDefined(typeof(RequireToValidateAttribute), false) || listValue == null)
				{
					continue;
				}
				object[] array3 = listValue;
				foreach (object obj in array3)
				{
					Element element3 = obj as Element;
					if (element3 != null && !element3.InvokeValidate())
					{
						return false;
					}
				}
			}
			List<string> list = enumerable.Select(GetXmlName).Union(enumerable4.SelectMany(GetAllXmlNames)).ToList();
			if (toValidate.AttributesUsed != string.Empty)
			{
				string[] array4 = array2;
				foreach (string text in array4)
				{
					if (!list.Contains(text))
					{
						MLog.WarnFormat("In {0} (at line {1}, column {2} in {3}): Attribute {4} is not recognized.", elementName, toValidate.LineNumber, toValidate.LinePosition, toValidate.FileName, text);
					}
				}
			}
			if (toValidate.ElementsUsed != string.Empty)
			{
				string[] array5 = array;
				foreach (string text2 in array5)
				{
					if (!list.Contains(text2))
					{
						MLog.WarnFormat("In {0} (at line {1}, column {2} in {3}): Child element {4} is not recognized.", elementName, toValidate.LineNumber, toValidate.LinePosition, toValidate.FileName, text2);
					}
				}
			}
			return true;
		}

		public static bool MissingElement(string elemName, string missing, IValidatable obj)
		{
			MLog.ErrorFormat("{0} (at line {1}, column {2} in {3}) must contain {4} element!", elemName, obj.LineNumber, obj.LinePosition, obj.FileName, missing);
			return false;
		}

		public static bool MissingAttribute(string elemName, string missing, IValidatable obj)
		{
			MLog.ErrorFormat("{0} (at line {1}, column {2} in {3}) must have {4} attribute!", elemName, obj.LineNumber, obj.LinePosition, obj.FileName, missing);
			return false;
		}

		public static bool InvalidData(string elemName, string error, IValidatable obj)
		{
			MLog.ErrorFormat("{0} (at line {1}, column {2} in {3}): {4}", elemName, obj.LineNumber, obj.LinePosition, obj.FileName, error);
			return false;
		}

		public static void Warn(string elemName, string warning, IValidatable obj)
		{
			MLog.WarnFormat("{0} (at line {1}, column {2} in {3}): {4}.", elemName, obj.LineNumber, obj.LinePosition, obj.FileName, warning);
		}

		public static void Reload(IReloadable o, IReloadable n)
		{
			if (o.GetType() != n.GetType())
			{
				throw new ArgumentException("Can only reload objects of the same type!");
			}
			n.PreprocessForReloading();
			Type type = o.GetType();
			IEnumerable<MemberInfo> enumerable = from m in type.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy)
				where m.IsDefined(typeof(ReloadableAttribute), true)
				select m;
			foreach (MemberInfo item in enumerable)
			{
				if (item.GetType().IsDefined(typeof(ReloadableAttribute), false))
				{
					object memberValue = GetMemberValue(item, o);
					object memberValue2 = GetMemberValue(item, n);
					if (!(memberValue is IReloadable))
					{
						throw new ArgumentException(string.Concat("Type ", item.GetType(), " has Reloadable attribute but does not implement IReloadable!"));
					}
					Reload((IReloadable)memberValue, (IReloadable)memberValue2);
				}
				else
				{
					SetMemberValue(item, o, GetMemberValue(item, n));
				}
			}
			o.OnReload(n);
		}
	}
}
