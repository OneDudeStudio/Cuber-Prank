using UnityEngine;
using System.Collections;

public class RearWheelDrive : MonoBehaviour {
	private WheelCollider[] wheels;
	public float maxAngle = 30;
	public float maxTorque = 300;
	public float breakTorque = 2000;
	public GameObject wheelShape;
	private MyCanvas canvas;
	private StartGame startgame;
	float smoothnessAngle;
	float angle;
	float torque;
	Rigidbody rb;
	//bool turnToLeft =false;
	//bool turnToRight = false;
	//bool noTurn = true;
	// here we find all the WheelColliders down in the hierarchy
	public void Start()
	{
		rb = GetComponent<Rigidbody>();
		//joystick = GameObject.Find("DynamicJoystick").GetComponent<DynamicJoystick>();
		canvas = GameObject.Find("Canvas").GetComponent<MyCanvas>();
		startgame = GameObject.Find("StartGameTrig").GetComponent<StartGame>();
		wheels = GetComponentsInChildren<WheelCollider>();

		for (int i = 0; i < wheels.Length; ++i) 
		{
			var wheel = wheels [i];

			// create wheel shapes only when needed
			if (wheelShape != null)
			{
				var ws = GameObject.Instantiate (wheelShape);
				ws.transform.parent = wheel.transform;

				if (wheel.transform.localPosition.x < 0f)
				{
					
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
		ControlCar();
		foreach (WheelCollider wheel in wheels)
		{
			if (canvas.health > 0)
			{
				// a simple car where front wheels steer while rear ones drive
				if (wheel.transform.localPosition.z > 0)
					wheel.steerAngle = angle;

				if (wheel.transform.localPosition.z < 0)
					wheel.motorTorque = torque;
			}

			if (canvas.health <= 0)//
			{
				// a simple car where front wheels steer while rear ones drive
				if (wheel.transform.localPosition.z > 0)
				{
					wheel.steerAngle = 0;
					//Debug.Log("Angle is not work");
				}
					


				if (wheel.transform.localPosition.z < 0)
				{
					wheel.brakeTorque = breakTorque;
					//Debug.Log("Torque is not work");
				}
					
			}
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
	void ControlCar()
	{
		//Debug.Log(torque);
		if (canvas.isAlive == true /*&& canvas.gameIsLoad == true*/)
		{
			if (canvas.turnLeft == true)
			{
				smoothnessAngle = Mathf.Lerp(angle, maxAngle * -1, 0.125f);
			}
			else if (canvas.turnRight == true)
			{
				smoothnessAngle = Mathf.Lerp(angle, maxAngle * 1, 0.125f);
			}
			else if (canvas.noTurn ==true )
			{
				smoothnessAngle = Mathf.Lerp(angle, maxAngle * 0, 0.125f);
			}

			angle = smoothnessAngle;

			//angle = maxAngle * Input.GetAxis("Horizontal");
			//torque = maxTorque * Input.GetAxis("Vertical");
			if(startgame.gameIsStart == false)
			{
				torque = maxTorque * 0.5f;
			}
			else if(startgame.gameIsStart == true)
			{
				torque = maxTorque * 1f;
			}
			
			if(canvas.brake == true && rb.velocity.magnitude*3.6f >10 )//добавить ограничение по времени
			{
				torque = maxTorque * -1;
			}
			else if (canvas.brake == false && startgame.gameIsStart == true)
			{
				torque = maxTorque * 1f;
			}
			else if (canvas.brake == false && startgame.gameIsStart == false)
			{
				torque = maxTorque * 0.5f;
			}


		}
		else
		{
			angle = 0;
			torque = 0;
		}
	}
	
}
