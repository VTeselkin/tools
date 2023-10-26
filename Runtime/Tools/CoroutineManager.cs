using System;
using UnityEngine;
using Zenject;
namespace GameSoft.Tools
{
	public class CoroutineManager : MonoBehaviour, IInitializable, IDisposable
	{
		public static CoroutineManager Instance;

		public void Initialize()
		{
			Instance = this;
		}

		public void Dispose()
		{
			if (Instance == null)
			{
				return;
			}

			Instance.StopAllCoroutines();
		}
	}
}