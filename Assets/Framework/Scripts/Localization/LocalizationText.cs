using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour {

    private string m_textName;

    private void Awake()
    {
        m_textName = gameObject.name;
    }

    private void Start()
    {
        GetComponent<Text>().text = LocalizationManager.Instance.GetValue(m_textName);
    }
}
