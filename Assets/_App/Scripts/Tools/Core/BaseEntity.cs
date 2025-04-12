using System;
using System.Collections.Generic;
using UniRx;

namespace _App.Scripts.Tools.Core
{
    public abstract class BaseEntity<TCtx> : BaseDisposable
    {
        protected TCtx Context {get; private set; }
        protected Container Container {get; private set; }

        private void SetCtx(TCtx ctx, Container parentContainer = null, bool useAutoResolve = true)
        {
            Container = new Container(parentContainer);
            Context = ctx;
            
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
    }
}