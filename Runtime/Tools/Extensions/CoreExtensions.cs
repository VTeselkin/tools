using System;
namespace GameSoft.Tools.Extensions
{
    /// <summary> Enum Extension Methods </summary>
    /// <typeparam name="T"> type of Enum </typeparam>
    public class Enum<T> where T : struct
    {
        public static T GetRandom
        {
            get
            {
                if (!typeof(T).IsEnum)
                {
                    throw new ArgumentException("T must be an enumerated type");
                }

                var type = typeof(T);
                var values = Enum.GetValues(type);
                var randId = UnityEngine.Random.Range(0, values.Length);
                return (T) values.GetValue(randId);
            }
        }

        public static int GetMax
        {
            get
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException("T must be an enumerated type");
                
                var type = typeof(T);
                var values = Enum.GetValues(type);
                return (int) values.GetValue(values.Length - 1);
            }
        }
        
        public static int GetMin
        {
            get
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException("T must be an enumerated type");
                
                var type = typeof(T);
                var values = Enum.GetValues(type);
                return (int) values.GetValue(0);
            }
        }
        
        public static int GetCount
        {
            get
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException("T must be an enumerated type");
                
                var type = typeof(T);
                var values = Enum.GetValues(type);
                return values.Length;
            }
        }
    }
    
    
}