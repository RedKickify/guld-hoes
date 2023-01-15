using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform topLeft;
    [SerializeField] private Transform bottomRight;
    [SerializeField] private Transform tilesParent;
    [SerializeField] private GameObject[] tiles;

    [SerializeField] private GameObject fruit;

    void Start()
    {
        for (float x = topLeft.position.x; x < bottomRight.position.x; x++)
        {
            for (float y = topLeft.position.y; y > bottomRight.position.y; y--)
            {
                GameObject tile = Instantiate(tiles[Random.Range(0, tiles.Length)], new Vector2(x, y), transform.rotation, tilesParent);
                tile.GetComponent<Tile>().SelfDestruct(Random.Range(10, 200));
            }
        }

        StartCoroutine(SpawnFruitRoutine());

    }

    IEnumerator SpawnFruitRoutine()
    {

        while (GameManager.instance.gameState != GameState.Ended)
        {
            GameObject _fruit = Instantiate(fruit, new Vector3(Random.Range(topLeft.position.x, bottomRight.position.x), Random.Range(topLeft.position.y, bottomRight.position.y)),Quaternion.identity);
            _fruit.GetComponent<Fruit>().OnSpawn();
            yield return new WaitForSeconds(Random.Range(1,3));
        }

    }
}
