using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EquipItem : MonoBehaviour,IPointerClickHandler
{
    public int ID {get;set; }
    private Image iconImage;
    void Awake()
    {
        iconImage = this.GetComponent<Image>();
    }
    public void SetEquipInfo(ObjectInfo info)
    {
        ID = info.id;
        iconImage.sprite=Resources.Load<Sprite>("Icon/"+info.iconName);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EquipPanel.instance.TakeOffEquip(ID, this.gameObject);      
    }
}
