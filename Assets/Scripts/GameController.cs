using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Key
{
  private string main;
  private string alt;

  public Key(string _main, string _alt)
  {
    main = _main;
    alt = _alt;
  }

  public string Main()
  {
    return main;
  }

  public string Alt()
  {
    return alt;
  }
}

public class GameController : MonoBehaviour
{
  private List<string> keyNames = new List<string>();
  private List<string> controlMappingNames = new List<string>();
  private Dictionary<string, string> controlMappings = new Dictionary<string, string>();
  private Dictionary<string, Key> keys = new Dictionary<string, Key>();
  private Dictionary<string, bool> pressedKeys = new Dictionary<string, bool>();

  private void SetUpKeyNames()
  {
    if (keyNames.Count > 0)
    {
      Debug.LogError("Key names already set up!");
      return;
    }
    keyNames.Add("up");
    keyNames.Add("left");
    keyNames.Add("down");
    keyNames.Add("right");
    keyNames.Add("space");
  }

  private void SetUpControlMappingNames()
  {
    if (controlMappingNames.Count > 0)
    {
      Debug.LogError("Control mapping names already set up!");
      return;
    }
    controlMappingNames.Add("forward");
    controlMappingNames.Add("left");
    controlMappingNames.Add("backward");
    controlMappingNames.Add("right");
    controlMappingNames.Add("fire");
  }

  private void SetUpControlMappings()
  {
    if (keyNames.Count == 0)
    {
      Debug.LogError("Key names not set up!");
      return;
    }
    if (controlMappingNames.Count == 0)
    {
      Debug.LogError("Control mapping names not set up!");
      return;
    }
    if (controlMappings.Count > 0)
    {
      Debug.LogError("Control mappings already set up!");
      return;
    }
    if (controlMappingNames.Count != keyNames.Count)
    {
      Debug.LogError("Counts don't match!");
      return;
    }
    for (int i = 0; i < controlMappingNames.Count; i++)
    {
      controlMappings.Add(controlMappingNames[i], keyNames[i]);
    }
  }

  private void SetUpKeys()
  {
    if (keyNames.Count == 0)
    {
      Debug.LogError("Key names not set up!");
      return;
    }
    if (keys.Count > 0)
    {
      Debug.LogError("Keys already set up!");
      return;
    }
    keys.Add(keyNames[0], new Key("up", "w"));
    keys.Add(keyNames[1], new Key("left", "a"));
    keys.Add(keyNames[2], new Key("down", "s"));
    keys.Add(keyNames[3], new Key("right", "d"));
    keys.Add(keyNames[4], new Key("space", null));
  }

  private void SetUpPressedKeys()
  {
    if (keyNames.Count == 0)
    {
      Debug.LogError("Key names not set up!");
      return;
    }
    if (pressedKeys.Count > 0)
    {
      Debug.LogError("Pressed keys already set up!");
      return;
    }
    foreach (string name in keyNames)
    {
      pressedKeys.Add(name, false);
    }
  }

  void Start()
  {
    SetUpKeyNames();
    SetUpControlMappingNames();
    SetUpControlMappings();
    SetUpKeys();
    SetUpPressedKeys();
    StartCoroutine(ChangeControlMappings());
  }

  private void UpdatePressedKeys()
  {
    foreach (string name in keyNames)
    {
      if (!keys.ContainsKey(name))
      {
        Debug.LogError("Key '" + name + "' doesn't exist in keys!");
        continue;
      }
      if (!pressedKeys.ContainsKey(name))
      {
        Debug.LogError("Key '" + name + "' doesn't exist in pressedKeys!");
        continue;
      }
      Key key = keys[name];
      pressedKeys[name] =
        (key.Main() != null && Input.GetKey(key.Main())) ||
        (key.Alt() != null && Input.GetKey(key.Alt()));
    }
  }

  void Update()
  {
    UpdatePressedKeys();
  }

  public bool ShouldMoveForward()
  {
    return pressedKeys[controlMappings[controlMappingNames[0]]];
  }

  public bool ShouldRotateLeft()
  {
    return pressedKeys[controlMappings[controlMappingNames[1]]];
  }

  public bool ShouldMoveBackward()
  {
    return pressedKeys[controlMappings[controlMappingNames[2]]];
  }

  public bool ShouldRotateRight()
  {
    return pressedKeys[controlMappings[controlMappingNames[3]]];
  }

  public bool ShouldFire()
  {
    return pressedKeys[controlMappings[controlMappingNames[4]]];
  }

  IEnumerator ChangeControlMappings()
  {
    yield return new WaitForSeconds(5);
    Debug.Log("changing controls!");
    List<string> keyNamesCopy = new List<string>(keyNames);
    foreach (string name in controlMappingNames)
    {
      if (!controlMappings.ContainsKey(name))
      {
        Debug.LogError("Key '" + name + "' doesn't exist in controlMappings!");
        continue;
      }
      int randomIndex = Random.Range(0, keyNamesCopy.Count);
      string randomKeyName = keyNamesCopy[randomIndex];
      controlMappings[name] = randomKeyName;
      keyNamesCopy.RemoveAt(randomIndex);
    }
  }
}
