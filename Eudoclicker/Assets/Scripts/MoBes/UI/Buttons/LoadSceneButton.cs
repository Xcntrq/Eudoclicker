using nsSceneField;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace nsLoadSceneButton
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private SceneField _sceneField;
        [SerializeField] private LoadSceneMode _loadSceneMode;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => SceneManager.LoadScene(_sceneField, _loadSceneMode));
        }
    }
}
