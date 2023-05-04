using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnScreenMessage
{
    public GameObject go;
    public float timeToLive;

    public OnScreenMessage(GameObject go)
    {
        this.go = go;
    }
}
public class OnScreenMesageSystem : MonoBehaviour
{

    [SerializeField] GameObject textPrefab;

    List<OnScreenMessage> onScreenMessagesList;
    List<OnScreenMessage> openList;
    [SerializeField] float horizontalScatter = 0.5f;
    [SerializeField] float verticalScatter = 1f;
    [SerializeField] float timeToLive = 3f;

    private void Awake()
    {
        onScreenMessagesList = new List<OnScreenMessage>();
        openList = new List<OnScreenMessage>();
    }
    private void Update()
    {
        for (int i = onScreenMessagesList.Count -1; i >= 0; i--)
        {
            onScreenMessagesList[i].timeToLive -= Time.deltaTime;
            if (onScreenMessagesList[i].timeToLive < 0)
            {
                onScreenMessagesList[i].go.SetActive(false);
                openList.Add(onScreenMessagesList[i]);
                onScreenMessagesList.RemoveAt(i);
            }
        }
     
    }
    public void PostMessage(Vector3 worldPosition, string message)
    {
        worldPosition.z = -1f;
        worldPosition.x += Random.Range(-horizontalScatter, horizontalScatter);
        worldPosition.y += Random.Range(-verticalScatter, verticalScatter);
        if (openList.Count > 0)
        {
            ReuseObjectFromOpenList(worldPosition, message);
        }
        else
        {
            CreateNewOnScreenMessageObject(worldPosition, message);

        }
    }

    private void ReuseObjectFromOpenList(Vector3 worldPosition, string message)
    {
        OnScreenMessage osm = openList[0];
        osm.go.SetActive(true);
        float timeToLive = 0;
        osm.timeToLive = timeToLive;
        osm.go.GetComponent<TextMeshPro>().text = message;
        osm.go.transform.position = worldPosition;

        openList.RemoveAt(0);
        onScreenMessagesList.Add(osm);
    }

    private void CreateNewOnScreenMessageObject(Vector3 wordPosition, string message)
    {
      

        GameObject textGO = Instantiate(textPrefab, transform);
        textGO.transform.position = wordPosition;
        TextMeshPro tmp = textGO.GetComponent<TextMeshPro>();
        tmp.text = message;
        OnScreenMessage onScreenMessage = new OnScreenMessage(textGO);
        onScreenMessage.timeToLive = timeToLive;
        onScreenMessagesList.Add(onScreenMessage);
    }
}
