using UnityEngine;
using System.Collections;

public class NewColorPresetButton : AnimatedGUITextureButton {

    public GameObject colorPickerGUI;
    public GameObject colorPresetsGUI;
    Color initialColor;

    protected new void Start()
    {
        base.Start();
        initialColor = guiTexture.color;
    }

    protected override void OnButtonUp()
    {
        colorPickerGUI.SetActive(!colorPickerGUI.activeSelf);
        colorPresetsGUI.SetActive(!colorPresetsGUI.activeSelf);
        if (colorPickerGUI.activeSelf)
            currentButtonColor = touchColor;
        else
            currentButtonColor = initialColor;
    }

    public void ToggleColorPicker()
    {
        colorPickerGUI.SetActive(!colorPickerGUI.activeSelf);
        colorPresetsGUI.SetActive(!colorPresetsGUI.activeSelf);
        if (colorPresetsGUI.activeSelf)
        {
            guiTexture.color = initialColor;
            currentButtonColor = initialColor;
        }
    }

}
