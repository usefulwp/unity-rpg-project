using UnityEngine;
using DG.Tweening;
public class BasePanel : MonoBehaviour {
    private bool isShow;
	// Use this for initialization
	public virtual void Start () {
        Tweener tweener = transform.DOLocalMove(Vector3.zero, 1f);
        tweener.SetAutoKill(false);
        tweener.Pause();
	}
	

    void Show()
    {
        transform.DOPlayForward();
    }
    void Hide()
    {
        transform.DOPlayBackwards();
    }
    public virtual void TransformState()
    {
        if (!isShow)
        {
            Show();
            isShow = true;
        }
        else 
        {
            Hide();
            isShow = false;
        }
    }
} 
