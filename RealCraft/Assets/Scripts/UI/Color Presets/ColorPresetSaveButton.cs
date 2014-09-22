using UnityEngine;
using System.Collections;

public class ColorPresetSaveButton : AnimatedGUITextureButton {

    public ColorPresetsManager presetsManager;
    public NewColorPresetButton colorPickerToogle;
    
    protected override void OnButtonUp()
    {
        presetsManager.AddNewColor(ColorReceiver.SelectedColor);
        ResetButtonAnims();
        colorPickerToogle.ToggleColorPicker();
    }

}
