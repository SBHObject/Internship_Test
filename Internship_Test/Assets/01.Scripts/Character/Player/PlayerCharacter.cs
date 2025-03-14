using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private PlayerStatSO statData;
    public PlayerStatSO StatData { get { return statData; } }

    public PlayerContoller Controller { get; private set; }
    public InputController InputController { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<PlayerContoller>();
        InputController = GetComponent<InputController>();
    }
}
