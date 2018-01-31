using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
    int nbounces = 5;
    float amplitude = 0.4f;
    float radiusOffset = 4;
    transform.localPosition = new Vector2(Mathf.Sin(Time.time * nbounces) * amplitude + radiusOffset, 0);
	}
}
