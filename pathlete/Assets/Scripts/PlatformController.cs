using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class PlatformController : MonoBehaviour {
  public GameObject pathPrefab;
  public GameObject wallPrefab;
  public GameObject platformPrefab;
  public TextAsset level;

  private SerialPort serial;
  private Coroutine nudger;
  private int dx;
  private int dy;
  private char[][] board;
  private GameObject platform;
  private GameObject prefabs;
  private int platformX;
  private int platformY;

  void Start() {
    serial = new SerialPort("/dev/tty.usbmodem1411", 9600);
    serial.Open();
    prefabs = GameObject.Find("/Prefabs");

    string[] lines = level.text.Split(new string[] {"\r\n", "\n", "\r"}, StringSplitOptions.RemoveEmptyEntries);
    board = new char[lines.Length][];
    for (int i = 0; i < lines.Length; ++i) {
      board[i] = lines[i].ToCharArray();
    }

    for (int r = 0; r < board.Length; ++r) {
      for (int c = 0; c < board[r].Length; ++c) {
        // if an x
        //   let's put a wall
        // else if a +
        //   let's put a platform
        if (board[r][c] == 'x') {
          GameObject wall = Instantiate(wallPrefab, new Vector2(c, board.Length - 1 - r), Quaternion.identity);
          wall.transform.parent = prefabs.transform;
        } else if (board[r][c] == '+') {
          board[r][c] = '.';
          platform = Instantiate(platformPrefab, new Vector2(c, board.Length - 1 - r), Quaternion.identity);
          platformX = c;
          platformY = r;
        }
      }
    }

    Camera.main.transform.position = new Vector3((board[0].Length - 1) * 0.5f,
                                                 (board.Length - 1) * 0.5f,
                                                 Camera.main.transform.position.z);
  }

  void Update() {
    while (serial.BytesToRead >= 2) {
      dx = serial.ReadByte() - 1;
      dy = -(serial.ReadByte() - 1);
      Debug.Log("dx: " + dx);
      Debug.Log("dy: " + dy);
      if (nudger == null) {
        nudger = StartCoroutine(Nudge());
      }
    }
  }

  IEnumerator Nudge() {
    while (platformX >= 0 && platformX < board[0].Length &&
           platformY >= 0 && platformY < board.Length &&
           board[platformY][platformX] == '.') {
      Vector3 startPosition = platform.transform.position;
      Vector3 endPosition = startPosition + new Vector3(dx, dy, 0);

      GameObject path = Instantiate(pathPrefab, new Vector2(platformX, board.Length - 1 - platformY), Quaternion.identity);
      path.transform.parent = prefabs.transform;
      board[platformY][platformX] = '_';

      platformX += dx;
      platformY -= dy;

      float startTime = Time.time;
      float targetDuration = 0.5f;
      float elapsed = 0.0f;

      while (elapsed < targetDuration) {
        float proportion = elapsed / targetDuration;
        platform.transform.position = Vector3.Lerp(startPosition, endPosition, proportion);
        yield return null;
        elapsed = Time.time - startTime;
      }

      platform.transform.position = endPosition;

      yield return new WaitForSeconds(0.25f);
    }
  }
}
