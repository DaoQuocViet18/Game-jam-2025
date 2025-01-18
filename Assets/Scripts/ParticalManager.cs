using System.Collections.Generic;
using UnityEngine;

public enum BubbleColor
{
    Blue,
    Yellow,
    Orange,
    White,
    Purple
}

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    [Header("Particle Settings")]
    public GameObject particlePrefab;
    public int poolSize = 10;

    private Queue<GameObject> particlePool;

    [Header("Bubble Textures")]
    [SerializeField] private Texture blueTexture;
    [SerializeField] private Texture yellowTexture;
    [SerializeField] private Texture orangeTexture;
    [SerializeField] private Texture whiteTexture;
    [SerializeField] private Texture purpleTexture;


    private void Awake()
    {
        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializePool();
    }

    private void InitializePool()
    {
        particlePool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject particle = Instantiate(particlePrefab);
            particle.SetActive(false); // Ban đầu tắt hiệu ứng
            particlePool.Enqueue(particle);
        }
    }

    public void PlayParticle(Vector3 position, Quaternion rotation, BubbleColor bubbleColor)
    {
        if (particlePool.Count > 0)
        {
            GameObject particle = particlePool.Dequeue(); // Lấy particle từ pool
            particle.transform.position = position;
            particle.transform.rotation = rotation;
            particle.SetActive(true);

            // Lấy Renderer và Material của Particle
            Renderer particleRenderer = particle.GetComponent<Renderer>();
            if (particleRenderer != null)
            {
                // Chọn Texture dựa trên BubbleColor
                Texture selectedTexture = null;
                switch (bubbleColor)
                {
                    case BubbleColor.Blue:
                        selectedTexture = blueTexture;
                        break;
                    case BubbleColor.Yellow:
                        selectedTexture = yellowTexture;
                        break;
                    case BubbleColor.Orange:
                        selectedTexture = orangeTexture;
                        break;
                    case BubbleColor.White:
                        selectedTexture = whiteTexture;
                        break;
                    case BubbleColor.Purple:
                        selectedTexture = purpleTexture;
                        break;   
                }

                // Đổi Albedo Map
                if (selectedTexture != null)
                {
                    particleRenderer.material.SetTexture("_MainTex", selectedTexture);
                }
            }

            // Tắt particle sau một khoảng thời gian (dựa trên duration của Particle System)
            float particleDuration = particle.GetComponent<ParticleSystem>().main.duration;
            StartCoroutine(ReturnToPool(particle, particleDuration));
        }
        else
        {
            Debug.LogWarning("Particle Pool is empty! Consider increasing pool size.");
        }
    }

    private System.Collections.IEnumerator ReturnToPool(GameObject particle, float delay)
    {
        yield return new WaitForSeconds(delay);
        particle.SetActive(false);
        particlePool.Enqueue(particle); // Đưa particle trở lại pool
    }
}
