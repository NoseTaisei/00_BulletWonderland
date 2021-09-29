using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVolumeCtrl : MonoBehaviour
{
	private	
    Slider slider;
 
    void Awake()
    {
        slider = GetComponent<Slider>();
    }
	void Start()
	{
		slider.value = SoundManager.instance.Volume;
	}
 
    public void OnValueChanged()
    {
    	SoundManager.instance.Volume = slider.value;
    }
}
