using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float moveSpeed = 4f;
  public float rotateSpeed = 250f;
  public float fireSpeed = 0.5f;
  public Transform spawnPoint;
  public GameObject bolt;

  private Rigidbody2D rb2D;
  private GameController gameController;
  private bool isMovementEnabled = true;
  private Coroutine fire;

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

  IEnumerator Fire()
  {
    while (true)
    {
      Instantiate(bolt, spawnPoint.position, spawnPoint.rotation);
      yield return new WaitForSeconds(fireSpeed);
    }
  }

  void Update()
  {
    bool shouldFire = gameController.ShouldFire();
    if (shouldFire && fire == null)
    {
      fire = StartCoroutine(Fire());
    }
    else if (!shouldFire && fire != null)
    {
      StopCoroutine(fire);
      fire = null;
    }
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
    if (!isMovementEnabled) { return; }
    if (gameController.ShouldMoveForward()) Move(moveSpeed);
    if (gameController.ShouldRotateLeft()) Rotate(rotateSpeed);
    if (gameController.ShouldMoveBackward()) Move(-moveSpeed);
    if (gameController.ShouldRotateRight()) Rotate(-rotateSpeed);
  }

  private void DisableMovement()
  {
    isMovementEnabled = false;
    gameController.DisableMovement();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    bool shouldRestart = other.CompareTag("Restart") || other.CompareTag("Destroy");
    bool shouldLoadNext = other.CompareTag("Finish");
    if (shouldRestart || shouldLoadNext)
    {
      DisableMovement();
      if (shouldRestart) gameController.RestartScene(1);
      else if (shouldLoadNext) gameController.LoadNextScene(1);
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Bounds"))
    {
      gameController.RestartScene();
    }
  }
}
