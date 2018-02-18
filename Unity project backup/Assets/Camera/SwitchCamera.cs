using UnityEngine;
using System.Collections;

public class SwitchCamera : MonoBehaviour {
	public Camera [] cams;  // Main 

	public void moveCameraMain(){  // 3
		cams[1].enabled = true;
		cams[2].enabled = false;
		cams[3].enabled = false;
		cams[4].enabled = false;
		cams[5].enabled = false;
	}
		
	public void moveCameraOne(){ // 1
		cams[1].enabled = false;
		cams[2].enabled = true;
		cams[3].enabled = false;
		cams[4].enabled = false;
		cams[5].enabled = false;
	}

	public void moveCameraTwo(){  // 2
		cams[1].enabled = false;
		cams[2].enabled = false;
		cams[3].enabled = true;
		cams[4].enabled = false;
		cams[5].enabled = false;
	}

	public void moveCameraThree(){  // 4
		cams[1].enabled = false;
		cams[2].enabled = false;
		cams[3].enabled = false;
		cams[4].enabled = true;
		cams[5].enabled = false;
	}
	public void moveCameraFour(){  // 4
		cams[1].enabled = false;
		cams[2].enabled = false;
		cams[3].enabled = false;
		cams[4].enabled = false;
		cams[5].enabled = true;
	}
}