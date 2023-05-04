using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
     {
    [SerializeField] GameObject highlighter; //Objelerin üzerinde beliren beyaz üçgen
    GameObject currentTarget;//vurgulanan hedefe referansı saklayacak yeni bir değişken yaratma

    public void Highlight(GameObject target)
      {
        if (currentTarget == target) //Hedef gösterimi
        {
            return;
        }
        currentTarget = target;
        Vector3 positiion = target.transform.position + Vector3.up * 0.6f;  //Objelerin üzerindeki üçgenin tam konumu
    Highlight(positiion);
     }
public void Highlight(Vector3 position)
     {
    highlighter.SetActive(true);
    highlighter.transform.position = position; // Beyaz üçgenin yakın pozisyonda aktif edilmesi
    }


    public void Hide()
    {
        currentTarget = null;
        highlighter.SetActive(false); //Hedef belirlenmediğinde beyaz üçgen kaybolur.
    }
      }
