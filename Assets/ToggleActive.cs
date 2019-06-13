using UnityEngine;
using UnityEngine.Serialization;

public class ToggleActive : MonoBehaviour
{ 
    private bool _isEasyMode;
    public GameObject objectToToggle;
    public GameObject text;
    // Start is called before the first frame update
    public void Toggle()
    {
        _isEasyMode = !_isEasyMode;
        objectToToggle.SetActive(!_isEasyMode);
        text.SetActive(_isEasyMode);
    }
}

