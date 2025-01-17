using UnityEngine;

public class IncreaseScale : MonoBehaviour
{
    public Vector3 incrementVector = new Vector3(0.2f, 0.2f, 0.2f); // Giá trị tăng mỗi lần click (10%)
    public void IncreaseObjectScale()
    {
        transform.localScale += incrementVector;
    }
}
