using UnityEngine;
using System.Collections;

public class CubeColorButtonScript : ToolButtonScript
{
    public GameObject colorGUI;
    public GameObject menuGUI;
    public InteractionScript interactionBehaviour;
    
    protected override void OnButtonEnabled()
    {
        SelectedTool = ToolButtonScript.Tool.APPLY_COLOR;
        colorGUI.SetActive(!colorGUI.activeSelf);
        menuGUI.SetActive(!colorGUI.activeSelf);
        interactionBehaviour.enabled = !colorGUI.activeSelf;
    }

    protected override void OnButtonDisabled()
    {
        CloseColorGui();
    }

    public void CloseColorGui()
    {
        colorGUI.SetActive(false);
        menuGUI.SetActive(true);
        interactionBehaviour.enabled = true;
    }

}
