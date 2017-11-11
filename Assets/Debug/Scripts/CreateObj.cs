using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateObj : MonoBehaviour
{
	[SerializeField]
	private GameObject[] _debugObj = null;

    [SerializeField]
    private string _resetSceneName;

	public void OnCreateButton1()
	{
		Instantiate(_debugObj[0]);
	}

	public void OnCreateButton10()
	{
        for (int i = 0; i < 10; i++)
        {
			Instantiate(_debugObj[0]);
        }
	}

	public void OnCreateButton100()
	{
		for (int i = 0; i < 100; i++)
		{
			Instantiate(_debugObj[0]);
		}
	}

    public void OnResetButton()
    {
        SceneManager.LoadScene(_resetSceneName);
    }
}
