using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniGame.MemoryTile
{
    public class MiniGameMemoryTileUiController : MonoBehaviour
    {
        private List<GameObject> _tileGroup;    // 6 x 6
        private List<GameObject> _targetTiles;
        public float timeToRemember = 5f;
        public int NumOfTilesToRemember = 8;
        public Color surfaceColor = Color.white;
        public Color hiddenColor = Color.gray;
        public Color hiddenColorForTarget = Color.black;

        public static MiniGameMemoryTileUiController Instance;

        // Enum
        private MemoryTileGameState gameState;

        public GameObject tileParent;

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

        // Start is called before the first frame update
        void Start()
        {
            gameState = MemoryTileGameState.Hidden;
            _tileGroup = new List<GameObject>();
            _targetTiles = new List<GameObject>();
            foreach (Transform child in tileParent.transform)
            {
                _tileGroup.Add(child.gameObject);   
            }

            for (int i = 0; i < NumOfTilesToRemember; i++)
            {
            
                int randomNum = Random.Range(0, _tileGroup.Count);
                while(_targetTiles.Contains(_tileGroup[randomNum]))
                {
                    randomNum = Random.Range(0, _tileGroup.Count);
                
                }
                _targetTiles.Add(_tileGroup[randomNum]);
            }

            foreach (GameObject tile in _tileGroup)
            {
                tile.GetComponent<MemoryTile>().hiddenColor = hiddenColor;
                tile.GetComponent<MemoryTile>().surfaceColor = surfaceColor;
            }
        
            foreach (GameObject targetTile in _targetTiles)
            {
                targetTile.GetComponent<MemoryTile>().hiddenColor = hiddenColorForTarget;
            }

            foreach (GameObject tile in _tileGroup)
            {
                tile.GetComponent<MemoryTile>().ToggleColor(false);
            }
            StartCoroutine(ShowHint(1));
        }

        public void UncoverTile(GameObject tile)
        {
            if (gameState != MemoryTileGameState.Guess)
                return;
            tile.GetComponent<MemoryTile>().ToggleColor(true);
            if (_targetTiles.Contains(tile))
            {
                _targetTiles.Remove(tile);
                if (_targetTiles.Count == 0)
                {
                    gameState = MemoryTileGameState.Success;
                    SaveSystem.UpdateMiniGameData(true);
                    LevelLoader.Instance.LoadMainGame();
                }
            }
            else
            {
                gameState = MemoryTileGameState.Failure;
                ToggleAllTileColor(true);
                SaveSystem.UpdateMiniGameData(false);
                LevelLoader.Instance.LoadMainGame();
            }
        }
    

        public IEnumerator ShowHint(float hintDuration)
        {
            yield return new WaitForSeconds(hintDuration);
            gameState = MemoryTileGameState.ShowHint;
            ToggleAllTileColor(true);
            StartCoroutine(HideTile(5));
        }
    
        public IEnumerator HideTile(float secToWait)
        {
            yield return new WaitForSeconds(secToWait);
            gameState = MemoryTileGameState.Guess;
            ToggleAllTileColor(false);
        }
    
        public void ToggleAllTileColor(bool hidden)
        {
            foreach (GameObject tile in _tileGroup)
            {
                tile.GetComponent<MemoryTile>().ToggleColor(hidden);
            }
        }
    }

    public enum MemoryTileGameState{Hidden, ShowHint, Guess, Success, Failure}
}