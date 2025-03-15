using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public static ScrollingBackground instance;
    public GameObject tilePrefab;
    public int tileSize = 64;
    public int poolSize = 8;

    public Sprite[] tileVariations;

    private Transform cam;
    private Vector2 lastCamPos;
    private Queue<GameObject> tiles = new Queue<GameObject>();
    private Dictionary<Vector2,GameObject> activeTiles = new Dictionary<Vector2,GameObject>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;

        for(int i = 0; i < poolSize; i++)
        {
            GameObject tile = Instantiate(tilePrefab);
            tile.SetActive(false);
            tiles.Enqueue(tile);
        }
        GenerateTiles();
    }

    private void Update()
    {
        Vector2 camPos = new Vector2(Mathf.Floor(cam.position.x / tileSize), Mathf.Floor(cam.position.y / tileSize));
        if (lastCamPos != camPos)
        {
            GenerateTiles();
            lastCamPos = camPos;

        }
    }

    void GenerateTiles()
    {
        int viewRangeX = Mathf.CeilToInt(Camera.main.orthographicSize * Screen.width / Screen.height / tileSize) + 1;
        int viewRangeY = Mathf.CeilToInt(Camera.main.orthographicSize / tileSize) + 1;
        int viewRange = Mathf.Max(viewRangeX, viewRangeY); // Take the larger value
        HashSet<Vector2> neededTiles = new HashSet<Vector2>();

        for (int x = -viewRange; x <= viewRange; x++)
        {
            for (int y = -viewRange; y <= viewRange; y++)
            {
                Vector2 tilePos = new Vector2(
                    Mathf.Floor(cam.position.x / tileSize) + x,
                    Mathf.Floor(cam.position.y / tileSize) + y
                );

                neededTiles.Add(tilePos);

                if (!activeTiles.ContainsKey(tilePos))
                {
                    GameObject tile = GetTileFromPool();
                    tile.transform.position = new Vector3(tilePos.x * tileSize, tilePos.y * tileSize, 0);
                    AssignRandomSprite(tile);
                    tile.SetActive(true);
                    activeTiles[tilePos] = tile;
                }
            }
        }

        List<Vector2> toRemove = new List<Vector2>();
        foreach (var tile in activeTiles)
        {
            if (!neededTiles.Contains(tile.Key))
            {
                tile.Value.SetActive(false);
                tiles.Enqueue(tile.Value);
                toRemove.Add(tile.Key);
            }
        }

        // Remove deactivated tiles from activeTiles dictionary
        foreach (var pos in toRemove)
        {
            activeTiles.Remove(pos);
        }
    }

    GameObject GetTileFromPool()
    {
        if (tiles.Count > 0)
        {
            return tiles.Dequeue();
        }
        else
        {
            // If pool runs out, create a new tile (this should rarely happen)
            GameObject newTile = Instantiate(tilePrefab);
            return newTile;
        }
    }

    void AssignRandomSprite(GameObject tile)
    {
        if (instance.tileVariations.Length > 0)
        {
            SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();

            

            if (renderer != null && instance.tileVariations.Length > 0)
            {
                renderer.sprite = instance.tileVariations[Random.Range(0, instance.tileVariations.Length)];
            }
        }
    }


}

