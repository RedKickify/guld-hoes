using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] private Transform topLeft;
    [SerializeField] private Transform bottomRight;
    [SerializeField] private Transform tilesParent;
    [SerializeField] private GameObject[] tiles;

    void Start()
    {
        for (float x = topLeft.position.x; x < bottomRight.position.x; x++)
        {
            for (float y = topLeft.position.y; y > bottomRight.position.y; y--)
            {
                Instantiate(tiles[Random.Range(0, tiles.Length)], new Vector2(x, y), transform.rotation, tilesParent);
            }
        }
    }

    void Update()
    {
        
    }
}
