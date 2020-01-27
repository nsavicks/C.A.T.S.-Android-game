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

    private Player player;
    private static float[] boxesPosition = { 4, 2, 0, -2 };

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

            GameObject boxObj = Instantiate(boxPrefab) as GameObject;
            boxObj.transform.position = new Vector3(-7, boxesPosition[i]);

            GameObject ngo = new GameObject("proba");
            ngo.transform.SetParent(canvas.transform, false);

            long now = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;

            long elapsed = (now - boxes[i].acquired) / (1000 * 60);
            string label = "OPEN";

            if (elapsed < 120) label = (120 - elapsed) + "m";

            Text text = ngo.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            text.text = label;
            text.rectTransform.anchoredPosition = new Vector3(-340.0f, boxesPosition[i] * 50.0f + -10.0f, 0f);

        }

    }

    private void LoadCar()
    {
    
        Car car = DatabaseDataAcces.getPlayersCar(player.id);

        CarRenderer carRenderer = gameObject.GetComponent<CarRenderer>();

        carRenderer.RenderCar(car, new Vector3(0,0), false, false, null, -1);

    }

}
