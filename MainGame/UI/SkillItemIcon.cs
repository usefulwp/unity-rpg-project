using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SkillItemIcon : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler{
   
    private GameObject iconClone;
    private Transform canvasTrans;
    private int skillID;
	// Use this for initialization
	void Start () {
        canvasTrans = GameObject.Find("Canvas").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        skillID = GetComponentInParent<SkillItem>().ID;
        iconClone = GameObject.Instantiate(this.gameObject,canvasTrans);
        iconClone.GetComponent<Image>().raycastTarget = false;
       
    }

    public void OnDrag(PointerEventData eventData)
    {
       iconClone.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject.Destroy(iconClone);
        GameObject go = eventData.pointerCurrentRaycast.gameObject;
        if (go!=null)
        {
            if (go.tag == Tags.shortCutSlot)
            {
                    go.GetComponent<ShortCutSlot>().Set(skillID);
            }
            else if (go.tag == Tags.shortCutIcon)//技能替换
            {
                    go.transform.parent.GetComponent<ShortCutSlot>().Set(skillID);
            }
        }
    }

  
}
