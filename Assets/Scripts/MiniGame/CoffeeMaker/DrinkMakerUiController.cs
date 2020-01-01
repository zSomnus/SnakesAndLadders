using System;
using System.Collections;
using System.Collections.Generic;
using MiniGame.CoffeeMaker;
using TMPro;
using UnityEngine;

public class DrinkMakerUiController : MonoBehaviour
{
    // Reference
    public TextMeshProUGUI requirementText;
    public Cup cup;
    public GameObject endingPanel;
    public TextMeshProUGUI endingText;

    public static DrinkMakerUiController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        requirementText.text = $"Total: {cup.capacity}ml\n"+
                               $"{(int) (cup.chocolateRatio * 100)}% chocolate\n" +
                               $"{(int) (cup.milkRatio * 100)}% milk\n" +
                               $"{(int) (cup.redBullRatio * 100)}% red bull energy drink";
    }

    private void Update()
    {
        if (CannonManager.instance.gameState == CoffeeMakerGameState.Victory)
        {
            endingText.text = "Congratulations. You win the game\n" +
                              $"{cup.GetStatistics()}";
        }
        if (CannonManager.instance.gameState == CoffeeMakerGameState.Failure)
        {
            endingText.text = "Sorry, you lose the game\n" +
                              $"{cup.GetStatistics()}";
        }
        
    }

    public void SendMessageToMainGame()
    {
        switch(CannonManager.instance.gameState)
        {
            case CoffeeMakerGameState.Victory:
                print("you win the game");
                SaveSystem.UpdateMiniGameData(true);
                LevelLoader.Instance.LoadMainGame();
                break;
            
            case CoffeeMakerGameState.Failure:
                print("You lose the game");
                SaveSystem.UpdateMiniGameData(false);
                LevelLoader.Instance.LoadMainGame();
                break;
        }
    }
}
