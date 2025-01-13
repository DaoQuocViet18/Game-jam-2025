using UnityEngine;

public class TrashCan : MonoBehaviour, IInteractableObject
{
    public void Interact(GameObject targetObject)
    {
        Destroy(targetObject);
        ChatBubble.Create(transform, new Vector3(0, 1), "Bye bye object");
        AudioManager.Instance.PlaySoundWithRandomPitch(GameAudioClip.POP);
    }
}
