using DG.Tweening;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DustAndShrinkEffectController : Singleton<DustAndShrinkEffectController>
{
    [SerializeField] private List<GameObject> poolObjs;

    private GameObject GetObjectFromPool(GameObject prefab, Vector3 scale, Vector3 position)
    {
        foreach (GameObject poolObj in this.poolObjs)
        {
            if (poolObj == null) continue;

            if (poolObj.GetComponent<SpriteRenderer>()?.sprite == prefab.GetComponent<SpriteRenderer>()?.sprite)
            {
                this.poolObjs.Remove(poolObj);
                poolObj.SetActive(true);
                poolObj.transform.localScale = scale;
                poolObj.transform.position = position;
                return poolObj;
            }
        }

        GameObject newPrefab = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
        newPrefab.name = prefab.name;
        newPrefab.transform.SetParent(this.gameObject.transform, true);
        newPrefab.transform.localScale = scale;
        newPrefab.transform.position = position;
        if (newPrefab.TryGetComponent<BoxCollider2D>(out BoxCollider2D collider))
            collider.enabled = false; // Vô hiệu hóa BoxCollider2D

        return newPrefab;
    }
    private void Despawn(GameObject obj)
    {
        this.poolObjs.Add(obj);
        obj.SetActive(false);
    }

    public async void StretchAndShrinkAnimation(GameObject objSprite, float stretchDuration, float shrinkDuration, Vector3 stretchScale)
    {
        GameObject animObj = GetObjectFromPool(objSprite, objSprite.transform.localScale, objSprite.transform.position);
       
        // Kéo giãn
        await animObj.transform.DOScale(stretchScale, stretchDuration).AsyncWaitForCompletion();

        // Thu nhỏ
        await animObj.transform.DOScale(Vector3.zero, shrinkDuration).AsyncWaitForCompletion();

        Despawn(animObj);
    }
}
