﻿using Dapper;
using Domain.Entity;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using MSIA.WebFresher052023.Infrastructure.Repository.Base;
using System.Data;
using static Dapper.SqlMapper;

namespace Infrastructure.Repository
{
    public class ReceiverRepository : BaseRepository<Receiver, ReceiverModel>, IReceiverRepository
    {
        #region Constructor
        public ReceiverRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        /// <summary>
        /// Lấy danh sách các bản ghi trong danh sách id
        /// </summary>
        /// <param name="ids">Danh sách id của các bản ghi cần lấy dữ liệu</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: ldtuan (24/07/2023)
        public override async Task<List<Receiver>> GetListByIdsAsync(List<Guid> ids)
        {
            var tableName = TableNameSnakeCase;
            var sql = $"select * from view_{tableName} where TransferAssetId in @listIds;";
            var param = new DynamicParameters();
            param.Add("listIds", ids);

            var listEntities = await _unitOfWork.Connection.QueryAsync<Receiver>(sql, param, transaction: _unitOfWork.Transaction);
            return listEntities.ToList();
        }
    }
}
