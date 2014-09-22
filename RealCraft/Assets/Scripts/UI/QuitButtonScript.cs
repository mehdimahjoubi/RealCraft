using UnityEngine;
using System.Collections;

public class QuitButtonScript : AnimatedGUITextureButton {

    protected override void OnButtonUp()
    {
        Application.Quit();
    }
}
