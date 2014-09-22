using UnityEngine;
using System.Collections;

public class CancelTargetBuildingButtonScript : AnimatedGUITextureButton {

    public NotificationScript notice;

    protected override void OnButtonUp()
    {
        notice.HideNotification();
    }

}
