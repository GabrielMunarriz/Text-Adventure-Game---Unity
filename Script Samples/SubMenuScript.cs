using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubMenuScript: MonoBehaviour
{

    public GameObject GameTextPanel;
    public GameObject MoveSubMenu;
    public GameObject BackpackSubMenu;
    public GameObject FlashlightSubMenu;
    public GameObject BatteriesSubMenu;
    public GameObject KeysSubMenu;

    public void OpenMoveSubMenu() // Opens submenu for movement choices (directions)
    {
        bool OpenMoveSubMenuisActive = MoveSubMenu.activeSelf;

        BackpackSubMenu.SetActive(false);
        FlashlightSubMenu.SetActive(false);
        BatteriesSubMenu.SetActive(false);
        KeysSubMenu.SetActive(false);

        MoveSubMenu.SetActive(!OpenMoveSubMenuisActive); 

        //GameTextPanel.SetActive(false);
    }

    public void OpenBackpackSubMenu() // Opens submenu for backpack items
    {
        bool OpenBackpackSubMenuisActive = BackpackSubMenu.activeSelf;

        MoveSubMenu.SetActive(false);
        FlashlightSubMenu.SetActive(false);
        BatteriesSubMenu.SetActive(false);
        KeysSubMenu.SetActive(false);

        BackpackSubMenu.SetActive(!OpenBackpackSubMenuisActive);
        
        //GameTextPanel.SetActive(false);
    }

    public void OpenFlashlightSubMenu()
    {
        bool OpenFlashlightSubMenuisActive = FlashlightSubMenu.activeSelf;

        MoveSubMenu.SetActive(false);
        BatteriesSubMenu.SetActive(false);
        KeysSubMenu.SetActive(false);

        FlashlightSubMenu.SetActive(!OpenFlashlightSubMenuisActive);
        //GameTextPanel.SetActive(false);
    }

    public void OpenBatteriesSubMenu()
    {
        bool OpenBatteriesSubMenuisActive = BatteriesSubMenu.activeSelf;

        MoveSubMenu.SetActive(false);
        FlashlightSubMenu.SetActive(false);
        KeysSubMenu.SetActive(false);

        BatteriesSubMenu.SetActive(!OpenBatteriesSubMenuisActive);
        //GameTextPanel.SetActive(false);
    }

    public void OpenKeysSubMenu()
    {
        bool OpenKeysSubMenuisActive = KeysSubMenu.activeSelf;

        MoveSubMenu.SetActive(false);
        FlashlightSubMenu.SetActive(false);
        BatteriesSubMenu.SetActive(false);

        KeysSubMenu.SetActive(!OpenKeysSubMenuisActive);
        //GameTextPanel.SetActive(false);
    }
}
