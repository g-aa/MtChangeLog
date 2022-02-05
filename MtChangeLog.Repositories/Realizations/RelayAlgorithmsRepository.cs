﻿using Microsoft.EntityFrameworkCore;
using MtChangeLog.Abstractions.Extensions;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Context.Realizations;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Repositories.Realizations
{
    public class RelayAlgorithmsRepository : IRelayAlgorithmsRepository
    {
        private readonly ApplicationContext context;
        
        public RelayAlgorithmsRepository(ApplicationContext context) 
        {
            this.context = context;
        }

        public IQueryable<RelayAlgorithmShortView> GetShortEntities() 
        {
            var result = this.context.RelayAlgorithms
                .AsNoTracking()
                .OrderBy(e => e.Group)
                .ThenBy(e => e.Title)
                .Select(e => e.ToShortView());
            return result;
        }
        
        public IQueryable<RelayAlgorithmEditable> GetTableEntities() 
        {
            var result = this.context.RelayAlgorithms
                .AsNoTracking()
                .OrderBy(e => e.Group)
                .ThenBy(e => e.Title)
                .Select(e => e.ToEditable());
            return result;
        }
        
        public RelayAlgorithmEditable GetTemplate() 
        {
            var result = new RelayAlgorithmEditable()
            {
                Id = Guid.Empty,
                Title = "наименование алгоритма РЗА",
                ANSI = "код ANSI",
                LogicalNode = "логический узел в МЭК-61850",
                Description = "шаблон функции релейной защиты"
            };
            return result;
        }

        public RelayAlgorithmEditable GetEntity(Guid guid)
        {
            var result = this.context.RelayAlgorithms
                .AsNoTracking()
                .Search(guid)
                .ToEditable();
            return result;
        }

        public void AddEntity(RelayAlgorithmEditable entity)
        {
            var dbAlgorithm = new RelayAlgorithm(entity);
            if (this.context.RelayAlgorithms.IsContained(dbAlgorithm)) 
            {
                throw new ArgumentException($"Сущность \"{entity}\" уже содержится в БД");
            }
            this.context.RelayAlgorithms.Add(dbAlgorithm);
            this.context.SaveChanges();
        }

        public void UpdateEntity(RelayAlgorithmEditable entity)
        {
            var dbAlgorithm = this.context.RelayAlgorithms
                .Search(entity.Id);
            dbAlgorithm.Update(entity);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid) 
        {
            throw new NotImplementedException("функционал по удалению алгоритма РЗиА на данный момент не доступен");
        }
    }
}
