using UnityEngine;
using System.Collections;

public class FloorCollisionScript : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        var cube = col.collider.gameObject.GetComponent<GravityToogleScript>();
        if (cube != null)
            cube.HandleFloorCollision();
    }
}
