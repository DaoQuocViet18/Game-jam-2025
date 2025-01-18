using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] GameObject btnGameObject;

    public void showhidebtn()
    {
        btnGameObject.SetActive(!btnGameObject.activeSelf);
    }

    private void Start()
    {
        setText();
    }

    void setText()
    {
        textMeshPro.text = GameManager.Instance.getCurrentHintText();
    }
}
