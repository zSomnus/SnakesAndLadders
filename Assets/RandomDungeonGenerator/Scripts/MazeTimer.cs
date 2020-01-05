using TMPro;
using UnityEngine;

namespace RandomDungeonGenerator.Scripts
{
    public class MazeTimer : MonoBehaviour
    {
        public TextMeshProUGUI t_timer;

        [SerializeField] private float timeToLose = 6f;

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
                    SaveSystem.UpdateMiniGameData(false);
                    LevelLoader.Instance.LoadMainGame();
                }
            }
        }
    }
}
