using System.Collections.Generic;

namespace BlockChain
{
    /// <summary>
    /// Цепочка блоков.
    /// </summary>
    public class Chain
    {
        /// <summary>
        /// Список блоков цепочки.
        /// </summary>
        public List<Block> Blocks { get; private set; }
        /// <summary>
        /// Последний блок.
        /// </summary>
        public Block Last { get; private set; }

        /// <summary>
        /// Конструктор создания цепочки блоков.
        /// </summary>
        public Chain()
        {
            Blocks = new List<Block>();
            var genesisBlock = new Block();

            Blocks.Add(genesisBlock);
            Last = genesisBlock;
        }

        /// <summary>
        /// Добавление нового блока.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public void Add(string data, string user)
        {
            var block = new Block(data, user, Last);
            Blocks.Add(block);
            Last = block;
        }
    }
}
