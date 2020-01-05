using UnityEngine;

namespace RandomDungeonGenerator.Scripts
{
    public class RoomSpawner : MonoBehaviour
    {
        [Tooltip("Indicate the door with a specific opening that the spawner needs")]
        public int openingDirection;
        // 1 --> need bottom door
        // 2 --> need top door
        // 3 --> need left door
        // 4 --> need right door

        [Tooltip("The time before the spawner will be destroyed to save performance")]
        public float waitTime = 4f;

        private RoomTemplates templates;
        private int rand;
        private bool spawned = false;

        private void Start() {
            Destroy(gameObject, waitTime);
            templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
            Invoke("Spawn", 0.1f);
        }


        void Spawn(){
            if(!spawned){
                if(openingDirection == 1){
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, transform.rotation);
                }
                else if(openingDirection == 2){
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, transform.rotation);
                }
                else if(openingDirection == 3){
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, transform.rotation);
                }
                else if(openingDirection == 4){
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, transform.rotation);
                }
                spawned = true;
            }

        
        }

        private void OnTriggerEnter2D(Collider2D other) {
            // if(other.CompareTag("SpawnPoint") && other.GetComponent<RoomSpawner>().spawned == true){
            //     print("destroy");
            //     Destroy(transform.gameObject);
            // }

            if (other.CompareTag("SpawnPoint"))
            {
                if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
                {
                    templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                spawned = true;
            }
        }
    }
}
