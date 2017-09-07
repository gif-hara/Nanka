using HK.Nanka.RobotSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Nanka
{
    public sealed class GameController : MonoBehaviour
    {
        [SerializeField]
        private ItemSpecs itemSpecs;

        [SerializeField]
        private RobotController robotController;

        public static GameController Instance { private set; get; }

        public Player Player { private set; get; }

        public ItemSpecs ItemSpecs { get { return this.itemSpecs; } }

        public RobotController RobotController { get { return this.robotController; } }

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;

            this.Player = new Player();
            this.ItemSpecs.Initialize();
        }
    }
}
