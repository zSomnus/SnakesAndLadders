using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    
    public GameObject player1RollButton;
    public GameObject player2RollButton;

    public Player player1;
    public Player player2;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState == GameState.Player1Turn && player1.playerState == PlayerState.Idle)
        {
            player1RollButton.SetActive(true);
        }
        else
        {
            player1RollButton.SetActive(false);
        }

        if (GameManager.Instance.gameMode == GameMode.TwoPlayers)
        {
            if (GameManager.Instance.gameState == GameState.Player2Turn && player2.playerState == PlayerState.Idle)
            { 
                player2RollButton.SetActive(true);
            }
            else
            {
                player2RollButton.SetActive(false);
            }

        }
        
        
        
    }
    
}
