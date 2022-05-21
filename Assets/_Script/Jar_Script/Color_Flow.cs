using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Flow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jar"))
        {//set layout cho model
            collision.gameObject.transform.Find("Model").GetComponent<SpriteRenderer>().sortingOrder = 1;
            List<Transform> waterColors = collision.GetComponent<JarController>().watersColors;
            foreach( Transform watercolor in waterColors)
            {
                GameObject color = watercolor.transform.Find("Color").gameObject;
                if (!color.activeSelf)
                {
                    watercolor.transform.Find("Color").gameObject.SetActive(true);
                    watercolor.transform.Find("Color").GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
                    break;
                }
            }
        }
    }
    public void DestroyWaterBegin()
    {
        GameObject waterBegin = GameManager.instance.WaterBegin;
        if (waterBegin)
        {
            int locationColor = waterBegin.GetComponent<JarController>().locationColor;
            waterBegin.GetComponent<JarController>().watersColors[locationColor].transform.Find("Color").gameObject.SetActive(false);
        }

    }
}
