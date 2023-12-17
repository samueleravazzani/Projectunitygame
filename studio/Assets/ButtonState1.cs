using UnityEngine;

public class ButtonState1 : MonoBehaviour
{
    public GameObject buttonToActivate;
    public GameObject buttonToDeactivate;
    public GameLevelData levelData;
    

    void Start()
    {
        ClearGameData();
        buttonToDeactivate.SetActive(true);
        buttonToActivate.SetActive(false);
        int currentState = ButtonState.GetButtonState();
        if (currentState==1)
        {
            buttonToDeactivate.SetActive(false);
            buttonToActivate.SetActive(true);
        }
    }
        public void ClearGameData()
    {
        string profile = GameManager.instance.profile;
        DataSaver.ClearGameData(profile,levelData);
    }
}