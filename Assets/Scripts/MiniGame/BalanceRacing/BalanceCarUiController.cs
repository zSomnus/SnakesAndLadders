using System.Collections;
using System.Collections.Generic;
using MiniGame.BalanceRacing;
using TMPro;
using UnityEngine;

public class BalanceCarUiController : MonoBehaviour
{
    public BalanceCarDestination destination;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    

    public void SendMessageToMainGame()
    {
        resultPanel.SetActive(true);
        StartCoroutine(ShowText(destination.playerInWinZone));
    }

    IEnumerator ShowText(bool success)
    {
        resultText.text = success ? $"Sometimes, giving up is wise" : $"Sorry, but you are so bad!!!!!!!!! This game has no time limit, but you still refuse to try to win. Shame on you.";

        yield return new WaitForSeconds(3);

        SaveSystem.UpdateMiniGameData(success);
        LevelLoader.Instance.LoadMainGame();
    }
}
