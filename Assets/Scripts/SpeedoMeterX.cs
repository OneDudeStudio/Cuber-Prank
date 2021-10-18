using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedoMeterX : MonoBehaviour
{
    public GameObject needle;
    private const float maxSpeedAngle = -40;
    private const float minSpeedAngle = 220;
    private float speedMax= 180f;
    private MyCanvas CanvasSpeed;
    // Start is called before the first frame update
    void Start()
    {
        CanvasSpeed = GameObject.Find("Canvas").GetComponent<MyCanvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanvasSpeed.speed > speedMax) CanvasSpeed.speed = speedMax;
        needle.transform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
    }
    private float GetSpeedRotation()
    {
        float totalAngleSize = minSpeedAngle - maxSpeedAngle;
        float speedNormalized = CanvasSpeed.speed / speedMax;
        return minSpeedAngle - speedNormalized * totalAngleSize;

    }
}
