using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
	[SerializeField] private float sineFrequency = 10f; 
	void Update()
	{
		float scaleAmount = Mathf.Sin( Time.time * sineFrequency ) * Time.deltaTime;
		transform.localScale += new Vector3( scaleAmount, scaleAmount, 0f );
	}
}
