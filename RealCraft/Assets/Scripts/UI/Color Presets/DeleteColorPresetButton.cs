using UnityEngine;
using System.Collections;

public class DeleteColorPresetButton : AnimatedGUITextureButton {

    public ColorPresetsManager presetsManager;
    
    protected override void OnButtonUp()
    {
        presetsManager.DeleteSelectedColor();
    }

}
