using UnityEngine;

public class PlayerTalk : MonoBehaviour {

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            ChatBubble.Create(transform, new Vector3(0, 1), "I dont like youuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu");
        }
    }
}
