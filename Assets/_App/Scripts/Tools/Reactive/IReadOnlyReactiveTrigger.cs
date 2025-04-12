using System;

namespace _App.Scripts.Tools.Reactive
{
    public interface IReadOnlyReactiveTrigger
    {
        IDisposable Subscribe(Action action);
    }
}