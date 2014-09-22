using UnityEngine;
using System.Collections;

public class LoadFileButton : AnimatedGUITextureButton {

    public GameObject rootGui;
    public GameObject fileBrowsingGui; 
    
    protected override void OnButtonUp()
    {
        var fileName = transform.GetChild(0).guiText.text;
        var craftIO = UserDefinedTargetEventHandler_preview.model.gameObject.GetComponent<CraftSavingScript>();
        craftIO.LoadCraft(SavedFilesBrowser.SavedFilesPath + "/" + fileName + "." + SavedFilesBrowser.FileExtension);
        rootGui.SetActive(true);
        ResetButtonAnims();
        fileBrowsingGui.SetActive(false);
    }

}
