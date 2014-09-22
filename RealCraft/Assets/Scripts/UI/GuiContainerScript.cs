using UnityEngine;
using System.Collections;

public class GuiContainerScript : MonoBehaviour {
    
    public void EnableGUI(bool enable)
    {
        transform.GetChild(0).gameObject.SetActive(enable);
    }

    public void DisableGUI(float disableTime)
    {
        EnableGUI(false);
        StartCoroutine(ReEnableGUI(disableTime));
    }

    IEnumerator ReEnableGUI(float timer)
    {
        yield return new WaitForSeconds(timer);
        EnableGUI(true);
    }

}
