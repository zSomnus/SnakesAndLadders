using System.Collections.Generic;
using UnityEngine;

namespace RandomDungeonGenerator.Scripts
{
    public class RoomTemplates : MonoBehaviour
    {
        public GameObject[] bottomRooms;
        public GameObject[] topRooms;
        public GameObject[] leftRooms;
        public GameObject[] rightRooms;
        public GameObject closedRoom;

        public List<GameObject> rooms;

        public float waitTime;

        private bool spawnedPlayer;
        public GameObject playerPrefab;
        public Camera playerCamera;

        private bool spawnedBoss;
        public GameObject boss;
        public MazeRunnerManager runnerManager;

        private void Update() {
            if(waitTime <= 0 && spawnedBoss == false){
                for (int i = 0; i < rooms.Count; i++)   
                {
                    if(i == rooms.Count - 1){
                        Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                        spawnedBoss = true;
                        
                        var player = Instantiate(playerPrefab, rooms[0].transform.position, Quaternion.identity);
                        spawnedPlayer = true;
                        runnerManager.gameStart = true;
                        
                        playerCamera.gameObject.SetActive(true);
                        playerCamera.GetComponent<DungeonCameraFollow>().enabled = true;
                        playerCamera.GetComponent<DungeonCameraFollow>().target = player.transform;
                    }
                }
            }
            else{
                waitTime -= Time.deltaTime;
            }
        }
    }
}
