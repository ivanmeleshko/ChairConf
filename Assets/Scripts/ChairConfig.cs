using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChairConfig : MonoBehaviour
{
    
    [SerializeField]
    GameObject[] ChairBase;
    [SerializeField]
    GameObject Model;
    [SerializeField]
    Text textValues;
    List<GameObject> parts;
    
    const string VAL1 = "\n\n\n\n77\n\n105\n\n22\n\n3,5\n\n990";
    const string VAL2 = "\n\n\n\n77\n\n85\n\n22\n\n3,5\n\n990";

    public static ScrollType scrollType;
    public enum ScrollType { Color, Base, Mod };


    void Start()
    {
        if (UI.mobileSupport)
        {
            Model.transform.position = new Vector3(0, -1.22f, 8);
        }
        parts = new List<GameObject>();
        parts.Add(FindObject(GameObject.Find(Model.name), "T_pillowhight"));
        parts.Add(FindObject(GameObject.Find(Model.name), "T-sidewalls"));
        parts.Add(FindObject(GameObject.Find(Model.name), "T_beck"));
        parts.Add(FindObject(GameObject.Find(Model.name), "T_pillow"));       
        parts.Add(FindObject(GameObject.Find(Model.name), "T-sidewalls"));
        parts.Add(FindObject(GameObject.Find(Model.name), "T_subcontractor"));
        parts.Add(FindObject(GameObject.Find(Model.name), "T_subcontractor2.0"));
    }


    public void SetMaterial(Material mat)
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }


    public void ChangeMaterial(Material mat)
    {
        foreach (GameObject part in parts)
        {
            if (part != null)
            {
                if (!part.name.Equals("T_subcontractor"))
                    part.GetComponent<MeshRenderer>().material = mat;
                else
                    part.GetComponent<SkinnedMeshRenderer>().material = mat;
            }
        }

        scrollType = ScrollType.Color;
    }


    public void ChangeBase(string basePart)
    {
        foreach (GameObject g in ChairBase)
        {
            g.SetActive(g.name.Equals(basePart));
        }

        scrollType = ScrollType.Base;
    }


    public void EnablePillowhight(bool pillowhight)
    {
        parts[0].SetActive(pillowhight);
        scrollType = ScrollType.Mod;
        textValues.text = pillowhight ? VAL1 : VAL2;      
    }


    public GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

}
