using UnityEngine;
using UnityEngine.EventSystems;
namespace GameSoft.Tools
{
    public class OnceOnScene : MonoBehaviour
    {
        private static OnceOnScene _instance;

        [SerializeField] private Camera _camera;
        [SerializeField] private EventSystem _eventSystem;

        public static Camera Camera => _instance._camera;
        public static EventSystem EventSystem => _instance._eventSystem;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
