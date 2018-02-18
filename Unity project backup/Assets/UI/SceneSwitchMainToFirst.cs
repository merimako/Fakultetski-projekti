using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchMainToFirst	 : MonoBehaviour
{
	public void NextScene()
	{
		SceneManager.LoadScene("Scene 1");
	}
}