using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    public void Craft(CraftingRecipe recipe)
    {
        if (inventory.CheckFreeSpace() == false)
        {
            Debug.Log("Üretim sonrasý eþyayý sýðdýracak alan yok");
            return;
        }

        for(int i = 0; i < recipe.elements.Count; i++)
        {
            if (inventory.CheckItem(recipe.elements[i]) == false)
            {
                
                Debug.Log("Üretim için gerekli malzemeler envanterde deðil");
                return;
            }    
        }


        for (int i = 0; i < recipe.elements.Count; i++)
        {
            inventory.Remove(recipe.elements[i].item, recipe.elements[i].count);
        }

        inventory.Add(recipe.output.item, recipe.output.count);
        
    }
}
