using UnityEngine;
using System.Collections;

public abstract class ToolButtonScript : RadioButton {

    public enum Tool
    {
        ADD_CUBE,
        DELETE_CUBE,
        APPLY_COLOR,
        APPLY_TEXTURE
    }

    public static Tool SelectedTool { get; protected set; }

}
