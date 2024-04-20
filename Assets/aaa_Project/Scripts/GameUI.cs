using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace aaa_Project.Scripts
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]private Button _restartButton;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnClickRestartButton);
        }

        private void OnClickRestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
