public class SceneManagement : Singleton<SceneManagement>
{
    public string SceneTransitionName { get; private set; }

    public void SetTransitionName(string sceneTransitionName)
    {
        
        this.SceneTransitionName = sceneTransitionName;
    }
}
