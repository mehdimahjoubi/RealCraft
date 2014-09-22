using UnityEngine;
using System.Collections;

public class MenuButtonScript : AnimatedGUITextureButton {

    public GameObject menuElements;
    public InteractionScript interactionHandler;
    bool menuIsDisplayed = false;
    Color initialColor;

    protected new void Start()
    {
        base.Start();
        initialColor = guiTexture.color;
    }

    protected override void OnButtonUp()
    {
        menuIsDisplayed = !menuIsDisplayed;
        interactionHandler.enabled = !menuIsDisplayed;
        menuElements.SetActive(menuIsDisplayed);
        if (menuIsDisplayed)
            currentButtonColor = touchColor;
        else
            currentButtonColor = initialColor;
    }

}
