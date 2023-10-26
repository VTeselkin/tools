using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
namespace GameSoft.Tools.Extensions
{
	public static class PlayerPrefsExtensions
	{
		public static bool Exists(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		public static string GetString(string key)
		{
			return PlayerPrefs.GetString(key);
		}

		public static void SetString(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
		}
		
		public static int GetInt(string key)
		{
			return PlayerPrefs.GetInt(key);
		}

		public static void SetInt(string key, int value)
		{
			PlayerPrefs.SetInt(key, value);
		}

		public static float GetFloat(string key)
		{
			return PlayerPrefs.GetFloat(key);
		}

		public static void SetFloat(string key, float value)
		{
			PlayerPrefs.SetFloat(key, value);
		}
		private const string SListSeparator = "\n";

		public static List<string> GetStringList(string key)
		{
			string s = GetString(key);
			if (string.IsNullOrEmpty(s))
			{
				return new List<string>();
			}

			return s.Split(new[] {SListSeparator}, StringSplitOptions.RemoveEmptyEntries).ToList();
		}

		public static void SetStringList(string key, List<string> list)
		{
			if (list != null)
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				foreach (string s in list)
				{
					sb.Append(s).Append(SListSeparator);
				}

				SetString(key, sb.ToString());
			}
		}
		public static Dictionary<int, int> GetDicInt(string key)
		{
			string s = GetString(key);
			if (string.IsNullOrEmpty(s))
			{
				return new Dictionary<int, int>();
			}

			return JsonConvert.DeserializeObject<Dictionary<int,int>>(s);
		}

		public static void SetDicInt(string key, Dictionary<int, int> dic)
		{
			if (dic != null)
			{
				SetString(key, JsonConvert.SerializeObject(dic));
			}
       
		}
		public static void Clear()
		{
			PlayerPrefs.DeleteAll();
		}
		public static DateTime GetDate(string key, DateTime defaultValue = default)
		{
			if (PlayerPrefs.HasKey(key))
			{
				long dateLong = long.Parse(PlayerPrefs.GetString(key));
				return DateTime.FromBinary(dateLong);
			}

			SetDate(key, defaultValue);
			return defaultValue;
		}

		public static void SetDate(string key, DateTime value)
		{
			PlayerPrefs.SetString(key, value.ToBinary().ToString());
		}

		public static bool GetBool(string key, bool defaultValue = false)
		{
			if (PlayerPrefs.HasKey(key))
			{
				bool value = PlayerPrefs.GetInt(key) == 1;
				return value;
			}

			SetBool(key, defaultValue);
			return defaultValue;
		}

		public static void SetBool(string key, bool value)
		{
			PlayerPrefs.SetInt(key, value ? 1 : 0);
		}
	}

	public abstract class PrefsValue<T>
	{
		public readonly string Key;

		protected bool _inited;
		protected T _defaultValue;
		protected T _value;
		public T Value {
			get
			{
				if (_inited) return _value;

				_value = Load(_defaultValue);
				_inited = true;
				return _value;
			}
			private set => _value = value;
		}

		public event Action<T> OnUpdate = ignore => { };

		public PrefsValue(string key, T defaultValue = default)
		{
			Key = key;
			_defaultValue = defaultValue;
		}

		public void Set(T value, bool isSilent = false)
		{
			Value = value;
			_inited = true;
			Save();
			if(!isSilent) OnUpdate(value);
		}

		protected abstract void Save();
		protected abstract T Load(T defaultValue = default);
	}

	public class PrefsString : PrefsValue<string>
	{
		public PrefsString(string key, string defaultValue = default) : base(key, defaultValue)
		{
		}

		protected override void Save() => PlayerPrefs.SetString(Key, Value);

		protected override string Load(string defaultValue = default) => PlayerPrefs.GetString(Key, defaultValue);
	}

	public class PrefsInt : PrefsValue<int>
	{
		public PrefsInt(string key, int defaultValue = default) : base(key, defaultValue)
		{
		}

		protected override void Save() => PlayerPrefs.SetInt(Key, Value);

		protected override int Load(int defaultValue = default) => PlayerPrefs.GetInt(Key, defaultValue);

		public void Add(int delta) => Set(Value + delta);
		public void Multiply(int mult) => Set(Value * mult);
		public void Multiply(float mult) => Set((int) (Value * mult));

		public static PrefsInt operator ++(PrefsInt value)
		{
			value.Set(value.Value + 1);
			return value;
		}

		public static implicit operator int(PrefsInt value) => value.Value;
	}

	public class PrefsBool : PrefsValue<bool>
	{
		public PrefsBool(string key, bool defaultValue = default) : base(key, defaultValue)
		{
		}

		protected override void Save() => PlayerPrefs.SetInt(Key, Value ? 1 : 0);

		protected override bool Load(bool defaultValue = default) => PlayerPrefs.GetInt(Key, defaultValue ? 1 : 0) == 1;
	}

	public class PrefsDateTime : PrefsValue<DateTime>
	{
		public PrefsDateTime(string key, DateTime defaultValue = default) : base(key, defaultValue)
		{
		}

		protected override void Save()
		{
			PlayerPrefs.SetString(Key, Value.ToBinary().ToString());
		}

		protected override DateTime Load(DateTime defaultValue = default)
		{
			if (PlayerPrefs.HasKey(Key))
			{
				long dateLong = long.Parse(PlayerPrefs.GetString(Key));
				return DateTime.FromBinary(dateLong);
			}

			return defaultValue;
		}
	}

	public class PrefsList<T> : PrefsValue<List<T>>
	{
		private char _separator;
		private readonly Func<string, T> _converter;
		
		public PrefsList(string key, Func<string, T> converter, char separator = ',', List<T> defaultValue = default) : base(key, defaultValue)
		{
			_separator = separator;
			_converter = converter;
		}

		public void ForceSave() => Save();

		protected override void Save()
		{
			PlayerPrefs.SetString(Key, string.Join(_separator.ToString(), Value));
		}

		protected override List<T> Load(List<T> defaultValue = default)
		{
			if (!PlayerPrefs.HasKey(Key)) return defaultValue;

			var str = PlayerPrefs.GetString(Key);
			if (string.IsNullOrEmpty(str)) return defaultValue;

			if (!str.Contains(_separator))
			{
				return new List<T> {_converter(str)};
			}

			return str.Split(_separator).Where(a => !string.IsNullOrEmpty(a)).Select(a => _converter(a)).ToList();
		}
	}
}