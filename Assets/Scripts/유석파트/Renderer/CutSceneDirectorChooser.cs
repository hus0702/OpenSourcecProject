using UnityEngine;
using UnityEngine.Playables;
using Mirror;

public class CutSceneDirectorChooser : MonoBehaviour
{
    [SerializeField] private PlayableDirector directorForBlind;
    [SerializeField] private PlayableDirector directorForLimp;

    void Awake()
    {
        PlayerObjectController localPlayer = NetworkClient.localPlayer.GetComponent<PlayerObjectController>();

        if(localPlayer.Role == PlayerObjectController.Blind)
        {
            directorForBlind.Play();
        }
        else
        {
            directorForLimp.Play();
        }
    }
}
