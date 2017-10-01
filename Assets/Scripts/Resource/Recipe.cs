using System;
using System.Collections.Generic;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace HK.Nanka
{
    /// <summary>
    /// アイテムを作成するためのレシピ
    /// </summary>
    [Serializable]
    public sealed class Recipe
    {
        /// <summary>
        /// このレシピで出来るアイテム
        /// </summary>
        [SerializeField]
        private StringAsset.Finder productName;

        /// <summary>
        /// このレシピで出来るアイテムの数
        /// </summary>
        [SerializeField]
        private int number;
        
        [SerializeField]
        private List<RequireItem> requireItems = new List<RequireItem>();
        
        [SerializeField]
        private List<RequireItem> materials = new List<RequireItem>();

        public ItemSpec ProductItemSpec
        {
            get { return GameController.Instance.ItemSpecs.Get(this.productName.Get.GetHashCode()); }
        }

        public StringAsset.Finder ProductItemName { get { return this.productName; } }

        public int Number { get { return this.number; } }

        /// <summary>
        /// 必要なアイテム
        /// </summary>
        public List<RequireItem> RequireItems { get { return requireItems; } }

        /// <summary>
        /// 必要な素材
        /// </summary>
        /// <remarks>
        /// アイテムを作成した際に消滅されるアイテム
        /// </remarks>
        public List<RequireItem> Materials { get { return this.materials; } }

        /// <summary>
        /// リストに表示可能か返す
        /// </summary>
        public bool CanVisibleList(Inventory inventory)
        {
            var findRequireItem = this.RequireItems.Find(r => !r.IsPossession(inventory)) == null;
            var findMaterial = this.materials.Find(r => !r.IsPossession(inventory)) == null;
            return findRequireItem && findMaterial;
        }

        /// <summary>
        /// 生成可能か返す
        /// </summary>
        public bool CanCreate(Inventory inventory)
        {
            var findRequireItem = this.RequireItems.Find(r => !r.CanCreate(inventory)) == null;
            var findMaterial = this.materials.Find(r => !r.CanCreate(inventory)) == null;
            return findRequireItem && findMaterial;
        }
    }
}
