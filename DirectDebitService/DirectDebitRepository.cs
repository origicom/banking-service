﻿using Accounts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Types;
using MongoDB.Driver;
using MongoDB.Bson;
using Accounts.Entities;
using Base.Providers;

namespace DirectDebitService
{
    public interface IDirectDebitRepository
    {
        Task<IEnumerable<DirectDebit>> GetDirectDebitsForToday();
        Task MarkDirectDebit(DirectDebitGuid id, DateTimeOffset timestamp);
    }

    public class DirectDebitRepository : IDirectDebitRepository
    {
        private readonly string _connectionString;
        private readonly IDateTimeProvider _dateTimeService;
        private readonly MongoClient _mongoClient;

        private IMongoCollection<DirectDebit> DebitCollection => _mongoClient.GetDatabase("local").GetCollection<DirectDebit>("directDebits");


        public DirectDebitRepository(string connectionString, IDateTimeProvider dateTimeService, MongoClient mongoClient)
        {
            _connectionString = connectionString;
            _dateTimeService = dateTimeService;
            _mongoClient = mongoClient;
        }

        public async Task<IEnumerable<DirectDebit>> GetDirectDebitsForToday()
        {
            var today = _dateTimeService.GetDateTimeOffset().Date;
            var directDebits = (await DebitCollection.FindAsync(d => true)).ToList();


            try
            {
                directDebits = (await DebitCollection.FindAsync(d => d.Frequency == DirectDebitFrequency.Once && d.StartDate == today || d.Frequency == DirectDebitFrequency.Monthly && (d.LastRunTimestamp.HasValue && d.LastRunTimestamp.Value < today.AddMonths(-1) || !d.LastRunTimestamp.HasValue && d.StartDate < today.AddMonths(-1)))).ToList();
                var directDebits1 = (await DebitCollection.FindAsync(d => d.StartDate == today)).ToList();
            }
            catch (Exception e)
            {
            }

            return directDebits;
        }

        public async Task MarkDirectDebit(DirectDebitGuid id, DateTimeOffset timestamp)
        {
            await DebitCollection.FindOneAndUpdateAsync((DirectDebit dd) => dd.Id == id, Builders<DirectDebit>.Update.Set("LastRunTimestamp", timestamp));
        }
    }
}
