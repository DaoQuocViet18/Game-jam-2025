using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour {

    public static void Create(Transform parent, Vector3 localPosition, string text) {
        Transform chatBubblePrefab = Resources.Load<Transform>("pfChatBubble");
        Transform chatBubbleTransform = Instantiate(chatBubblePrefab, parent);
        chatBubbleTransform.localPosition = localPosition;

        ChatBubble chatBubble = chatBubbleTransform.GetComponent<ChatBubble>();
        chatBubble.gameObject.SetActive(true); // Ensure the game object is active
        chatBubble.Setup(text);

        Destroy(chatBubbleTransform.gameObject, 6f);
    }


    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro textMeshPro;

    private TypeWriter typeWriter;

    private void Setup(string text) {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
        typeWriter = GetComponent<TypeWriter>();
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        Vector2 padding = new Vector2(7f, 3f);
        backgroundSpriteRenderer.size = textSize + padding;

        Vector3 offset = new Vector3(-3f, 0f);
        backgroundSpriteRenderer.transform.localPosition = 
            new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;

        typeWriter.ShowText(text, .05f, null);
    }
}
