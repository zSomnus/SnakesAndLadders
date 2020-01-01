using UnityEngine;

namespace MiniGame.BalanceRacing
{
    public class BalanceCarDestination : MonoBehaviour
    {
        public bool playerInWinZone;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerInWinZone = true;
            }
        
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerInWinZone = false;
            }
        }
    }
}
