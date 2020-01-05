using System;
using UnityEngine;

namespace RandomDungeonGenerator.Scripts
{
    public class KnobController : MonoBehaviour
    {
        private int direction;

        private Vector2 destination;
        public Swipe swipeControls;

        private bool isMoving;

        public LayerMask room;

        private void Start()
        {
            destination = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(transform.position, destination) > 0.1)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            if (!isMoving)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || swipeControls.swipeLeft)
                {
                    if (!IsBlocked(3))
                    {
                        destination = destination + new Vector2(-1, 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || swipeControls.swipeRight)
                {
                    if (!IsBlocked(4))
                    {
                        destination = destination + new Vector2(1, 0);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow)|| swipeControls.swipeUp)
                {
                    if (!IsBlocked(1))
                    {
                        destination = destination + new Vector2(0, 1);
                    }
            
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) || swipeControls.swipeDown)
                {
                    if (!IsBlocked(2))
                    {
                        destination = destination + new Vector2(0, -1);
                    }
                }
            }
            transform.position = Vector3.Lerp(transform.position, destination, 0.1f);

            
        
        }
        
        public bool IsBlocked(int directionToDetect)
        {
            RaycastHit2D hitInfo;
            switch (directionToDetect)
            {
                case 1: 
                    hitInfo = Physics2D.Raycast(transform.position, Vector2.up, 0.7f,room);
                    break;
                case 2:
                    hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.7f,room);
                    
                    break;
                case 3:
                    hitInfo = Physics2D.Raycast(transform.position, Vector2.left, 0.7f,room);
                    break;
                case 4:
                    hitInfo = Physics2D.Raycast(transform.position, Vector2.right, 0.7f,room);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }

            return hitInfo.collider != null;

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Destination"))
            {
                SaveSystem.UpdateMiniGameData(true);
                LevelLoader.Instance.LoadMainGame();
            }
        }
    }
}
