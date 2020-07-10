using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  private bool isUpPressed = false;
  private bool isLeftPressed = false;
  private bool isDownPressed = false;
  private bool isRightPressed = false;
  private bool isSpacePressed = false;

  void Update()
  {
    isUpPressed = Input.GetKey("w") || Input.GetKey("up");
    isLeftPressed = Input.GetKey("a") || Input.GetKey("left");
    isDownPressed = Input.GetKey("s") || Input.GetKey("down");
    isRightPressed = Input.GetKey("d") || Input.GetKey("right");
    isSpacePressed = Input.GetKey("space");
  }
}
