using UnityEngine;
using System.Collections;
using System;

public class AIChase : MonoBehaviour
{
	///References
	
	private MyCanvas playerhealth;

	private Rigidbody otherCarRb;

	

	private WheelCollider[] wheels;

	public float maxAngle = 30;
	public float maxTorque = 300;

	public GameObject wheelShape;

	public GameObject target;

	private StartGame startGame;
	
	// here we find all the WheelColliders down in the hierarchy
	public void Start()
	{
		otherCarRb = GetComponent<Rigidbody>();
		startGame = GameObject.Find("StartGameTrig").GetComponent<StartGame>();

		playerhealth = GameObject.Find("Canvas").GetComponent<MyCanvas>();
		
		target = GameObject.FindGameObjectWithTag("Player");
		wheels = GetComponentsInChildren<WheelCollider>();

		for (int i = 0; i < wheels.Length; ++i)
		{
			var wheel = wheels[i];

			// create wheel shapes only when needed
			if (wheelShape != null)
			{
				var ws = GameObject.Instantiate(wheelShape);
				ws.transform.parent = wheel.transform;

				if (wheel.transform.localPosition.x < 0f)
				{
					Debug.Log("Done");
					ws.transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
				}
			}
		}
	}

	// this is a really simple approach to updating wheels
	// here we simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero
	// this helps us to figure our which wheels are front ones and which are rear
	public void FixedUpdate()
	{
		if (startGame.gameIsStart==true)
		{
			Movement();
		}
		
		
	}

	void Movement()
	{

		Vector3 targetPosition = transform.position - target.transform.position;

		targetPosition.Normalize();

		float valueForAngle = Vector3.Cross(targetPosition, transform.forward).y;


		float valueForTorque = Vector3.Cross(targetPosition, transform.up).x;
		if (valueForTorque < 0)
		{
			valueForTorque *= -1;
		}
		if (valueForTorque < 0.5)
		{
			valueForTorque = Vector3.Cross(targetPosition, transform.up).z;
			if (valueForTorque < 0)
			{
				valueForTorque *= -1;
			}
		}


		//Debug.Log(valueForTorque);
		//Debug.Log(valueForAngle);


		// Все зависит от этих строк
		float angle = maxAngle * valueForAngle;

		float torque = maxTorque * valueForTorque;


		foreach (WheelCollider wheel in wheels)
		{
			// a simple car where front wheels steer while rear ones drive
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
				wheel.motorTorque = torque;

			// update visual wheels if any
			if (wheelShape)
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose(out p, out q);

				// assume that the only child of the wheelcollider is the wheel shape
				Transform shapeTransform = wheel.transform.GetChild(0);
				shapeTransform.position = p;
				shapeTransform.rotation = q;
			}

		}
	}

	/*private void OnCollisionEnter(Collision collision)
	{
		if (collision.rigidbody.gameObject.CompareTag("Player"))
		{
			Debug.Log("Hit");
			playerhealth.SetHealthPoint(25);

			otherCarRb.drag = 1;
		}
	}*/
	//////////////////////////////////////////////////
	private void OnCollisionExit(Collision collision)
	{
		Invoke("RefreshDrag", 1f);
		
	}

	private void RefreshDrag()
	{
		otherCarRb.drag = 0.15f;
	}
	//////////////////////////////////////////////////
}

