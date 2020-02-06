using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Garage : MonoBehaviour
{
    public GameObject canvas;
    public GameObject boxPrefab;
    public GameObject gameObject;

    public GameObject[] boxPlaces;

    public GameObject newItem;
    public Image stars;

    public Image nextBoxImage;
    public Text nextBoxText;

    public GameObject settingsMenu, gameHistory;

    private Sprite oneStar, twoStars, threeStars;

    private Player player;

    private CarRenderer carRenderer;

    private List<Box> boxes;

    // Start is called before the first frame update
    void Start()
    {

        oneStar = Resources.Load<Sprite>("one-star");
        twoStars = Resources.Load<Sprite>("two-stars");
        threeStars = Resources.Load<Sprite>("three-stars");

        string nickname = PlayerPrefs.GetString("player");

        player = DatabaseDataAcces.getPlayerWithNickname(nickname);

        int lastWon = DatabaseDataAcces.getNumberofWonInRow(player.id) % 3;

        nextBoxImage.fillAmount = (lastWon * 1f) / 3.0f;
        nextBoxText.text = lastWon + " / 3 Won";

        LoadBoxes();

        LoadCar();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            Transform crate = boxPlaces[i].transform.GetChild(0);
            Transform text = boxPlaces[i].transform.GetChild(1);

            long now = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;

            long elapsed = (now - boxes[i].acquired) / (1000 * 60);
            string label = "OPEN";

            if (elapsed < 120) label = (120 - elapsed) + "m";

            text.gameObject.GetComponent<Text>().text = label;

        }
    }

    private void LoadBoxes()
    {

        for (int i = 0; i < 4; i++)
        {
            Transform crate = boxPlaces[i].transform.GetChild(0);
            Transform text = boxPlaces[i].transform.GetChild(1);

            crate.gameObject.SetActive(false);
            text.gameObject.SetActive(false);         
        }

        boxes = DatabaseDataAcces.getPlayerBoxes(player.id);

        for (int i = 0; i < boxes.Count; i++)
        {
            Transform crate = boxPlaces[i].transform.GetChild(0);
            Transform text = boxPlaces[i].transform.GetChild(1);

            long now = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;

            long elapsed = (now - boxes[i].acquired) / (1000 * 60);
            string label = "OPEN";

            if (elapsed < 120) label = (120 - elapsed) + "m";

            text.gameObject.GetComponent<Text>().text = label;
            text.gameObject.SetActive(true);

            BoxOnClick onClick = crate.GetComponent<BoxOnClick>();
            onClick.box = boxes[i];
            onClick.garage = this;
            crate.gameObject.SetActive(true);

        }

    }

    public void OpenBox(int id)
    {
        DatabaseDataAcces.OpenBox(id);

        int randomItemId = Random.Range(1, 11);
        int randomStars = Random.Range(1, 4);

        DatabaseDataAcces.InsertHasCarPart(player.id, randomItemId, randomStars);

        newItem.SetActive(true);
        newItem.transform.localScale = new Vector3(1, 1, 1);

        if (randomStars == 1) stars.sprite = oneStar;
        else if (randomStars == 2) stars.sprite = twoStars;
        else stars.sprite = threeStars;

        GameObject item = carRenderer.RenderItem(randomItemId);

        item.transform.SetParent(newItem.transform, false);
        item.transform.position = new Vector3(0f, 0f, 0f);
        item.transform.localScale = new Vector3(100f, 100f);
        item.GetComponent<SpriteRenderer>().sortingOrder = 5;

        LoadBoxes();
    }

    private void LoadCar()
    {
    
        Car car = DatabaseDataAcces.getPlayersCar(player.id);

        carRenderer = gameObject.GetComponent<CarRenderer>();

        RenderedCar renderedCar = carRenderer.RenderCar(car, new Vector3(0,0), false, false, null, -1, false);

        renderedCar.chassis.transform.localScale = new Vector3(2f, 2f);
        renderedCar.chassis.AddComponent<PolygonCollider2D>();
        CarOnClick carOnClick = renderedCar.chassis.AddComponent<CarOnClick>();
        carOnClick.garage = this;

    }

}
