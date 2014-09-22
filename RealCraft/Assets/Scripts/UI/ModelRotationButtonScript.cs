using UnityEngine;
using System.Collections;

public class ModelRotationButtonScript : AnimatedGUITextureButton
{
    public float rotationSpeed = 50;

    protected override void OnButtonDown()
    {
        base.OnButtonDown();
        StartCoroutine("RotateCoroutine");
    }

    protected override void OnButtonUp()
    {
        StopCoroutine("RotateCoroutine");
    }

    IEnumerator RotateCoroutine()
    {
        while (true)
        {
            UserDefinedTargetEventHandler_preview.model.Rotate(UserDefinedTargetEventHandler_preview.model.up, rotationSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

}
