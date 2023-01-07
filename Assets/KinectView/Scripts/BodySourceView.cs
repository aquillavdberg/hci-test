using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;


public class BodySourceView : MonoBehaviour 
{
    public Material BoneMaterial;
    public GameObject BodySourceManager;
    public GameObject HandPrefab;
    public GameObject JointColliderPrefab;

    public static List<GameObject> joints = new List<GameObject>();
    public static Dictionary<GameObject, bool > jointCollided = new Dictionary < GameObject, bool > ();
    public static Dictionary<string, float[] > jointColliderPos = new Dictionary < string, float[] > ();
    public static int made = 0;
    
    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;


    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };
    
    void Update () 
    {

        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }
        
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                
                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }


        if (jointColliderPos.Count < 1)
        {
            Debug.Log("if empty jointColliderPose dict");
            poseDict();
        }

        if (jointCollided.Count < 1)
        {
            Debug.Log("if empty jointCollided dict");
            createJointCollider();
        }
    }
    
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {   
                
                GameObject jointObj = GameObject.Instantiate(HandPrefab);
                LineRenderer lr = jointObj.AddComponent<LineRenderer>();
                lr.SetVertexCount(2);
                lr.material = BoneMaterial;
                lr.SetWidth(0.05f, 0.05f);
                
                jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                jointObj.name = jt.ToString();
                jointObj.transform.parent = body.transform;
                Debug.Log("creating jointobj" + jointObj.name);
                Debug.Log("jointObj" + jointObj + jointObj.name);

                if (!joints.Contains(jointObj))
                {
                    Debug.Log("adding jointObj");
                    joints.Add(jointObj);
                }
                Debug.Log("creating joints list" + joints.Count + joints);
        }
     

        return body;
    }

    public void poseDict()
    {
        float[] vector = {0f, 0f, 0f};      
        Debug.Log("create posedict"); 
        Debug.Log("joint =" + joints.Count + joints); 
        for (int i = 0; i < joints.Count; i++)
        {   
            GameObject jt = joints[i];
            // debug: blijft steeds lopen en probeerd steeds joints toe te voegen die er al zijn. gebruik try en catch? of gebruik de joints hashmap?
            // alle colliders worden wel gemaakt maar ver buiten frame, afmetingen van de vector aanpassen?
            // nog maken ontrigger, change colour
            Debug.Log("creating jointcollider x joint collider pos" + jt.ToString() + vector);
            

            jointColliderPos.Add(jt.ToString(), vector);
            vector[0] = vector[0] + 1f;
            vector[1] = vector[1] + 1f;
            vector[2] = vector[2] + 1f;
            Debug.Log("vector update" + vector);

        }
    }    
    public void createJointCollider()
    {    
        Debug.Log("creat joint colliders");
        for (int i = 0; i < joints.Count; i++)
        {   
            GameObject jt = joints[i];
            Debug.Log("in for each loop @" + jt.ToString());
            GameObject Colliderbody = new GameObject("collider" + jt.ToString());
                GameObject jointCollider = GameObject.Instantiate(JointColliderPrefab);
                LineRenderer lr = jointCollider.AddComponent<LineRenderer>();
                lr.SetVertexCount(2);
                lr.material = BoneMaterial;
                lr.SetWidth(0.05f, 0.05f);

                Debug.Log("jointcollider possition wordt" + jointColliderPos[jt.ToString()]);

                jointCollider.transform.position = new Vector3(jointColliderPos[jt.ToString()][0], jointColliderPos[jt.ToString()][1], jointColliderPos[jt.ToString()][2]);
                jointCollider.transform.localScale = new Vector3(3f, 6f, 3f);
                jointCollider.name = jt.ToString();
                jointCollider.transform.parent = Colliderbody.transform;

                Debug.Log("joincollider position ==" + jointCollider.transform.position);

                // jointsCollider.Add(jointCollider);
                
                Debug.Log("jointcollider gemaakt:");
                Debug.Log(jointCollider.ToString());
                jointCollided.Add(jointCollider, false);
        }
    }
  
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;
            
            if(_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }
            
            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);
            
            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if(targetJoint.HasValue)
            {
                lr.SetPosition(0, jointObj.localPosition);
                lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }
            else
            {
                lr.enabled = false;
            }
        }
    }
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
