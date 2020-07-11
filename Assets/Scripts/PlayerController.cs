using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float moveSpeed = 5f;
  public float rotateSpeed = 250f;

  private Rigidbody2D rb;
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
    rb = GetRigidbody2D();
    gameController = GetGameController();
  }

  private void Move(float speed)
  {
    transform.Translate(new Vector3(0f, Time.deltaTime, 0f) * speed);
  }

  private void Rotate(float speed)
  {
    transform.Rotate(new Vector3(0f, 0f, Time.deltaTime) * speed);
  }

  void Update()
  {
    if (gameController.ShouldMoveForward()) Move(moveSpeed);
    if (gameController.ShouldRotateLeft()) Rotate(rotateSpeed);
    if (gameController.ShouldMoveBackward()) Move(-moveSpeed);
    if (gameController.ShouldRotateRight()) Rotate(-rotateSpeed);
  }
}
