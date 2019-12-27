using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConcentrationContorl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float waitTime = 3f;
    //[SerializeField] private Image[] blockList;
    private Color[] colorArray = { Color.cyan, Color.magenta, Color.yellow, Color.black };
    private int[] count = { 0, 0, 0, 0 };
    private int success;
    [SerializeField] List<Color> compareList = new List<Color>();
    [SerializeField] private Block[] blockList;
    void Start()
    {
        RandomColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(compareList.Count == 2)
        {
            if(compareList[0] == compareList[1])
            {
                success++;
                Debug.Log("Got one!");
                
                compareList.Clear();
                if(success == blockList.Length / 2)
                {
                    // Success
                    Debug.Log("Success");
                    SaveSystem.UpdateMiniGameData(true);
                    LevelLoader.Instance.LoadMainGame();
                }
            }
            else
            {
                // Fail
                Debug.Log("Fail");
                SaveSystem.UpdateMiniGameData(false);
                LevelLoader.Instance.LoadMainGame();
            }
        }

        if(waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            if(waitTime <= 0)
            {
                foreach(var member in blockList)
                {
                    member.GetImage().color = Color.white;
                }
            }
        }
    }
    private void RandomColor()
    {
        foreach (var member in blockList)
        {
            int index = Random.Range(0, 4);
            while (true)
            {
                if (count[index] < 2)
                {
                    count[index] += 1;
                    member.GetImage().color = colorArray[index];
                    member.SetColor(colorArray[index]);
                    break;
                }
                else
                {
                    index = Random.Range(0, 4);
                }
            }
        }
    }

    public List<Color> GetCompareList()
    {
        return compareList;
    }
    public float GetWaitTime()
    {
        return waitTime;
    }
}
