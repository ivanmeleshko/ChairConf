using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    Canvas canvas, canvasMobile;
    [SerializeField]
    GameObject[] scrolls;
    [SerializeField]
    Text[] textScrolls;
    [SerializeField]
    Image[] outlinesColor, outlinesBase, outlinesMod;
    const string COLORMAGHEX = "#E6194D";
    const string COLORMAGHALFTRANSP = "#E6194D96";
    const string COLORTRANSP = "#00000000";
    public static bool mobileSupport;


    private void Awake()
    {
        mobileSupport = !(SystemInfo.operatingSystem.Contains("Windows") || SystemInfo.operatingSystem.Contains("Mac"));
        canvasMobile.gameObject.SetActive(mobileSupport);
        canvas.gameObject.SetActive(!mobileSupport);
    }


    public void SetActiveScroll(string scroll)
    {
        foreach (GameObject g in scrolls)
        {
            g.SetActive(g.name.Equals(scroll));
        }
    }


    public void ChangeTextColor(string textScroll)
    {
        foreach (Text t in textScrolls)
        {
            if (t.name.Equals(textScroll))
            {
                Color color;
                if (ColorUtility.TryParseHtmlString(COLORMAGHEX, out color))
                {
                    t.color = color;
                }
            }
            else
            {
                t.color = Color.black;
            }
        }
    }


    public void SetBorder(Image imgOutline)
    {
        switch (ChairConfig.scrollType)
        {
            case ChairConfig.ScrollType.Color:
                Border(imgOutline, outlinesColor);
                break;
            case ChairConfig.ScrollType.Base:
                Border(imgOutline, outlinesBase);
                break;
            case ChairConfig.ScrollType.Mod:
                Border(imgOutline, outlinesMod);
                break;
        }
    }


    private void Border(Image imgOutline, Image[] outline)
    {
        foreach (Image img in outline)
        {
            if (img.name.Equals(imgOutline.name))
            {
                Color color;
                if (img.sprite != null)
                {
                    if (ColorUtility.TryParseHtmlString(COLORMAGHALFTRANSP, out color))
                    {
                        img.color = color;
                    }
                }
                else
                {
                    if (ColorUtility.TryParseHtmlString(COLORMAGHEX, out color))
                    {
                        img.color = color;
                    }
                }
            }
            else
            {
                Color color;
                if (ColorUtility.TryParseHtmlString(COLORTRANSP, out color))
                {
                    img.color = color;
                }
            }
        }
    }


    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
