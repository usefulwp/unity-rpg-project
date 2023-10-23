using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    void OnMouseEnter()
    {
            CursorManager.instance.SetCursorNPCTalk();    
    }
    void OnMouseExit()
    {
        CursorManager.instance.SetCursorNormal();
    }
    
}
