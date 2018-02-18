using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu	 : MonoBehaviour
{
	public void NextScene()
	{
		SceneManager.LoadScene("MainMenu");
	}
}