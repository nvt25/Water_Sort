using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int numberJar;
    public Color red = Color.white;
    public Color blue = Color.white;
    public Color yellow = Color.white;
    public Color grenn = Color.white;
    public Color colorBegin;
    public Color colorEnd;
    public List<Color> listColor;
    private GameObject waterBegin;
    private GameObject waterEnd;
    private Vector3 waterBeginPosition;
    private Vector3 waterEndPosition;
    public int zeroActive;
    public int numberSameColor;
    private AudioSource audioSource;
    public AudioClip audioWin;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Debug.Log("Value Error");
        }
        GameManager.instance = this;
        this.audioSource = GetComponent<AudioSource>();
        this.listColor.Add(this.red);
        this.listColor.Add(this.blue);
        this.listColor.Add(this.yellow);
        this.listColor.Add(this.grenn);
    }
    /// <summary>
    /// Lay doi tuong can di chuyen
    /// </summary>
    public GameObject WaterBegin
    {
        set
        {
            this.waterBegin = value;
            if (this.waterBegin == null) return;
            this.waterBeginPosition = this.waterBegin.transform.position;
            this.waterBegin.GetComponent<JarController>().GetColor();
            this.CheckWaterBegin();
        }
        get { return waterBegin; }
    }
    /// <summary>
    /// Lay doi tuong nhan 
    /// </summary>
    public GameObject WaterEnd
    {
        set
        {
            this.waterEnd = value;
            if (this.waterEnd == null) return;
            this.waterEndPosition = this.waterEnd.transform.position;
            CheckWaterEnd();
            CheckValid();
        }
        get { return waterEnd; }
    }
    public Vector3 WaterBeginPosition
    {
        get { return this.waterBeginPosition; }
    }
    public Vector3 WaterEndPisition
    {
        get { return this.waterEndPosition; }
    }
    public List<Color> ListColor
    {
        get { return this.listColor; }
    }
    /// <summary>
    /// Kiem tra mau doi tuong can chuyen 
    /// </summary>
    public void CheckWaterBegin()
    {
        List<Transform> waterJarBegin = this.waterBegin.GetComponent<JarController>().watersColors;
        int lengthJarBegin = waterJarBegin.Count;
        this.numberSameColor = 0;
        for (int i = lengthJarBegin - 1; i >= 0; i--)
        {
            GameObject colorOfWater = waterJarBegin[i].transform.Find("Color").gameObject;
            bool isColorActive = colorOfWater.activeSelf;
            if (isColorActive)
            {
                numberSameColor++;
                this.colorBegin = colorOfWater.GetComponent<SpriteRenderer>().color;
                if (i == 0) break;
                if (this.colorBegin.ToString() != waterJarBegin[i - 1].transform.Find("Color").GetComponent<SpriteRenderer>().color.ToString())
                {
                    break;
                }

            }
        }
        //Debug.Log(numberSameColor);
    }
    /// <summary>
    /// Kiem tra mau doi tuong nhan
    /// </summary>
    public void CheckWaterEnd()
    {
        List<Transform> waterJarEnd = this.waterEnd.GetComponent<JarController>().watersColors;
        int lengthJarBegin = waterJarEnd.Count;
        this.zeroActive = 0;
        for (int i = lengthJarBegin - 1; i >= 0; i--)
        {
            GameObject colorOfWater = waterJarEnd[i].transform.Find("Color").gameObject;
            bool isColorActive = colorOfWater.activeSelf;
            if (isColorActive)
            {
                this.colorEnd = colorOfWater.GetComponent<SpriteRenderer>().color;
                break;
            }
            else
            {
                // so con trong
                this.zeroActive++;
            }

        }
    }

    /// <summary>
    /// So sanh mau 2 doi tuong 
    /// </summary>
    public void CheckValid()
    {
        if (this.colorEnd.ToString() == this.colorBegin.ToString() || this.zeroActive == 4)
        {
            // dung thi bo qua
            return;
        }
        else
        {
            //sai tra ve mac dinh
            this.waterBegin.transform.Find("Movement").GetComponent<JarMovement>().IsUp = false;
            this.waterBegin = null;
            this.waterEnd = null;
        }
    }
    public void onAudio()
    {
        if (this.audioSource && this.audioWin)
        {
            this.audioSource.PlayOneShot(audioWin);
        }
    }
    public void DestroyWaterBegin()
    {
        if (!this.waterBegin || !this.waterEnd) return;
        List<Transform> waterJarBegin = this.waterBegin.GetComponent<JarController>().watersColors;
        int lengthJarBegin = waterJarBegin.Count;
        for (int i = lengthJarBegin - 1; i >= 0; i--)
        {
            GameObject colorOfWater = waterJarBegin[i].transform.Find("Color").gameObject;
            bool isColorActive = colorOfWater.activeSelf;
            if (isColorActive)
            {
                colorOfWater.SetActive(false);
                break;
            }
        }
    }
}
