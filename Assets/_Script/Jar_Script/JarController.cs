using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarController : Core
{
    [SerializeField]
    private JarMovement jarMovement;
    public GameObject flowWater;
    public List<Transform> watersColors;
    [SerializeField]
    private List<int> color;
    public int locationColor;
    public Transform PosFlow;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Reset()
    {
        base.Reset();
    }
    protected override void LoadComponent()
    {
        this.LoadScript();
        this.LoadObject();
    }
    private void Start()
    {
        this.LoadWaterColor();
        SetColorWater();
    }
    private void LoadScript()
    {
        this.jarMovement = transform.Find("Movement").GetComponent<JarMovement>();
    }
    private void LoadWaterColor()
    {
        for (int i = 1; i <= 4; i++)
        {
            string name = "Water" + i.ToString();
            this.watersColors.Add(transform.Find(name));
        }
        this.PosFlow = transform.Find("PosFlow");
    }
    private void LoadObject()
    {
        this.flowWater = GameObject.Find("FlowWater");
    }
    /// <summary>
    /// set color ban dau cho game
    /// </summary>
    private void SetColorWater()
    {
        int i = 0;
        foreach (Transform waterColor in this.watersColors)
        {
            int stt = this.color[i];
            SpriteRenderer render = waterColor.transform.Find("Color").GetComponent<SpriteRenderer>();
            render.color = GameManager.instance.ListColor[stt];
            i++;
        }
    }
    /// <summary>
    /// Xu li su kien click
    /// </summary>
    private void OnMouseDown()
    {
        if (!GameManager.instance.WaterBegin && !GameManager.instance.WaterEnd)
        {
            if (CheckNumberActiveColor() <= 0) return;//<0 thi k co mau de chuyen
            this.jarMovement.IsUp = true;
            transform.Find("Model").GetComponent<SpriteRenderer>().sortingOrder = 10;
            for (int i = 0; i < 4; i++)
            {
                GameObject color = watersColors[i].transform.Find("Color").gameObject;
                if (color)
                {
                    color.GetComponent<SpriteRenderer>().sortingOrder = 11;
                }
            }
            GameManager.instance.WaterBegin = gameObject;
        }
        else if (GameManager.instance.WaterBegin != gameObject && !GameManager.instance.WaterEnd)
        {
            if (CheckNumberActiveColor() == 4)//>4 da day k the nhan
            {
                GameManager.instance.WaterBegin.transform.Find("Movement").GetComponent<JarMovement>().IsUp = false;
                GameManager.instance.WaterBegin = null;
                return;
            }
            GameManager.instance.WaterEnd = gameObject;
        }
    }
    /// <summary>
    /// Kiem tra xem co bao nhieu phan tu rong
    /// </summary>
    /// <returns>so phan tu rong</returns>
    private int CheckNumberActiveColor()
    {
        int number = 0;
        int lengthJarBegin = this.watersColors.Count;
        for (int i = lengthJarBegin - 1; i >= 0; i--)
        {
            GameObject colorOfWater = watersColors[i].transform.Find("Color").gameObject;
            bool isColorActive = colorOfWater.activeSelf;
            if (!isColorActive)
            {
                continue;
            }
            else
            {
                number++;
            }
        }
        return number;
    }
    //lay mau tren cung
    public void GetColor()
    {
        int lengthJarBegin = this.watersColors.Count;
        for (int i = lengthJarBegin - 1; i >= 0; i--)
        {
            GameObject colorOfWater = watersColors[i].transform.Find("Color").gameObject;
            bool isColorActive = colorOfWater.activeSelf;
            if (isColorActive)
            {
                this.locationColor = i;
                break;
            }

        }
    }
}
