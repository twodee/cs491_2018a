using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class CannonController : MonoBehaviour {
  private SerialPort serial;

	void Start() {
    serial = new SerialPort("/dev/tty.usbmodem1411", 9600);
    serial.Open();
	}
	
	void Update() {
	  while (serial.BytesToRead > 0) {
      int angle = serial.ReadByte();
      float mapped = angle * 140.0f / 255 - 70;
      /* Debug.Log(angle); */
      transform.eulerAngles = new Vector3(0, 0, mapped);
    }  
	}
}
