using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private GameController gameController;

  void Start()
  {
    GameObject gameObject = GameObject.FindGameObjectWithTag("GameController");
    gameController = gameObject ? gameObject.GetComponent<GameController>() : null;
    if (!gameController) { Debug.Log("Something has gone very wrong."); }
  }

  void Update()
  {
  }
}
