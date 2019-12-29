using UnityEngine;

namespace Assets
{
    public class PhysicsSportManager : MonoBehaviour
    {
        // Singleton
        public static PhysicsSportManager Instance;
        
        // Reference
        public BallSpawner BallSpawner;
        public BasketryChecker basketryChecker;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        

        public void SendMessageToMainGame(bool success)
        {
            SaveSystem.UpdateMiniGameData(success);
            LevelLoader.Instance.LoadMainGame();
            
        }

      
    }
}
