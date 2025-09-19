using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;


namespace Components.Container
{
    public class ObjectsContainer : IObjectsContainer, IDisposable
    {
        private readonly Stack<IDisposable> _disposables = new();

        private readonly Dictionary<Type, object> _objects = new();

        public static ObjectsContainer DebugInstance { get; private set; }

        public void Initialize()
        {
            DebugInstance = this;
        }

        public void Dispose()
        {
            while (_disposables.Count > 0) _disposables.Pop().Dispose();

            _objects.Clear();
            DebugInstance = null;
        }

        public T Register<T>(T instance) where T : class
        {
            var targetType = typeof(T);
            if (_objects.ContainsKey(targetType))
            {
                Debug.LogError($"Container contains object for type '{targetType.FullName}'.");
                return instance;
            }

            _objects.Add(targetType, instance);

            if (instance is IDisposable disposableInstance)
                if (_disposables.Contains(disposableInstance) == false)
                    _disposables.Push(disposableInstance);

            return instance;
        }

        public T Get<T>() where T : class
        {
            var targetType = typeof(T);
            if (_objects.ContainsKey(targetType) == false)
            {
                Debug.LogError($"Container does not contains object for type '{targetType.FullName}'.");
                return default;
            }

            var instance = (T)_objects[targetType];

            return instance;
        }

        public void Unregister<T>() where T : class
        {
            var targetType = typeof(T);
            if (_objects.ContainsKey(targetType) == false)
            {
                Debug.LogError($"Container does not contains object for type '{targetType.FullName}'.");
                return;
            }

            var instance = (T)_objects[targetType];
            _objects.Remove(targetType);

            if (instance is IDisposable disposableInstance)
                if (_disposables.Contains(disposableInstance))
                {
                    var temp = new Stack<IDisposable>();

                    while (_disposables.Count > 0)
                    {
                        var top = _disposables.Pop();
                        if (top == disposableInstance)
                            break;
                        temp.Push(top);
                    }

                    _disposables.PushRange(temp);
                }
        }
    }
}