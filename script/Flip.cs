using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    public int col ;
    public int row ;
    public int player;

    // private transform T ;
    private float duration = 0.15f; // 旋转的持续时间
    private bool isRotating = false; // 标记是否正在旋转
    
    // Start is called before the first frame update
    void Start()
    {
        if(player == 2){
            immediate_flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject boardobj = GameObject.Find("Board");
        if (boardobj != null)
        {
            GameLogic logic = boardobj.GetComponent<GameLogic>();
            for (int i = 0; i < logic.toflip.Count; i++)
            {
                if(logic.toflip[i].Item1+1==col&&logic.toflip[i].Item2+1==row){
                    logic.toflip.Remove(logic.toflip[i]);

                    player = 3 - player;
                    logic.board[col-1,row-1] = player;

                    if(!isRotating){
                        StartCoroutine(RotateOverTime(180f, duration));
                    }
                    break;
                }
            }
        }
    }

    IEnumerator RotateOverTime(float angle, float time)
    {
        isRotating = true; // 标记开始旋转

        Quaternion startRotation = transform.rotation; // 初始旋转
        Quaternion endRotation = startRotation * Quaternion.AngleAxis(angle, Vector3.left);  // 目标旋转

        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保最终旋转到目标角度
        transform.rotation = endRotation;
        
        isRotating = false; // 标记旋转结束
    }

    void immediate_flip(){
        this.transform.Rotate(Vector3.left*180);
    }

}
