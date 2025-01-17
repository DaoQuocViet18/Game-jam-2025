using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Slider progressBar;
    public Button clickButton;
    private float progress = 0f;
    private const float increment = 0.1f; // Giá trị tăng mỗi lần click (10%)
    public Vector3 incrementVector = new Vector3(0.1f, 0.1f, 0.1f); // Giá trị tăng mỗi lần click (10%)

    void Start()
    {
        clickButton.onClick.AddListener(OnButtonClick);
        progressBar.value = progress;
    }
    void OnButtonClick()
    {
        if (progressBar.value < 1f)
        {
            progress += increment;
            progressBar.value = progress;
        }
        transform.localScale += incrementVector;
    }
}