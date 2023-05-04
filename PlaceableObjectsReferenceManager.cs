using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectsReferenceManager : MonoBehaviour
{
    public PlaceableObjectsManager placeableObjectsManager;

    public void Place(Item item, Vector3Int pos)
    {
        if (placeableObjectsManager == null)
        {
            Debug.LogWarning("placeableObjectsManager referans� alg�lanmad�");
            return;
        }
        placeableObjectsManager.Place(item, pos);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        if (placeableObjectsManager == null)
        {
            Debug.LogWarning("placeableObjectsManager referans� alg�lanmad�");
            return;
        }

        placeableObjectsManager.PickUp(gridPosition);
    }

    public bool Check(Vector3Int pos)
    {
        if (placeableObjectsManager == null)
        {
            Debug.LogWarning("placeableObjectsManager referans� alg�lanmad�");
            return false;
        }
       return placeableObjectsManager.Check(pos);
    }
}
