using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager :MonoBehaviour
{
    public static CursorManager instance; 
    public Texture2D cursorNormal;
    public Texture2D cursorNPCTalk;
    public Texture2D cursorAttack;
    public Texture2D cursorLockTarget;
    public Texture2D cursorPick;
    void Start()
    {
        instance = this;
    }
    public void SetCursorNormal()
    {
        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
    }
    public void SetCursorNPCTalk()
    {
        Cursor.SetCursor(cursorNPCTalk, Vector2.zero, CursorMode.Auto);
    }
    public void SetCursorAttack()
    {
        Cursor.SetCursor(cursorAttack, Vector2.zero, CursorMode.Auto);
    }
    public void SetCursorLockTarget()
    {
        Cursor.SetCursor(cursorLockTarget, Vector2.zero, CursorMode.Auto);
    }
    public void SetCursorPick()
    {
        Cursor.SetCursor(cursorPick, Vector2.zero, CursorMode.Auto);
    }
}
