using UnityEngine;
using System.Collections;

public class SimpleLevelLoadButton : AnimatedGUITextureButton {

    public string levelName;

    protected override void OnButtonUp()
    {
        Application.LoadLevel(levelName);
    }

}
