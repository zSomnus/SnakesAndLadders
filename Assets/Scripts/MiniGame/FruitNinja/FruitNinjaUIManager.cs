using System.Collections;
using TMPro;
using UnityEngine;

namespace MiniGame.FruitNinja
{
    public class FruitNinjaUIManager : MonoBehaviour
    {
        public TextMeshProUGUI wordToSlice;
        public TextMeshProUGUI tip;
        public TextMeshProUGUI currentAlphabet;
    
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(DisableTipText());
        }

        // Update is called once per frame
        void Update()
        {
            if (FruitNinjaManager.instance.gameState == FruitNinjaManager.MiniGameState.Pending)
            {
                wordToSlice.text = FruitNinjaManager.instance.wordsToSlice[FruitNinjaManager.instance.currentIndex];
                currentAlphabet.text = FruitNinjaManager.instance.wordCollected;
            }
        
        }

        public IEnumerator DisableTipText()
        {
            yield return new WaitForSeconds(4);
            tip.gameObject.SetActive(false);
        }
    }
}
