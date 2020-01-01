using UnityEngine;

namespace MiniGame.CoffeeMaker
{
    public class Cup : MonoBehaviour
    {
        [Tooltip("How many cube the cup needs")]
        public int capacity = 1000;
        public int capacityFalseTolerance = 100;

        public float chocolateRatio = 0.4f;
        public float milkRatio = 0.4f;
        public float redBullRatio = 0.2f;
        public float ratioFalseTolerance = 0.1f;
    
        private int chocolateCubeReceive;
        private int milkCubeReceive;
        private int redBullCubeReceive;
    

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Cube"))
            {
                switch (other.gameObject.GetComponent<Cube>().drinkType)
                {
                    case DrinkType.Chocelate:
                        chocolateCubeReceive++;
                        break;
                    case DrinkType.Milk:
                        milkCubeReceive++;
                        break;
                    case DrinkType.RedBull:
                        redBullCubeReceive++;
                        break;
                }
            
            }
        }

        public string GetStatistics()
        {
            return $"{nameof(chocolateRatio)}: {(float)chocolateCubeReceive/capacity*100}%\n" +
                   $"{nameof(milkRatio)}: {(float)milkCubeReceive/capacity*100}%\n" +
                   $"{nameof(redBullRatio)}: {(float)redBullCubeReceive/capacity*100}%";

        }

        public void Judge()
        {
        
        
            if (chocolateCubeReceive + milkCubeReceive + redBullCubeReceive < capacity - capacityFalseTolerance || chocolateCubeReceive + milkCubeReceive + redBullCubeReceive > capacity + capacityFalseTolerance)
            {
                CannonManager.instance.gameState = CoffeeMakerGameState.Failure;
            }

            if ((float) chocolateCubeReceive / capacity > chocolateRatio + ratioFalseTolerance ||
                (float) chocolateCubeReceive / capacity < chocolateRatio - ratioFalseTolerance)
            {
                CannonManager.instance.gameState = CoffeeMakerGameState.Failure;

            }

            if ((float) milkCubeReceive / capacity > milkRatio + ratioFalseTolerance ||
                (float) milkCubeReceive / capacity < milkRatio - ratioFalseTolerance)
            {
                CannonManager.instance.gameState = CoffeeMakerGameState.Failure;

            }

            if ((float) redBullCubeReceive / capacity > redBullRatio + ratioFalseTolerance ||
                (float) redBullCubeReceive / capacity < redBullRatio - ratioFalseTolerance)
            {
                CannonManager.instance.gameState = CoffeeMakerGameState.Failure;

            }


            if (CannonManager.instance.gameState == CoffeeMakerGameState.Pending)
            {
                CannonManager.instance.gameState = CoffeeMakerGameState.Victory;
            }

            Time.timeScale = 0f;
            DrinkMakerUiController.instance.endingPanel.SetActive(true);
        }
    }
}
