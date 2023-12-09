using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResizeObject : MonoBehaviour
{
    public Vector3 initialScale;
    public Vector3 targetScale;
    public float expandTime = 4f;
    public float holdTime = 7f;
    public float holdTime1 = 0.5f;
    public float returnTime = 8f;
    private float startTime;
    public int repeatCount;
    private int currentRepeatCount = 0;
    private bool done = false;
    private bool go = false;
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
        if (!DialogeManager.GetInstance().dialogueIsPlaying && !done && go)
        {
            done = true;
            repeatCount = GestioneCanvasBreathing.GetInstance().y;
            StartCoroutine(AnimateObject());
        }

        go = true;
    }


    IEnumerator AnimateObject()
    {
        begin = true;
        while (currentRepeatCount < repeatCount)
        {
            RepeatText.text=string.Format("Repeat: {00}",repeatCount-currentRepeatCount);
            //startTime = Time.time;
            yield return new WaitForSeconds(holdTime1);
            startTime = Time.time;


            // Espansione graduale per 4 secondi
            while (Time.time - startTime <= expandTime)
            {
                float progress = (Time.time - startTime) / expandTime;
                transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
                yield return null;
            }

            // Mantieni le dimensioni per 7 secondi
            yield return new WaitForSeconds(holdTime);

            startTime = Time.time;

            while (Time.time - startTime <= returnTime)
            {
                float progress = (Time.time - startTime) / returnTime;
                transform.localScale = Vector3.Lerp(targetScale, initialScale, progress);
                yield return null;
            }
            
            //yield return new WaitForSeconds(holdTime1);
            
            currentRepeatCount++;
        }
        begin = false;
        RepeatPanel.SetActive(false);
        PhasePnael.SetActive(false);
        timer.GetInstance().TimerText.text = "DONE!";
        yield return new WaitForSeconds(1);
        GestioneCanvasBreathing.GetInstance().winning();

    }


}
