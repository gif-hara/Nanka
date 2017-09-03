using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Nanka
{
    public sealed class GameController : MonoBehaviour
    {
        public static GameController Instance { private set; get; }

        public Player Player { private set; get; }

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;

            this.Player = new Player();
        }
    }
}
