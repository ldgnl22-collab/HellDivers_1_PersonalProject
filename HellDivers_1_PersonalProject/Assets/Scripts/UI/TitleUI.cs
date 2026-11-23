using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartButton);
    }

    private void OnStartButton()
    {
        SceneManager.LoadScene("TestScene");
    }
}
