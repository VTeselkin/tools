using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace GameSoft.Tools
{
	public class DDebug
	{
#if UNITY_IOS
        // [DllImport("__Internal")]
        // private static extern void _logToiOS(string s);
#endif
		private const string Tag = "[GameSoft] ";

		public static LogLevel LogLevelType { get; set; }
#if !RT_DEV
			= LogLevel.None;
#endif

		private const int LOG_SIZE = 50000;

		[Conditional("RT_DEV")]
		public static void Log(object message)
		{
			if (LogLevelType <= LogLevel.Debug)
			{
				Debug.Log(message);
#if UNITY_IOS && !UNITY_EDITOR
				// _logToiOS(message.ToString());
#endif
			}
		}

		[Conditional("RT_DEV")]
		public static void LogWarning(object message)
		{
			if (LogLevelType <= LogLevel.Warning)
			{
				Debug.LogWarning(message);
#if UNITY_IOS && !UNITY_EDITOR
				// _logToiOS(message.ToString());
#endif
			}
		}

		[Conditional("RT_DEV")]
		public static void LogError(object message)
		{
			if (LogLevelType <= LogLevel.Error)
			{
				Debug.LogError(message);
#if UNITY_IOS && !UNITY_EDITOR
				// _logToiOS(message.ToString());
#endif
			}
		}

		[Conditional("RT_DEV")]
		public static void LogException(Exception exception)
		{
			if (LogLevelType <= LogLevel.Error)
				Debug.LogException(exception);
		}

		[Conditional("RT_DEV")]
		public static void LogErrorFormat(string format, params object[] args)
		{
			if (LogLevelType <= LogLevel.Error)
				Debug.LogErrorFormat(Tag + format, args);
		}

		[Conditional("RT_DEV")]
		public static void LogFormat(string format, params object[] args)
		{
			if (LogLevelType <= LogLevel.Debug)
				Debug.LogFormat(format, args);
		}

		private static string[] GetSplitLongLog(object message)
		{
			if (message is string strMsg)
			{
				if (strMsg.Length <= LOG_SIZE) return new[] {strMsg};

				var parts = strMsg.Length / LOG_SIZE;
				var result = new string[parts];
				for (var i = 0; i < parts; i++)
				{
					result[i] = strMsg.Substring(i * LOG_SIZE, Mathf.Min(LOG_SIZE, strMsg.Length - i * LOG_SIZE));
				}

				return result;
			}

			return new[] {message.ToString()};
		}

		private static void SendLogWithSplitLongMessage(Action<string> logSender, object message)
		{
			var parts = GetSplitLongLog(message);
			if (parts.Length == 1)
			{
				logSender?.Invoke(Tag + message);
				return;
			}

			for (var i = 0; i < parts.Length; i++)
			{
				logSender?.Invoke($"{Tag} {(i + 1)}/{parts.Length} {parts[i]}");
			}
		}

		public enum LogLevel
		{
			Debug,
			Warning,
			Error,
			None
		}
	}
}