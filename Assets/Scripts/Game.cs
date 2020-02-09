using System;
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
    public GameObject explosionPrefab;

    public Text playerName;
    public Text opponentName;

    public AudioSource audioSource;
    

    private bool finished = false;

    private Player player;
    private Player opponent;

    private Car playerCar;
    private Car opponentCar;

    private int playerHP;
    private int opponentHP;

    private bool playerAttacking = false;
    private bool opponentAttacking = false;

    private int HealthRefreshRate = 5;

    private int RocketMoveRate = 10;
    private int RocketFireRate = 75;

    private float lastHPChangeTime;

    private bool moveWalls;
    private bool playerHittingWall;
    private bool opponentHittingWall;

    private bool playerControls;

    // Start is called before the first frame update
    void Start()
    {

        moveWalls = false;
        playerHittingWall = false;
        opponentHittingWall = false;
        lastHPChangeTime = Time.time;
        playerControls = PlayerPrefs.GetInt("controls") == 1;

        string nickname = PlayerPrefs.GetString("player");

        player = DatabaseDataAcces.getPlayerWithNickname(nickname);

        winnerText.gameObject.SetActive(false);
        goToGarage.gameObject.SetActive(false);
        winnerBackground.gameObject.SetActive(false);

        GetOpponent();

        playerName.text = player.nickname;
        opponentName.text = opponent.nickname;

        LoadCars();
        IgnoreCollisions();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            UpdateHealth();

            FireRockets();

           if (Time.time - lastHPChangeTime > 10.0f) moveWalls = true;

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
        List<Player> players = DatabaseDataAcces.getAllPlayers();

        while (true)
        {
            int ind = UnityEngine.Random.Range(0, players.Count);

            opponent = players[ind];

            if (opponent.id != player.id) break;
        }
    }

    void LoadCars()
    {

        CarRenderer carRenderer = gObj.GetComponent<CarRenderer>();

        playerCar = DatabaseDataAcces.getPlayersCar(player.id);

        playerCar.renderedCar = carRenderer.RenderCar(playerCar, new Vector3(-5.0f, -2.5f), true, false, this, 0, playerControls);

        opponentCar = DatabaseDataAcces.getPlayersCar(opponent.id);

        opponentCar.renderedCar =  carRenderer.RenderCar(opponentCar, new Vector3(5.0f, -2.5f), true, true, this, 1, false);

        playerHP = playerCar.GetCarHealth();
        opponentHP = opponentCar.GetCarHealth();

        if (playerControls)
        {
            PlayerControls playerControls = gObj.AddComponent<PlayerControls>();
            playerControls.car = playerCar;
        }

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
                ReduceHP(1, playerCar.attack1.power * playerCar.attack1Stars);
            }

            if (opponentAttacking)
            {
                ReduceHP(0, opponentCar.attack1.power * opponentCar.attack1Stars);
            }

            if (playerHittingWall && moveWalls)
            {
                ReduceHP(0, 5);
            }

            if (opponentHittingWall && moveWalls)
            {
                ReduceHP(1, 5);
            }

            HealthRefreshRate = 5;
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

        playerHB.fillAmount = playerHP / (playerCar.GetCarHealth() * 1f);
        opponentHB.fillAmount = opponentHP / (opponentCar.GetCarHealth() * 1f);

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

    public void GenerateRocket(Vector3 position, int player, int attackId)
    {
        GameObject missile = null;
        

        if (player == 1)
        {
            missile = Instantiate(missilePrefab, position, opponentCar.renderedCar.chassis.transform.rotation);
            missile.GetComponent<SpriteRenderer>().flipX = true;
            missile.GetComponent<Rigidbody2D>().AddForce(-opponentCar.renderedCar.chassis.transform.right * 300);
        }
        else
        {
            missile = Instantiate(missilePrefab, position, playerCar.renderedCar.chassis.transform.rotation);
            missile.GetComponent<Rigidbody2D>().AddForce(playerCar.renderedCar.chassis.transform.right * 300);
        }

        missile.transform.localScale = new Vector3(0.5f, 0.5f);

        PolygonCollider2D polygonCollider = missile.AddComponent<PolygonCollider2D>();

        RocketAttackDetection rocketAttack = missile.AddComponent<RocketAttackDetection>();
        rocketAttack.game = this;
        rocketAttack.player = player;
        rocketAttack.rocket = missile;
        rocketAttack.attackId = attackId;

        // Remove collisions
        IgnoreMissileCollisions(polygonCollider, player);
 
    }

    private void IgnoreMissileCollisions(PolygonCollider2D polygonCollider, int player)
    {
        if (player == 0)
        {
            Physics2D.IgnoreCollision(polygonCollider, playerCar.renderedCar.chassis.GetComponent<PolygonCollider2D>());
        }
        else
        {
            Physics2D.IgnoreCollision(polygonCollider, opponentCar.renderedCar.chassis.GetComponent<PolygonCollider2D>());
        }

        if (playerCar.forklift != null)
            Physics2D.IgnoreCollision(polygonCollider, playerCar.renderedCar.forklift.GetComponent<PolygonCollider2D>());
        if (opponentCar.forklift != null)
            Physics2D.IgnoreCollision(polygonCollider, opponentCar.renderedCar.forklift.GetComponent<PolygonCollider2D>());

        if (playerCar.frontWheel != null)
        {
            Physics2D.IgnoreCollision(polygonCollider, playerCar.renderedCar.frontWheel.GetComponent<CircleCollider2D>());
        }

        if (playerCar.backWheel != null)
        {
            Physics2D.IgnoreCollision(polygonCollider, playerCar.renderedCar.backWheel.GetComponent<CircleCollider2D>());
        }

        if (opponentCar.frontWheel != null)
        {
            Physics2D.IgnoreCollision(polygonCollider, opponentCar.renderedCar.frontWheel.GetComponent<CircleCollider2D>());
        }

        if (opponentCar.backWheel != null)
        {
            Physics2D.IgnoreCollision(polygonCollider, opponentCar.renderedCar.backWheel.GetComponent<CircleCollider2D>());
        }

    }

    public void RocketHit(int player, GameObject rocket, Vector3 direction)
    {
        if (player == 0)
        {
            GameObject.Destroy(rocket);
        }
        else
        {
            GameObject.Destroy(rocket);
        }

        audioSource.Play();
    }

    public void RocketHitChassis(int player, int attackId, GameObject rocket, Vector3 direction)
    {
        if (player == 0)
        {

            //Vector2 vel = opponentCar.renderedCar.chassis.GetComponent<Rigidbody2D>().velocity;
            //vel.y += 1.5f;
            //vel.x += 1f;

            //opponentCar.renderedCar.chassis.GetComponent<Rigidbody2D>().velocity = vel;

            if (attackId == 1)
            {
                ReduceHP(1, playerCar.attack1.power * playerCar.attack1Stars);
            }
            else
            {
                ReduceHP(1, playerCar.attack2.power * playerCar.attack2Stars);
            }
        }
        else
        {

            //Vector2 vel = playerCar.renderedCar.chassis.GetComponent<Rigidbody2D>().velocity;
            //vel.y += 1.5f;
            //vel.x -= 1f;

            //playerCar.renderedCar.chassis.GetComponent<Rigidbody2D>().velocity = vel;

            if (attackId == 1)
            {
                ReduceHP(0, opponentCar.attack1.power * opponentCar.attack1Stars);
            }
            else
            {
                ReduceHP(0, opponentCar.attack2.power * opponentCar.attack2Stars);
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

            DatabaseDataAcces.insertGamePlayed(player.id, opponent.id, 1, 0);

            int wonCount = DatabaseDataAcces.getNumberofWonInRow(player.id);
            int cntNotOpened = DatabaseDataAcces.getNumberOfNotOpenedBoxes(player.id);

            if (wonCount % 3 == 0 && cntNotOpened < 4)
            {
                long now = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
                DatabaseDataAcces.insertBox(player.id, now);
            }

            winnerText.text = "You won!";
        }
        else
        {
            DatabaseDataAcces.insertGamePlayed(player.id, opponent.id, 2, 0);
            winnerText.text = "You lost!";
        }

        winnerText.gameObject.SetActive(true);
        goToGarage.gameObject.SetActive(true);
        winnerBackground.gameObject.SetActive(true);
    }
}
