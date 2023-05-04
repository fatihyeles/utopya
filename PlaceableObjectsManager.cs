using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObjectsManager : MonoBehaviour
{
    [SerializeField] PlaceableObjectsContainer placeableObjects;
    [SerializeField] Tilemap targetTilemap;

    private void Start()
    {
        GameManeger.instance.GetComponent<PlaceableObjectsReferenceManager>().placeableObjectsManager = this;
        VisualizeMap();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < placeableObjects.placeableObjects.Count; i++)
        {
            if (placeableObjects.placeableObjects[i].targetObject == null) { continue; }
            IPersistant persistant = placeableObjects.placeableObjects[i].targetObject.GetComponent<IPersistant>();
            if (persistant != null)
            {
                string jsonString = persistant.Read();
                placeableObjects.placeableObjects[i].objectState = jsonString;

            }

            placeableObjects.placeableObjects[i].targetObject = null;
        }
    }

    private void VisualizeMap()
    {
        for (int i = 0; i < placeableObjects.placeableObjects.Count; i++)
        {
            VisualizeItem(placeableObjects.placeableObjects[i]);
        }
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        PlaceableObject placedObject = placeableObjects.Get(gridPosition);

        if (placedObject == null)
        {
            return;
        }

        ItemSpawnManager.instance.SpawnItem(
            targetTilemap.CellToWorld(gridPosition),
            placedObject.placedItem,
            1
            );

        Destroy(placedObject.targetObject.gameObject);

        placeableObjects.Remove(placedObject);
    }

    private void VisualizeItem(PlaceableObject placeableObject)
    {
        GameObject go = Instantiate(placeableObject.placedItem.itemPrefab);
        go.transform.parent = transform;
        Vector3 position =
            targetTilemap.CellToWorld(placeableObject.positionOnGrid)
            + targetTilemap.cellSize / 2;
        position += Vector3.forward * 0.1f;


        go.transform.position = position;

        IPersistant persistant = go.GetComponent<IPersistant>();
        if (persistant != null)
        {
            persistant.Load(placeableObject.objectState);
        }
        placeableObject.targetObject = go.transform;
    }

    public bool Check(Vector3Int position)
    {
        return placeableObjects.Get(position) != null;
    }

    public void Place(Item item, Vector3Int positionOnGrid)
    {
        if (Check(positionOnGrid) == true) { return; }
        PlaceableObject placeableObject = new PlaceableObject(item, positionOnGrid);
        VisualizeItem(placeableObject);
        placeableObjects.placeableObjects.Add(placeableObject);
    }
}
