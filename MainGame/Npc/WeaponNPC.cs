using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNPC : NPC {
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShopWeaponPanel.instance.TransformState();
        }
    }
}
