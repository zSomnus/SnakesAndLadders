using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    public List<Cannon> cannons;

    private Cannon cannonSelected;
    public static CannonManager instance;

    public CoffeeMakerGameState gameState;

    private void Awake()
    {
        gameState = CoffeeMakerGameState.Pending;
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Select(Cannon targetCannon)
    {
        foreach (var cannon in cannons)
        {
            if (cannon == targetCannon)
            {
                cannon.isSelected = true;
            }
            else
            {
                cannon.isSelected = false;
            }
        }

        cannonSelected = targetCannon;
    }

    public void ToggleCurrentCannonSelectState()
    {
        if (cannonSelected == null)
        {
            print("No cannon selected");
            return;
        }
        cannonSelected.ToggleOpenState();
    }
}

public enum CoffeeMakerGameState
{
    Pending,
    Victory,
    Failure
};
