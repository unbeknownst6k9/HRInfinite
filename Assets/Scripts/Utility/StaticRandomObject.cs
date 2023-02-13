/*Purpose: this is a helper class that creates a random object which can be used across all the classes
 */

namespace CustomRandom
{
    public class StaticRandomObject
    {
        #region Constructor
        private StaticRandomObject() {}
        #endregion

        #region Singleton
        private static StaticRandomObject _instance;
        private static readonly object _lock = new object();

        public static StaticRandomObject Instance { 
            get {
                if(_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new StaticRandomObject();
                        }
                    }
                }
                return _instance;
            } 
        }
        #endregion
        
        private System.Random rant = new System.Random();
        //make sure this class cannot have any instance
        

        public int getRandomInt(int min, int max)
        {
            //the upper bound is always excluded so it always needs a +1
            return rant.Next(min, max+1);
        }
    }
}
