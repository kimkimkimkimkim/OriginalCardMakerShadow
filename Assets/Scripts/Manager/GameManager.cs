using GameBase;
using UniRx;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    void Start()
    {
        HomeWindowFactory.Create(new HomeWindowRequest()).Subscribe();
    }
}
