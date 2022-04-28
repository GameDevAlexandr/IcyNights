using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutScene : MonoBehaviour
{
    public GameObject planeExplose;
    public Transform finish;
    void Start()
    {
        StartCoroutine(CutEpizode1());
    }

    // Update is called once per frame
    void Update()
    {
        planeExplose.transform.Translate(finish.transform.position * Time.deltaTime);
    }


    IEnumerator CutEpizode1()
    {
        yield return new WaitForSeconds(1f);
        planeExplose.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(2);

    }







}
