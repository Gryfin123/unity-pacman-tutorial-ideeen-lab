using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour
{
    public void LoadAnotherScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
