using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItemInfo
{
    public ItemData item;

    [Range(0f, 1f)]
    public float dropChance; // 0.0 ~ 1.0 사이 확률

    [Min(1)]
    public int quantity = 1; // 드랍 수량 (기본값 1)
}

public class Resource : MonoBehaviour
{
    [Header("드랍 설정")]
    public List<DropItemInfo> dropTable;
    public int quantityPerHit = 1;
    public int capacity;
    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityPerHit; i++)
        {
            if (capacity <= 0)
                break;

            capacity -= 1;

            // 확률 기반 드랍 처리
            DropItemInfo drop = GetRandomDrop();
            if (drop != null && drop.item != null && drop.item.dropPrefab != null)
            {
                for (int j = 0; j < drop.quantity; j++)
                {
                    Instantiate(drop.item.dropPrefab, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));
                }
            }

            if (capacity <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private DropItemInfo GetRandomDrop()
    {
        float roll = Random.value; // 0.0 ~ 1.0
        float cumulative = 0f;

        foreach (var drop in dropTable)
        {
            cumulative += drop.dropChance;
            if (roll <= cumulative)
            {
                return drop;
            }
        }

        return null; // 아무것도 안 나올 확률도 가능
    }
}
