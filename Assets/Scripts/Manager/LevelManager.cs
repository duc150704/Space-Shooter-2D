using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<Wave> waves = new List<Wave>();
    [SerializeField] float timeBetweenWaves = 1.0f;
    [SerializeField] TextMeshProUGUI wavesNameTextMPGUI;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        for (int i = 0; i < waves.Count && waves[i]; i++)
        {
            if (!waves[i].gameObject.activeSelf)
                waves[i].gameObject.SetActive(true);
            waves[i].State = WaveSate.SHOWINGNAME;
            StartCoroutine(ShowingWavesName(waves[i]));
            yield return new WaitUntil(() => waves[i].State == WaveSate.WAITING);
            yield return new WaitUntil(() => !AnyEnermyIsAlive());
            WaveCompleted(waves[i]);
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    public IEnumerator ShowingWavesName(Wave wave)
    {
        if (!wavesNameTextMPGUI.gameObject.activeSelf)
        {
            wavesNameTextMPGUI.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        wavesNameTextMPGUI.text = wave.WaveName;
        wavesNameTextMPGUI.enabled = true;
        yield return new WaitForSeconds(3f);
        wavesNameTextMPGUI.enabled = false;
        yield return new WaitForSeconds(1f);
        wave.State = WaveSate.SPAWNINGENERMY;
    }

    bool AnyEnermyIsAlive()
    {
        if (!GameObject.FindGameObjectWithTag("Enermy"))
        {
            return false;
        }
        return true;
    }

    void WaveCompleted(Wave wave) 
    {
        wave.State = WaveSate.DONE;
        wave.gameObject.SetActive(false);
    }
}
