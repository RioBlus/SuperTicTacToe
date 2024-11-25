using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using TMPro;
public class TicTacToe : MonoBehaviour
{
    public int win; // 0 , none ; 1 blue , 2 orange
    public int[,] Tmap = new int[3, 3];
    public GameObject Chessox;
    public GameObject Chesswall;
    public GameObject[,] OX = new GameObject[3, 3];
    public GameObject ChessEdge;
    public void build()
    {
        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                List<float> A = new List<float>() { -1.9f, 0, 1.9f };
                List<float> B = new List<float>() { 1.9f, 0, -1.9f };
                OX[i,j] = Instantiate(Chessox, Chesswall.transform.position,Quaternion.identity);
                OX[i,j].transform.Translate(new Vector3(A[i], B[j],0));
            }
        }
        Clean();
        win = 0;
    }

    public void Clean()
    {
        for(int i = 0;i<3;i++)
        {
            for (int j = 0;j<3;j++)
            {
                Tmap[i,j] = 0;
            }
        }
    }
    public int AskNone(int x,int y)
    {
        if (Tmap[x, y] == 0) return 1;
        return 0;
    }
    public void Place(int x,int y,int who)
    {
        Tmap[x, y] = who;
    }
    public int checkWin()
    {
        int k = 0;
        for( int i = 0;i<3; i++)
        {
            k = Tmap[i, 0] & Tmap[i, 1] & Tmap[i, 2];
            if( k == 1 )
            {
                win = 1;
                return win;
            }
            if(k == 2 )
            {
                win = 2;
                return win;
            }
            k = Tmap[0, i] & Tmap[1, i] & Tmap[2, i];
            if (k == 1)
            {
                win = 1;
                return win;
            }
            if (k == 2)
            {
                win = 2;
                return win;
            }
        }
        k = Tmap[0, 0] & Tmap[1,1] & Tmap[2, 2];
        if (k == 1)
        {
            win = 1;
            return win;
        }
        if (k == 2)
        {
            win = 2;
            return win;
        }
        k = Tmap[0, 2] & Tmap[1, 1] & Tmap[2, 0];
        if(k == 1)
        {
            win = 1;
            return win;
        }
        if(k ==2)
        {
            win = 2;
            return win;
        }
        return win;
    }
    public int Assert(int x, int y,int who)
    {
        int[,] tmpTmap = new int[3,3];
        for(int i=0; i<3; i++)
        {
            for(int j=0; j<3; j++)
            {
                tmpTmap[i,j] = Tmap[i,j]; 
            }
        }
        tmpTmap[x,y] = who;
        int k = 0;
        for (int i = 0; i < 3; i++)
        {
            k = tmpTmap[i, 0] & tmpTmap[i, 1] & tmpTmap[i, 2];
            if (k == 1)
            {
                return 1;
            }
            if (k == 2)
            {
                return 2;
            }
            k = tmpTmap[0, i] & tmpTmap[1, i] & tmpTmap[2, i];
            if (k == 1)
            {
                return 1;
            }
            if (k == 2)
            {
                return 2;
            }
        }
        k = tmpTmap[0, 0] & tmpTmap[1, 1] & tmpTmap[2, 2];
        if (k == 1)
        {
            return 1;
        }
        if (k == 2)
        {
            return 2;
        }
        k = tmpTmap[0, 2] & tmpTmap[1, 1] & tmpTmap[2, 0];
        if (k == 1)
        {
            return 1;
        }
        if (k == 2)
        {
            return 2;
        }
        return 0;
    }
}

public class MainGame : MonoBehaviour
{
    //TicTacToe[,] tictactoe = new TicTacToe[3, 3];
    GameObject[,] chess = new GameObject[3, 3];
    public GameObject Chesswall;
    public GameObject Chessox;
    public GameObject ChessEdge;
    public List<Sprite> pic = new List<Sprite>();
    public List<Sprite> picEgde = new List<Sprite>();
    void Start()
    {
        for(int i = 0;i<3;i++)
        {
            for(int j = 0;j<3;j++)
            {
                List<float> A = new List<float>() { -4.93f - 6.6f, -4.93f, -4.93f + 6.6f};
                List<float> B = new List<float>() { 0f + 6.6f, 0f, 0f - 6.6f };
                chess[i, j] = Instantiate(Chesswall);
                chess[i, j].transform.SetPositionAndRotation(new Vector3(A[i], B[j], 0), Quaternion.identity);

                
                chess[i,j].AddComponent<TicTacToe>();
                chess[i, j].GetComponent<TicTacToe>().Chesswall = chess[i,j];
                chess[i, j].GetComponent<TicTacToe>().Chessox = Chessox;
                chess[i, j].GetComponent <TicTacToe>().ChessEdge = Instantiate(ChessEdge, chess[i,j].transform.position,Quaternion.identity);
                chess[i, j].GetComponent<TicTacToe>().build();
                //tictactoe[i, j].Chess = chess[i, j];
                //tictactoe[i, j]TicTacToe(ChessChess);
            }
        }

    }
    int CanPlaceX = -1, CanPlaceY = -1 , NowWhoPlay = 2;// 0 , none ; 1 blue O , 2 orange X
    public void SetChess(GameObject nwObject,int chess,int tcolor,int touming)
    {
        // tcolor = 1 -> black
        SpriteRenderer nwspriteRenderer = nwObject.GetComponent<SpriteRenderer>();
        
        if(touming == 1)
        {
            nwspriteRenderer.color = new UnityEngine.Color(nwspriteRenderer.color.r, nwspriteRenderer.color.g, nwspriteRenderer.color.b, 0.3f);
            if(tcolor == 1)
            {
                nwspriteRenderer.color = new UnityEngine.Color(nwspriteRenderer.color.r, nwspriteRenderer.color.g, nwspriteRenderer.color.b, 0.8f);
            }
        }
        else
        {
            nwspriteRenderer.color = new UnityEngine.Color(nwspriteRenderer.color.r, nwspriteRenderer.color.g, nwspriteRenderer.color.b, 1);
        }
        if (chess == 1)
        {
            if (tcolor == 0)
            {
                nwspriteRenderer.sprite = pic[1];
            }
            else
            {
                nwspriteRenderer.sprite = pic[0];
            }
        }
        if (chess == 2)
        {
            if (tcolor == 0)
            {
                nwspriteRenderer.sprite = pic[3];
            }
            else
            {
                nwspriteRenderer.sprite = pic[2];
            }
        }
        if (chess == 0)
        {
            nwspriteRenderer.sprite = pic[4];
        }
    }
    public int[,] tagWin= new int[3,3];
    public int TagWin = 0;
    private void EndGame()
    {
        TextMeshProUGUI myText2 = text2.GetComponent<TextMeshProUGUI>();
        myText2.text = "恭喜赢啦";
        Destroy(text3);

    }
    private int CheckFinalWin()
    {
        int[,] tmpTmap = new int[3, 3];
        for(int i = 0;i<3;i++)
        {
            for(int j = 0;j<3;j++)
            {
                TicTacToe nw = chess[i, j].GetComponent<TicTacToe>();
                tmpTmap[i,j] = nw.win;
                tagWin[i, j] = 0;
            }
        }
        int k = 0;
        for (int i = 0; i < 3; i++)
        {
            k = tmpTmap[i, 0] & tmpTmap[i, 1] & tmpTmap[i, 2];
            if (k == 1)
            {
                tagWin[i, 0] = tagWin[i, 1] = tagWin[i, 2] = 1;
                return 1;
            }
            if (k == 2)
            {
                tagWin[i, 0] = tagWin[i, 1] = tagWin[i, 2] = 2;
                return 2;
            }
            k = tmpTmap[0, i] & tmpTmap[1, i] & tmpTmap[2, i];
            if (k == 1)
            {
                tagWin[0, i] = tagWin[1, i] = tagWin[2, i] = 1;
                return 1;
            }
            if (k == 2)
            {
                tagWin[0, i] = tagWin[1, i] = tagWin[2, i] = 2;
                return 2;
            }
        }
        k = tmpTmap[0, 0] & tmpTmap[1, 1] & tmpTmap[2, 2];
        if (k == 1)
        {
            tagWin[0, 0] = tagWin[1, 1] = tagWin[2, 2] = 1;
            return 1;
        }
        if (k == 2)
        {
            tagWin[0, 0] = tagWin[1, 1] = tagWin[2, 2] = 2;
            return 2;
        }
        k = tmpTmap[0, 2] & tmpTmap[1, 1] & tmpTmap[2, 0];
        if (k == 1)
        {
            tagWin[0, 2] = tagWin[1, 1] = tagWin[2, 0] = 1;
            return 1;
        }
        if (k == 2)
        {
            tagWin[0, 2] = tagWin[1, 1] = tagWin[2, 0] = 2;
            return 2;
        }
        return 0;
    }
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && TagWin == 0)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    TicTacToe nw = chess[i, j].GetComponent<TicTacToe>();
                    if ((CanPlaceX == i && CanPlaceY == j || CanPlaceX + CanPlaceY == -2) && nw.win == 0)
                    {
                        for (int s = 0; s < 3; s++)
                        {
                            for (int v = 0; v < 3; v++)
                            {
                                
                                GameObject nwObject = nw.OX[s, v];
                                int svOX = nw.Tmap[s, v];
                                if(hit.collider.GameObject() == nwObject && svOX == 0)
                                {
                                    nw.Tmap[s, v] = NowWhoPlay;
                                    if(nw.checkWin() > 0 )
                                    {
                                        //Destroy(nw.Chesswall);
                                        SpriteRenderer nwRenderer = nw.Chesswall.GetComponent<SpriteRenderer>();
                                        nwRenderer.color = new UnityEngine.Color(nwRenderer.color.r, nwRenderer.color.g, nwRenderer.color.b, 1f);
                                        Destroy(nw.Chesswall.GetComponent<Animator>());
                                        nw.Chesswall.GetComponent<Transform>().localScale += Vector3.one * 3;
                                        Debug.Log(nw.win);
                                        if(nw.win == 1)nwRenderer.sprite = pic[1];
                                        else nwRenderer.sprite = pic[3];
                                        for (int ss = 0; ss < 3; ss++)
                                        {
                                            for (int vv = 0; vv < 3; vv++)
                                            {
                                                Destroy(nw.OX[ss, vv]);
                                            }
                                        }
                                        TagWin = CheckFinalWin();
                                        if(TagWin > 0)
                                        {
                                            EndGame();
                                        }
                                    }
                                    NowWhoPlay = 3 - NowWhoPlay;
                                    CanPlaceX = s;
                                    CanPlaceY = v;
                                    if(chess[s, v].GetComponent<TicTacToe>().win >0)
                                    {
                                        CanPlaceX = -1;
                                        CanPlaceY = -1;
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
        }
    }
    public GameObject text1,text2,text3;
    private void FixedUpdate()
    {
        //GameObject nwObject = chess[1, 1].GetComponent<TicTacToe>().OX[1, 1];
        //SetChess(nwObject, NowWhoPlay, 0, 1);
        //if (NowWhoPlay == 2) NowWhoPlay--;
        //else NowWhoPlay++;
        //Debug.Log(NowWhoPlay);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        int maybenewX = -2, maybenewY = -2;
        //Debug.Log((CanPlaceX, CanPlaceY));
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (TagWin > 0) continue;

                TicTacToe nw = chess[i, j].GetComponent<TicTacToe>();
                if ((CanPlaceX == i && CanPlaceY == j || CanPlaceX + CanPlaceY == -2) && nw.win == 0)
                {
                    
                    for (int s = 0; s < 3; s++)
                    {
                        for (int v = 0; v < 3; v++)
                        {
                            GameObject nwObject = nw.OX[s, v];
                            int svOX = nw.Tmap[s, v];
                            if (hit.collider.GameObject() == nwObject && svOX == 0) svOX = svOX + 1 - 1;
                            else continue;
                            if (chess[s, v].GetComponent<TicTacToe>().win == 0 && (i != s || j != v) || i == s && j == v && nw.Assert(i, j, NowWhoPlay) == 0)
                            {
                                maybenewX = s; maybenewY = v;
                            }
                            else
                            {
                                maybenewX = -1; maybenewY = -1;
                            }
                        }
                    }
                }
            }
        }
        //Debug.Log((maybenewX, maybenewY));
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                TicTacToe nw = chess[i, j].GetComponent<TicTacToe>();
                Animator ChessWallanimator = chess[i, j].GetComponent<Animator>();
                if (TagWin > 0)
                {
                    SpriteRenderer edgespriteRenderer = nw.ChessEdge.GetComponent<SpriteRenderer>();
                    edgespriteRenderer.sprite = picEgde[TagWin];
                    if (tagWin[i, j] != 0)
                    {
                        nw.ChessEdge.SetActive(true);
                    }
                    else
                    {
                        nw.ChessEdge.SetActive(false);
                    }
                    continue;
                }
                if (nw.win == 0)
                {
                    if (i == maybenewX && j == maybenewY || maybenewX + maybenewY == -2)
                    {
                        nw.ChessEdge.SetActive(true);
                    }
                    else
                    {
                        nw.ChessEdge.SetActive(false);
                    }
                    SpriteRenderer nwspriteRenderer = nw.Chesswall.GetComponent<SpriteRenderer>();
                    if (maybenewX == i && maybenewY == j || maybenewX == -1 && maybenewY == -1)
                    {
                        nwspriteRenderer.color = new UnityEngine.Color(nwspriteRenderer.color.r, nwspriteRenderer.color.g, nwspriteRenderer.color.b, 1f);
                    }
                    else
                    {
                        nwspriteRenderer.color = new UnityEngine.Color(nwspriteRenderer.color.r, nwspriteRenderer.color.g, nwspriteRenderer.color.b, 0.8f);
                    }
                    SpriteRenderer edgespriteRenderer = nw.ChessEdge.GetComponent<SpriteRenderer>();
                    if ((CanPlaceX == i && CanPlaceY == j || CanPlaceX + CanPlaceY == -2))
                    {
                        ChessWallanimator.SetInteger("ChessWallState", NowWhoPlay);
                        edgespriteRenderer.sprite = picEgde[NowWhoPlay];
                        //Debug.Log(NowWhoPlay);
                        for (int s = 0; s < 3; s++)
                        {
                            for (int v = 0; v < 3; v++)
                            {
                                GameObject nwObject = nw.OX[s, v];
                                int svOX = nw.Tmap[s, v];
                                if (hit.collider.GameObject() == nwObject && svOX == 0)
                                {
                                    SetChess(nwObject, NowWhoPlay, 0, 1);
                                    //Debug.Log((i,j,s,v));
                                }
                                else if (hit.collider.GameObject() != nwObject && svOX == 0)
                                {
                                    SetChess(nwObject, 0, 0, 0);
                                }
                                else
                                {
                                    SetChess(nwObject, svOX, 0, 0);
                                }
                            }
                        }
                    }
                    else
                    {
                        edgespriteRenderer.sprite = picEgde[0];
                        ChessWallanimator.SetInteger("ChessWallState", 0);
                        for (int s = 0; s < 3; s++)
                        {
                            for (int v = 0; v < 3; v++)
                            {
                                GameObject nwObject = nw.OX[s, v];
                                int svOX = nw.Tmap[s, v];
                                if(i == maybenewX && j == maybenewY || maybenewX + maybenewY == -2)
                                SetChess(nwObject, svOX, 1, 0);
                                else SetChess(nwObject, svOX, 1, 1);
                            }
                        }
                    }
                }
                else
                {
                    nw.ChessEdge.SetActive(false);
                    SpriteRenderer nwspriteRenderer = nw.Chesswall.GetComponent<SpriteRenderer>();
                    nwspriteRenderer.color = new UnityEngine.Color(nwspriteRenderer.color.r, nwspriteRenderer.color.g, nwspriteRenderer.color.b, 1f);
                }
            }
        }
        if(TagWin == 0)
        {
            TextMeshProUGUI myText1 = text1.GetComponent<TextMeshProUGUI>();
            Debug.Log(myText1);
            if(NowWhoPlay == 1)
            {
                myText1.text = "圆圈" ;
                myText1.color = new UnityEngine.Color(0, 162.0f / 255f, 232.0f / 255f, 1);
            }
            else
            {
                myText1.text = "叉叉";
                myText1.color = new UnityEngine.Color(232.0f/255f, 157.0f/255f,0,1);
            }
        }
    }
}
