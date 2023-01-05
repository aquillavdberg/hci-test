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

    public static HashSet<GameObject> joints = new HashSet<GameObject>();
    public static Dictionary<GameObject, bool > jointCollided = new Dictionary < GameObject, bool > ();
    public static Dictionary<string, int[] > jointColliderPos = new Dictionary < string, int[] > ();
    
    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;



    public void poseDict()
    {
        int[] vector = {0, 0, 0};        
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            // debug: blijft steeds lopen en probeerd steeds joints toe te voegen die er al zijn. gebruik try en catch? of gebruik de joints hashmap?
            // alle colliders worden wel gemaakt maar ver buiten frame, afmetingen van de vector aanpassen?
            // nog maken ontrigger, change colour
            Debug.Log("creating jointcollider x joint collider pos");
            Debug.Log(jt.ToString());

            try
            {
            jointColliderPos.Add(jt.ToString(), vector);
            vector[0] = vector[0] + 1;
            vector[1] = vector[1] + 1;
            vector[2] = vector[2] + 1;

            }
            catch
            {
            
            }
        }
    }    
    public void createJointCollider()
    {    
        Debug.Log("creat joint colliders");
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {   
            GameObject Colliderbody = new GameObject("collider"+jt);
                GameObject jointCollider = GameObject.Instantiate(JointColliderPrefab);
                LineRenderer lr = jointCollider.AddComponent<LineRenderer>();
                lr.SetVertexCount(2);
                lr.material = BoneMaterial;
                lr.SetWidth(0.05f, 0.05f);

                jointCollider.transform.position = new Vector3(jointColliderPos[jt.ToString()][0], jointColliderPos[jt.ToString()][1], jointColliderPos[jt.ToString()][2]);
                jointCollider.transform.localScale = new Vector3(3f, 6f, 3f);
                jointCollider.name = jt.ToString();
                jointCollider.transform.parent = Colliderbody.transform;

                // jointsCollider.Add(jointCollider);
                
                Debug.Log("jointcollider gemaakt:");
                Debug.Log(jointCollider);
                jointCollided.Add(jointCollider, false);
        }
    }
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
        int count = 0;
        if (count == 0)
        { poseDict();}
        count += 1;
        

        if (jointCollided.Count < 1)
        {
            Debug.Log("if empty dict");
            createJointCollider();
        }

        

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

                joints.Add(jointObj);
        }
     

        return body;
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
