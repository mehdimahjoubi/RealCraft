using UnityEngine;
using System.Collections;

public class DeleteCubeButtonScript : ToolButtonScript
{
    protected override void OnButtonEnabled()
    {
        SelectedTool = ToolButtonScript.Tool.DELETE_CUBE;
    }

}
