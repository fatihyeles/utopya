using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;//eşyanın resmi gözükmesi için
    [SerializeField] Text text;
    [SerializeField] Image highlight;

    int myIndex;

    ItemPanel itemPanel;

    public void SetIndex(int index)//evanter düğmesinin dizini ayarı 
    {
        myIndex = index;
    }

    public void SetItemPanel(ItemPanel source)
    {
        itemPanel = source;
    }

    public void Set(ItemSlot slot)//öğemizi ekranda gösterecek şekilde ayarlama
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;

        if (slot.item.stackable == true)
        {
            text.gameObject.SetActive(true);//öğelerin istilenebilir olmaması durumunda öğe sayısını gizleme
            text.text = slot.count.ToString();
        }
        else
        {
            text.gameObject.SetActive(false);//öğelerin istilenebilir olmaması durumunda öğe sayısını gizleme
        }
    }
    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);


        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        itemPanel.OnClick(myIndex);
    }
    public void Highlight(bool b)
    {
        highlight.gameObject.SetActive(b);
    }

}
