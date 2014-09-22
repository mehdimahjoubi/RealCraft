using UnityEngine;
using System.Collections;

public class ResetButtonScript : AnimatedGUITextureButton {

    protected override void OnButtonUp()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

}
