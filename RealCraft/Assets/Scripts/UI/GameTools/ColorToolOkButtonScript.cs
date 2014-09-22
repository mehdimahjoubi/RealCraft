using UnityEngine;
using System.Collections;

public class ColorToolOkButtonScript : AnimatedGUITextureButton {

    public CubeColorButtonScript colorButton;
    
    protected override void OnButtonUp()
    {
        ResetButtonAnims();
        colorButton.CloseColorGui();
    }

}
