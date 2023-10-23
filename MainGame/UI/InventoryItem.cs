using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,ICanvasRaycastFilter,IPointerClickHandler
{
    private Transform canvasTransform;
    private int id;
    private int num;
    private Image iconImage;
    private Text numLabel;
    private Transform nowParent;
    private bool isDrag;
    private bool isRaycastLocationValid=true;
    private PlayerStatus playerStatus;
    public int GetID()
    {
        return id;
    }
	// Use this for initialization
	void Awake() {
        iconImage = this.GetComponent<Image>();
        numLabel = transform.Find("Text").GetComponent<Text>();
        canvasTransform = GameObject.Find("Canvas").transform;
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
	}
	
    public void SetNum(int count)
    {
        this.num += count;
        numLabel.text = this.num.ToString();
    }
 
    public void SetInfo(int id,int num)
    {
        this.id = id;
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        iconImage.sprite = Resources.Load<Sprite>("Icon/" + info.iconName);
        this.num = num;
        numLabel.text= num.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
        if (!isDrag)
        { 
                InventoryDesPanel.instance.Show(id);
        }     
    }

    public void OnPointerExit(PointerEventData eventData)
    {    
            InventoryDesPanel.instance.Hide();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        nowParent = transform.parent;
        transform.SetParent(canvasTransform);
        isRaycastLocationValid = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        InventoryDesPanel.instance.Hide();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject go=eventData.pointerCurrentRaycast.gameObject;
        if (go != null)
        {
            if (go.name.Equals(nowParent.name))
            {
                SetParentAndPosition(transform,nowParent);
            }
            if (go.tag.Equals(Tags.inventorySlot))
            {
                SetParentAndPosition(transform, go.transform);
                InventorySlot slot1 = nowParent.GetComponent<InventorySlot>();
                InventorySlot slot2 = go.GetComponent<InventorySlot>();
                slot2.id = slot1.id;
                slot1.id = 0;
            }
            else if (go.tag.Equals(Tags.inventoryItem))
            {
                InventorySlot slot1 = nowParent.GetComponent<InventorySlot>();
                InventorySlot slot2 = go.transform.parent.GetComponent<InventorySlot>();
                int tempID = slot1.id;
                slot1.id = slot2.id;
                slot2.id = tempID;
                SetParentAndPosition(transform, go.transform.parent);
                SetParentAndPosition(go.transform, nowParent);
            }
            else if(go.tag.Equals(Tags.shortCutSlot))
            {
                go.GetComponent<ShortCutSlot>().SetDrug(id);
                SetParentAndPosition(transform, nowParent);
            }
            else if (go.tag.Equals(Tags.shortCutIcon))
            {
                go.GetComponentInParent<ShortCutSlot>().SetDrug(id);
                SetParentAndPosition(transform, nowParent);
            }
            else
            {
                SetParentAndPosition(transform, nowParent);
            }
        }
        else
        {
            Debug.Log("丢弃物品");
        }
       isRaycastLocationValid = true;
       isDrag = false;
    }
    void SetParentAndPosition(Transform child,Transform parent)
    {
        child.SetParent(parent);
        child.position = parent.position;
    }
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return isRaycastLocationValid;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (EquipPanel.instance.Wear(id))
        {
            JudgeEquipNum();
            playerStatus.GetTotalAttack();
            playerStatus.GetTotalDefence();
            playerStatus.GetTotalSpeed();
        }
    }
   public  void JudgeEquipNum(int num=1)
    {
        if (this.num >= num)
        {
            this.num -= num;
            if (this.num == 0)
            {
                transform.parent.GetComponent<InventorySlot>().ClearInfo();
                InventoryDesPanel.instance.Hide();
                GameObject.Destroy(this.gameObject);
            }
            numLabel.text = this.num.ToString();
        }
    }
   public int JudgeDrugNum(int num = 1)
   {
       if (this.num >= num)
       {
           this.num -= num;
           if (this.num == 0)
           {
               transform.parent.GetComponent<InventorySlot>().ClearInfo();
               InventoryDesPanel.instance.Hide();
               GameObject.Destroy(this.gameObject);             
           }
           numLabel.text = this.num.ToString();
       }
       return this.num;
   }
}
