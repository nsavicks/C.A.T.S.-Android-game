using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Garage : MonoBehaviour
{
    public GameObject canvas;
    public GameObject boxPrefab;
    public GameObject gameObject;

    public GameObject[] boxPlaces;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        string nickname = PlayerPrefs.GetString("player");

        player = DatabaseDataAcces.getPlayerWithNickname(nickname);

        Debug.Log("ID " + player.id);

        LoadBoxes();

        LoadCar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadBoxes()
    {

        List<Box> boxes = DatabaseDataAcces.getPlayerBoxes(player.id);

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
            crate.gameObject.SetActive(true);

        }

    }

    private void LoadCar()
    {
    
        Car car = DatabaseDataAcces.getPlayersCar(player.id);

        CarRenderer carRenderer = gameObject.GetComponent<CarRenderer>();

        RenderedCar renderedCar = carRenderer.RenderCar(car, new Vector3(0,0), false, false, null, -1);

        renderedCar.chassis.transform.localScale = new Vector3(2f, 2f);

    }

}
