using UnityEngine;
using System.Collections;

public class ModelColorButtonScript : AnimatedGUITextureButton {

    public bool reverseBtn;
    public ModelColorButtonScript otherButton;
    static Color[] availableColors;
    static int currentColorIndex = 0;
    static bool availableColorsInitialized = false;

    protected new void Start()
    {
        base.Start();
        if (!availableColorsInitialized)
        {
            availableColors = new Color[] { UserDefinedTargetEventHandler_preview.model.renderer.material.color, Color.white, Color.yellow, Color.red, Color.magenta, Color.grey, Color.green, Color.gray, Color.cyan, Color.blue };
            availableColorsInitialized = true;
        }
    }
    
    protected override void OnButtonUp()
    {
        int newIndex = 0;
        if (reverseBtn)
            newIndex = currentColorIndex - 1;
        else
            newIndex = currentColorIndex + 1;
        if ((!reverseBtn && newIndex < availableColors.Length) || (reverseBtn && newIndex >= 0))
        {
            otherButton.EnableButton(true);
            UserDefinedTargetEventHandler_preview.model.renderer.material.color = availableColors[newIndex];
            currentColorIndex = newIndex;
        }
        if ((!reverseBtn && currentColorIndex == availableColors.Length - 1) || (reverseBtn && currentColorIndex == 0))
            EnableButton(false);
    }

}
