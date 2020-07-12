using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
  public float moveSpeed = 1f;
  public float fireSpeed = 1f;
  public Transform spawnPoint;
  public GameObject bolt;

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
    Vector3 worldOffset3D = transform.TransformDirection(new Vector3(moveSpeed, 0f, 0f));
    Vector2 worldOffset2D = new Vector2(worldOffset3D.x, worldOffset3D.y);
    rb2D.MovePosition(rb2D.position + worldOffset2D * Time.fixedDeltaTime);
  }

  void FixedUpdate()
  {
    Move();
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Range"))
    {
      moveSpeed *= -1;
    }
  }
}
