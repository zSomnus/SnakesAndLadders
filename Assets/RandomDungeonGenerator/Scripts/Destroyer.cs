using UnityEngine;

namespace RandomDungeonGenerator.Scripts
{
    public class Destroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.CompareTag("Player"))
                Destroy(other.gameObject);
        }
    }
}
