using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCarController : MonoBehaviour
{
    public GameObject target;
    private Rigidbody rb;
    public float speed = 30, rotatingSpeed = 8;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
        {
            target = GameObject.Find("TestCar");
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = transform.position - target.transform.position;
        targetPosition.Normalize();
        float value = Vector3.Cross(targetPosition, transform.forward).y;
        Debug.Log(value);
        rb.angularVelocity = rotatingSpeed * value * new Vector3(0,1,0) ;
        rb.velocity = transform.forward * speed;
    }
}
