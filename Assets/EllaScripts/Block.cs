using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private Image image;
    ConcentrationContorl controller;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<Image>().color;
        image = GetComponent<Image>();
        color = image.color;
        controller = GameObject.Find("GameControl").GetComponent<ConcentrationContorl>();
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (controller.GetWaitTime() <= 0)
        {
            image.color = color;
            controller.GetCompareList().Add(color);
            button.enabled = false;
        }
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }

    public Image GetImage()
    {
        return image;
    }
}
