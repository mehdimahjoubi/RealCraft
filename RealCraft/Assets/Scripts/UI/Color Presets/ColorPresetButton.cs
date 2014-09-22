using UnityEngine;
using System.Collections;

public class ColorPresetButton : MonoBehaviour
{
    public int ButtonIndex { get; set; }
    public ColorPresetsManager PresetsManager { get; set; }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && guiTexture.HitTest(touch.position))
            {
                PresetsManager.ChangeSelectedPresetColor(ButtonIndex);
            }
        }
        else if (Input.GetMouseButtonDown(0) && guiTexture.HitTest(Input.mousePosition))
        {
            PresetsManager.ChangeSelectedPresetColor(ButtonIndex);
        }
    }

}
