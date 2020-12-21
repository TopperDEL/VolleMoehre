using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.IO;

namespace VolleMoehre.Adapter.LiteDB
{
    public class LiteDBStore : Contracts.Interfaces.IDBAdapter
    {
        private static string DATABASE;
        private static LiteDatabase Database;

        static LiteDBStore()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DATABASE = Path.Combine(assemblyFolder, "Datenbank");
            DATABASE = Path.Combine(DATABASE, "VolleMoehre.db");

            Database = new LiteDatabase(DATABASE);
        }

        public async Task<bool> DeleteAsync<T>(string Id)
        {
            var collection = Database.GetCollection<T>();

            return collection.Delete(new BsonValue(Id));
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var collection = Database.GetCollection<T>();

            return collection.FindAll().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> whereClause)
        {
            var collection = Database.GetCollection<T>();

            return (collection.Find(whereClause)).ToList();
        }

        public async Task<T> GetAsync<T>(string Id)
        {
            var collection = Database.GetCollection<T>();

            return collection.FindById(new BsonValue(Id));
        }

        [Obsolete]
        public object GetUnderlyingSession()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAsync<T>(T objectToSave)
        {
            var collection = Database.GetCollection<T>();

            try
            {
                var result = collection.Insert(objectToSave);
                if (string.IsNullOrEmpty(result))
                    return false;
                else
                    return true;
            }
            catch
            {
                var result = collection.Update(objectToSave);

                return true;
            }
        }
    }
}
