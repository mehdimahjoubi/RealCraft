using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UserDefinedTargetBuildingBehaviour))]
public class UserDefinedTargetEventHandler : MonoBehaviour, IUserDefinedTargetEventHandler {

	public Transform model;
	UserDefinedTargetBuildingBehaviour buildingBehaviour;

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
	
	/// <summary>
	/// called when the UserDefinedTargetBehaviour reports a new frame Quality
	/// </summary>
	public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
	{
		if (frameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH ||
		    frameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_MEDIUM)
		{
			Debug.Log("BUILDING STARTED...");
			buildingBehaviour.BuildNewTarget ("newTarget", Screen.width);
		}
		Debug.Log ("frame quality changed...");
	}
	
	/// <summary>
	/// called when an error is reported during initialization
	/// </summary>
	public void OnNewTrackableSource(TrackableSource trackableSource)
	{
		Debug.Log ("new trackable");
		ImageTracker imageTracker =
			(ImageTracker)TrackerManager.Instance.GetTracker<ImageTracker>();
		var db = imageTracker.CreateDataSet ();
		var trackable = db.CreateTrackable (trackableSource, "new trackable");
		model.parent = trackable.transform;
		model.localScale *= 50;
		imageTracker.ActivateDataSet (db);
		//var imgTarget = trackable.Trackable as ExtendedTrackable;
		//imgTarget.StartExtendedTracking ();
	}
}
