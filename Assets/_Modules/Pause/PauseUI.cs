using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button selectLevelButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject mascot;
    [SerializeField] private float stretchDuration;
    [SerializeField] private Vector3 stretchScale = new Vector3(5f, 5f, 1f); 
    [SerializeField] private Vector3 originalScale;
    [SerializeField] private GameObject pausePanel;

    private ConfirmDialogue confirmDialog;

    private bool isPaused;

    private void Start() {
        originalScale = this.mascot.transform.localScale;

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
        mascot.SetActive(false);
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

    private async void Pause() {
        isPaused = true;
        mascot.SetActive(true);
        await mascot.transform.DOScale(stretchScale, stretchDuration).AsyncWaitForCompletion();
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
            mascot.SetActive(false);
            PauseController.Instance.Resume();
            mascot.transform.localScale = originalScale;
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
