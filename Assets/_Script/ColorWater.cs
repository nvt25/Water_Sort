using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWater : MonoBehaviour
{
    public void CheckDone()
    {
        GameManager.instance.DestroyWaterBegin();
        Debug.Log("Water");
        if(GameManager.instance.zeroActive > 0 && GameManager.instance.numberSameColor >0)
        {
            JarController jar = transform.parent.parent.GetComponent<JarController>();
            List<Transform> waterColor = jar.watersColors;
            int lengthWaterColor = waterColor.Count;
            for (int i = 0; i < lengthWaterColor; i++)
            {
                GameObject color = waterColor[i].transform.Find("Color").gameObject;
                Debug.Log(color.activeSelf);
                if (!color.activeSelf)
                {
                    color.SetActive(true);
                    color.GetComponent<SpriteRenderer>().color = waterColor[i-1].transform.Find("Color").GetComponent<SpriteRenderer>().color;
                    GameManager.instance.zeroActive--;
                    GameManager.instance.numberSameColor--;
                    break;
                }
            }
        }
        else
        {
            DestroyFlow();
        }
    }
    public void DestroyFlow()
    {
        if (GameObject.Find("Flow"))
        {
            Destroy(GameObject.Find("Flow"));
            GameManager.instance.WaterBegin.transform.Find("Movement").GetComponent<JarMovement>().IsUp = false;
            CheckWin();
            GameManager.instance.WaterBegin = null;
            GameManager.instance.WaterEnd = null;
        }
    }
    private void CheckWin()
    {

        List<Transform> waterColors = GameManager.instance.WaterEnd.GetComponent<JarController>().watersColors;
        int numberDone = 0;
        for (int i = 1; i < 4; i++)
        {
            GameObject colorOfWater = waterColors[i].transform.Find("Color").gameObject;
            bool isColorActive = colorOfWater.activeSelf;
            if (isColorActive)
            {
                if(waterColors[i-1].transform.Find("Color").GetComponent<SpriteRenderer>().color.ToString() == waterColors[i].transform.Find("Color").GetComponent<SpriteRenderer>().color.ToString())
                {
                    numberDone++;
                }
            }
        }
        if (numberDone >= 3)
        {
            GameManager.instance.onAudio();
        }
    }
}
