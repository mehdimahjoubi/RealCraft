using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityToogleScript : MonoBehaviour
{

    static List<GravityToogleScript> cubes = new List<GravityToogleScript>();
    static float sizeError = 0.1f;
    static Transform floor;
    static float floarScaleBoost = 0;
    static int idGen = 0;
    List<GravityToogleScript> neighboors = new List<GravityToogleScript>();
    bool isParentNeighboor = false;
    int id;

    IEnumerator Start()
    {
        id = idGen++;
        if (floor == null)
            floor = UserDefinedTargetEventHandler_preview.model.FindChild("invisible plane");
        yield return new WaitForSeconds(0.5f);
        var cubeSize = UserDefinedTargetEventHandler_preview.model.localScale.x * UserDefinedTargetEventHandler_preview.model.parent.localScale.x;
        if (Mathf.Abs(transform.localPosition.x) > floarScaleBoost)
        {
            floarScaleBoost = Mathf.Abs(transform.localPosition.x);
            floor.localScale += new Vector3(15 * cubeSize, 0, 15 * cubeSize);
        }
        if (Mathf.Abs(transform.localPosition.z) > floarScaleBoost)
        {
            floarScaleBoost = Mathf.Abs(transform.localPosition.z);
            floor.localScale += new Vector3(15 * cubeSize, 0, 15 * cubeSize);
        }
        var minCubeSize = cubeSize - sizeError;
        var maxCubeSize = cubeSize + sizeError;
        var distanceFromRoot = Vector3.Distance(UserDefinedTargetEventHandler_preview.model.position, transform.position);
        if (distanceFromRoot >= minCubeSize && distanceFromRoot <= maxCubeSize)
        {
            isParentNeighboor = true;
        }
        foreach (var c in cubes)
        {
            var distanceFromCube = Vector3.Distance(c.transform.position, transform.position);
            if (distanceFromCube >= minCubeSize && distanceFromCube <= maxCubeSize)
            {
                neighboors.Add(c);
                c.AddNeighboorIfNotExist(this);
            }
        }
        cubes.Add(this);
    }

    public void AddNeighboorIfNotExist(GravityToogleScript neighboor)
    {
        if (!neighboors.Contains(neighboor))
            neighboors.Add(neighboor);
    }

    public void RemoveNeighboor(GravityToogleScript neighboor)
    {
        if (neighboors.Contains(neighboor))
            neighboors.Remove(neighboor);
    }

    public bool GravityCheck(GravityToogleScript caller = null)
    {
        // checks if the gravity should be enabled, reccursively...
        Debug.Log("GravityCheck on cube " + id);
        if (isParentNeighboor || transform.localPosition.y == 0)
        {
            Debug.Log("Cube " + id + " is grounded");
            return false;
        }
        foreach (var n in neighboors)
        {
            if (n != caller && !n.GravityCheck(this))
            {
                Debug.Log("Cube " + id + " not in the air");
                return false;
            }
        }
        Debug.Log("activating gravity");
        rigidbody.useGravity = true;
        StartCoroutine(DestroyIfNotMovingAfterCollapse(this));
        return true;
    }

    void OnDestroy()
    {
        cubes.Remove(this);
        foreach (var n in neighboors)
        {
            n.RemoveNeighboor(this);
            n.GravityCheck();
        }
    }

    void OnLevelWasLoaded(int lvl)
    {
        cubes = new List<GravityToogleScript>();
    }

    public void HandleFloorCollision()
    {
        if (rigidbody.useGravity)
            StartCoroutine(SelfDestructCoroutine(3));
    }

    IEnumerator SelfDestructCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    IEnumerator DestroyIfNotMovingAfterCollapse(GravityToogleScript cube)
    {
        Vector3 lastPos = cube.transform.position;
        yield return new WaitForSeconds(0.1f);
        bool b = true;
        while (b)
        {
            if (lastPos == cube.transform.position)
            {
                cube.HandleFloorCollision();
                b = false;
            }
            else
            {
                lastPos = cube.transform.position;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

}
