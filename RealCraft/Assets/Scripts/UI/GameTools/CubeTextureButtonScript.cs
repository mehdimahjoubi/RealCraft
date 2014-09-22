using UnityEngine;
using System.Collections;

public class CubeTextureButtonScript : ToolButtonScript
{

    public GameObject texturesGUI;
    public GameObject menuGUI;
    public InteractionScript interactionBehaviour;

    protected override void OnButtonEnabled()
    {
        SelectedTool = ToolButtonScript.Tool.APPLY_TEXTURE;
        texturesGUI.SetActive(!texturesGUI.activeSelf);
        menuGUI.SetActive(!texturesGUI.activeSelf);
        interactionBehaviour.enabled = !texturesGUI.activeSelf;
    }

    protected override void OnButtonDisabled()
    {
        CloseTexturesGui();
    }

    public void CloseTexturesGui()
    {
        texturesGUI.SetActive(false);
        menuGUI.SetActive(true);
        interactionBehaviour.enabled = true;
    }

}
