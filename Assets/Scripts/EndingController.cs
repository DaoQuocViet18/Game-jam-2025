using DG.Tweening;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject character;
    [SerializeField] GameObject BGScroll;
    [SerializeField] GameObject thankYou;
    [SerializeField] GameObject homeBtn;

    private void Start()
    {
        bGScroll();
        characterTween();
        cameraTween();
    }

    void bGScroll()
    {
        // Smooth infinite background scrolling
        BGScroll.transform.DOMoveY(-58.3f, 3.8f)
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
             character.transform.DOMoveY(20f, 2f)
              .SetEase(Ease.OutQuad)
              .OnComplete(() =>
              {
                  thankYou.transform.DOScale(1.6f, 0.5f)
                  .OnComplete(() =>
                  {
                      homeBtn.SetActive(true);
                  });

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

    public void OnHomeBtnClick()
    {
        // Go to home scene
        Loader.Instance.LoadWithFade(SceneName.MainMenuScene);
    }
}
