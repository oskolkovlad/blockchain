﻿using System;
using System.Collections.Generic;
using System.Linq;

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
        //private BlockchainContext context;

        /// <summary>
        /// Конструктор создания цепочки блоков.
        /// </summary>
        public Chain()
        {
            Blocks = Load();
            if (Blocks.Count == 0)
            {
                var genesisBlock = new Block();

                Blocks.Add(genesisBlock);
                Last = genesisBlock;

                Save(Last);
            }
            else
            {
                if (Check())
                {
                    Last = Blocks.Last();
                }
                else
                {
                    throw new Exception("Ошибка получения блоков из базы данных. Цепочка не прошла проверку на целостность.");
                }
            }
        }

        /// <summary>
        /// Добавление нового блока.
        /// </summary>
        /// <param name="data">Данные блока.</param>
        /// <param name="user">Имя пользователя.</param>
        public void Add(string data, string user)
        {
            var block = new Block(data, user, Last);
            Blocks.Add(block);
            Last = block;

            Save(block);
        }

        /// <summary>
        /// Метод проверки корректности цепочки.
        /// </summary>
        /// <returns>Результат проверки.</returns>
        public bool Check()
        {
            var genesisBlock = new Block();
            var previousHash = genesisBlock.Hash;

            foreach (var block in Blocks.Skip(1))
            {
                var hash = block.PreviousHash;

                if (hash != previousHash)
                    return false;

                previousHash = block.Hash;
            }

            return true;
        }

        /// <summary>
        /// Сохранение блоков в БД.
        /// </summary>
        /// <param name="block">Блок, который нужно сохранить.</param>
        private void Save(Block block)
        {
            using (var db = new BlockchainContext())
            {
                db.Blocks.Add(block);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Загрузка блоков из БД.
        /// </summary>
        /// <returns>Список блоков данных.</returns>
        private List<Block> Load()
        {
            List<Block> result;

            using (var db = new BlockchainContext())
            {
                var count = db.Blocks.OrderByDescending(b => b.Id).Count();
                result = new List<Block>(count * 2);
                result.AddRange(db.Blocks);
            }

            return result;
        }

        private void Sync()
        {
            // TODO
        }
    }
}
