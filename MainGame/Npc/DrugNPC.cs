using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugNPC : NPC {

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShopDrugPanel.instance.TransformState();
        }
    }
 
}
