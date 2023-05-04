using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string nameEssentialScene;
    [SerializeField] string nameNewGameStartScene;

    [SerializeField] PlayerData playerData;
    public Gender slectedGender;
    public TMPro.TMP_Text genderText;
    public TMPro.TMP_InputField nameIputField;
    AsyncOperation operation;
    private void Start()
    {
        SetGenderFemale();
        UpdateName();
    }


    public void ExitGame()
    {
        Debug.Log("Çýkýþ yapýlýyor...");
        Application.Quit();
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(nameNewGameStartScene, LoadSceneMode.Single);
        SceneManager.LoadScene(nameEssentialScene, LoadSceneMode.Additive);
    }
    public void SetGenerMale()
    {
        slectedGender = Gender.Male;
        playerData.playerCharacterGender = slectedGender;
        genderText.text = "Male";
    }
    public void SetGenderFemale()
    {
        slectedGender = Gender.Female;
        playerData.playerCharacterGender = slectedGender;
        genderText.text = "Female";
    }

    public void UpdateName()
    {
        playerData.characterName = nameIputField.text;
    }
    public  void SetSavingSlot (int num)
    {
        playerData.saveSlotId = num;
    }

}
