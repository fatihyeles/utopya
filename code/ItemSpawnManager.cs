using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject pickUpItemPrefab; //Düşen ürünün görüntüsü

    public void SpawnItem(Vector3 position, Item item, int count)
    {
        GameObject o = Instantiate(pickUpItemPrefab, position, Quaternion.identity); //Ağaçtan tahta düşmesi
        o.GetComponent<woodSC>().Set(item, count);
    }
}
