using UnityEngine;
using System.Collections;

public class ConfirmTargetBuildingButtonScript : AnimatedGUITextureButton {

    public TargetBuildingButtonScriptScript targetBuildBtn;
    public NotificationScript notice;
    
    protected override void OnButtonUp()
    {
        targetBuildBtn.StartBuildingTarget();
        notice.HideNotification();
    }
}
