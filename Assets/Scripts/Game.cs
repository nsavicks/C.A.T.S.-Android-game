using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public GameObject gObj;
    public GameObject ground, leftWall, rightWall;
    public GameObject missilePrefab;
    public Image playerHB, opponentHB;
    public Text winnerText;
    public Button goToGarage;
    public GameObject winnerBackground;

    private bool finished = false;

    private Player player;
    private Player opponent;

    private Car playerCar;
    private Car opponentCar;

    private int playerHP = 100;
    private int opponentHP = 100;

    private bool playerAttacking = false;
    private bool opponentAttacking = false;

    private int HealthRefreshRate = 30;

    private int RocketMoveRate = 10;
    private int RocketFireRate = 75;
    private List<GameObject> playerRockets = new List<GameObject>();
    private List<Vector3> playerRocketsDirection = new List<Vector3>();
    private List<GameObject> opponentRockets = new List<GameObject>();
    private List<Vector3> opponentRocketsDirection = new List<Vector3>();

    private float lastHPChangeTime = 0;

    private bool moveWalls = false;
    private bool playerHittingWall = false;
    private bool opponentHittingWall = false;

    // Start is called before the first frame update
    void Start()
    {
        string nickname = PlayerPrefs.GetString("player");

        player = DatabaseDataAcces.getPlayerWithNickname(nickname);

        winnerText.gameObject.SetActive(false);
        goToGarage.gameObject.SetActive(false);
        winnerBackground.gameObject.SetActive(false);

        GetOpponent();
        LoadCars();
        IgnoreCollisions();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            UpdateHealth();

            UpdateRockets();

            FireRockets();

           // if (Time.time - lastHPChangeTime > 10.0f) moveWalls = true;

            UpdateWalls();
        }
        
    }

    void UpdateWalls()
    {
        if (moveWalls)
        {
            float distance = rightWall.transform.position.x - leftWall.transform.position.x;

            if (distance > 6.0f)
            {
                leftWall.transform.position += new Vector3(0.01f, 0);

                rightWall.transform.position -= new Vector3(0.01f, 0);
            }
            
        }
    }

    void IgnoreCollisions()
    {
        // fork and ground
        if (playerCar.forklift != null && ground != null)
            Physics2D.IgnoreCollision(playerCar.renderedCar.forklift.GetComponent<PolygonCollider2D>(), ground.GetComponent<BoxCollider2D>());

        if (opponentCar.forklift != null && ground != null)
            Physics2D.IgnoreCollision(opponentCar.renderedCar.forklift.GetComponent<PolygonCollider2D>(), ground.GetComponent<BoxCollider2D>());

        // fork and chassis
        if (playerCar.forklift != null && playerCar.chassis != null)
            Physics2D.IgnoreCollision(playerCar.renderedCar.forklift.GetComponent<PolygonCollider2D>(), playerCar.renderedCar.chassis.GetComponent<PolygonCollider2D>());

        if (opponentCar.forklift != null && opponentCar.chassis != null)
            Physics2D.IgnoreCollision(opponentCar.renderedCar.forklift.GetComponent<PolygonCollider2D>(), opponentCar.renderedCar.chassis.GetComponent<PolygonCollider2D>());

        // fork and wheels
        if (playerCar.forklift != null && playerCar.frontWheel != null)
            Physics2D.IgnoreCollision(playerCar.renderedCar.forklift.GetComponent<PolygonCollider2D>(), playerCar.renderedCar.frontWheel.GetComponent<CircleCollider2D>());

        if (playerCar.forklift != null && playerCar.backWheel != null)
            Physics2D.IgnoreCollision(playerCar.renderedCar.forklift.GetComponent<PolygonCollider2D>(), playerCar.renderedCar.backWheel.GetComponent<CircleCollider2D>());

        if (opponentCar.forklift != null && opponentCar.frontWheel != null)
            Physics2D.IgnoreCollision(opponentCar.renderedCar.forklift.GetComponent<PolygonCollider2D>(), opponentCar.renderedCar.frontWheel.GetComponent<CircleCollider2D>());

        if (opponentCar.forklift != null && opponentCar.backWheel != null)
            Physics2D.IgnoreCollision(opponentCar.renderedCar.forklift.GetComponent<PolygonCollider2D>(), opponentCar.renderedCar.backWheel.GetComponent<CircleCollider2D>());

        // Ignore same player and attach collider
        if (playerCar.chassis != null && playerCar.attack1 != null)
            Physics2D.IgnoreCollision(playerCar.renderedCar.chassis.GetComponent<PolygonCollider2D>(), playerCar.renderedCar.attack1.GetComponent<PolygonCollider2D>());

        if (opponentCar.chassis != null && opponentCar.attack1 != null)
            Physics2D.IgnoreCollision(opponentCar.renderedCar.chassis.GetComponent<PolygonCollider2D>(), opponentCar.renderedCar.attack1.GetComponent<PolygonCollider2D>());
    }

    void GetOpponent()
    {
        // TODO Get random opponent
        opponent = player;
    }

    void LoadCars()
    {

        CarRenderer carRenderer = gObj.GetComponent<CarRenderer>();

        playerCar = DatabaseDataAcces.getPlayersCar(player.id);

        playerCar.renderedCar = carRenderer.RenderCar(playerCar, new Vector3(-5.0f, -2.5f), true, false, this, 0);

        opponentCar = DatabaseDataAcces.getPlayersCar(opponent.id);

        opponentCar.renderedCar =  carRenderer.RenderCar(opponentCar, new Vector3(5.0f, -2.5f), true, true, this, 1);

    }

    public void StartedAttack(int player)
    {
        if (player == 0)
        {
            playerAttacking = true;
        }
        else
        {
            opponentAttacking = true;
        }
    }

    public void EndedAttack(int player)
    {
        if (player == 0)
        {
            playerAttacking = false;
        }
        else
        {
            opponentAttacking = false;
        }
    }

    void UpdateHealth()
    {
        if (HealthRefreshRate == 0)
        {
            if (playerAttacking)
            {
                ReduceHP(1, playerCar.attack1.power);
            }

            if (opponentAttacking)
            {
                ReduceHP(0, opponentCar.attack1.power);
            }

            if (playerHittingWall)
            {
                ReduceHP(0, 10);
            }

            if (opponentHittingWall)
            {
                ReduceHP(1, 10);
            }

            HealthRefreshRate = 30;
        }
        else
        {
            HealthRefreshRate--;
        }
    }

    void ReduceHP(int player, int amount)
    {
        lastHPChangeTime = Time.time;

        if (player == 0)
        {
            playerHP -= amount;
        }
        else
        {
            opponentHP -= amount;
        }

        playerHB.fillAmount = playerHP / 100.0f;
        opponentHB.fillAmount = opponentHP / 100.0f;

        if (playerHP <= 0)
        {
            finished = true;
            ShowWinner(1);
        }
        
        if (opponentHP <= 0)
        {
            finished = true;
            ShowWinner(0);
        }

    }

    void FireRockets()
    {
        if (RocketFireRate == 0)
        {
            if (playerCar.attack1 != null && playerCar.attack1.id == 9)
            {
                GenerateRocket(playerCar.renderedCar.attack1.transform.position, 0, 1);
            }

            if (playerCar.attack2 != null && playerCar.attack2.id == 9)
            {
                GenerateRocket(playerCar.renderedCar.attack2.transform.position, 0, 2);
            }

            if (opponentCar.attack1 != null && opponentCar.attack1.id == 9)
            {
                GenerateRocket(opponentCar.renderedCar.attack1.transform.position, 1, 1);
            }

            if (opponentCar.attack2 != null && opponentCar.attack2.id == 9)
            {
                GenerateRocket(opponentCar.renderedCar.attack2.transform.position, 1, 2);
            }

            RocketFireRate = 150;
        }
        else
        {
            RocketFireRate--;
        }

    }

    public void UpdateRockets()
    {

        for (int i = 0; i < playerRockets.Count; i++)
        {
            Vector3 direction = playerRocketsDirection[i];
            playerRockets[i].transform.position += direction * Time.deltaTime * RocketMoveRate;
        }

        for (int i = 0; i < opponentRockets.Count; i++)
        {
            Vector3 direction = opponentRocketsDirection[i];
            direction.x *= -1f;
            opponentRockets[i].transform.position += direction * Time.deltaTime * RocketMoveRate;
        }

    }

    public void GenerateRocket(Vector3 position, int player, int attackId)
    {
        GameObject missile = Instantiate(missilePrefab, position, Quaternion.identity);
        missile.transform.localScale = new Vector3(0.5f, 0.5f);

        if (player == 1)
        {
            missile.GetComponent<SpriteRenderer>().flipX = true;
            missile.transform.Rotate(opponentCar.renderedCar.chassis.transform.eulerAngles);
        }
        else
        {
            missile.transform.Rotate(playerCar.renderedCar.chassis.transform.eulerAngles);
        }

        PolygonCollider2D polygonCollider = missile.AddComponent<PolygonCollider2D>();
        polygonCollider.isTrigger = true;

        RocketAttackDetection rocketAttack = missile.AddComponent<RocketAttackDetection>();
        rocketAttack.game = this;
        rocketAttack.player = player;
        rocketAttack.rocket = missile;
        rocketAttack.attackId = attackId;

        if (player == 0)
        {
            rocketAttack.direction = playerCar.renderedCar.chassis.transform.right;
            playerRockets.Add(missile);
            playerRocketsDirection.Add(playerCar.renderedCar.chassis.transform.right);
        }
        else
        {
            rocketAttack.direction = opponentCar.renderedCar.chassis.transform.right;
            opponentRockets.Add(missile);
            opponentRocketsDirection.Add(opponentCar.renderedCar.chassis.transform.right);
        }    

        // Remove collisions with his chassis
        if (player == 0)
            Physics2D.IgnoreCollision(polygonCollider, playerCar.renderedCar.chassis.GetComponent<PolygonCollider2D>());
        else
            Physics2D.IgnoreCollision(polygonCollider, opponentCar.renderedCar.chassis.GetComponent<PolygonCollider2D>());
    }

    public void RocketHit(int player, GameObject rocket, Vector3 direction)
    {
        if (player == 0)
        {
            playerRockets.Remove(rocket);
            playerRocketsDirection.Remove(direction);
            GameObject.Destroy(rocket);
        }
        else
        {
            opponentRockets.Remove(rocket);
            opponentRocketsDirection.Remove(direction);
            GameObject.Destroy(rocket);
        }
    }

    public void RocketHitChassis(int player, int attackId, GameObject rocket, Vector3 direction)
    {
        if (player == 0)
        {
            if (attackId == 1)
            {
                ReduceHP(1, playerCar.attack1.power);
            }
            else
            {
                ReduceHP(1, playerCar.attack2.power);
            }
        }
        else
        {
            if (attackId == 1)
            {
                ReduceHP(0, opponentCar.attack1.power);
            }
            else
            {
                ReduceHP(0, opponentCar.attack2.power);
            }
        }

        RocketHit(player, rocket, direction);
    }

    public void HitWall(int player)
    {
        if (player == 0)
        {
            playerHittingWall = true;
        }
        else
        {
            opponentHittingWall = true;
        }
    }

    public void ExitFromWall(int player)
    {
        if (player == 0)
        {
            playerHittingWall = false;
        }
        else
        {
            opponentHittingWall = false;
        }
    }

    public void ShowWinner(int winner)
    {
        if (winner == 0)
        {
            winnerText.text = "You won!";
        }
        else
        {
            winnerText.text = "You lost!";
        }

        winnerText.gameObject.SetActive(true);
        goToGarage.gameObject.SetActive(true);
        winnerBackground.gameObject.SetActive(true);
    }
}
