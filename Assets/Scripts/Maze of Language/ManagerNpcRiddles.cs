using SpatialSys.UnitySDK;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerNpcRiddles : MonoBehaviour
{
    public List<NpcRiddles> npcs = new List<NpcRiddles>();
    [HideInInspector]public List<AudioSource> audioSource = new List<AudioSource>();

    public GameObject panel;
    public SpatialQuest quest;
    public int indexTaskCompleted;
    // Start is called before the first frame update
    void Start()
    {
       foreach (NpcRiddles npc in npcs) {

            npc.Manag_riddle = this;
            audioSource.Add(npc.audioSource);
       }
    }
    public void StopsAudio()
    {
        foreach (AudioSource audioSource in audioSource)
                audioSource.Stop();
    }

    public void RiddlesUpdate(NpcRiddles npc)
    {
        panel.SetActive(false);
        panel.SetActive(true);
        var respon = npc.responses;
        foreach(Response response in respon)
        {
            response.button.onClick.RemoveAllListeners();
            response.button.GetComponentInChildren<TextMeshProUGUI>().text = response.text;
            if (response.correct)
            {
                response.button.onClick.AddListener(() =>
                {
                    DisableEmpty();
                    npc.complete.Invoke();
                    npc.completed = true;
                    npc.interactable.enabled = false;
                    CheckComplete();
                    if (response.audioClip != null)
                    {
                        npc.AudiosResponse(response.audioClip);
                    }
                    
                });
            }
            else
            {
                response.button.onClick.AddListener(() =>
                {
                    DisableEmpty();
                    npc.incorrect.Invoke();
                    if (response.audioClip != null)
                    {
                        npc.AudiosResponse(response.audioClip);
                    }
                       
                });
                
            }
        }
    }

    private void DisableEmpty()
    {
        panel.SetActive(false);
    }

    public void CheckComplete()
    {
        foreach (var item in npcs)
        {
            if (!item.completed)
            {
                return;
            }
        }

        quest.tasks[indexTaskCompleted].CompleteTask();
    }
}

[Serializable]

public class Response
{
    public string text;
    public Button button;
    public AudioClip audioClip;
    public bool correct;
}
