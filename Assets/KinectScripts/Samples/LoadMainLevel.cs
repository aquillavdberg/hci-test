using UnityEngine;
using System.Collections;

public class LoadMainLevel : MonoBehaviour 
{
	private bool levelLoaded = false;
	
	
	void Update() 
	{
		
		if(!levelLoaded && BodySourceView.Initialization)
		{
			levelLoaded = true;
			Application.LoadLevel(1);
		}
	}
	
}
