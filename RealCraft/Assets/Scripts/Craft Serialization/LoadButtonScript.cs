using UnityEngine;
using System.Collections;

public class LoadButtonScript : AnimatedGUITextureButton
{

    public GameObject fileBrowserRoot;
    public GameObject rootGui;
    
    protected override void OnButtonUp()
    {
        fileBrowserRoot.SetActive(!fileBrowserRoot.activeSelf);
        ResetButtonAnims();
        StopAllCoroutines();
        rootGui.SetActive(false);
    }

}
