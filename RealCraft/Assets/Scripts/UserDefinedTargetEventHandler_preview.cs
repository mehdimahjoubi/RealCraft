using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(UserDefinedTargetBuildingBehaviour))]
public class UserDefinedTargetEventHandler_preview : MonoBehaviour, IUserDefinedTargetEventHandler, ITrackableEventHandler {

	public static Transform model;
    public ImageTargetBuilder.FrameQuality CurrentFrameQuality { get; private set; }
	UserDefinedTargetBuildingBehaviour buildingBehaviour;
    ImageTracker imageTracker;

	void Start ()
	{
		buildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour> ();
		buildingBehaviour.RegisterEventHandler (this);
	}

	/// <summary>
	/// called when the UserDefinedTargetBehaviour has been initialized
	/// </summary>
	public void OnInitialized()
	{
		Debug.Log ("init......");
	}
	
	public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
	{
        CurrentFrameQuality = frameQuality;
	}
	
	public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        Debug.Log("new trackable");
        imageTracker = (ImageTracker)TrackerManager.Instance.GetTracker<ImageTracker>();
        var db = imageTracker.CreateDataSet();
        var trackable = db.CreateTrackable(trackableSource, "new trackable");
        trackable.transform.localScale = new Vector3(1, 1, 1);
        model.parent = trackable.transform;
        //model.localScale *= 50;
        imageTracker.ActivateDataSet(db);
        StopExtendedTracking();
        var imgBhv = GameObject.Find("new trackable").GetComponent<ImageTargetBehaviour>();
        imgBhv.ImageTarget.StartExtendedTracking();
        imgBhv.RegisterTrackableEventHandler(this);
        //imageTracker.PersistExtendedTracking(true);
        //imageTracker.ResetExtendedTracking();
    }

    public void BuildTarget()
    {
        buildingBehaviour.BuildNewTarget("newTarget", 5);
    }

    private void StopExtendedTracking()
    {
        StateManager stateManager = TrackerManager.Instance.GetStateManager();
        foreach(var behaviour in stateManager.GetTrackableBehaviours())
        {
            var imageBehaviour = behaviour as ImageTargetBehaviour;
            if(imageBehaviour != null)
            {
                imageBehaviour.ImageTarget.StopExtendedTracking();
            }
        }
        //List<TrackableBehaviour> list =  stateManager.GetTrackableBehaviours().ToList();
        //ImageTargetBehaviour bhvr = (ImageTargetBehaviour)list[.Count - 1] as ImageTargetBehaviour;
        //if(bhvr != null)
        //{
        //    bhvr.ImageTarget.StartExtendedTracking();
        //}
    }


    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        //if (newStatus == TrackableBehaviour.Status.DETECTED ||
        //newStatus == TrackableBehaviour.Status.TRACKED ||
        //newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        //{
        //    OnTrackingFound();
        //}
        //else
        //{
        //    OnTrackingLost();
        //}
    }
}
