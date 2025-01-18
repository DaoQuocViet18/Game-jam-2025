using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : MonoBehaviour
{
    // Mảng các GameObject
    [SerializeField] private GameObject[] gameObjectKeys; // Các GameObject đầu vào
    [SerializeField] private GameObject[] gameObjectValues; // Các GameObject cần kích hoạt

    [SerializeField] BubbleColor bubbleColor;

    public void Active(GameObject impactObj, GameObject staticObj)
    {
        // Kiểm tra mảng đã được khởi tạo và có cùng kích thước
        if (gameObjectKeys == null || gameObjectValues == null || gameObjectKeys.Length != gameObjectValues.Length)
        {
            Debug.LogWarning("Mảng gameObjectKeys và gameObjectValues chưa được khởi tạo hoặc không đồng bộ kích thước!");
            return;
        }

        // Duyệt qua mảng để kiểm tra và kích hoạt
        for (int i = 0; i < gameObjectKeys.Length; i++)
        {
            Debug.Log("gameObjectKeys[i]: " + gameObjectKeys[i]);
            Debug.Log("obj: " + impactObj);
            if (gameObjectKeys[i] == impactObj) // Nếu obj khớp với phần tử trong mảng gameObjectKeys
            {
                Debug.Log("gameObjectKeys[i]: " + gameObjectKeys[i]);
                if (gameObjectValues[i] != null)
                {
                    ParticleManager.Instance.PlayParticle(this.transform.position, this.transform.rotation, bubbleColor);

                    gameObjectValues[i].SetActive(true); // Kích hoạt GameObject tương ứng
                    Debug.Log($"Đã kích hoạt {gameObjectValues[i].name}");
                    EventDispatcher.Dispatch(new EventDefine.OnIncreasePoint());

                    DustAndShrinkEffectController.Instance.StretchAndShrinkAnimation(impactObj, 0.5f, 0.5f, new Vector3(1f, 2f, 1f));
                    ChatBubble.Create(transform, new Vector3(0, 1), "Thank you ill take that");
                    AudioManager.Instance.PlaySoundWithRandomPitch(GameAudioClip.POP);
                    Destroy(impactObj);
                    Destroy(staticObj);
                }
                else
                {
                    Debug.LogWarning($"Giá trị tại gameObjectValues[{i}] là null!");
                }
            }
        }
    }
}
