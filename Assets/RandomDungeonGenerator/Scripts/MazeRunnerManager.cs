using TMPro;
using UnityEngine;

namespace RandomDungeonGenerator.Scripts
{
    public class MazeRunnerManager : MonoBehaviour
    {
        public TextMeshProUGUI t_timer;
        public static MiniGameState gameState;

        [SerializeField] private float timeToLose = 10f;

        public bool gameStart;

        // Update is called once per frame
        void Update()
        {
            if (timeToLose > 0 && gameStart)
            {
                timeToLose -= Time.deltaTime;
                t_timer.text = $"Time left: {(int) timeToLose}";

                if (timeToLose <= 0)
                {
                    if (gameState == MiniGameState.Pending)
                    {
                        gameState = MiniGameState.Failure;
                        SaveSystem.UpdateMiniGameData(false);
                        LevelLoader.Instance.LoadMainGame();
                    }
                }
            }
        }
    }
}
