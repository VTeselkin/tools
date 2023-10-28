using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameSoft.Tools.ZenjectExtensions
{
    public class InstantiateManager : IInitializable
    {
        private static InstantiateManager _instance;
        [Inject] private readonly DiContainer _diContainer;

        public void Initialize()
        {
            _instance = this;
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

        public static T InstantiateAndInject<T>(T component, Transform parent, bool stayWorldPosition)
            where T : Component
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

        public static T InstantiateAndInject<T>(T component, Transform parent, Vector3 position, Quaternion rotation)
            where T : Component
        {
            var result = Object.Instantiate(component, parent);
            var transform = result.transform;
            transform.position = position;
            transform.rotation = rotation;
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
            CheckInit();
            _instance._diContainer.Inject(injectable);
            _instance.AfterInject(injectable);
        }

        public static void Inject(Component component)
        {
            CheckInit();
            _instance._diContainer.Inject(component);
            var injectChildren = component.GetComponentsInChildren<IAutoInjectable>();
            foreach (var t in injectChildren)
            {
                _instance._diContainer.Inject(t);
                _instance.AfterInject(t);
            }

            _instance.AfterInject(component);
        }

        private void AfterInject(object injectable)
        {
            var initialized = injectable as IInitializable;
            initialized?.Initialize();
        }

        private static void CheckInit()
        {
            if (_instance == null)
                throw new Exception("InstantiateManager is not initialized! " +
                                    "Add ToolInstaller in to Prefab Installer Zenject!");
        }
    }
}