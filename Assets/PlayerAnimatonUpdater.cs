using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatonUpdater : MonoBehaviour
{
    public Player player;
    public Animator playerAnimator;
    
    // Start is called before the first frame update

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetInteger("PlayerMovementDirection", (int)player.playerMovingDirection);
        if (player.playerState == PlayerState.Idle)
        {
            playerAnimator.SetBool("isPlayerMoving",false);
        }
        else
        {
            playerAnimator.SetBool("isPlayerMoving",true);
        }
    }
}
