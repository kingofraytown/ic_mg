public class Toolbox : Singleton<Toolbox> {
    protected Toolbox () {} // guarantee this will be always a singleton only - can't use the constructor!
    
    public int myGlobalVar = 0;
    
    //public Language language = new Language();
    
    void Awake () {
        // Your initialization code here
        myGlobalVar = 0;
    }
    
    // (optional) allow runtime registration of global objects
    //static public T RegisterComponent<T> () {
      //  return Instance.GetOrAddComponent<T>();
    //}
}