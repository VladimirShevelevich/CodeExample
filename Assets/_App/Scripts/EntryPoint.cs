using _App.Scripts.Content;
using _App.Scripts.Root;
using _App.Scripts.Tools.Core;
using UnityEngine;

namespace _App.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ContentProvider _contentProvider;
        [SerializeField] private Transform _uiCanvas;

        private RootEntity _rootEntity;

        private void Start()
        {
            CreateRootEntity();
        }

        private void CreateRootEntity()
        {
            _rootEntity = new RootEntity(new RootEntity.Ctx
            {
                ContentProvider = _contentProvider,
                UiCanvas = _uiCanvas
            }, 
                new Container());
        }

        private void OnDestroy()
        {
            _rootEntity.Dispose();
        }
    }
}