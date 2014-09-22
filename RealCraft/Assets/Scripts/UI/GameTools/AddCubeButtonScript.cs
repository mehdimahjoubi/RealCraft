using UnityEngine;
using System.Collections;

public class AddCubeButtonScript : ToolButtonScript
{
    protected void Start()
    {
        base.Start();
        EnableButton();
    }

    protected override void OnButtonEnabled()
    {
        SelectedTool = ToolButtonScript.Tool.ADD_CUBE;
    }

    void OnLevelWasLoaded()
    {
        EnableButton();
    }

}
