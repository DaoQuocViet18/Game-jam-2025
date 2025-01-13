using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button selectLevelButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject pausePanel;

    private ConfirmDialogue confirmDialog;

    private bool isPaused;

    private void Start() {
        pauseButton.onClick.AddListener(() => {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        });

        homeButton.onClick.AddListener(OnHomeBtnClick);
        selectLevelButton.onClick.AddListener(OnSelectLevelBtnClick);
        resumeButton.onClick.AddListener(() => {
            Resume();
        });

        pausePanel.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    private void Pause() {
        isPaused = true;
        PauseController.Instance.Pause();
        pausePanel.SetActive(true);
    }

    private void Resume() {
        if (confirmDialog != null) {
            confirmDialog.Hide();
            return;
        }

        UtilClass.PlayTransformFadeOutAnimation(pausePanel.transform, pausePanel.GetComponent<CanvasGroup>(), () => {
            isPaused = false;
            PauseController.Instance.Resume();
            pausePanel.SetActive(false);
        });
    }

    private async void OnHomeBtnClick() {
        confirmDialog = ConfirmDialogue.Create();
        bool result = await confirmDialog.Show();
        if(result) {
            Loader.Instance.LoadWithFade(SceneName.MainMenuScene);
        }
    }

    private async void OnSelectLevelBtnClick() {
        confirmDialog = ConfirmDialogue.Create();
        bool result = await confirmDialog.Show();
        if(result) {
            Loader.Instance.LoadWithFade(SceneName.SelectLevelScene);
        }
    }
}
