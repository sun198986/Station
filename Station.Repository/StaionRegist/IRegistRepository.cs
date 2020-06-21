using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Station.Entity.DB2Admin;

namespace Station.Repository.StaionRegist
{
    public interface IRegistRepository:IRepositoryBase
    {
        /// <summary>
        /// 查询所有信息
        /// </summary>
        Task<IEnumerable<Regist>> GetRegistsAsync();

        /// <summary>
        /// 根据id获取信息
        /// </summary>
        /// <param name="registId"></param>
        /// <returns></returns>

        Task<Regist> GetRegistsAsync(string registId);

        /// <summary>
        /// 根据id集合查询
        /// </summary>
        Task<IEnumerable<Regist>> GetRegistsAsync(IEnumerable<string> registIds);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="regist"></param>
        void AddRegist(Regist regist);


        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="regists"></param>
        void AddRegist(IEnumerable<Regist> regists);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="regist"></param>
        void DeleteRegist(Regist regist);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="regists"></param>
        void DeleteRegist(IEnumerable<Regist> regists);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="regist"></param>
        void UpdateRegist(Regist regist);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="registId"></param>
        /// <returns></returns>
        Task<bool> RegistExistsAsync(string registId);

        /// <summary>
        /// 异步保存
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAsync();

        ///// <summary>
        ///// 同步保存
        ///// </summary>
        ///// <returns></returns>
       // bool SaveChange();

    }
}