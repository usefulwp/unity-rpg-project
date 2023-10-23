using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionUIManager : MonoBehaviour {
    private bool isHide = false;
    public void HidePanel()
    {
        if (!isHide)
        {
            transform.gameObject.SetActive(false);
            isHide = true;
        }
        else
        {
            transform.gameObject.SetActive(true);
            isHide = false;
        }
    }
}
