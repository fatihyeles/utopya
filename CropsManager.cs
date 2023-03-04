using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Crops
{

}
public class CropsManager : MonoBehaviour
{
    [SerializeField] TileBase plowed; //Sürülmüş toprak
    [SerializeField] TileBase seeded; //Ekilmiş toprak
    [SerializeField] Tilemap targetTilemap; //Haritadaki tüm toprak



    Dictionary<Vector2Int, Crops> crops; //Ekinleri kütüphaneye eklemek

    private void Start()
    {
        crops = new Dictionary<Vector2Int, Crops>(); //Ekinleri kütüphaneye eklemek
    }

    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }
    public void Plow(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))    //Toprağı sürmek
        {
            return;
        }

        CreatePlowedTile(position); }
        public void Seed(Vector3Int position)        //Sürülmüş toprağa ekin eklemek.
    {
        targetTilemap.SetTile(position, seeded);
    }

        private void CreatePlowedTile(Vector3Int position)
    {
        Crops crop = new Crops();
        crops.Add((Vector2Int)position, crop);


        targetTilemap.SetTile((Vector3Int)position, plowed);
    }


}

