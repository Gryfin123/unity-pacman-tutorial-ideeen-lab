using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour
{
    public void LoadAnotherScene(int index)
    {
        StartCoroutine(LoadScene(index));
    }

    private IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene(index);
    }
}
