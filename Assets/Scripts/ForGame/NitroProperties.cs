using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroProperties : MonoBehaviour
{
    [SerializeField] private int speedRotate = 50;
    Transform baloon;
    Vector3 dir = Vector3.down;
    Vector3 startPos;
    private MyCanvas canvasRefNitro;
    // Start is called before the first frame update
    void Start()
    {
        canvasRefNitro = GameObject.Find("Canvas").GetComponent<MyCanvas>();

        baloon = gameObject.GetComponent<Transform>();
        ///
        startPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        baloon.Translate(dir * Time.deltaTime *0.2f);
        if(baloon.position.y <= startPos.y - 0.2)
        {
            dir = Vector3.up;
        }
        else if (baloon.position.y >= startPos.y + 0.2)
        {
            dir = Vector3.down;
        }
        transform.Rotate(0, speedRotate*Time.deltaTime, 0, Space.World);
    }
   
}
