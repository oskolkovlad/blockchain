using System;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain
{
    /// <summary>
    /// Блок данных.
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Данные.
        /// </summary>
        public string Data { get; private set; }
        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime Created { get; private set; }
        /// <summary>
        /// Хэш блока.
        /// </summary>
        public string Hash { get; private set; }
        /// <summary>
        /// Хэш предыдущего блока.
        /// </summary>
        public string PreviousHash { get; private set; }
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string User { get; private set; }


        /// <summary>
        /// Конструктор генезис блока.
        /// </summary>
        public Block()
        {
            Id = 1;
            Data = "Hello world";
            Created = DateTime.Parse("03.04.2020 00:00:00.000").ToUniversalTime();
            PreviousHash = "111111";
            User = "Admin";
            Hash = GetHash(GetData());
        }
        
        /// <summary>
        /// Конструктор блока.
        /// </summary>
        /// <param name="data">Данные блока.</param>
        /// <param name="user">Имя пользователя.</param>
        /// <param name="block">Предыдущий блок.</param>
        public Block(string data, string user, Block block)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentNullException(nameof(data), "Данные не могут быть пустыми...");
            }

            if (string.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentNullException(nameof(user), "Пользователь не может быть пустым...");
            }

            if (block is null)
            {
                throw new ArgumentNullException(nameof(block), "Блок, не может быть пустым...");
            }

            Data = data;
            User = user;
            Hash = GetHash(GetData());
            PreviousHash = block.Hash;
            Created = DateTime.UtcNow;
            Id = ++block.Id;
        }

        /// <summary>
        /// Получение данных блока.
        /// </summary>
        /// <returns></returns>
        private string GetData()
        {
            var result = "";
            result += Id.ToString();
            result += Data;
            result += Created.ToString("dd.MM.yyyy HH:mm:ss.fff");
            result += PreviousHash;
            result += User;

            return result;
        }

        /// <summary>
        /// Хэширование.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetHash(string data)
        {
            var message = Encoding.ASCII.GetBytes(data);
            SHA256Managed hash = new SHA256Managed();
            var value = hash.ComputeHash(message);

            var hex = "";
            foreach (var x in value)
            {
                hex += string.Format("{0:x2}", x);
            }

            return hex;
        }


        public override string ToString() => Data;
    }
}
