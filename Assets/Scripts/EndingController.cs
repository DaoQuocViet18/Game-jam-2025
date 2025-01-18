using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EndingController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject character;
    [SerializeField] GameObject BGScroll;
    [SerializeField] Button exitButton;    
    [SerializeField] GameObject popupPanel;

    private void Start()
    {
        bGScroll();
        characterTween();
        cameraTween();
        popupPanel.SetActive(false);
        exitButton.gameObject.SetActive(false);
        exitButton.onClick.AddListener(() => {
            Application.Quit();
        });

    }

    void bGScroll()
    {
        // Smooth infinite background scrolling
        BGScroll.transform.DOMoveY(-58.3f, 2.8f)
         .SetLoops(-1, LoopType.Restart)
         .SetEase(Ease.InOutSine); // Smooth start and end for each loop
    }

    void characterTween()
    {
        // Smooth character bounce with easing
        character.transform.DOMoveY(-3.2f, 0.85f)
         .SetLoops(15, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad) // Smooth Yoyo effect
            .OnComplete(() =>
         {
             // Final rise of the character with a smooth finish
             character.transform.DOMoveY(10f, 1f) // Shorten the duration
              .SetEase(Ease.OutQuad)
              .OnComplete(() =>
              {
                 EndScene();
                 ShowPopupAndExitButton(); // Show popup and exit button immediately
              });
         });
    }

    void cameraTween()
    {
        // Smooth zoom effect for the camera
        DOVirtual.Float(cam.orthographicSize, 5, 4f, value =>
        {
            cam.orthographicSize = value;
        })
        .SetEase(Ease.InOutCubic); // Smooth zoom with acceleration and deceleration
    }

    void EndScene()
    {
        // No need for Invoke here as we are calling ShowPopupAndExitButton directly
    }

    void ShowPopupAndExitButton()
    {
        ShowPopupPanel();
        ShowExitButton();
    }

    void ShowPopupPanel()
    {
        popupPanel.SetActive(true);
    }
    
    void ShowExitButton()
    {
        exitButton.gameObject.SetActive(true);
    }
}
