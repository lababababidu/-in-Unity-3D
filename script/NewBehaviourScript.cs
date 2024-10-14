using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public Transform T;

    // 生成的位置
    [SerializeField] private Camera mainCamera;

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) // 0 表示鼠标左键，1 表示右键，2 表示中键
        {
            // 获取鼠标点击的屏幕位置
            Vector3 mousePosition = Input.mousePosition;

            // 将屏幕位置转换为世界坐标
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 在点击位置实例化预制件
                Vector3 hitpoint_v3 = hit.point;
                if(hitpoint_v3[0]>-5&&hitpoint_v3[0]<5&&hitpoint_v3[2]>-15&&hitpoint_v3[2]<-5){
                    
                    GameLogic logic = GetComponent<GameLogic>();

                    if(hitpoint_v3[0]>0){
                        hitpoint_v3[0] = (float)(int)hitpoint_v3[0]+0.5f;
                    }
                    else{
                        hitpoint_v3[0] = (float)(int)hitpoint_v3[0]-0.5f;
                    }
                    
                    hitpoint_v3[2] = (float)(int)hitpoint_v3[2]-0.5f;

                    if(logic.board[(int)(hitpoint_v3[0] + 0.5f - (-5))-1,(int)(hitpoint_v3[2] + 0.5f - (-15))-1] != 0){
                        Debug.Log("collision!");
                        return ;
                    }

                    GameObject clone = Instantiate(prefabToInstantiate, hitpoint_v3+ new Vector3(0,1,0), Quaternion.identity);

                    clone.tag = "clone";
                    Flip FlipScript = clone.GetComponent<Flip>();

                    FlipScript.col = (int)(hitpoint_v3[0] + 0.5f - (-5));
                    FlipScript.row = (int)(hitpoint_v3[2] + 0.5f - (-15));
                    
                    FlipScript.player = logic.CurrentPlayer;

                    
                    logic.board[FlipScript.col-1,FlipScript.row-1] = logic.CurrentPlayer;

                    logic.lastdropx = FlipScript.col-1;
                    logic.lastdropy = FlipScript.row-1;

                    logic.isdrop = true;

                }
                
            }
        }
    }
}
