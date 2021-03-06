﻿using UnityEngine;

namespace MainGame
{
    public class MainGameRollButtonUiController : MonoBehaviour
    {
        public GameObject[] playersRollButtons;
        // public GameObject player1RollButton;
        // public GameObject player2RollButton;

        public Player[] players;
        // public Player player1;
        // public Player player2;
    
    
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.gameTurnState == GameTurnState.Player1Turn && GameManager.Instance.players[0].playerState == PlayerState.Idle)
            {
                playersRollButtons[0].SetActive(true);
            }
            else
            {
                playersRollButtons[0].SetActive(false);
            }

            // Only update the player 2 button if it is in two player mode
            if (GameManager.Instance.gameMode == GameMode.TwoPlayers || GameManager.Instance.gameMode == GameMode.ThreePlayers || GameManager.Instance.gameMode == GameMode.FourPlayers)
            {
                if (GameManager.Instance.gameTurnState == GameTurnState.Player2Turn && GameManager.Instance.players[1].playerState == PlayerState.Idle)
                { 
                    playersRollButtons[1].SetActive(true);
                }
                else
                {
                    playersRollButtons[1].SetActive(false);
                }

            }
        
            if (GameManager.Instance.gameMode == GameMode.ThreePlayers || GameManager.Instance.gameMode == GameMode.FourPlayers)
            {
                if (GameManager.Instance.gameTurnState == GameTurnState.Player3Turn && GameManager.Instance.players[2].playerState == PlayerState.Idle)
                { 
                    playersRollButtons[2].SetActive(true);
                }
                else
                {
                    playersRollButtons[2].SetActive(false);
                }

            }
        
            if (GameManager.Instance.gameMode == GameMode.FourPlayers)
            {
                if (GameManager.Instance.gameTurnState == GameTurnState.Player4Turn && GameManager.Instance.players[3].playerState == PlayerState.Idle)
                { 
                    playersRollButtons[3].SetActive(true);
                }
                else
                {
                    playersRollButtons[3].SetActive(false);
                }

            }
        
        
        
        }
    
    }
}
