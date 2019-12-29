using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryTile : MonoBehaviour
{
    public Color surfaceColor;
    public Color hiddenColor;
    public bool isTarget;

    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = surfaceColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleColor(bool hidden)
    {
        if (hidden)
        {
            image.color = hiddenColor;
        }
        else
        {
            image.color = surfaceColor;
        }
    }

    public void UnCover()
    {
        
        MiniGameMemoryTileUiController.Instance.UncoverTile(this.gameObject);
    }
}
