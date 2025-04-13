using System;
using Object = UnityEngine.Object;

namespace _App.Scripts.Tools.Disposables
{
    public class GameObjectDisposer : IDisposable
    {
        private readonly Object _gameObject;

        public GameObjectDisposer(Object gameObject)
        {
            _gameObject = gameObject;
        }

        public void Dispose()
        {
            Object.Destroy(_gameObject);
        }
    }
}