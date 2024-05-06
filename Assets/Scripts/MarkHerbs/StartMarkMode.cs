using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMarkMode : MonoBehaviour
{
    public Texture2D cursorTextureMark;
    public GameObject clickMask;

    private FindMarkScripts penBtn;
    private Transform penOriginalParent;
    private int penOriginalSiblingIndex;

    private GameObject backPack;
    private Transform backPackOriginalParent;
    private int backPackOriginalSiblingIndex;

    private CommonTips ct;
    public string failMarkText;

    private void Awake()
    {
        clickMask = transform.Find("ClickMask").gameObject;
        clickMask.SetActive(false);
        penBtn = GetComponent<FindMarkScripts>();
        ct = FindObjectOfType<CommonTips>();
    }

    private void Start()
    {
        penOriginalParent = penBtn.penButton.transform.parent;
        penOriginalSiblingIndex = penBtn.penButton.transform.GetSiblingIndex();
    }

    public void ChangeToMarkMode()
    {
        if(!MarkState.isOnMarkMode)
        {
            if(DataManager.Instance.SignTimes > 0)
            {
                Cursor.SetCursor(cursorTextureMark, new Vector2(5,5), CursorMode.Auto);
                MarkState.isOnMarkMode = true;

                clickMask.SetActive(true);
                penBtn.penButton.transform.SetParent(clickMask.transform.parent);
                // 将标记按钮置于遮罩之上
                penBtn.penButton.transform.SetAsLastSibling();

                GetBackPackPanel();
            }
            else
            {
                ct.GetTipsText(failMarkText);
            }
            

        }
        else
        {
            CancelMarkMode();
        }
        
    }

    public void CancelMarkMode()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        MarkState.isOnMarkMode = false;
        clickMask.SetActive(false);
        penBtn.penButton.transform.SetParent(penOriginalParent);
        penBtn.penButton.transform.SetSiblingIndex(penOriginalSiblingIndex);

        if(backPack != null)
        {
            backPack.transform.SetParent(backPackOriginalParent);
            backPack.transform.SetSiblingIndex(backPackOriginalSiblingIndex);
        }

    }

    //获取药材列表并提高层级
    public void GetBackPackPanel()
    {
        backPack = GameObject.Find("BackpackPanel");

        if(backPack != null)
        {
            backPackOriginalParent = backPack.transform.parent;
            backPackOriginalSiblingIndex = backPack.transform.GetSiblingIndex();

            backPack.transform.SetParent(clickMask.transform.parent);
            backPack.transform.SetAsLastSibling();
        }

        
    }




}
