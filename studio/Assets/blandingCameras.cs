using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera camera1;
    public CinemachineVirtualCamera camera2;
    private float transitionDuration = 0.5f;
    private GameObject VirtualCameraObject;
    private bool isCamera1Active = true;


    void Start()
    {
        // Assicurati che le telecamere siano assegnate nell'Inspector
        VirtualCameraObject=GameObject.Find("Virtual Camera");
        camera1=VirtualCameraObject.GetComponent<CinemachineVirtualCamera>();
        if (camera1 == null || camera2 == null)
        {
            Debug.LogError("Assegna le telecamere nel componente CameraSwitcher nell'Inspector.");
        }
    }

    public void OnSwitchButtonPress()
    {
        // Avvia la coroutine per il blending graduale
        StartCoroutine(SwitchCameras());
    }

    IEnumerator SwitchCameras()
    {
        // Determina le telecamere coinvolte nella transizione
        CinemachineVirtualCamera fromCamera = isCamera1Active ? camera1 : camera2;
        CinemachineVirtualCamera toCamera = isCamera1Active ? camera2 : camera1;
        

        // Attendere un breve momento prima di iniziare il blending
        //yield return null;

        // Gradualmente diminuisci la priorità della telecamera di partenza e aumenta quella della destinazione
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float blend = Mathf.Clamp01(elapsedTime / transitionDuration);

            fromCamera.Priority = (int)Mathf.Lerp(10, 5, blend);
            toCamera.Priority = (int)Mathf.Lerp(5, 10, blend);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Assicurati che le priorità siano impostate correttamente alla fine
        fromCamera.Priority = 5;
        toCamera.Priority = 10;

        // Inverti lo stato della telecamera attiva
        isCamera1Active = !isCamera1Active;
    }
}