using UnityEngine;
using System.Collections;

public class MenuButtonScript : MonoBehaviour {

    public void OnClicked()
    {
        Application.LoadLevel("Main");
    }
}
