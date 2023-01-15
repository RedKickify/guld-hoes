using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTypeController : MonoBehaviour
{
    public PlayerType PlayerType { get; private set; }

    [SerializeField] private GameObject _tankGameObject;

    public void EnableTank()
    {
        PlayerType = PlayerType.Tank;
        _tankGameObject.SetActive(true);
        _tankGameObject.GetComponent<AudioSource>().PlayDelayed(1);
    }
}

public enum PlayerType
{
    Frog, Tank
}
