using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRenderer : MonoBehaviour
{

    public GameObject chassis1;
    public GameObject chassis2;
    public GameObject chassis3;
    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject forklift;
    public GameObject blade;
    public GameObject chainsaw;
    public GameObject missile;
    public GameObject rocket;
    public GameObject stinger;


    public RenderedCar RenderCar(Car car, Vector3 chassisPos, bool isGame, bool flipped, Game game, int player, bool playerControls)
    {

        GameObject chassis = null;
        GameObject attack1 = null;
        GameObject attack2 = null;
        GameObject forkliftObj = null;
        GameObject frontWheel = null;
        GameObject backWheel = null;

        Vector3[] attack1Pos = new Vector3[4];
        Vector3 attack2Pos = Vector3.zero;
        Vector3 forkliftPos = Vector3.zero;
        Vector3 frontWheelPos = Vector3.zero;
        Vector3 backWheelPos = Vector3.zero;
        Vector3 anchorPos = Vector3.zero;
        Vector3 connectedAnchor = Vector3.zero;

        Vector3 bladeAnchorPos = Vector3.zero;
        Vector3 bladeConnectedAnchor = Vector3.zero;

        float speed = (flipped) ? -300f : 300f;

        if (car.chassis != null)
        {

            switch (car.chassis.id)
            {
                case 1:
                    chassis = Instantiate(chassis1, chassisPos, Quaternion.identity); 
                    
                    if (!flipped)
                    {
                        attack1Pos[0] = new Vector3(1.21f, -0.14f);
                        attack1Pos[1] = new Vector3(1.21f, -0.14f);
                        attack1Pos[2] = new Vector3(0.5f, -0.17f);
                        attack1Pos[3] = new Vector3(1.21f, -0.14f);

                        forkliftPos = new Vector3(-0.62f, -0.7f);
                        anchorPos = new Vector3(1.84f, 0f);
                        connectedAnchor = new Vector3(0.3f, -0.7f);

                        frontWheelPos = new Vector3(0.55f, -1.24f);
                        backWheelPos = new Vector3(-0.55f, -1.24f);

                        bladeAnchorPos = new Vector3(-1.25f, -0.09f);
                        bladeConnectedAnchor = new Vector3(0.585f, -0.185f);

                    }
                    else
                    {
                        attack1Pos[0] = new Vector3(-1.16f, -0.11f);  
                        attack1Pos[1] = new Vector3(-1.16f, -0.11f);
                        attack1Pos[2] = new Vector3(-0.55f, -0.16f);
                        attack1Pos[3] = new Vector3(-1.16f, -0.11f);

                        forkliftPos = new Vector3(0.63f, -0.67f);
                        anchorPos = new Vector3(-1.78f, 0f);
                        connectedAnchor = new Vector3(-0.26f, -0.67f);

                        frontWheelPos = new Vector3(-0.48f, -1.24f);
                        backWheelPos = new Vector3(0.53f, -1.24f);

                        bladeAnchorPos = new Vector3(1.35f, -0.09f);
                        bladeConnectedAnchor = new Vector3(-0.485f, -0.155f);
                    }

                    break;
                case 2:

                    chassis = Instantiate(chassis2, chassisPos, Quaternion.identity);

                    if (!flipped)
                    {
                        attack1Pos[0] = new Vector3(1.65f, 0.3f);
                        attack1Pos[1] = new Vector3(1.65f, 0.3f);
                        attack1Pos[2] = new Vector3(0.97f, 0.24f);
                        attack1Pos[3] = new Vector3(1.65f, 0.3f);

                        forkliftPos = new Vector3(-0.29f, -0.08f);
                        anchorPos = new Vector3(1.83f, 0f);
                        connectedAnchor = new Vector3(0.625f, -0.08f);

                        frontWheelPos = new Vector3(0.55f, -0.48f);
                        backWheelPos = new Vector3(-0.91f, -0.48f);

                        bladeAnchorPos = new Vector3(-1.29f, -0.09f);
                        bladeConnectedAnchor = new Vector3(1.005f, 0.255f);

                    }
                    else
                    {
                        attack1Pos[0] = new Vector3(-1.61f, 0.29f);   
                        attack1Pos[1] = new Vector3(-1.61f, 0.29f);
                        attack1Pos[2] = new Vector3(-0.99f, 0.25f);
                        attack1Pos[3] = new Vector3(-1.61f, 0.29f);

                        forkliftPos = new Vector3(0.27f, -0.09f);
                        anchorPos = new Vector3(-1.8f, 0f);
                        connectedAnchor = new Vector3(-0.63f, -0.09f);

                        frontWheelPos = new Vector3(-0.49f, -0.48f);
                        backWheelPos = new Vector3(0.92f, -0.48f);

                        bladeAnchorPos = new Vector3(1.33f, -0.09f);
                        bladeConnectedAnchor = new Vector3(-0.945f, 0.245f);
                    }
                    break;
                case 3:

                    chassis = Instantiate(chassis3, chassisPos, Quaternion.identity);

                    if (!flipped)
                    {
                        attack1Pos[0] = new Vector3(1.77f, 0.18f);
                        attack1Pos[1] = new Vector3(1.77f, 0.18f);
                        attack1Pos[2] = new Vector3(1.13f, 0.14f);
                        attack1Pos[3] = new Vector3(1.77f, 0.18f);

                        attack2Pos = new Vector3(-0.2f, 0.08f);

                        forkliftPos = new Vector3(-0.05f, -0.1f);
                        anchorPos = new Vector3(1.8f, 0f);
                        connectedAnchor = new Vector3(0.85f, -0.1f);

                        frontWheelPos = new Vector3(1.07f, -0.57f);
                        backWheelPos = new Vector3(-0.17f, -0.57f);

                        bladeAnchorPos = new Vector3(-1.26f, -0.11f);
                        bladeConnectedAnchor = new Vector3(1.14f, 0.125f);

                    }
                    else
                    {
                        attack1Pos[0] = new Vector3(-1.77f, 0.23f);
                        attack1Pos[1] = new Vector3(-1.77f, 0.23f);
                        attack1Pos[2] = new Vector3(-1.11f, 0.18f);
                        attack1Pos[3] = new Vector3(-1.77f, 0.23f);

                        attack2Pos = new Vector3(0.26f, 0.1f);

                        forkliftPos = new Vector3(0.1f, -0.15f);
                        anchorPos = new Vector3(-1.78f, 0f);
                        connectedAnchor = new Vector3(-0.79f, -0.15f);

                        frontWheelPos = new Vector3(-1.03f, -0.57f);
                        backWheelPos = new Vector3(0.2f, -0.57f);

                        bladeAnchorPos = new Vector3(1.33f, -0.11f);
                        bladeConnectedAnchor = new Vector3(-1.105f, 0.175f);
                    }
                    break;
            }

            if (flipped)
            {
                chassis.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (isGame)
            {
                chassis.AddComponent<Rigidbody2D>().mass = 5;
                chassis.AddComponent<PolygonCollider2D>();
                WallAttackDetection wallAttack = chassis.AddComponent<WallAttackDetection>();
                wallAttack.game = game;
                wallAttack.player = player;

                chassis.tag = "Chassis";

            }

        }

        if (car.attack1 != null)
        { 

            switch (car.attack1.id)
            {
                case 7:
                    attack1 = Instantiate(blade, new Vector3(0, 0), Quaternion.identity);
                    attack1.transform.position = chassisPos + attack1Pos[0];
                    break;
                case 8:
                    attack1 = Instantiate(chainsaw, new Vector3(0, 0), Quaternion.identity);
                    attack1.transform.position = chassisPos + attack1Pos[1];
                    break;
                case 9:
                    attack1 = Instantiate(rocket, new Vector3(0, 0), Quaternion.identity);
                    attack1.transform.position = chassisPos + attack1Pos[2];
                    break;
                case 10:
                    attack1 = Instantiate(stinger, new Vector3(0, 0), Quaternion.identity);
                    attack1.transform.position = chassisPos + attack1Pos[3];
                    break;
            }  

            attack1.transform.SetParent(chassis.transform);
            attack1.transform.localScale = new Vector3(0.5f, 0.5f);
            attack1.GetComponent<SpriteRenderer>().sortingOrder = 1;

            if (flipped)
            {
                attack1.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (isGame)
            {
                if (car.attack1.id == 7)
                {
                    HingeJoint2D joint = attack1.AddComponent<HingeJoint2D>();
                    joint.connectedBody = chassis.GetComponent<Rigidbody2D>();
                    joint.anchor = bladeAnchorPos;
                    joint.connectedAnchor = bladeConnectedAnchor;

                    JointMotor2D motor = joint.motor;
                    motor.motorSpeed = 200;
                    motor.maxMotorTorque = 50;
                    joint.motor = motor;

                    attack1.GetComponent<Rigidbody2D>().mass = 0.3f;
                }
                else
                {
                    FixedJoint2D joint = attack1.AddComponent<FixedJoint2D>();
                    joint.connectedBody = chassis.GetComponent<Rigidbody2D>();
                    attack1.GetComponent<Rigidbody2D>().mass = 0.1f;

                }

                PolygonCollider2D polygonCollider = attack1.AddComponent<PolygonCollider2D>();
                polygonCollider.isTrigger = true;

                if (car.attack1.id != 9)
                {
                    AttackDetection attackDetection = attack1.AddComponent<AttackDetection>();
                    attackDetection.game = game;
                    attackDetection.player = player;
                }
     
            }
            
        }

        if (car.frontWheel != null)
        {
            float radius = -1;

            switch (car.frontWheel.id)
            {
                case 4:
                    radius = 0.73f;
                    frontWheel = Instantiate(wheel1, new Vector3(0, 0), Quaternion.identity);
                    break;
                case 5:
                    radius = 0.61f;
                    frontWheel = Instantiate(wheel2, new Vector3(0, 0), Quaternion.identity);
                    break;
            }

            frontWheel.transform.SetParent(chassis.transform);

            frontWheel.transform.position = chassisPos + frontWheelPos;
            frontWheel.transform.localScale = new Vector3(0.5f, 0.5f);
            frontWheel.GetComponent<SpriteRenderer>().sortingOrder = 1;

            if (flipped)
            {
                frontWheel.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (isGame)
            {
                WheelJoint2D wheelJoint2D = frontWheel.AddComponent<WheelJoint2D>();
                wheelJoint2D.connectedBody = chassis.GetComponent<Rigidbody2D>();
                wheelJoint2D.connectedAnchor = frontWheelPos;

                JointSuspension2D jointSuspension = wheelJoint2D.suspension;
                jointSuspension.dampingRatio = 0.9f;
                jointSuspension.frequency = 10;
                wheelJoint2D.suspension = jointSuspension;


                wheelJoint2D.useMotor = true;
                JointMotor2D motor = wheelJoint2D.motor;
                motor.motorSpeed = speed;
                wheelJoint2D.motor = motor;

                wheelJoint2D.useMotor = !playerControls;

                CircleCollider2D circleCollider = frontWheel.AddComponent<CircleCollider2D>();
                circleCollider.radius = radius;
            }
        }

        if (car.backWheel != null)
        {

            float radius = -1;

            switch (car.backWheel.id)
            {
                case 4:
                    radius = 0.73f;
                    backWheel = Instantiate(wheel1, new Vector3(0, 0), Quaternion.identity);
                    break;
                case 5:
                    radius = 0.61f;
                    backWheel = Instantiate(wheel2, new Vector3(0, 0), Quaternion.identity);
                    break;
            }

            backWheel.transform.SetParent(chassis.transform);

            backWheel.transform.position = chassisPos + backWheelPos;
            backWheel.transform.localScale = new Vector3(0.5f, 0.5f);
            backWheel.GetComponent<SpriteRenderer>().sortingOrder = 1;

            if (flipped)
            {
                backWheel.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (isGame)
            {
                WheelJoint2D wheelJoint2D = backWheel.AddComponent<WheelJoint2D>();
                wheelJoint2D.connectedBody = chassis.GetComponent<Rigidbody2D>();
                wheelJoint2D.connectedAnchor = backWheelPos;

                JointSuspension2D jointSuspension = wheelJoint2D.suspension;
                jointSuspension.dampingRatio = 0.9f;
                jointSuspension.frequency = 10;
                wheelJoint2D.suspension = jointSuspension;


                wheelJoint2D.useMotor = true;
                JointMotor2D motor = wheelJoint2D.motor;
                motor.motorSpeed = speed;
                wheelJoint2D.motor = motor;

                wheelJoint2D.useMotor = !playerControls;

                CircleCollider2D circleCollider = backWheel.AddComponent<CircleCollider2D>();
                circleCollider.radius = radius;
            }

        }

        if (car.forklift != null)
        {

            forkliftObj = Instantiate(forklift);
            forkliftObj.transform.position = chassisPos + forkliftPos;
            forkliftObj.transform.localScale = new Vector3(0.5f, 0.5f);
            forkliftObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
            forkliftObj.transform.SetParent(chassis.transform);

            if (!flipped)
            {
                forkliftObj.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (isGame)
            {
                forkliftObj.AddComponent<PolygonCollider2D>();
                HingeJoint2D joint = forkliftObj.AddComponent<HingeJoint2D>();
                joint.connectedBody = chassis.GetComponent<Rigidbody2D>();
                joint.anchor = anchorPos;
                joint.connectedAnchor = connectedAnchor;

                JointMotor2D motor = joint.motor;
                if (player == 0) motor.motorSpeed = -50;
                else motor.motorSpeed = 50;
                motor.maxMotorTorque = 500;
                joint.motor = motor;

                joint.useMotor = !playerControls;

                forkliftObj.GetComponent<Rigidbody2D>().mass = 0.3f;

            }

        }

        if (car.attack2 != null)
        {

            attack2 = Instantiate(rocket);
            attack2.transform.position = chassisPos + attack2Pos;
            attack2.transform.localScale = new Vector3(0.5f, 0.5f);
            attack2.GetComponent<SpriteRenderer>().sortingOrder = 1;

            attack2.transform.SetParent(chassis.transform);

            if (flipped)
            {
                attack2.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (isGame)
            {
                FixedJoint2D joint = attack2.AddComponent<FixedJoint2D>();
                joint.connectedBody = chassis.GetComponent<Rigidbody2D>();
                attack2.GetComponent<Rigidbody2D>().mass = 0;
            }

        }

        RenderedCar renderedCar = new RenderedCar();
        renderedCar.chassis = chassis;
        renderedCar.attack1 = attack1;
        renderedCar.attack2 = attack2;
        renderedCar.forklift = forkliftObj;
        renderedCar.frontWheel = frontWheel;
        renderedCar.backWheel = backWheel;

        return renderedCar;

    }

    public GameObject RenderItem(int id)
    {
        switch (id)
        {
            case 1:
                return Instantiate(chassis1);
            case 2:
                return Instantiate(chassis2);
            case 3:
                return Instantiate(chassis3);
            case 4:
                return Instantiate(wheel1);
            case 5:
                return Instantiate(wheel2);
            case 6:
                return Instantiate(forklift);
            case 7:
                return Instantiate(blade);
            case 8:
                return Instantiate(chainsaw);
            case 9:
                return Instantiate(rocket);
            case 10:
                return Instantiate(stinger);
        }

        return null;
    }
}
