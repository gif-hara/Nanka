using UnityEngine;

namespace HK.Nanka
{
    public sealed class Item
    {
        public int Id { private set; get; }

        public Item(int id)
        {
            this.Id = id;
        }
    }
}
