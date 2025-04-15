using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace _App.Scripts.Tools.Core
{
    public class Container : BaseDisposable
    {
        private readonly Dictionary<Type, object> _dependencies = new();
        private readonly Container _parent;
        
        public Container(Container parent = null)
        {
            _parent = parent;
        }

        public void Register<T>(object instance)
        {
            _dependencies[typeof(T)] = instance;
        }

        public T Resolve<T>()
        {
            if (_dependencies.TryGetValue(typeof(T), out var instance))
                return (T)instance;

            if (_parent != null)
                return _parent.Resolve<T>();

            throw new InvalidOperationException($"Dependency {typeof(T)} is not registered.");
        }
    }
}