using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameSoft.Tools.ZenjectExtensions
{
	public class InstantiateManager: IInitializable
	{
		[Inject] private readonly DiContainer _diContainer;

		public static InstantiateManager Instance;

		public void Initialize()
		{
			Instance = this;
		}

		public static T InstantiateAndInject<T>(T component) where T : Component
		{
			var result = Object.Instantiate(component);
			Inject(result);

			return result;
		}

		public static T InstantiateAndInject<T>(T component, Transform parent) where T : Component
		{
			var result = Object.Instantiate(component, parent);
			Inject(result);

			return result;
		}

		public static T InstantiateAndInject<T>(T component, Transform parent, bool stayWorldPosition) where T : Component
		{
			var result = Object.Instantiate(component, parent, stayWorldPosition);
			Inject(result);

			return result;
		}
		
		public static T InstantiateAndInject<T>(T component, Transform parent, Vector3 position) where T : Component
		{
			var result = Object.Instantiate(component, parent);
			result.transform.position = position;
			Inject(result);

			return result;
		}
		
		public static T InstantiateAndInject<T>(T component, Transform parent, Vector3 position, Quaternion rotation) where T : Component
		{
			var result = Object.Instantiate(component, parent);
			result.transform.position = position;
			result.transform.rotation = rotation;
			Inject(result);

			return result;
		}

		public static T InstantiateAndInject<T>(T component, Vector3 position, Quaternion rotation) where T : Component
		{
			var result = Object.Instantiate(component, position, rotation);
			Inject(result);

			return result;
		}

		public static T NewAndInject<T>() where T : new()
		{
			var result = new T();
			Inject(result);

			return result;
		}
		
		public static T NewAndInject<T>(Func<T> newInstanceSelector)
		{
			var result = newInstanceSelector();
			Inject(result);

			return result;
		}

		public static void Inject(object injectable)
		{
			Instance._diContainer.Inject(injectable);
			Instance.AfterInject(injectable);
		}

		public static void Inject(Component component)
		{
			Instance._diContainer.Inject(component);

			var injectChildren = component.GetComponentsInChildren<IAutoInjectable>();
			for (var i = 0; i < injectChildren.Length; i++)
			{
				Instance._diContainer.Inject(injectChildren[i]);
				Instance.AfterInject(injectChildren[i]);
			}
			
			Instance.AfterInject(component);
		}

		private void AfterInject(object injectable)
		{
			var initializable = injectable as IInitializable;
			initializable?.Initialize();
		}
	}
}