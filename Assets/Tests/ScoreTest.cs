using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class ScoreTest
{

    [UnityTest]
    public IEnumerator TestScore()
    {
        Sprite sprite;
        Texture2D tex = new Texture2D(64, 64);
        sprite = Sprite.Create(tex, new Rect(1, 1, 1, 1), new Vector2(1, 1));


        GameObject score = new GameObject("score");
        score.AddComponent<ScoreBoard>();
        GameObject ones = new GameObject("1");
        ones.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteR1 = ones.GetComponent<SpriteRenderer>();
        spriteR1.sprite = sprite;
        GameObject twos = new GameObject("2");
        twos.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteR2 = ones.GetComponent<SpriteRenderer>();
        spriteR2.sprite = sprite;
        GameObject threes = new GameObject("3");
        threes.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteR3 = ones.GetComponent<SpriteRenderer>();
        spriteR3.sprite = sprite;
        GameObject four = new GameObject("4");
        four.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteR4 = ones.GetComponent<SpriteRenderer>();
        spriteR4.sprite = sprite;
        GameObject five = new GameObject("5");
        five.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteR5 = ones.GetComponent<SpriteRenderer>();
        spriteR5.sprite = sprite;
        GameObject six = new GameObject("5");
        six.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteR6 = ones.GetComponent<SpriteRenderer>();
        spriteR6.sprite = sprite;




        ScoreBoard scoreBoard = score.GetComponent<ScoreBoard>();
        Assert.IsNotNull(scoreBoard);

        scoreBoard.place = new SpriteRenderer[6];
        scoreBoard.place[0] = spriteR1;
        scoreBoard.place[1] = spriteR2;
        scoreBoard.place[2] = spriteR3;
        scoreBoard.place[3] = spriteR4;
        scoreBoard.place[4] = spriteR5;
        scoreBoard.place[5] = spriteR6;




        Sprite sprite1;
        sprite1 = Sprite.Create(tex, new Rect(1, 1, 1, 1), new Vector2(1, 1));

        Sprite sprite2;
        sprite2 = Sprite.Create(tex, new Rect(1, 1, 1, 1), new Vector2(1, 1));
        Sprite sprite3;
        sprite3 = Sprite.Create(tex, new Rect(1, 1, 1, 1), new Vector2(1, 1));
        Sprite sprite4;
        sprite4 = Sprite.Create(tex, new Rect(1, 1, 1, 1), new Vector2(1, 1));
        Sprite sprite5;
        sprite5 = Sprite.Create(tex, new Rect(1, 1, 1, 1), new Vector2(1, 1));
        Sprite sprite6;
        sprite6 = Sprite.Create(tex, new Rect(1, 1, 1, 1), new Vector2(1, 1));
        Sprite sprite7;
        sprite7 = Sprite.Create(tex, new Rect(1, 1, 1, 1), new Vector2(1, 1));
        Sprite sprite8;
        sprite8 = Sprite.Create(tex, new Rect(1, 1, 1, 1), new Vector2(1, 1));
        Sprite sprite9;
        sprite9 = Sprite.Create(tex, new Rect(1, 1, 1, 1), new Vector2(1, 1));


        scoreBoard.digits = new Sprite[10];
        scoreBoard.digits[0] = sprite;
        scoreBoard.digits[1] = sprite1;
        scoreBoard.digits[2] = sprite2;
        scoreBoard.digits[3] = sprite3;
        scoreBoard.digits[4] = sprite4;
        scoreBoard.digits[5] = sprite5;
        scoreBoard.digits[6] = sprite6;
        scoreBoard.digits[7] = sprite7;
        scoreBoard.digits[8] = sprite8;
        scoreBoard.digits[9] = sprite9;

        scoreBoard.adjustScore(5);
        Assert.AreEqual(scoreBoard.adjustScore(10), 15);
        Assert.AreEqual(scoreBoard.adjustScore(20), 35);
        Assert.AreEqual(scoreBoard.adjustScore(30), 65);
        Assert.AreEqual(scoreBoard.adjustScore(10), 75);
        Assert.AreEqual(scoreBoard.adjustScore(-5), 70);
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
