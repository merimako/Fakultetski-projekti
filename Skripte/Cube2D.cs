
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cube2D
{

    
    // Main structure
    private string[,] cube1 = new string[3, 3];
    private string[,] cube2 = new string[3, 3];
    private string[,] cube3 = new string[3, 3];

    // Backup structure for AI Solving reasons
    private string[,] cubeA = new string[3, 3];
    private string[,] cubeB = new string[3, 3];
    private string[,] cubeC = new string[3, 3];

    // Use this for initialization
    public Cube2D()
    {
        initArray();
        init2DTilesCube();
    }

    private void initArray()
    {

        //center pieces
        cube2[1, 2] = "Center Piece 1";
        cube3[1, 1] = "Center Piece 2";
        cube2[1, 0] = "Center Piece 3";
        cube1[1, 1] = "Center Piece 4";
        cube2[2, 1] = "Center Piece 5";
        cube2[0, 1] = "Center Piece 6";

        //corner pieces
        cube3[0, 2] = "Corner Piece 1";
        cube1[0, 2] = "Corner Piece 2";
        cube1[2, 2] = "Corner Piece 3";
        cube3[2, 2] = "Corner Piece 4";
        cube3[0, 0] = "Corner Piece 5";
        cube3[2, 0] = "Corner Piece 6";
        cube1[0, 0] = "Corner Piece 7";
        cube1[2, 0] = "Corner Piece 8";

        //edge pieces
        cube2[0, 2] = "Edge Piece 1";
        cube2[2, 2] = "Edge Piece 2";
        cube3[1, 2] = "Edge Piece 3";
        cube1[1, 2] = "Edge Piece 4";
        cube2[0, 0] = "Edge Piece 5";
        cube2[2, 0] = "Edge Piece 6";
        cube1[1, 0] = "Edge Piece 7";
        cube3[1, 0] = "Edge Piece 8";
        cube3[0, 1] = "Edge Piece 9";
        cube3[2, 1] = "Edge Piece 10";
        cube1[2, 1] = "Edge Piece 11";
        cube1[0, 1] = "Edge Piece 12";

        //core piece
        cube2[1, 1] = "Void";

    }

    public string[,] getFace(int face)
    {
        if (face == 0)
        {
            return getBlueFace();
        }
        else if (face == 1)
        {
            return getOrangeFace();
        }
        else if (face == 2)
        {
            return getGreenFace();
        }
        else if (face == 3)
        {
            return getRedFace();
        }
        else if (face == 4)
        {
            return getYellowFace();
        }
        else if (face == 5)
        {
            return getWhiteFace();
        }
        return null;
    }

    public string[,] getRedFace()
    {
        string[,] arr = new string[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                arr[i, j] = cube1[i, j];
        return arr;
    }

    public string[,] getOrangeFace()
    {
        string[,] arr = new string[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                arr[i, j] = cube3[i, j];
        return arr;
    }

    public string[,] getBlueFace()
    {
        string[,] arr = new string[3, 3];
        arr[0, 0] = cube1[0, 2]; arr[0, 1] = cube2[0, 2]; arr[0, 2] = cube3[0, 2];
        arr[1, 0] = cube1[1, 2]; arr[1, 1] = cube2[1, 2]; arr[1, 2] = cube3[1, 2];
        arr[2, 0] = cube1[2, 2]; arr[2, 1] = cube2[2, 2]; arr[2, 2] = cube3[2, 2];
        return arr;
    }

    public string[,] getGreenFace()
    {
        string[,] arr = new string[3, 3];
        arr[0, 0] = cube1[0, 0]; arr[0, 1] = cube2[0, 0]; arr[0, 2] = cube3[0, 0];
        arr[1, 0] = cube1[1, 0]; arr[1, 1] = cube2[1, 0]; arr[1, 2] = cube3[1, 0];
        arr[2, 0] = cube1[2, 0]; arr[2, 1] = cube2[2, 0]; arr[2, 2] = cube3[2, 0];
        return arr;
    }

    public string[,] getWhiteFace()
    {
        string[,] arr = new string[3, 3];
        arr[0, 0] = cube1[0, 0]; arr[0, 1] = cube2[0, 0]; arr[0, 2] = cube3[0, 0];
        arr[1, 0] = cube1[0, 1]; arr[1, 1] = cube2[0, 1]; arr[1, 2] = cube3[0, 1];
        arr[2, 0] = cube1[0, 2]; arr[2, 1] = cube2[0, 2]; arr[2, 2] = cube3[0, 2];
        return arr;
    }

    public string[,] getYellowFace()
    {
        string[,] arr = new string[3, 3];
        arr[0, 0] = cube1[2, 0]; arr[0, 1] = cube2[2, 0]; arr[0, 2] = cube3[2, 0];
        arr[1, 0] = cube1[2, 1]; arr[1, 1] = cube2[2, 1]; arr[1, 2] = cube3[2, 1];
        arr[2, 0] = cube1[2, 2]; arr[2, 1] = cube2[2, 2]; arr[2, 2] = cube3[2, 2];
        return arr;
    }

    public void Rotate(int face)
    {
        if (face == 3)
        { //red
          //rotate corners
            string temp = cube1[0, 0];
            cube1[0, 0] = cube1[2, 0];
            cube1[2, 0] = cube1[2, 2];
            cube1[2, 2] = cube1[0, 2];
            cube1[0, 2] = temp;

            //rotate edges
            temp = cube1[0, 1];
            cube1[0, 1] = cube1[1, 0];
            cube1[1, 0] = cube1[2, 1];
            cube1[2, 1] = cube1[1, 2];
            cube1[1, 2] = temp;

            //rotate red face tiles
            rotate2DTRed();
        }
        else if (face == 0)
        {//blue

            //rotate corners
            string temp = cube1[0, 2];
            cube1[0, 2] = cube1[2, 2];
            cube1[2, 2] = cube3[2, 2];
            cube3[2, 2] = cube3[0, 2];
            cube3[0, 2] = temp;

            //rotate edges
            temp = cube2[0, 2];
            cube2[0, 2] = cube1[1, 2];
            cube1[1, 2] = cube2[2, 2];
            cube2[2, 2] = cube3[1, 2];
            cube3[1, 2] = temp;

            //rotate blue face tiles
            rotate2DTBlue();
        }
        else if (face == 1)
        {//orange
         //rotate corners
            string temp = cube3[0, 2];
            cube3[0, 2] = cube3[2, 2];
            cube3[2, 2] = cube3[2, 0];
            cube3[2, 0] = cube3[0, 0];
            cube3[0, 0] = temp;

            //rotate edges
            temp = cube3[0, 1];
            cube3[0, 1] = cube3[1, 2];
            cube3[1, 2] = cube3[2, 1];
            cube3[2, 1] = cube3[1, 0];
            cube3[1, 0] = temp;

            //rotate orange face tiles
            rotate2DTOrange();
        }
        else if (face == 2)
        {//green
         //rotate corners
            string temp = cube3[0, 0];
            cube3[0, 0] = cube3[2, 0];
            cube3[2, 0] = cube1[2, 0];
            cube1[2, 0] = cube1[0, 0];
            cube1[0, 0] = temp;

            //rotate edges
            temp = cube2[0, 0];
            cube2[0, 0] = cube3[1, 0];
            cube3[1, 0] = cube2[2, 0];
            cube2[2, 0] = cube1[1, 0];
            cube1[1, 0] = temp;

            //rotate green face tiles
            rotate2DTGreen();
        }
        else if (face == 5)
        {//white
         //rotate corners
            string temp = cube1[0, 0];
            cube1[0, 0] = cube1[0, 2];
            cube1[0, 2] = cube3[0, 2];
            cube3[0, 2] = cube3[0, 0];
            cube3[0, 0] = temp;

            //rotate edges
            temp = cube2[0, 0];
            cube2[0, 0] = cube1[0, 1];
            cube1[0, 1] = cube2[0, 2];
            cube2[0, 2] = cube3[0, 1];
            cube3[0, 1] = temp;

            //rotate white face tiles
            rotate2DTWhite();
        }
        else if (face == 4)
        {//yellow
         //rotate corners
            string temp = cube1[2, 0];
            cube1[2, 0] = cube3[2, 0];
            cube3[2, 0] = cube3[2, 2];
            cube3[2, 2] = cube1[2, 2];
            cube1[2, 2] = temp;

            //rotate edges
            temp = cube1[2, 1];
            cube1[2, 1] = cube2[2, 0];
            cube2[2, 0] = cube3[2, 1];
            cube3[2, 1] = cube2[2, 2];
            cube2[2, 2] = temp;

            //rotate yellow face tiles
            rotate2DTYellow();
        }
    }
    

    private string[,] white, FSWhite, whiteB;
    private string[,] red, FSRed, redB;
    private string[,] blue, FSBlue, blueB;
    private string[,] orange, FSOrange, orangeB;
    private string[,] green, FSGreen, greenB;
    private string[,] yellow, FSYellow, yellowB;

    private void init2DTilesCube()
    {
        white = new string[3, 3]; FSWhite = new string[3, 3]; whiteB = new string[3, 3];
        red = new string[3, 3]; FSRed = new string[3, 3]; redB = new string[3, 3];
        blue = new string[3, 3]; FSBlue = new string[3, 3]; blueB = new string[3, 3];
        orange = new string[3, 3]; FSOrange = new string[3, 3]; orangeB = new string[3, 3];
        green = new string[3, 3]; FSGreen = new string[3, 3]; greenB = new string[3, 3];
        yellow = new string[3, 3]; FSYellow = new string[3, 3]; yellowB = new string[3, 3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                white[i, j] = FSWhite[i, j] = "white";
                red[i, j] = FSRed[i, j] = "red";
                blue[i, j] = FSBlue[i, j] = "blue";
                orange[i, j] = FSOrange[i, j] = "orange";
                green[i, j] = FSGreen[i, j] = "green";
                yellow[i, j] = FSYellow[i, j] = "yellow";
            }
        }
    }

    //clockwise (starting at top red layer)
    private void rotate2DTWhite()
    {
        string[] temp = new string[3];
        for (int i = 0; i < 3; i++)
            temp[i] = red[0, i]; //store red

        /*Rotation of neighbour colors tiles*/

        //red <- blue
        for (int i = 0; i < 3; i++)
            red[0, i] = blue[0, i];

        //blue <- orange
        for (int i = 0; i < 3; i++)
            blue[0, i] = orange[0, i];

        //orange <- green
        for (int i = 0; i < 3; i++)
            orange[0, i] = green[0, i];

        //green <- red
        for (int i = 0; i < 3; i++)
            green[0, i] = temp[i];


        /*Rotation of white face tiles*/

        //rotate corners
        temp[0] = white[0, 0];
        white[0, 0] = white[2, 0];
        white[2, 0] = white[2, 2];
        white[2, 2] = white[0, 2];
        white[0, 2] = temp[0];

        //rotate edges
        temp[0] = white[0, 1];
        white[0, 1] = white[1, 0];
        white[1, 0] = white[2, 1];
        white[2, 1] = white[1, 2];
        white[1, 2] = temp[0];
    }

    //clockwise (starting at bottom orange layer)
    private void rotate2DTYellow()
    {
        string[] temp = new string[3];
        for (int i = 0; i < 3; i++)
            temp[i] = orange[2, i]; //store orange

        /*Rotation of neighbour colors tiles*/

        //orange <- blue
        for (int i = 0; i < 3; i++)
            orange[2, i] = blue[2, i];

        //blue <- red
        for (int i = 0; i < 3; i++)
            blue[2, i] = red[2, i];

        //red <- green
        for (int i = 0; i < 3; i++)
            red[2, i] = green[2, i];

        //green <- orange
        for (int i = 0; i < 3; i++)
            green[2, i] = temp[i];


        /*Rotation of yellow face tiles*/

        //rotate corners
        temp[0] = yellow[0, 0];
        yellow[0, 0] = yellow[2, 0];
        yellow[2, 0] = yellow[2, 2];
        yellow[2, 2] = yellow[0, 2];
        yellow[0, 2] = temp[0];

        //rotate edges
        temp[0] = yellow[0, 1];
        yellow[0, 1] = yellow[1, 0];
        yellow[1, 0] = yellow[2, 1];
        yellow[2, 1] = yellow[1, 2];
        yellow[1, 2] = temp[0];
    }

    //clockwise (starting at top yellow layer)
    private void rotate2DTRed()
    {
        string[] temp = new string[3];
        for (int i = 0; i < 3; i++)
            temp[i] = yellow[0, i]; //store yellow

        /*Rotation of neighbour colors tiles*/

        //yellow <- blue
        for (int i = 0; i < 3; i++)
            yellow[0, i] = blue[2 - i, 0];

        //blue <- white
        for (int i = 0; i < 3; i++)
            blue[2 - i, 0] = white[2, 2 - i];

        //white <- green
        for (int i = 0; i < 3; i++)
            white[2, 2 - i] = green[i, 2];

        //green <- yellow
        for (int i = 0; i < 3; i++)
            green[i, 2] = temp[i];


        /*Rotation of red face tiles*/

        //rotate corners
        temp[0] = red[0, 0];
        red[0, 0] = red[2, 0];
        red[2, 0] = red[2, 2];
        red[2, 2] = red[0, 2];
        red[0, 2] = temp[0];

        //rotate edges
        temp[0] = red[0, 1];
        red[0, 1] = red[1, 0];
        red[1, 0] = red[2, 1];
        red[2, 1] = red[1, 2];
        red[1, 2] = temp[0];
    }

    //clockwise (starting at bottom yellow layer)
    private void rotate2DTOrange()
    {
        string[] temp = new string[3];
        for (int i = 0; i < 3; i++)
            temp[i] = yellow[2, i]; //store yellow

        /*Rotation of neighbour colors tiles*/

        //yellow <- green
        for (int i = 0; i < 3; i++)
            yellow[2, i] = green[i, 0];

        //green <- white
        for (int i = 0; i < 3; i++)
            green[i, 0] = white[0, 2 - i];

        //white <- blue
        for (int i = 0; i < 3; i++)
            white[0, 2 - i] = blue[2 - i, 2];

        //blue <- yellow
        for (int i = 0; i < 3; i++)
            blue[2 - i, 2] = temp[i];


        /*Rotation of orange face tiles*/

        //rotate corners
        temp[0] = orange[0, 0];
        orange[0, 0] = orange[2, 0];
        orange[2, 0] = orange[2, 2];
        orange[2, 2] = orange[0, 2];
        orange[0, 2] = temp[0];

        //rotate edges
        temp[0] = orange[0, 1];
        orange[0, 1] = orange[1, 0];
        orange[1, 0] = orange[2, 1];
        orange[2, 1] = orange[1, 2];
        orange[1, 2] = temp[0];
    }

    //clockwise (starting at right yellow layer)
    private void rotate2DTBlue()
    {
        string[] temp = new string[3];
        for (int i = 0; i < 3; i++)
            temp[i] = yellow[i, 2]; //store yellow

        /*Rotation of neighbour colors tiles*/

        //yellow <- orange
        for (int i = 0; i < 3; i++)
            yellow[i, 2] = orange[2 - i, 0];

        //orange <- white
        for (int i = 0; i < 3; i++)
            orange[2 - i, 0] = white[i, 2];

        //white <- red
        for (int i = 0; i < 3; i++)
            white[i, 2] = red[i, 2];

        //red <- yellow
        for (int i = 0; i < 3; i++)
            red[i, 2] = temp[i];


        /*Rotation of blue face tiles*/

        //rotate corners
        temp[0] = blue[0, 0];
        blue[0, 0] = blue[2, 0];
        blue[2, 0] = blue[2, 2];
        blue[2, 2] = blue[0, 2];
        blue[0, 2] = temp[0];

        //rotate edges
        temp[0] = blue[0, 1];
        blue[0, 1] = blue[1, 0];
        blue[1, 0] = blue[2, 1];
        blue[2, 1] = blue[1, 2];
        blue[1, 2] = temp[0];
    }

    //clockwise (starting at left yellow layer)
    private void rotate2DTGreen()
    {
        string[] temp = new string[3];
        for (int i = 0; i < 3; i++)
            temp[i] = yellow[i, 0]; //store yellow

        /*Rotation of neighbour colors tiles*/

        //yellow <- blue
        for (int i = 0; i < 3; i++)
            yellow[i, 0] = red[i, 0];

        //red <- white
        for (int i = 0; i < 3; i++)
            red[i, 0] = white[i, 0];

        //white <- orange
        for (int i = 0; i < 3; i++)
            white[i, 0] = orange[2 - i, 2];

        //orange <- yellow
        for (int i = 0; i < 3; i++)
            orange[2 - i, 2] = temp[i];


        /*Rotation of blue face tiles*/

        //rotate corners
        temp[0] = green[0, 0];
        green[0, 0] = green[2, 0];
        green[2, 0] = green[2, 2];
        green[2, 2] = green[0, 2];
        green[0, 2] = temp[0];

        //rotate edges
        temp[0] = green[0, 1];
        green[0, 1] = green[1, 0];
        green[1, 0] = green[2, 1];
        green[2, 1] = green[1, 2];
        green[1, 2] = temp[0];
    }

    public string[,] GetWhiteFaceTiles()
    {
        return white;
    }

    public string[,] GetRedFaceTiles()
    {
        return red;
    }

    public string[,] GetYellowFaceTiles()
    {
        return yellow;
    }

    public string[,] GetOrangeFaceTiles()
    {
        return orange;
    }

    public string[,] GetBlueFaceTiles()
    {
        return blue;
    }

    public string[,] GetGreenFaceTiles()
    {
        return green;
    }

    // check if the cube is solved
    public bool isSolved()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (orange[i, j] != FSOrange[i, j])
                {
                    return false;
                }
                else if (green[i, j] != FSGreen[i, j])
                {
                    return false;
                }
                else if (white[i, j] != FSWhite[i, j])
                {
                    return false;
                }
                else if (blue[i, j] != FSBlue[i, j])
                {
                    return false;
                }
                else if (red[i, j] != FSRed[i, j])
                {
                    return false;
                }
                else if (yellow[i, j] != FSYellow[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }

}

