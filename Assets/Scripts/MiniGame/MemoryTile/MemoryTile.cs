using System;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGame.MemoryTile
{
    public class MemoryTile : MonoBehaviour
    {
        public Color surfaceColor;
        public Color hiddenColor;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
            image.color = surfaceColor;
        }

        public void ToggleColor(bool hidden)
        {
            image.color = hidden ? hiddenColor : surfaceColor;
        }

        public void UnCover()
        {
        
            MiniGameMemoryTileUiController.Instance.UncoverTile(this.gameObject);
        }
    }
}
