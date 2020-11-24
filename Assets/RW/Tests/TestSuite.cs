using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class TestSuite
{
    private Game game;
    [SetUp]
    public void SetUp()
    {
        GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();

        float initialYPos = asteroid.transform.position.y;

        yield return new WaitForSeconds(0.1f);

        Assert.Less(asteroid.transform.position.y, initialYPos);
    }
    [UnityTest]
    public IEnumerator NewGameRestartsGame()
    {
        game.isGameOver = true;
        game.NewGame();

        Assert.False(game.isGameOver);
        yield return null;
    }
    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = game.GetShip().transform.position;

        yield return new WaitForSeconds(0.5f);

        Assert.True(game.isGameOver);
    }
    [UnityTest]
    public IEnumerator ShipMoveLeft()
    {
        float initialXPos = game.GetShip().transform.position.x;

        game.GetShip().MoveLeft();
        game.GetShip().MoveLeft();

        yield return new WaitForSeconds(0.1f);

        Assert.Less(game.GetShip().transform.position.x, initialXPos);
    }
    [UnityTest]
    public IEnumerator ShipMoveRight()
    {
        float initialXPos = game.GetShip().transform.position.x;

        game.GetShip().MoveRight();
        game.GetShip().MoveRight();

        yield return new WaitForSeconds(0.1f);

        Assert.Greater(game.GetShip().transform.position.x, initialXPos);
    }
    [UnityTest]
    public IEnumerator LaserSpawn()
    {
        GameObject lazer = game.GetShip().SpawnLaser();
        Assert.True(lazer != null);
        yield return null;
    }
    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        GameObject laser = game.GetShip().SpawnLaser();

        float initialYPos = laser.transform.position.y;

        yield return new WaitForSeconds(0.1f);

        Assert.Greater(laser.transform.position.y, initialYPos);
    }
    [UnityTest]
    public IEnumerator LaserDestroysAsteroidAndItselfOnCollision()
    {
        GameObject lazer = game.GetShip().SpawnLaser();
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();

        Assert.False(lazer == null && asteroid == null);

        lazer.transform.position = Vector3.zero;
        asteroid.transform.position = Vector3.zero;

        yield return new WaitForSeconds(0.5f);

        Assert.True(lazer == null && asteroid == null);
    }


    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game.gameObject);
    }
}