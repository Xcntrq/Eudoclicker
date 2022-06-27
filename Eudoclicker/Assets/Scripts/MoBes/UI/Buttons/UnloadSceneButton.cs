using nsSceneField;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace nsUnloadSceneButton
{
    public class UnloadSceneButton : MonoBehaviour
    {
        [SerializeField] private SceneField _sceneField;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => SceneManager.UnloadSceneAsync(_sceneField));
        }
    }
}
