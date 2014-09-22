using UnityEngine;
using System.Collections;

public class ModelScaleUpButtonScript : AnimatedGUITextureButton {

    public float scaleSpeed = 0.5f;
    public float maxScale = 20;
    
    protected override void OnButtonDown()
    {
        base.OnButtonDown();
        StartCoroutine("ScaleCoroutine");
    }

    protected override void OnButtonUp()
    {
        StopCoroutine("ScaleCoroutine");
    }

    IEnumerator ScaleCoroutine()
    {
        while (true)
        {
            UserDefinedTargetEventHandler_preview.model.localScale = Vector3.MoveTowards(UserDefinedTargetEventHandler_preview.model.localScale, UserDefinedTargetEventHandler_preview.model.localScale + Vector3.one, Time.deltaTime * scaleSpeed);
            if (UserDefinedTargetEventHandler_preview.model.localScale.x > maxScale)
                UserDefinedTargetEventHandler_preview.model.localScale = new Vector3(maxScale, maxScale, maxScale);
            yield return new WaitForEndOfFrame();
        }
    }

}
