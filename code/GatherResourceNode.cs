using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType //Kaynak çeşitleri
{
    Undefined,  //Belirsiz
    Tree,       //Ağaç
    Ore         //Maden
}


[CreateAssetMenu(menuName ="Data/Tool action/Gather Resource Node")]
public class GatherResourceNode : ToolAction
{
    [SerializeField] float sizeOfInteractableArea = 1f; 
    [SerializeField] List<ResourceNodeType> canHitNodesOfType; //Hasar verilebilir kaynaklar listesi
    public override bool OnApply(Vector2 worldPoint)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea); //Colliderlar fiziğinin erişilebilir
                                                                                                  //alana bağlanması
        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null) 
            {

                if (hit.CanBeHit(canHitNodesOfType) == true) //Doğru eşya ile doğru kaynağı toplama
                {
                    hit.Hit();
                    return true;
                }
                
            }
        }

        return false;
    }
}
