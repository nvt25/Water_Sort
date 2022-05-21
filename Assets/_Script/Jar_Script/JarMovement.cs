using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarMovement : MonoBehaviour
{
    private int numberFlow = 0;
    public float speed;
    [SerializeField]
    private bool isUp;
    private Transform jar;
    private Vector3 beginPosition;
    private Vector3 endPosition;
    private Vector3 targetPosition;
    private float distance;
    private float rotationMax = 70;
    private void Awake()
    {
        this.LoadParent();
        this.LoadPosition();
    }
    private void Update()
    {
        this.GetPositionWaterEnd();
        this.Movement();
    }
    private void LoadParent()
    {
        this.jar = transform.parent;
    }
    private void LoadPosition()
    {
        this.beginPosition = this.jar.position;
        this.endPosition = this.beginPosition + new Vector3(0, 0.3f, 0);
    }
    private void GetPositionWaterEnd()
    {
        if (GameManager.instance.WaterEnd)
        {
            this.targetPosition = GameManager.instance.WaterEndPisition + new Vector3(0, 1.2f, 0);
        }
    }
    private void Movement()
    {
        if(isUp && !GameManager.instance.WaterEnd)
        {
            this.jar.position = Vector3.MoveTowards(this.jar.position, this.endPosition, this.speed * Time.deltaTime);
        }
        else if(isUp && GameManager.instance.WaterEnd)
        {
            bool isDone = false;
            // do nuoc
            if(this.jar.eulerAngles.z < this.rotationMax)
            {
                this.jar.Rotate(new Vector3(0, 0, GetVelocity()*Time.deltaTime));
            }
            else if(this.jar.eulerAngles.z > this.rotationMax)
            {
                this.jar.transform.localRotation = Quaternion.Euler(0, 0, this.rotationMax);
            }
            else
            {
                isDone = true;
            }
            this.jar.position = Vector3.MoveTowards(this.jar.position, this.targetPosition, this.speed * Time.deltaTime);
            if (this.jar.GetComponent<JarController>().flowWater && this.numberFlow <1 && this.jar.position == this.targetPosition && isDone)
            {
                this.numberFlow++;
                GameObject flow = Instantiate(this.jar.GetComponent<JarController>().flowWater);
                flow.name = "Flow";
                flow.transform.position = this.jar.GetComponent<JarController>().PosFlow.position;
                SpriteRenderer ren = flow.transform.Find("Color").GetComponent<SpriteRenderer>();
                int loacationColor = this.jar.GetComponent<JarController>().locationColor;
                ren.color = this.jar.GetComponent<JarController>().watersColors[loacationColor].transform.Find("Color").GetComponent<SpriteRenderer>().color;
            }

        }
        else
        {
            // xoay lai
            if (this.jar.eulerAngles.z > 0 && this.jar.eulerAngles.z <= this.rotationMax)
            {
                this.jar.Rotate(new Vector3(0, 0, -GetVelocity() * Time.deltaTime));
            }
            else
            {
                this.numberFlow = 0;
                this.jar.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            this.jar.position = Vector3.MoveTowards(this.jar.position, this.beginPosition, this.speed * Time.deltaTime);
        }
    }
    private float GetVelocity()
    {
        // lay khoang cach begin to end
        this.distance = Vector3.Distance(this.targetPosition, GameManager.instance.WaterBeginPosition);
        // lay thoi gian chay 
        float timeMove = Mathf.Abs(distance) / this.speed;
        // lay van toc 
        float velocity = this.rotationMax / timeMove;
        return velocity;
    }
    public bool IsUp
    {
        set { this.isUp = value; }
    }
}
