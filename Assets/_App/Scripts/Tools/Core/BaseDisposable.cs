using System;
using System.Collections.Generic;

namespace _App.Scripts.Tools.Core
{
    public abstract class BaseDisposable : IDisposable
    {
        private List<IDisposable> _disposables;
        protected bool Disposed { get; private set; }

        protected void AddDisposable(IDisposable disposable)
        {
            _disposables ??= new List<IDisposable>();
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            if (Disposed)
                return;

            Disposed = true;

            if (_disposables != null)
            {
                foreach (var disposable in _disposables)
                    disposable?.Dispose();

                _disposables.Clear();
            }
            OnDispose();
        }

        protected virtual void OnDispose() { }
    }
}