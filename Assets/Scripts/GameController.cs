using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
  public Text alertText;
  public Text upText;
  public Text leftText;
  public Text downText;
  public Text rightText;
  public Text spaceText;

  private List<string> alertMessages = new List<string>();
  private List<string> keyNames = new List<string>();
  private List<string> controlMappingNames = new List<string>();
  private Dictionary<string, string> controlMappings = new Dictionary<string, string>();
  private Dictionary<string, Key> keys = new Dictionary<string, Key>();
  private Dictionary<string, bool> pressedKeys = new Dictionary<string, bool>();

  private void SetUpAlertMessages()
  {
    if (alertMessages.Count > 0)
    {
      Debug.LogError("Alert messages already set up!");
      return;
    }
    alertMessages.Add("Haha good luck with that");
    alertMessages.Add("Oops");
    alertMessages.Add("Um...");
    alertMessages.Add("Someone dropped a cat on the keyboard");
    alertMessages.Add("Really need to get that fixed");
    alertMessages.Add("LOL");
    alertMessages.Add("Oh, the inanity!");
    alertMessages.Add("Pizza's here");
    alertMessages.Add("Have you tried turning it off and on again");
    alertMessages.Add("Do or do not. There is no try");
  }

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
    keys.Add(keyNames[0], new Key("w", "up"));
    keys.Add(keyNames[1], new Key("a", "left"));
    keys.Add(keyNames[2], new Key("s", "down"));
    keys.Add(keyNames[3], new Key("d", "right"));
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
    SetUpAlertMessages();
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

  private void UpdateControlText(string keyName, string controlMappingName)
  {
    Text text;
    Key key;
    string divider = " / ";
    string newLine = "\n";

    switch (keyName)
    {
      case "up":
        text = upText;
        key = keys[keyNames[0]];
        break;
      case "left":
        text = leftText;
        key = keys[keyNames[1]];
        break;
      case "down":
        text = downText;
        key = keys[keyNames[2]];
        break;
      case "right":
        text = rightText;
        key = keys[keyNames[3]];
        break;
      case "space":
        text = spaceText;
        key = keys[keyNames[4]];
        break;
      default:
        text = null;
        key = null;
        break;
    }

    string updatedTop =
        key.Main() + (key.Alt() != null ? divider + key.Alt() : "");
    string updatedBottom = controlMappingName;
    text.text = updatedTop.ToUpper() + newLine + updatedBottom;
  }

  private string RandomAlertMessage()
  {
    return alertMessages[Random.Range(0, alertMessages.Count)];
  }

  private void UpdateAlertText(bool shouldClear = false)
  {
    alertText.text =
        shouldClear ? "" : "Controls switch!\n" + RandomAlertMessage();
  }

  IEnumerator ChangeControlMappings()
  {
    UpdateAlertText(true);
    for (int i = 0; i < keyNames.Count; i++)
    {
      string keyName = keyNames[i];
      string controlMappingName = controlMappingNames[i];
      UpdateControlText(keyName, controlMappingName);
    }
    while (true)
    {
      yield return new WaitForSeconds(5);
      UpdateAlertText();
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
        UpdateControlText(randomKeyName, name);
        keyNamesCopy.RemoveAt(randomIndex);
      }
      yield return new WaitForSeconds(2);
      UpdateAlertText(true);
    }
  }
}
