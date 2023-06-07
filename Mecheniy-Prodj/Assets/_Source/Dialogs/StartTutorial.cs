using _Source.Services;
using _Source.SignalsEvents.CoreEvents;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartTutorial : MonoBehaviour
{
    [SerializeField] private Button okBtn;
    [SerializeField] private TextMeshProUGUI dialogField;
    [SerializeField] private List<string> dialogs;

    private int _counter;

    void Start()
    {
        Signals.Get<OnPaused>().Dispatch();
        okBtn.onClick.AddListener(NextDialog);
        dialogField.text = dialogs[0];
    }


    private void NextDialog()
    {
        _counter++;
        if (_counter < dialogs.Count)
            dialogField.text = dialogs[_counter];
        else
        {
            gameObject.SetActive(false);
            Signals.Get<OnResume>().Dispatch();
            okBtn.onClick.RemoveListener(NextDialog);
        }
    }
}
