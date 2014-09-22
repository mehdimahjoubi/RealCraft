using UnityEngine;
using System.Collections;

public class ColorPresetsBrowsingButton : AnimatedGUITextureButton {

    public bool ReverseBrowsing { get; set; }
    public ColorPresetsManager PresetsManage { get; set; }

    protected override void OnButtonUp()
    {
        if (ReverseBrowsing)
            PresetsManage.DisplayPreviousPage();
        else
            PresetsManage.DisplayNextPage();
    }

}
