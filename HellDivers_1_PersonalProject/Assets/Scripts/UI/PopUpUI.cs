using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpUI : MonoBehaviour
{
    [SerializeField] private Image _gameOverPopup;
    [SerializeField] private Button _titleButton;
    [SerializeField] private Button _retryButton;

    [SerializeField] private Image _ClearPopup;
    [SerializeField] private TextMeshProUGUI _totalKillText;
    [SerializeField] private Button _clearTitleButton;

    private void Awake()
    {
        Time.timeScale = 1;
        _gameOverPopup.gameObject.SetActive(false);
        _clearTitleButton.gameObject.SetActive(false);
    }

    private void Start()
    {
        PlayerController.Instance.OnGameOver += SetGameOverPopup;
        _retryButton.onClick.AddListener(OnRetryButton);
        _titleButton.onClick.AddListener(OnTitleButton);
        _clearTitleButton.onClick.AddListener(OnTitleButton);
    }
    
    // 클리어 시 신호준다
    private void SetGameClearPopup()
    {
        if (GameManager.Instance.IsClear)
        {
            _totalKillText.text = $"Total Kill Enemy : {GameManager.Instance.TotalKill}";
            Time.timeScale = 0;
            _gameOverPopup.gameObject.SetActive(true);
        }
    }

    private void SetGameOverPopup()
    {
        Time.timeScale = 0;
        _gameOverPopup.gameObject.SetActive(true);
    }

    private void OnRetryButton()
    {
        Time.timeScale = 1;
        _gameOverPopup.gameObject.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }
    
    private void OnTitleButton()
    {
        Time.timeScale = 1;
        _gameOverPopup.gameObject.SetActive(false);
        SceneManager.LoadScene("TitleScene");
    }
}
