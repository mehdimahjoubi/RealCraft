using UnityEngine;
using System.Collections;
using System;

public class NewFileButton : AnimatedGUITextureButton {

    public NotificationScript notification;
    public SavedFilesBrowser fileBrowser;

    protected override void OnButtonUp()
    {
        var craftIO = UserDefinedTargetEventHandler_preview.model.gameObject.GetComponent<CraftSavingScript>();
        var filePath = SavedFilesBrowser.SavedFilesPath + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + SavedFilesBrowser.FileExtension;
        craftIO.SaveCraft(filePath);
        fileBrowser.ReInitializeFilesList();
        notification.HideButtons();
        notification.ShowNotification("\n\n\nFile saved under : \n" + filePath, false, 0.45f, FontStyle.Bold);
    }

}
