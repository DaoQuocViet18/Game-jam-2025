using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class TypeWriter : MonoBehaviour {
    [Header("Dont touch me")]
    public string currentText = "";
    public bool isFinishedTyping = false;

    private Action onTextShowed;
    private Coroutine coroutineTypewriting;
    [SerializeField] private TextMeshPro textMeshPro;
    private float typingDelay;

    public void ShowText(string text, float typingDelay, Action onTextShowed = null) {
        this.onTextShowed = onTextShowed;
        this.typingDelay = typingDelay;

        if (coroutineTypewriting != null) {
            StopCoroutine(coroutineTypewriting);
        }

        coroutineTypewriting = StartCoroutine(Typewriting(text));
    }

    private IEnumerator Typewriting(string text) {
        isFinishedTyping = false;
        currentText = text;
        textMeshPro.text = "";

        foreach (char c in text) {
            textMeshPro.text += c;

            if (c == '.' || c == '!') {
                yield return new WaitForSeconds(typingDelay * 2);
            } else {
                yield return new WaitForSeconds(typingDelay);
            }
        }

        // Wait a bit before enabling the next sentence
        yield return new WaitForSeconds(0.6f);
        onTextShowed?.Invoke();
        isFinishedTyping = true;

    }

    public void SkipTypewriter() {
        if (coroutineTypewriting != null) {
            StopCoroutine(coroutineTypewriting);
        }

        textMeshPro.text = currentText;
        onTextShowed?.Invoke();
        isFinishedTyping = true;
    }
}