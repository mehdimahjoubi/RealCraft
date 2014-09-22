using UnityEngine;
using System.Collections;

public class AutoDestructionScript : MonoBehaviour
{

    public float timer = 1;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

}
