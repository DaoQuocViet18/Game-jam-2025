using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinGameUI : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private GameObject WinPanel;

    private ConfirmDialogue confirmDialog;
    private bool isLost;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNextLevelBtnClick);
        replayButton.onClick.AddListener(OnReplayBtnClick);
        homeButton.onClick.AddListener(OnHomeBtnClick);
    }
    
    private void OnNextLevelBtnClick()
    {
        // Move to next level
        SceneManager.LoadScene(nextSceneLoad);

        // Setting the next level to be unlocked
        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }
    }
    
    private void OnReplayBtnClick()
    {
        // Replay current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
        
    private void OnHomeBtnClick()
    {
        // Go to home scene
        Loader.Instance.LoadWithFade(SceneName.MainMenuScene);
    }
    
    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
    }
    
    public void HideWinPanel()
    {
        WinPanel.SetActive(false);
    }
    
    public void ShowConfirmDialogue(ConfirmDialogue confirmDialogue)
    {
        confirmDialog = confirmDialogue;
    }

    public int nextSceneLoad;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnWinGame(IEventParam param)
    {
        ShowWinPanel();
    }
}