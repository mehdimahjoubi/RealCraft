using UnityEngine;
using System.Collections;

public class OverrideExistingFileButton : AnimatedGUITextureButton {

    public NotificationScript notification;

    protected override void OnButtonUp()
    {
        var craftIO = UserDefinedTargetEventHandler_preview.model.gameObject.GetComponent<CraftSavingScript>();
        craftIO.SaveCraft();
        notification.HideNotification();
    }

}
