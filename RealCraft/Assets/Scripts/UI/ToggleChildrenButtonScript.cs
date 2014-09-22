using UnityEngine;
using System.Collections;

public class ToggleChildrenButtonScript : AnimatedGUITextureButton {

    GameObject[] children;

    protected void Start()
    {
        base.Start();
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++ )
            children[i] = transform.GetChild(i).gameObject;
    }

    protected override void OnButtonUp()
    {
        foreach (var t in children)
        {
            t.SetActive(!t.activeSelf);
        }
    }

}