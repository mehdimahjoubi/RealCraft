using UnityEngine;

public class Draggable : MonoBehaviour
{
	public Camera renderCam;
    public bool fixX;
	public bool fixY;
	public Transform thumb;	
	bool dragging;
    bool touching = false;

	void Update()
	{
        //if (Input.GetMouseButtonDown(0)) {
        //    dragging = false;
        //    var ray = renderCam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (collider.Raycast(ray, out hit, 100)) {
        //        dragging = true;
        //    }
        //}
        //if (Input.GetMouseButtonUp(0)) dragging = false;
        //if (dragging && Input.GetMouseButton(0)) {
        //    var point = renderCam.ScreenToWorldPoint(Input.mousePosition);
        //    point = collider.ClosestPointOnBounds(point);
        //    SetThumbPosition(point);
        //    SendMessage("OnDrag", Vector3.one - (thumb.position - collider.bounds.min) / collider.bounds.size.x);
        //}
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                StartColorProcessing(touch.position);
                touching = true;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                dragging = false;
                touching = true;
            }
            if (dragging && touching)
                ProcessDragging(touch.position);
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartColorProcessing(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
                dragging = false;
            if (dragging && Input.GetMouseButton(0))
            {
                ProcessDragging(Input.mousePosition);
            }
        }
	}

    private void ProcessDragging(Vector3 newScreenPos)
    {
        var point = renderCam.ScreenToWorldPoint(newScreenPos);
        point = collider.ClosestPointOnBounds(point);
        SetThumbPosition(point);
        SendMessage("OnDrag", Vector3.one - (thumb.position - collider.bounds.min) / collider.bounds.size.x);
    }

    private void StartColorProcessing(Vector3 screenPos)
    {
        dragging = false;
        var ray = renderCam.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (collider.Raycast(ray, out hit, 100))
        {
            dragging = true;
        }
    }

	void SetDragPoint(Vector3 point)
	{
		point = (Vector3.one - point) * collider.bounds.size.x + collider.bounds.min;
		SetThumbPosition(point);
	}

	void SetThumbPosition(Vector3 point)
	{
		thumb.position = new Vector3(fixX ? thumb.position.x : point.x, fixY ? thumb.position.y : point.y, thumb.position.z);
	}
}
