using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class CannonController : MonoBehaviour {
  public GameObject bulletPrefab;
  public float bulletSpeed;

  private SerialPort serial;

	void Start() {
    serial = new SerialPort("/dev/tty.usbmodem1411", 9600);
    serial.Open();
	}
	
	void Update() {
	  while (serial.BytesToRead >= 2) {
      int device = serial.ReadByte();
      int val = serial.ReadByte();

      if (device == 0) {
        float mapped = val * 140.0f / 255 - 70;
        transform.eulerAngles = new Vector3(0, 0, mapped);
      } else {
        // fire
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 direction = -transform.up;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
      }
    }  
	}
}
