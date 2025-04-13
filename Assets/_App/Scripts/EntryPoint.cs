using _App.Scripts.Content;
using _App.Scripts.Root;
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
            _rootEntity = RootEntity.CreateEntityManually<RootEntity, RootEntity.Ctx>(new RootEntity.Ctx
            {
                ContentProvider = _contentProvider,
                UiCanvas = _uiCanvas
            });
        }

        private void OnDestroy()
        {
            _rootEntity.Dispose();
        }
    }
}