using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class woodSC : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f;//odun alınırken oyuncuya doğru harekte etme hızı sorumlu
    [SerializeField] float pickUpDistance = 1.5f;//odunları toplama değişkeni
    [SerializeField] float tff = 10f;//kaç saniye sonra toplanır

    public Item item;
    public int count = 1;

    private void Awake()//oyuncu karakterin,n dönüşümüne ilişkin referansı saklama 
    {
        player = GameManeger.instance.player.transform;
    }
    
    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;

    }
    private void Update()
    {
        tff -= Time.deltaTime;
        if (tff <0) { Destroy(gameObject); }

        float distance = Vector3.Distance(transform.position, player.position);//karakter ile nesne arasındaki mesafeyi kontrol etme
        if ( distance > pickUpDistance)//oyunucunun toplama mesafesinde olup olmadığını kontrol etme
        {
            return;
        }
        transform.position = Vector3.MoveTowards(
            transform.position, player.position, speed * Time.deltaTime);//nesneyi oynatıcıya doğru hareket ettirme

        if (distance < 0.1f)//mesafe (kaçsa) yok etme 
        {
            if (GameManeger.instance.inventoryContainer != null)//oyuncu eşyaya ulaştı zaman envantere eklemek için
            {
                GameManeger.instance.inventoryContainer.Add(item, count);
            }
            else
            {
                Debug.LogWarning("Game managere inventory container baglanmadi");
            }
            Destroy(gameObject);
        }
    }

}
