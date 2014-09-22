using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class SaveButtonScript : AnimatedGUITextureButton
{

    public NotificationScript notification;
    public GameObject loadButton;
    public SavedFilesBrowser fileBrowser;
    
    protected override void OnButtonUp()
    {
        var craftIO = UserDefinedTargetEventHandler_preview.model.gameObject.GetComponent<CraftSavingScript>();
        if (craftIO.FilePath == null || craftIO.FilePath.Trim() == "")
        {
            var filePath = SavedFilesBrowser.SavedFilesPath + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + SavedFilesBrowser.FileExtension;
            craftIO.SaveCraft(filePath);
            fileBrowser.ReInitializeFilesList();
            if (!loadButton.activeSelf)
                loadButton.SetActive(true);
            notification.ShowNotification("\n\n\nFile saved under : \n" + filePath, false, 0.45f, FontStyle.Bold);
        }
        else
        {
            notification.ShowNotification("\nOverride current file : \n'"+ Path.GetFileNameWithoutExtension(craftIO.FilePath) + "' ?", false, 1, FontStyle.Normal, true);
        }

    }

}
