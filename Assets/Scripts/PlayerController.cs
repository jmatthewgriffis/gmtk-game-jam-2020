using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float moveSpeed = 5f;
  public float rotateSpeed = 250f;

  private Rigidbody2D rb2D;
  private GameController gameController;

  private void MissingComponent(string name)
  {
    Debug.LogError("Missing " + name + "!");
  }

  private Rigidbody2D GetRigidbody2D()
  {
    Rigidbody2D component = GetComponent<Rigidbody2D>();
    if (!component) { MissingComponent("Rigidbody2D"); }
    return component;
  }

  private GameController GetGameController()
  {
    GameObject gameObject = GameObject.FindGameObjectWithTag("GameController");
    GameController script = gameObject ? gameObject.GetComponent<GameController>() : null;
    if (!script) { MissingComponent("GameController"); }
    return script;
  }
  void Start()
  {
    rb2D = GetRigidbody2D();
    gameController = GetGameController();
  }

  private void Move(float speed)
  {
    Vector3 worldOffset3D = transform.TransformDirection(new Vector3(0f, speed, 0f));
    Vector2 worldOffset2D = new Vector2(worldOffset3D.x, worldOffset3D.y);
    rb2D.MovePosition(rb2D.position + worldOffset2D * Time.fixedDeltaTime);
  }

  private void Rotate(float speed)
  {
    rb2D.MoveRotation(rb2D.rotation + speed * Time.fixedDeltaTime);
  }

  void FixedUpdate()
  {
    if (gameController.ShouldMoveForward()) Move(moveSpeed);
    if (gameController.ShouldRotateLeft()) Rotate(rotateSpeed);
    if (gameController.ShouldMoveBackward()) Move(-moveSpeed);
    if (gameController.ShouldRotateRight()) Rotate(-rotateSpeed);
  }
}
