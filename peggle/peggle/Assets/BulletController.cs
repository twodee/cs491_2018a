using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

  void OnCollisionEnter2D(Collision2D collision) {
    Destroy(collision.gameObject);
  }

  void OnTriggerExit2D(Collider2D collider) {
    Destroy(gameObject);
  }
}
