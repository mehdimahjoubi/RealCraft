using UnityEngine;
using System.Collections;

public class FileBrowserCloseButton : AnimatedGUITextureButton {

    public GameObject rootGui;
    
    protected override void OnButtonUp()
    {
        rootGui.SetActive(true);
        ResetButtonAnims();
        transform.parent.gameObject.SetActive(false);
    }

}
