using UnityEngine;
using UnityEngine.UI;

public class CreditScrollUI : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private Button homeButton;

    [SerializeField] private bool isScrolling = true;

    private void Start()
    {
        homeButton.onClick.AddListener(() => Loader.Instance.LoadWithFade(SceneName.MainMenuScene));

        // Reset scroll position to the top
        scrollRect.verticalNormalizedPosition = 1f;
    }

    private void Update()
    {
        if (isScrolling)
        {
            // Scroll the credits down
            scrollRect.verticalNormalizedPosition -= Time.deltaTime / scrollSpeed;

            // Stop scrolling at the end
            if (scrollRect.verticalNormalizedPosition <= 0f)
            {
                isScrolling = false;
            }
        }
    }
}
