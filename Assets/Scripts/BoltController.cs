using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : MonoBehaviour
{
  public float moveSpeed = 5f;

  private Rigidbody2D rb2D;

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

  void Start()
  {
    rb2D = GetRigidbody2D();
  }

  private void Move()
  {
    Vector3 worldOffset3D = transform.TransformDirection(new Vector3(0f, moveSpeed, 0f));
    Vector2 worldOffset2D = new Vector2(worldOffset3D.x, worldOffset3D.y);
    rb2D.MovePosition(rb2D.position + worldOffset2D * Time.fixedDeltaTime);
  }

  void FixedUpdate()
  {
    Move();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Restart"))
    {
      Destroy(gameObject);
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Bounds"))
    {
      Destroy(gameObject);
    }
  }
}
