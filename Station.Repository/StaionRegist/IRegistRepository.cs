using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Station.Entity.DB2Admin;

namespace Station.Repository.StaionRegist
{
    public interface IRegistRepository
    {
        /// <summary>
        /// 查询所有信息
        /// </summary>
        Task<IEnumerable<Regist>> GetRegistsAsync();

        /// <summary>
        /// 根据id集合查询
        /// </summary>
        Task<IEnumerable<Regist>> GetRegistsAsync(IEnumerable<string> companyIds);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="regist"></param>
        void AddRegist(Regist regist);


        Task<bool> SaveAsync();

    }
}