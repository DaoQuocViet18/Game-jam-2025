using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TutorialDialogueUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;

    [SerializeField] private int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && index < lines.Length - 1)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                textComponent.text = lines[index];
            }    
        }
    }

    private void StartDialogue()
    {
        textComponent.text = string.Empty;
        this.index = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (var item in lines[index].ToCharArray())
        {
            textComponent.text += item;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
