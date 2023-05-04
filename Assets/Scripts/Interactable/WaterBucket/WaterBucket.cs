using System;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    [SerializeField] private int maxTiles; 
    [SerializeField] private LayerMask walkableTileMask;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private GameObject waterTilePrefab;

    [SerializeField] private int interactableLayer;
    [SerializeField] private int usedLayer;
    [SerializeField] private AssignmentOfKeyInteraction buttonVisual;

    [SerializeField] private Queue<GameObject> tilesToWater;
    [SerializeField] private List<GameObject> wateredTiles; // ��� ������

    private bool pushed = false;


    private PlayerMovement playerMovement;
    private Vector3 waterDirection;
    private LevelController levelController;

    public void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        levelController = FindAnyObjectByType<LevelController>();
        levelController._cloneEvent += ResetBucket;
    }

    public void PushBucket(Vector3 actorPosition, bool isClone = false)
    {
        if (!pushed)
        {
            tilesToWater = new Queue<GameObject>();
            wateredTiles = new List<GameObject>();

            // ������� �����
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -60)); // ����������

            // ����� ����������� ���� ����
            Vector3 actor = new Vector3(actorPosition.x, 0, actorPosition.z);
            Vector3 bucket = new Vector3(transform.position.x, 0, transform.position.z);
            var direction = bucket - actor;
            // ������ ��������� ����
            Activate(direction.normalized, isClone);

            // ��������� ��������������
            gameObject.layer = usedLayer;
            buttonVisual?.TurningOffText();

            pushed= true;
        }
    }


    private void Activate(Vector3 direction, bool isClone)
    {
        waterDirection = direction;
        
        // ����� ������ ������� ����� ��������a
        FindTilesToWater();

        // ������ ������ ������
        // ���� ����� - ������ �� ������
        if (isClone)
        {
            Spill();
        }
        

        // ���������� �� �������� ������
        if (playerMovement != null)
        {
            playerMovement.PlayerMoved += PlayerMovement_PlayerMoved;
        }
    }

    private void PlayerMovement_PlayerMoved(object sender, System.EventArgs e)
    {
        // ��������� ����
        Spill();
    }

    private void Spill()
    {
        GameObject tile;
        if (tilesToWater.TryDequeue(out tile))
        {
            // �������� ������ � �����
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
        float rayLength = maxTiles * Constants.TILE_SIZE;
        // ��������� ���� �� �� ���� �����
        bool findWall = Physics.Raycast(transform.position, waterDirection, out wallHit, rayLength, wallMask, QueryTriggerInteraction.Ignore);
        if (findWall)
        {
            // ���� ����� ������ �� �����
            rayLength = wallHit.distance;
        }
        // ���� ����� ������� ����� ������ �����
        RaycastHit[] hits = Physics.RaycastAll(transform.position, waterDirection, rayLength, walkableTileMask, QueryTriggerInteraction.Ignore);
        if (hits.Length > 0)
        {
            // ������������� �� ���������� �� �����
            Array.Sort(hits, (a,b) => a.distance.CompareTo(b.distance));
        }
        foreach(RaycastHit hit in hits)
        {
            tilesToWater.Enqueue(hit.collider.gameObject);
        }
       
    }

    private void ResetBucket()
    {
        pushed = false;
        // ������� �� ����� �����
        gameObject.transform.rotation= Quaternion.identity;
        // ������� ����
        foreach(GameObject waterTile in wateredTiles)
        {
            Destroy(waterTile);
        }
        tilesToWater?.Clear();
        wateredTiles?.Clear();
        waterDirection = Vector3.zero;

        // �������� interactable
        gameObject.layer = interactableLayer;

        if (playerMovement != null)
        {
            playerMovement.PlayerMoved -= PlayerMovement_PlayerMoved;
        }
    }

}
