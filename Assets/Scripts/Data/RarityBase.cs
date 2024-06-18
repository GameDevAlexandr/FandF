using System.Collections.Generic;
using UnityEngine;

public class RarityBase : MonoBehaviour
{
    public static Dictionary<int, Sprite> frames { get { Init(); return _frames; } }
    public static Dictionary<int, Sprite> backs { get { Init(); return _backs; } }

    private static Dictionary<int, Sprite> _frames;
    private static Dictionary<int, Sprite> _backs;

    private static void Init()
    {
        if(_frames == null)
        {
            _frames = new Dictionary<int, Sprite>();
            _backs = new Dictionary<int, Sprite>();

            var fs = Resources.LoadAll<RarityIndicator>("Rarity/Frames");
            var bk = Resources.LoadAll<RarityIndicator>("Rarity/Backs");

            for (int i = 0; i < fs.Length; i++)
            {
                _frames.Add(fs[i].index, fs[i].image);
            }
            for (int i = 0; i < bk.Length; i++)
            {
                _backs.Add(bk[i].index, bk[i].image);
            }
        }
    }

}
