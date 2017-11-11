﻿using CarDealerSystem.Data.Models;
using CarDealerSystem.Services.Contracts;
using CarDealerSystem.Services.Models.Logs;
using CarDealerSystem.Web.Data;
using System;
using System.Linq;

namespace CarDealerSystem.Services
{
    public class SimpleLoggerService : ISimpleLoggerService
    {
        private readonly CarDealerDbContext db;

        public SimpleLoggerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public void ClearLogs(string username)
        {
            var query = this.db.Logs.AsQueryable();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrWhiteSpace(username))
            {
                query = query.Where(l => l.Username == username).AsQueryable();
            }

            var logs = query.ToList();

            this.db.RemoveRange(logs);
            this.db.SaveChanges();
        }

        public MainLogsViewModel GetLogs(string username)
        {
            var query = this.db.Logs.AsQueryable();

            if (username != null && !string.IsNullOrWhiteSpace(username))
            {
                query = query.Where(l => l.Username == username.ToLower()).AsQueryable();
            }

            MainLogsViewModel model = new MainLogsViewModel()
            {
                Logs = query.Select(l => new LogDetailsModel()
                {
                    Username = l.Username,
                    Operation = l.LogType,
                    Table = l.Table,
                    Time = l.Time
                }).OrderByDescending(l => l.Time).ToList()
            };

            return model;
        }

        public void LogToDb(string username, LogType operation, string table)
        {
            var entry = new LogEntry()
            {
                Username = username,
                LogType = operation,
                Table = table,
                Time = DateTime.UtcNow
            };

            this.db.Logs.Add(entry);
            this.db.SaveChanges();
        }
    }
}