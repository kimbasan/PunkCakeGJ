using System;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    [SerializeField] private int maxTiles; 
    [SerializeField] private float tileSize;
    [SerializeField] private LayerMask walkableTileMask;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private GameObject waterTilePrefab;

    [SerializeField] private Queue<GameObject> tilesToWater;
    [SerializeField] private List<GameObject> wateredTiles; // для отката
    
    private PlayerMovement playerMovement;
    private Vector3 waterDirection;

    public void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    public void PushBucket(Vector3 actorPosition)
    {
        tilesToWater = new Queue<GameObject>();
        wateredTiles = new List<GameObject>();

        // уронить ведро
        
        // найти направление куда лить
        Vector3 actor = new Vector3(actorPosition.x, 0, actorPosition.z);
        Vector3 bucket = new Vector3(transform.position.x, 0, transform.position.z);
        var direction = bucket - actor;
        // начать разливать воду
        Activate(direction.normalized);

    }

    private void Activate(Vector3 direction)
    {
        waterDirection = direction;
        if (playerMovement != null)
        {
            playerMovement.PlayerMoved += PlayerMovement_PlayerMoved;
        }

        // найти клетки которые будем заливатьa
        FindTilesToWater();

        // залить первую клетку
        Spill();
    }

    private void PlayerMovement_PlayerMoved(object sender, System.EventArgs e)
    {
        // разливать воду
        Spill();
    }

    private void Spill()
    {
        GameObject tile;
        if (tilesToWater.TryDequeue(out tile))
        {
            // добавить клетку с водой
            if (waterTilePrefab != null)
            {
                var waterTile = Instantiate(waterTilePrefab, tile.transform.position, tile.transform.rotation);
                wateredTiles.Add(waterTile);
                waterTile.GetComponent<Water>().SetWaterList(ref wateredTiles);
            }
        }

    }

    private void FindTilesToWater()
    {
        RaycastHit wallHit;
        float rayLength = maxTiles * tileSize;
        // проверяем есть ли на пути стена
        bool findWall = Physics.Raycast(transform.position, waterDirection, out wallHit, rayLength, wallMask, QueryTriggerInteraction.Ignore);
        if (findWall)
        {
            // ищем тайлы только до стены
            rayLength = wallHit.distance;
        }
        // ищем тайлы которые можно залить водой
        RaycastHit[] hits = Physics.RaycastAll(transform.position, waterDirection, rayLength, walkableTileMask, QueryTriggerInteraction.Ignore);
        if (hits.Length > 0)
        {
            // отсортировать по расстоянию от ведра
            Array.Sort(hits, (a,b) => a.distance.CompareTo(b.distance));
        }
        foreach(RaycastHit hit in hits)
        {
            tilesToWater.Enqueue(hit.collider.gameObject);
        }
       
    }

}
