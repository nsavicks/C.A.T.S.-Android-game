using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyCar : MonoBehaviour
{
    public GameObject singlePrefab;
    public GameObject content;
    public Text powerText, energyText, healthText;

    public GameObject attack1Place, attack2Place, forkliftPlace, backWheelPlace, frontWheelPlace, chassisPlace;

    public HasCarPart selected = null;

    private Player player;
    private Sprite oneStar, twoStars, threeStars;

    private List<HasCarPart> parts;

    public Car playerCar;

    private Sprite bladeSprite, chainsawSprite, chassis1Sprite, chassis2Sprite, chassis3Sprite, forkliftSprite, rocketSprite, stingerSprite, wheel1Sprite, wheel2Sprite;

    private CarRenderer carRenderer;
    private RenderedCar renderedCar = null;

    // Start is called before the first frame update
    void Start()
    {
        oneStar = Resources.Load<Sprite>("one-star");
        twoStars = Resources.Load<Sprite>("two-stars");
        threeStars = Resources.Load<Sprite>("three-stars");
        bladeSprite = Resources.Load<Sprite>("blade");
        chainsawSprite = Resources.Load<Sprite>("chainsaw");
        chassis1Sprite =  Resources.Load<Sprite>("chassis_1");
        chassis2Sprite = Resources.Load<Sprite>("chassis_2");
        chassis3Sprite = Resources.Load<Sprite>("chassis_3");
        forkliftSprite = Resources.Load<Sprite>("forklift");
        rocketSprite = Resources.Load<Sprite>("rocket");
        stingerSprite = Resources.Load<Sprite>("stinger");
        wheel1Sprite = Resources.Load<Sprite>("wheel_1");
        wheel2Sprite = Resources.Load<Sprite>("wheel_2");

        string nickname = PlayerPrefs.GetString("player");

        player = DatabaseDataAcces.getPlayerWithNickname(nickname);

        ItemDropHandler dropHandler;

        dropHandler = attack1Place.AddComponent<ItemDropHandler>();
        dropHandler.modifyCar = this;
        dropHandler.type = 4;
        dropHandler.subtype = 1;

        dropHandler = attack2Place.AddComponent<ItemDropHandler>();
        dropHandler.modifyCar = this;
        dropHandler.type = 4;
        dropHandler.subtype = 2;

        dropHandler = forkliftPlace.AddComponent<ItemDropHandler>();
        dropHandler.modifyCar = this;
        dropHandler.type = 3;

        dropHandler = frontWheelPlace.AddComponent<ItemDropHandler>();
        dropHandler.modifyCar = this;
        dropHandler.type = 2;
        dropHandler.subtype = 1;

        dropHandler = backWheelPlace.AddComponent<ItemDropHandler>();
        dropHandler.modifyCar = this;
        dropHandler.type = 2;
        dropHandler.subtype = 2;

        dropHandler = chassisPlace.AddComponent<ItemDropHandler>();
        dropHandler.modifyCar = this;
        dropHandler.type = 1;

        LoadItems();

        LoadCar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadItems()
    {

        foreach(Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        parts = DatabaseDataAcces.getCarPartsForUser(player.id);

        foreach(HasCarPart carItem in parts)
        {
            GameObject single = Instantiate(singlePrefab);

            Transform stars = single.transform.Find("stars");
            Transform item = single.transform.Find("item");
            Transform attack = single.transform.Find("attackText");
            Transform health = single.transform.Find("healthText");
            Transform energy = single.transform.Find("energyText");

            item.GetComponent<Image>().sprite = GetItemSprite(carItem.carPart.id);

            if (carItem.stars == 1) stars.GetComponent<Image>().sprite = oneStar;
            else if (carItem.stars == 2) stars.GetComponent<Image>().sprite = twoStars;
            else stars.GetComponent<Image>().sprite = threeStars;

            attack.gameObject.GetComponent<Text>().text = carItem.carPart.power * carItem.stars + "";
            health.gameObject.GetComponent<Text>().text = carItem.carPart.health * carItem.stars + "";
            energy.gameObject.GetComponent<Text>().text = carItem.carPart.energy * carItem.stars + "";

            single.transform.SetParent(content.transform, false);

            ItemDragHandler dragHandler = item.gameObject.AddComponent<ItemDragHandler>();
            dragHandler.hasCarPart = carItem;
            dragHandler.modifyCar = this;

        }
    }

    private Sprite GetItemSprite(int id)
    {
        switch (id)
        {
            case 1:
                return chassis1Sprite;
            case 2:
                return chassis2Sprite;
            case 3:
                return chassis3Sprite;
            case 4:
                return wheel1Sprite;
            case 5:
                return wheel2Sprite;
            case 6:
                return forkliftSprite;
            case 7:
                return bladeSprite;
            case 8:
                return chainsawSprite;
            case 9:
                return rocketSprite;
            case 10:
                return stingerSprite;
        }

        return null;
    }

    public void LoadCar()
    {

        if (renderedCar != null)
        {
            Destroy(renderedCar.chassis);
            renderedCar = null;
        }

        playerCar = DatabaseDataAcces.getPlayersCar(player.id);

        powerText.text = playerCar.GetCarPower() + "";
        energyText.text = playerCar.GetCarEnergyLeft() + "";
        healthText.text = playerCar.GetCarHealth() + "";

        carRenderer = gameObject.GetComponent<CarRenderer>();

        renderedCar = carRenderer.RenderCar(playerCar, new Vector3(0f, -1.5f), false, false, null, -1, false);

        renderedCar.chassis.transform.localScale = new Vector3(2f, 2f);

        if (renderedCar.attack1 != null)
        {
            RemoveItemOnClick removeItem = renderedCar.attack1.AddComponent<RemoveItemOnClick>();
            removeItem.item = renderedCar.attack1;
            removeItem.type = 1;
            removeItem.modifyCar = this;

            renderedCar.attack1.AddComponent <PolygonCollider2D>();

        }

        if (renderedCar.attack2 != null)
        {
            RemoveItemOnClick removeItem = renderedCar.attack2.AddComponent<RemoveItemOnClick>();
            removeItem.item = renderedCar.attack2;
            removeItem.type = 2;
            removeItem.modifyCar = this;
            renderedCar.attack2.AddComponent<PolygonCollider2D>();
        }

        if (renderedCar.forklift != null)
        {
            RemoveItemOnClick removeItem = renderedCar.forklift.AddComponent<RemoveItemOnClick>();
            removeItem.item = renderedCar.forklift;
            removeItem.type = 3;
            removeItem.modifyCar = this;
            renderedCar.forklift.AddComponent<PolygonCollider2D>();
        }

        if (renderedCar.frontWheel != null)
        {
            RemoveItemOnClick removeItem = renderedCar.frontWheel.AddComponent<RemoveItemOnClick>();
            removeItem.item = renderedCar.frontWheel;
            removeItem.type = 4;
            removeItem.modifyCar = this;
            renderedCar.frontWheel.AddComponent<PolygonCollider2D>();
        }

        if (renderedCar.backWheel != null)
        {
            RemoveItemOnClick removeItem = renderedCar.backWheel.AddComponent<RemoveItemOnClick>();
            removeItem.item = renderedCar.backWheel;
            removeItem.type = 5;
            removeItem.modifyCar = this;
            renderedCar.backWheel.AddComponent<PolygonCollider2D>();
        }

        LoadPlaces(playerCar.chassis);
    }

    public void SelectItem(HasCarPart hasCarPart)
    {
        selected = hasCarPart;

        switch (selected.carPart.type)
        {
            case 1:
                chassisPlace.SetActive(true);
                break;
            case 2:
                frontWheelPlace.SetActive(true);
                backWheelPlace.SetActive(true);
                break;
            case 3:
                forkliftPlace.SetActive(true);
                break;
            case 4:
                attack1Place.SetActive(true);

                if (selected.carPart.id == 9 && playerCar.chassis.id == 3) attack2Place.SetActive(true);
                break;
        }
    }

    public void UnselectItem()
    {
        if (selected == null) return;

        switch (selected.carPart.type)
        {
            case 1:
                chassisPlace.SetActive(false);
                break;
            case 2:
                frontWheelPlace.SetActive(false);
                backWheelPlace.SetActive(false);
                break;
            case 3:
                forkliftPlace.SetActive(false);
                break;
            case 4:
                attack1Place.SetActive(false);

                if (selected.carPart.id == 9) attack2Place.SetActive(false);
                break;
        }

        selected = null;

    }

    private void LoadPlaces(CarPart chassis)
    {
        switch (chassis.id)
        {
            case 1:

                attack1Place.transform.localPosition = new Vector3(149f, -262f);
                forkliftPlace.transform.localPosition = new Vector3(84f, -402f);
                frontWheelPlace.transform.localPosition = new Vector3(133f, -558f);
                backWheelPlace.transform.localPosition = new Vector3(-132f, -558f);
                chassisPlace.transform.localPosition = new Vector3(6f, -273f);

                break;
            case 2:

                attack1Place.transform.localPosition = new Vector3(281f, -148f);
                forkliftPlace.transform.localPosition = new Vector3(195f, -234f);
                frontWheelPlace.transform.localPosition = new Vector3(133f, -351f);
                backWheelPlace.transform.localPosition = new Vector3(-249f, -351f);
                chassisPlace.transform.localPosition = new Vector3(6f, -211f);

                break;
            case 3:

                attack1Place.transform.localPosition = new Vector3(322f, -190f);
                attack2Place.transform.localPosition = new Vector3(-69f, -190f);
                forkliftPlace.transform.localPosition = new Vector3(250f, -261f);
                frontWheelPlace.transform.localPosition = new Vector3(309f, -373f);
                backWheelPlace.transform.localPosition = new Vector3(-40f, -373f);
                chassisPlace.transform.localPosition = new Vector3(101f, -211f);

                break;
        }
    }

    public void RemoveItem(GameObject item, int type)
    {
        Destroy(item);
        DatabaseDataAcces.removeItem(player.id, type);

        LoadItems();
        LoadCar();
    }
}
