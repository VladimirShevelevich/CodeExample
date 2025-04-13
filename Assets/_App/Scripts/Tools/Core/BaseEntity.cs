namespace _App.Scripts.Tools.Core
{
    public abstract class BaseEntity<TCtx> : BaseDisposable
    {
        protected TCtx Ctx {get; private set; }
        protected Container Container {get; private set; }

        private void SetCtx(TCtx ctx, Container parentContainer = null, bool useAutoResolve = true)
        {
            Container = new Container(parentContainer);
            Ctx = ctx;
            
            Initialize();
        }

        protected abstract void Initialize();

        protected TEntity CreateEntity<TEntity, UCtx>(UCtx ctx) where TEntity : BaseEntity<UCtx>, new()
        {
            var newEntity = new TEntity();
            newEntity.SetCtx(ctx, Container);
            AddDisposable(newEntity);
            return newEntity;
        }
        
        /// <summary>
        /// for the creation outside of the composition root
        /// </summary>
        public static TEntity CreateEntityManually<TEntity, UCtx>(UCtx ctx) where TEntity : BaseEntity<UCtx>, new()
        {
            var newEntity = new TEntity();
            newEntity.SetCtx(ctx);
            return newEntity;
        }
    }
}