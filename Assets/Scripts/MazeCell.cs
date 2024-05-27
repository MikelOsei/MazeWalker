using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private GameObject _frontWall;

    [SerializeField]
    private GameObject _backWall;

    [SerializeField]
    private GameObject _unvisitedBlock;

    [SerializeField]
    private GameObject wispObject;

    public List<MazeCell> neighbors; // list of "connected" cells

    public bool IsVisited { get; private set; }

    public bool inPath {get; set;}

    public void Visit() {
        IsVisited = true;
        wispObject.SetActive(false);
        _unvisitedBlock.SetActive(false);
    }

    public void ClearLeftWall() {
        _leftWall.SetActive(false);   
    }

    public void ClearRightWall() {
        _rightWall.SetActive(false);
    }

    public void ClearFrontWall() {
        _frontWall.SetActive(false);
    }

    public void ClearBackWall() {
        _backWall.SetActive(false);
    }

    public bool isClosed() {
        if (_frontWall.activeSelf && _leftWall.activeSelf && _backWall.activeSelf && _rightWall.activeSelf) return true;
        return false;
     }

    public void Close() {
        _unvisitedBlock.SetActive(true);
    }

    public void ActivateWisps() {
        wispObject.SetActive(true);
        ParticleSystem ps = wispObject.GetComponent<ParticleSystem>();
        var wisps = ps.main;
        ps.Play(true);
    }

    public void HideWisps() {
        wispObject.SetActive(false);
    }


}
