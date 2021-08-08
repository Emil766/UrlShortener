using Models.RepositoryModels.ShortUrlRepository;
using MongoDB.Driver;
using Repository.MongoDB.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Repository.MongoDB
{
    public static class MongoCollectionsInitializer
    {
        public static void InitializeCollections(IMongoDatabase database, MongoCollections mongoCollections)
        {
            var existingCollectionNames = database.ListCollectionNames().ToList();
            if (!existingCollectionNames.Any(x => x == mongoCollections.ShortUrlCollectionName))
            {
                database.CreateCollection(mongoCollections.ShortUrlCollectionName);
                var shortUrlCollection = database.GetCollection<ShortUrl>(mongoCollections.ShortUrlCollectionName);
                var shortUrlIndexes = new List<CreateIndexModel<ShortUrl>> 
                {
                    new CreateIndexModel<ShortUrl>(Builders<ShortUrl>.IndexKeys.Ascending(x => x.CreateDate)),
                    new CreateIndexModel<ShortUrl>(Builders<ShortUrl>.IndexKeys.Text(x => x.LongUrl))
                };
                shortUrlCollection.Indexes.CreateMany(shortUrlIndexes);
            }
        }
    }
}
