using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : MonoBehaviour
{
    // Mảng các GameObject
    [SerializeField] private GameObject[] gameObjectKeys; // Các GameObject đầu vào
    [SerializeField] private GameObject[] gameObjectValues; // Các GameObject cần kích hoạt

    public void Active(GameObject obj)
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
            Debug.Log("obj: " + obj);
            if (gameObjectKeys[i] == obj) // Nếu obj khớp với phần tử trong mảng gameObjectKeys
            {
                Debug.Log("gameObjectKeys[i]: " + gameObjectKeys[i]);
                if (gameObjectValues[i] != null)
                {
                    gameObjectValues[i].SetActive(true); // Kích hoạt GameObject tương ứng
                    Debug.Log($"Đã kích hoạt {gameObjectValues[i].name}");
                }
                else
                {
                    Debug.LogWarning($"Giá trị tại gameObjectValues[{i}] là null!");
                }
            }
        }
    }
}
