using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class GameController : MonoBehaviour {
  public GameObject planet;
  public float angularVelocity;

  private SerialPort serial;

  void Start() {
    serial = new SerialPort("/dev/tty.usbmodem1411", 9600);
    serial.Open();
  }
  
  void Update() {
    if (serial.BytesToRead > 0 && serial.ReadByte() == 1) {
      angularVelocity *= -1;
    }
    planet.transform.Rotate(new Vector3(0, 0, angularVelocity)); 
  }
}
