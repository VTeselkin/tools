using UnityEngine.UI;

namespace GameSoft.Tools.Extensions
{
    public static class UIExtensions
    {
        public static void SetAlpha(this Graphic target, float alpha)
        {
            var color = target.color;
            color.a = alpha;
            target.color = color;
        }
    }

}