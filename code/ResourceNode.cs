using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : ToolHit
{
    [SerializeField] GameObject pickUpDrop; //Kaynaklardan düþen toplanabilir ürünler.
   
    [SerializeField] float spread = 0.7f; //Ürünlerin düştükten sonra dağılma uzaklığı.

    [SerializeField] Item item; //Araçlar ve eşyalar.
    [SerializeField] int itemCountInOneDrop = 1; //Tek ürünün toplandıktan sonra gösterdiği ürün sayısı.
    [SerializeField] int dropCount = 5; //Tek kaynaktan düşen toplam ürün.
    [SerializeField] ResourceNodeType nodeType;


    public override void Hit()
    {
        while (dropCount > 0)//ağacı kestiğinizde onu oduna çevrime 
        {

            dropCount -= 1;
            Vector3 position = transform.position;//kaç odun topladığımız gösterecek 
            position.x += spread * UnityEngine.Random.value - spread / 2;       //Ürünlerin 2 boyutta dağıtılması
            position.y += spread * UnityEngine.Random.value - spread / 2;

            ItemSpawnManager.instance.SpawnItem(position, item, itemCountInOneDrop);
        }
        Destroy(gameObject);//basitçe yok etme
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}
