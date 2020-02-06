using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public Car car;

    private int width, height;

    // Start is called before the first frame update
    void Start()
    {
        width = Screen.width;
        height = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(Input.touchCount);
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Vector2 touchPos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                if (touchPos.x <= width / 2.0f)
                {
                    SetWheelMotors(true);
                }
                else
                {
                    SetForkliftMotor(true);
                }

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (touchPos.x <= width / 2.0f)
                {
                    SetWheelMotors(false);
                }
                else
                {
                    SetForkliftMotor(false);
                }
            }
        }

    }

    void SetWheelMotors(bool value)
    {
        if (car.frontWheel != null)
        {
            WheelJoint2D wheelJoint2D = car.renderedCar.frontWheel.GetComponent<WheelJoint2D>();
            wheelJoint2D.useMotor = value;
        }

        if (car.backWheel != null)
        {
            WheelJoint2D wheelJoint2D = car.renderedCar.backWheel.GetComponent<WheelJoint2D>();
            wheelJoint2D.useMotor = value;
        }
    }

    void SetForkliftMotor(bool value)
    {
        if (car.forklift != null)
        {
            HingeJoint2D joint = car.renderedCar.forklift.GetComponent<HingeJoint2D>();
            joint.useMotor = value;
        }
    }
}
