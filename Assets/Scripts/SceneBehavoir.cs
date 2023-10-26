using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehavoir : MonoBehaviour
{
    private IEnumerator RestartLevelCoroutine(int sceneIndex)
    {
        yield return new WaitForSeconds(2f);

        if (sceneIndex < 0)
        {
            throw new System.Exception("Scene index cannot be negative!");
        }
        SceneManager.LoadScene(sceneIndex);
    }

    public void StartCoroutineLevel()
    {
        StartCoroutine(RestartLevelCoroutine(0));
    }
}
