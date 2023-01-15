using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    [SerializeField] private Sprite[] _sprites;

    public void OnSpawn()
    {
        this.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0,_sprites.Length)];
    }
}
