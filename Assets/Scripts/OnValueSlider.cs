using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnValueSlider : MonoBehaviour {
	public void ChangeValue (float n) {
        gameObject.GetComponent<Text>().text = "" + n;
	}
}
