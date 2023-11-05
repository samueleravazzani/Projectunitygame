using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResizeObject : MonoBehaviour
{
    public Vector3 initialScale;
    public Vector3 targetScale;
    public float expandTime = 3f;
    public float holdTime = 4f;
    public float holdTime1 = 0.5f;
    public float returnTime = 7f;
    private float startTime;
    public int repeatCount = 3;
    private int currentRepeatCount = 0;
    private bool done = false;
    public bool begin { get; private set; }
    
    [SerializeField] private TextMeshProUGUI RepeatText;
    private static ResizeObject instance;
    
    
    [SerializeField]
    private GameObject RepeatPanel;
    [SerializeField]
    private GameObject PhasePnael;

    private void Awake() //creation singleton
    {
        instance = this;
    }

    public static ResizeObject GetInstance()
    {
        return instance;
    }
    

    
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!DialogeManager.GetInstance().dialogueIsPlaying && !done)
        {
            done = true;
            StartCoroutine(AnimateObject());
        }

        
    }


    IEnumerator AnimateObject()
    {
        begin = true;
        while (currentRepeatCount < repeatCount)
        {
            RepeatText.text=string.Format("Repeat: {00}",repeatCount-currentRepeatCount);
            startTime = Time.time;
            yield return new WaitForSeconds(holdTime1);


            // Espansione graduale per 3 secondi
            while (Time.time - startTime < expandTime)
            {
                float progress = (Time.time - startTime) / expandTime;
                transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
                yield return null;
            }

            // Mantieni le dimensioni per 4 secondi
            yield return new WaitForSeconds(holdTime);

            startTime = Time.time;

            while (Time.time - startTime < returnTime)
            {
                float progress = (Time.time - startTime) / returnTime;
                transform.localScale = Vector3.Lerp(targetScale, initialScale, progress);
                yield return null;
            }
            
            yield return new WaitForSeconds(holdTime1);
            
            currentRepeatCount++;
        }
        begin = false;
        RepeatPanel.SetActive(false);
        PhasePnael.SetActive(false);
        timer.GetInstance().TimerText.text = "DONE!";

    }


}
