using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum GameState { Intro , Main , Ended }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameState gameState= GameState.Intro;
    [SerializeField] private TMP_Text countDownText;
    [SerializeField] private TMP_Text fruitCounterText;
    [SerializeField] private Button buyTankButton;
    [SerializeField] private PlayerTypeController playerTypeController;
    private int fruitsEaten;

    private void Start()
    {
        StartCoroutine(CountDown());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            FruitEaten();
            FruitEaten();
            FruitEaten();
            FruitEaten();
            FruitEaten();
        }
    }

    IEnumerator CountDown()
    {
        for (int i = 3; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        countDownText.text = "";
        gameState = GameState.Main;
    }

    public void PlayerDied()
    {
        gameState = GameState.Ended;
        countDownText.fontSize = 64;
        countDownText.text = "RIP in peace";
    }

    public void FruitEaten()
    {
        fruitsEaten++;
        fruitCounterText.text = fruitsEaten.ToString();

        if (fruitsEaten >= 5 && playerTypeController.PlayerType != PlayerType.Tank)
        {
            buyTankButton.interactable = true;
        }
    }

    public void SubtractFruitsEaten(int value)
    {
        fruitsEaten-= value;
        fruitCounterText.text = fruitsEaten.ToString();


        if (fruitsEaten < 5)
        {
            buyTankButton.interactable = false;
        }
    }
}
