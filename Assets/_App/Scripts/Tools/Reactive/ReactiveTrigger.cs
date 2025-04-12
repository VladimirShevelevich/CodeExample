using System;
using UniRx;

namespace _App.Scripts.Tools.Reactive
{
    public class ReactiveTrigger : IReadOnlyReactiveTrigger, IDisposable
    {
        private readonly Subject<bool> _subject;

        public ReactiveTrigger()
        {
            _subject = new Subject<bool>();
        }

        public void Dispose()
        {
            _subject.Dispose();
        }

        public IDisposable Subscribe(Action action)
        {
            return _subject.Subscribe(b => action?.Invoke());
        }

        public void Notify()
        {
            _subject.OnNext(true);
        }
    }
}