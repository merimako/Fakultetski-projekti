using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CubeController : MonoBehaviour {

	// cube pieces
	private Transform[] centerP ;
	private Transform[] cornerP ;
	private Transform[] edgeP;
	private Transform cube_3x3x3;
	
	// rotation variables
	private float sensitivityX = 15.0f;
	private float sensitivityY = 15.0f;
    [HideInInspector]
	public int[] rotationSpeeds;
   
	private int speed = 10;
	private int count = 10;
	private Transform cameraTm;
	private bool rotate = false;
	private bool rotateInit = false;

	// scrambling
	private List<int> scrambleList;
	private List<bool> scrambleReverseList;
	private bool scramble = false;
	
	// move selection indicator
	private int selection = -1;
	
	// rotation speed drop down initial selection , the speed list contains index to the rotation speed list
    public enum speedList {
        _1 = 0,_2 = 1,_3 = 2,_5 = 3,_6 = 4,_9 = 5,_10 = 6,_15 = 7,_18 = 8,_30 = 9,_45 = 10,_90 = 11
    }

    public speedList speedOption;

    [HideInInspector]
    public int selectedItemIndex;
	
	// reverse control
	private GameObject reverseObj;
	public bool reverse = false;
	
	/* 2D Cube Logic used to map cubies to parents , 
	 * another solution is to go with cube colliders and triggers (semi-unreliable + time consuming to debug)*/
	private Cube2D cube2d;
	private string[,] face;
	

	
	// Audio Controlls for BGM/SFX
	private AudioController audioCon;
	
	//  Variables to Simulate Piston Effect
    public Toggle piston;
	private int state = -1;
	private bool stateOddOrEven = false; // false -> Even | true -> Odd
	private int countState = 0;
	

	// AI Solve
	private bool solveMode = false;
	private List<int> solveMoveList;
	private List<bool> solveMoveReverseList;
    public Text solved;

	void Start () {  

		// initialize 2D RB Logic
		cube2d = new Cube2D ();
		
		// initialize pieces
		initPieces ();
        

		// initialize game objects
		initETC ();
		
		// initialize rotation speed list and combo box
		initSpeedList ();

	}

	
	
	private void initETC(){
		
		cube_3x3x3 = GameObject.Find ("Cube 3x3x3").transform;
		reverseObj = GameObject.Find("reverse");

		scrambleList = new List<int>();
		scrambleReverseList = new List<bool>();

		solveMoveList = new List<int> ();
		solveMoveReverseList = new List<bool> ();

		cameraTm = Camera.main.transform;
        

        selectedItemIndex = (int)speedOption;
     
	}

	private void initPieces(){

		centerP = new Transform[6];
		cornerP = new Transform[8];
		edgeP = new Transform[12];
//		Transform core = GameObject.FindGameObjectWithTag ("Core").transform;
		for (int i = 0; i < 6; i++) {
			centerP[i] = GameObject.FindGameObjectWithTag ("Center Piece " + (i + 1)).transform;
			 
		}

		for (int i = 0; i < 8; i++) {
			cornerP[i] = GameObject.FindGameObjectWithTag ("Corner Piece " + (i + 1)).transform;
		
		}

		for (int i = 0; i < 12; i++) {
			edgeP[i] = GameObject.FindGameObjectWithTag ("Edge Piece " + (i + 1)).transform;
			
		}
	}
    
	
	private void initSpeedList(){
		// The factors of 90. Answer : 1,2,3,5,6,9,10,15,18,30,45,90
		/* using facotrs of 90 to properly rotate the cube, other values non factor of 90 are suspect of 
		 * floating point precision limitation which can cause marginal precision errors. -> wow
		 */ 
		rotationSpeeds = new int[]{1,2,3,5,6,9,10,15,18,30,45,90};		
	}

	void Update () {

		if (scramble && !rotate)
			if(scrambleList.Count > 0){
				selection = scrambleList[0];
				scrambleList.RemoveAt(0);
				reverse = scrambleReverseList[0];
				scrambleReverseList.RemoveAt(0);
				rotate = true;
			} else {
				scramble = false;
				rotate = false;
				reverse = scrambleReverseList[0];
				scrambleReverseList.RemoveAt(0);
			}

		if (solveMode && !rotate) {
			if(solveMoveList.Count > 0){
				selection = solveMoveList[0];
				solveMoveList.RemoveAt(0);
				reverse = solveMoveReverseList[0];
				solveMoveReverseList.RemoveAt(0);
				rotate = true;
			}else{
				solveMode = false;
				rotate = false;
				reverse = solveMoveReverseList[0];
				solveMoveReverseList.RemoveAt(0);
			}
		}

		controls ();
		rotation ();
	}

	// Keyboard inputs
	private void controls(){
		if (Input.GetKeyDown (KeyCode.R) && !rotate) {
			selection = 0;
			rotate = true;
		} else if (Input.GetKeyDown (KeyCode.L) && !rotate) {
			selection = 2;
			rotate = true;
		} else if (Input.GetKeyDown (KeyCode.U) && !rotate) {
			selection = 5;
			rotate = true;
		} else if (Input.GetKeyDown (KeyCode.D) && !rotate) {
			selection = 4;
			rotate = true;
		} else if (Input.GetKeyDown (KeyCode.F) && !rotate) {
			selection = 3;
			rotate = true;
		} else if (Input.GetKeyDown (KeyCode.B) && !rotate) {
			selection = 1;
			rotate = true;
		} else if (Input.GetKeyDown (KeyCode.LeftControl) && !rotate) {
			if (!reverse) {
				if (reverseObj != null)
					reverseObj.GetComponentInChildren<Text> ().text = "Reverse On";
				reverse = true;
			} else if (reverse) {
				if (reverseObj != null)
					reverseObj.GetComponentInChildren<Text> ().text = "Reverse Off";
				reverse = false;
			}
		}
	}



	private void rotation(){

		
		// rotate cube face
		if (rotate) {
			if(!rotateInit){
                
				// get selected face
				face = cube2d.getFace(selection);
				
				for(int i = 0;i < 3;i++){
					for(int j = 0;j < 3;j++){
						// assign face cubies to parent
						GameObject.FindWithTag(face[i,j]).transform.parent = centerP[selection];
					}
				}

				// rotate 2d cube

				cube2d.Rotate(selection);
				if(reverse){ // a little cheat so I don't have to code too much = slight performance impact :]
					cube2d.Rotate(selection);
					cube2d.Rotate(selection);
				}

				
				count = speed = rotationSpeeds [selectedItemIndex]; // set rotation speed
				rotateInit = true;

				// play cube turning sound if sfx is enabled
				if(audioCon != null && audioCon.enableSFX)
					audioCon.sfxsource.PlayOneShot(audioCon.rubikturnsfx);

				// calculate if increments total results in odd or even number
				state = 90 / speed;
				stateOddOrEven = state % 2 == 0 ? false : true;
			}
			
			// rotate face
			centerP [selection].Rotate(new Vector3(reverse ? -speed : speed, 0 , 0));
			countState++;

			

			// check if rotation should finish to reset state
			if(count >= 90){
				resetParents();
				rotateInit = false;
				rotate = false;
				count = 0;
				countState = 0;
                if (piston != null)
			    	piston.interactable = true;

				if(cube2d.isSolved()){
                    if(solved != null)
                        solved.enabled = true;
				
				}else{
                    if (solved != null)
                        solved.enabled = false;
				}
				
			}

			// update count flag to keep up with rotation bounds
			count += speed;
		}
        
	}

	
	// set face pieces parents to main cube to avoid any unwanted rotation bugs/glitches
	private void resetParents(){
		for(int i = 0; i < 3; i++)
			for(int j = 0; j < 3; j++)
				GameObject.FindWithTag(face[i,j]).transform.parent = cube_3x3x3;
	}
	
	public void onButtonClick(string button){
		int result;
		if (!rotate && int.TryParse (button, out result)) {
			// assign selected face to be rotated
			selection = result;
			rotate = true;
		} else {
			// check string conditions and apply changes accordingly
			if(button.Equals("reset")){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

			}else if(!rotate && button.Equals("reverse")){
				if(!reverse){
					reverseObj.GetComponentInChildren<Text>().text = "Reverse On";
					reverse = true;
				}else if(reverse){
					reverseObj.GetComponentInChildren<Text>().text = "Reverse Off";
					reverse = false;
				}
			}else if(!rotate && button.Equals("scramble")){
				// trigger scramble mode
				scramble = true;
				// give a number of random faces to be rotated , populate scramble reverse list
				for(int i = 0;i < 30;i++){
					scrambleList.Add(Random.Range(0 , 6));	
					scrambleReverseList.Add(Random.Range(0 , 2) == 1 ? true : false);
				}
				scrambleReverseList.Add(reverse); // store current reverse status for later reset
			}
		}
	}

    


	

	
}