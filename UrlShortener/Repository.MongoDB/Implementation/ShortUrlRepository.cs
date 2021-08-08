using Models.RepositoryModels.ShortUrlRepository;
using MongoDB.Driver;
using Repository.Interfaces.Interfaces;
using Repository.MongoDB.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.MongoDB.Implementation
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly IMongoCollection<ShortUrl> _collection;

        public ShortUrlRepository(
            IMongoDatabase mongoDatabase,
            MongoCollections mongoCollections)
        {
            _collection = mongoDatabase.GetCollection<ShortUrl>(mongoCollections.ShortUrlCollectionName);
        }

        public async Task<string> GetLongUrlByIdAsync(string id)
        {
            var filter = Builders<ShortUrl>.Filter.Eq(x => x.Id, id);
            var projection = Builders<ShortUrl>.Projection.Expression(v => v.LongUrl);
            var longUrl = await _collection.Find(filter).Project(projection).FirstOrDefaultAsync();
            return longUrl;
        }

        public async Task<IEnumerable<ShortUrl>> GetManyAsync(int take, string lastId)
        {
            if (take < 1)
            {
                return new ShortUrl[0];
            }

            var filter = Builders<ShortUrl>.Filter.Empty;
            if (!string.IsNullOrEmpty(lastId))
            {
                var lastIdCreateDateFilter = Builders<ShortUrl>.Filter.Eq(x => x.Id, lastId);
                var lastIdCreateDateProjection = Builders<ShortUrl>.Projection.Expression(x => x.CreateDate);
                var lastIdCreateDate = await _collection.Find(lastIdCreateDateFilter).Project(lastIdCreateDateProjection).FirstOrDefaultAsync();
                if (lastIdCreateDate != default)
                {
                    filter = Builders<ShortUrl>.Filter.Gt(x => x.CreateDate, lastIdCreateDate);
                }
            }

            var result = await _collection.Find(filter).Limit(take).ToListAsync();
            return result;
        }

        public async Task IncrementRedirectionsCountAsync(string id)
        {
            var filter = Builders<ShortUrl>.Filter.Eq(x => x.Id, id);
            var update = Builders<ShortUrl>.Update.Inc(x => x.RedirectionsCount, 1);

            await _collection.FindOneAndUpdateAsync(filter, update);
        }

        public async Task<string> InsertAsync(string longUrl)
        {
            var newId = Guid.NewGuid().ToString("N");

            var newItem = new ShortUrl
            {
                Id = newId,
                LongUrl = longUrl,
                RedirectionsCount = 0,
                CreateDate = DateTime.UtcNow
            };

            await _collection.InsertOneAsync(newItem);

            return newId;
        }
    }
}
