namespace _App.Scripts.Tools.Core
{
    public abstract class BaseEntity: BaseDisposable
    {
        protected Container Container {get; private set; }

        protected BaseEntity(Container parentContainer)
        {
            Container = new Container(parentContainer);
        }
    }
}