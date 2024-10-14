using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public int CurrentPlayer;

    public int lastdropx=0,lastdropy=0;

    public int[,] board = new int[10,10]
    {
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0}
        };

    public bool isdrop = false;

    private int winner = 0;

    public List<(int, int)> toflip;
    

    public static List<(int, int)> GetFlippedDiscs(int[,] board,int x, int y, int player)
    {
        int opponent = (player == 1) ? 2 : 1;
        List<(int, int)> flippedDiscs = new List<(int, int)>();
        
        // 8 directions: {dx, dy}
        int[][] directions = new int[][]
        {
            new int[] {0, 1},  // right
            new int[] {1, 1},  // down-right
            new int[] {1, 0},  // down
            new int[] {1, -1}, // down-left
            new int[] {0, -1}, // left
            new int[] {-1, -1},// up-left
            new int[] {-1, 0}, // up
            new int[] {-1, 1}  // up-right
        };

        // Check each direction
        foreach (var direction in directions)
        {
            List<(int, int)> tempFlipped = new List<(int, int)>();
            int dx = direction[0];
            int dy = direction[1];
            int nx = x + dx;
            int ny = y + dy;

            while (nx >= 0 && nx < board.GetLength(0) && ny >= 0 && ny < board.GetLength(1) && board[nx, ny] == opponent)
            {
                tempFlipped.Add((nx, ny));
                nx += dx;
                ny += dy;
            }

            if (nx >= 0 && nx < board.GetLength(0) && ny >= 0 && ny < board.GetLength(1) && board[nx, ny] == player)
            {
                flippedDiscs.AddRange(tempFlipped);
            }
        }
        return flippedDiscs;
    }
    void Start()
    {
        Init();
    }

    void OnGUI() {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float windowWidth = 200;
        float windowHeight = 200;
        if(!isover()){
            Debug.Log("not over");
            GUI.Box(new Rect((screenWidth - windowWidth) / 2, 25, 200, 25), "now player: "+CurrentPlayer.ToString());
        }
        else{
            Debug.Log("over");
            if (winner != 0)
                GUI.Box(new Rect(
                    (screenWidth - windowWidth) / 2,
                    (screenHeight - windowHeight) / 2,
                    windowWidth,
                    windowHeight
                ), "\n\n\n\n\nCongratulations!\n Player "+winner+" has won.");
            else
                GUI.Box(new Rect(
                    (screenWidth - windowWidth) / 2,
                    (screenHeight - windowHeight) / 2,
                    windowWidth,
                    windowHeight
                ), "\n\n\n\n\n\nThis is a draw!");
                if (GUI.Button(new Rect((screenWidth - 100) / 2, 270, 100, 30), "Restart")) Init();
        }

    }

    void Init() {
        CurrentPlayer = 1;
        winner = 0;

        for(int i = 0; i < 10; i++)
            for(int j = 0; j < 10; j++)
                board[i, j] = 0;

        GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag("clone");

        foreach (GameObject obj in objectsToDelete)
        {
            Destroy(obj);
        }
    }

    bool isover(){
        int P1_Score = 0;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if(board[i,j] == 0){
                    return false;
                }
                if(board[i,j] == 1){
                    P1_Score += 1;
                }
            }
        }
        if(P1_Score>50){
            winner = 1;
        }
        else if(P1_Score<50){
            winner = 2;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isdrop == true){
            List<(int, int)> t = GetFlippedDiscs(board,lastdropx,lastdropy,CurrentPlayer);
            toflip = t;

            CurrentPlayer = 3 - CurrentPlayer;
            
            isdrop = false;
        }   
    }
}
