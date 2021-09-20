using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.DAL;

namespace PMS.BLL
{
    public class BaseBLL<T> where T :class
    {
        private BaseDAL<T> dal = new BaseDAL<T>();

        #region 添加

        /// <summary>
        ///   添加
        /// </summary>
        /// <param name="t"></param>
        /// <param name="strCols"></param>
        /// <returns></returns>
        public bool Add(T t,string strCols)
        {
            return dal.Add(t, strCols, 0) > 0;
        }

        public int AddReturnId(T t,string strCols)
        {
            return dal.Add(t, strCols, 1);
        }

        public bool AddList(List<T> list,string strCols)
        {
            return dal.AddList(list, strCols);
        }
        #endregion

        #region 修改
        public bool Update(T t,string strCols)
        {
            return dal.Update(t, strCols);
        }

        public bool UpdateList(List<T> list,string strCols)
        {
            return dal.UpdateList(list, strCols);
        }
        #endregion

        #region 删除

        public bool Delete(int id,int delType)
        {
            return dal.Delete(id, delType);
        }

        public bool DeleteList(List<int> idList,int actType)
        {
            return dal.DeleteList(idList, actType);
        }

        #endregion

        #region 查询 

        public T GetById(int id,string strCols)
        {
          return  dal.GetById(id, strCols);
        }

        public bool ExistsByName(string sName,string vName)
        {
            return dal.ExistsByName(sName, vName);
        }

        public bool ExistsByName(string sName,string vName,string sParent,int parId)
        {
            return dal.ExistsByName(sName, vName, sParent, parId);
        }

        public List<T> GetModelList(string strCols)
        {
            return dal.GetModelList(strCols);
            
        }

        
        #endregion
    }
}
