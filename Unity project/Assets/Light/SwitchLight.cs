using UnityEngine;
using System.Collections;

public class SwitchLight : MonoBehaviour {
	public Light [] lights;  // Main 

	public void switchLightMain(){  // 3
		lights[1].enabled = true;
		lights[2].enabled = false;
		lights[3].enabled = false;
		lights[4].enabled = false;
	}

	public void switchLightOne(){ // 1
		lights[1].enabled = false;
		lights[2].enabled = true;
		lights[3].enabled = false;
		lights[4].enabled = false;
	}

	public void switchLightTwo(){  // 2
		lights[1].enabled = false;
		lights[2].enabled = false;
		lights[3].enabled = true;
		lights[4].enabled = false;
	}

	public void switchLightThree(){  // 4
		lights[1].enabled = false;
		lights[2].enabled = false;
		lights[3].enabled = false;
		lights[4].enabled = true;
	}
}