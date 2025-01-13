using UnityEngine;

public class Person : MonoBehaviour, IInteractableObject {
    public void Interact(GameObject targetObject) {
        Destroy(targetObject);
        ChatBubble.Create(transform, new Vector3(0, 1), "Thank you ill take that");
        AudioManager.Instance.PlaySoundWithRandomPitch(GameAudioClip.POP);
    }
}
