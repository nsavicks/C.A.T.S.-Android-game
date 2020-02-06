using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{

    public ModifyCar modifyCar;
    public int type;
    public int subtype;

    public void OnDrop(PointerEventData eventData)
    {

        if (modifyCar.selected == null) return;

        if (type != modifyCar.selected.carPart.type) return;

        if (CheckEnoughEnergy())
        {
            DatabaseDataAcces.changePart(modifyCar.selected, subtype);

            modifyCar.LoadItems();
            modifyCar.LoadCar();

            modifyCar.UnselectItem();
        }
       
    }

    private bool CheckEnoughEnergy()
    {
        CarPart old;
        int oldStars;
        bool enough = false;

        switch (type)
        {
            case 1:
                old = modifyCar.playerCar.chassis;
                oldStars = modifyCar.playerCar.chassisStars;

                modifyCar.playerCar.chassis = modifyCar.selected.carPart;
                modifyCar.playerCar.chassisStars = modifyCar.selected.stars;

                enough = modifyCar.playerCar.GetCarEnergyLeft() >= 0;

                modifyCar.playerCar.chassis = old;
                modifyCar.playerCar.chassisStars = oldStars;

                break;
            case 2:
                if (subtype == 1)
                {
                    old = modifyCar.playerCar.frontWheel;
                    oldStars = modifyCar.playerCar.frontWheelStars;

                    modifyCar.playerCar.frontWheel = modifyCar.selected.carPart;
                    modifyCar.playerCar.frontWheelStars = modifyCar.selected.stars;

                    enough = modifyCar.playerCar.GetCarEnergyLeft() >= 0;

                    modifyCar.playerCar.frontWheel = old;
                    modifyCar.playerCar.frontWheelStars = oldStars;
                }
                else
                {
                    old = modifyCar.playerCar.backWheel;
                    oldStars = modifyCar.playerCar.backWheelStars;

                    modifyCar.playerCar.backWheel = modifyCar.selected.carPart;
                    modifyCar.playerCar.backWheelStars = modifyCar.selected.stars;

                    enough = modifyCar.playerCar.GetCarEnergyLeft() >= 0;

                    modifyCar.playerCar.backWheel = old;
                    modifyCar.playerCar.backWheelStars = oldStars;
                }
                break;
            case 3:
                old = modifyCar.playerCar.forklift;
                oldStars = modifyCar.playerCar.forkliftStars;

                modifyCar.playerCar.forklift = modifyCar.selected.carPart;
                modifyCar.playerCar.forkliftStars = modifyCar.selected.stars;

                enough = modifyCar.playerCar.GetCarEnergyLeft() >= 0;

                modifyCar.playerCar.forklift = old;
                modifyCar.playerCar.forkliftStars = oldStars;
                break;
            case 4:
                if (subtype == 1)
                {
                    old = modifyCar.playerCar.attack1;
                    oldStars = modifyCar.playerCar.attack1Stars;

                    modifyCar.playerCar.attack1 = modifyCar.selected.carPart;
                    modifyCar.playerCar.attack1Stars = modifyCar.selected.stars;

                    enough = modifyCar.playerCar.GetCarEnergyLeft() >= 0;

                    modifyCar.playerCar.attack1 = old;
                    modifyCar.playerCar.attack1Stars = oldStars;
                }
                else
                {
                    old = modifyCar.playerCar.attack2;
                    oldStars = modifyCar.playerCar.attack2Stars;

                    modifyCar.playerCar.attack2 = modifyCar.selected.carPart;
                    modifyCar.playerCar.attack2Stars = modifyCar.selected.stars;

                    enough = modifyCar.playerCar.GetCarEnergyLeft() >= 0;

                    modifyCar.playerCar.attack2 = old;
                    modifyCar.playerCar.attack2Stars = oldStars;
                }
                break;
        }

        return enough;
    }
}
