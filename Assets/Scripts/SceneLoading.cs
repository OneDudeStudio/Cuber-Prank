using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoading : MonoBehaviour
{   //SceneManager
    public Image LoadingImage;
    public TextMeshProUGUI progressText;
    private AsyncOperation operation;
    public int sceneID;
    //Other
    //public TextMeshProUGUI tipsText;
    public TextMeshProUGUI readyText;
    private void Start()
    {
        //tipsText.gameObject.SetActive(true);
         StartCoroutine(AsyncLoad());
    }


    IEnumerator AsyncLoad()
    {
         operation = SceneManager.LoadSceneAsync(sceneID,LoadSceneMode.Single);
         operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            LoadingImage.fillAmount = progress;
            progressText.SetText((Mathf.RoundToInt(progress*100)).ToString()+"%");
            yield return null;
        }
    }
    private void Update()
    {
        if (operation.progress>0.89)
        {
            readyText.gameObject.SetActive(true);
        }
    }

    public void ReadyToGame()
    {
        operation.allowSceneActivation = true;
    }
}
