﻿using GPRO.Core.Mvc;
using GPRO.Ultilities;
using GPRO_IED_A.Business.Model;
using GPRO_IED_A.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPRO_IED_A.Business
{
    public class BLLWorkerLevel
    {
        #region constructor
        IEDEntities db;
        static object key = new object();
        private static volatile BLLWorkerLevel _Instance;
        public static BLLWorkerLevel Instance
        {
            get
            {
                if (_Instance == null)
                    lock (key)
                        _Instance = new BLLWorkerLevel();

                return _Instance;
            }
        }
        private BLLWorkerLevel() { }
        #endregion

        private bool CheckExists(string name, int Id, int companyId, int[] relationCompanyId, IEDEntities db)
        {
            try
            {
                var obj = db.SWorkerLevels.FirstOrDefault(c => !c.IsDeleted && c.Id != Id && (c.CompanyId == null || c.CompanyId == companyId || relationCompanyId.Contains(c.CompanyId ?? 0)) && c.Name.Trim().ToUpper().Equals(name.Trim().ToUpper()));
                if (obj == null)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseBase InsertOrUpdate(WorkerLevelModel model, int[] relationCompanyId)
        {
            var result = new ResponseBase();
            result.IsSuccess = false;
            var flag = false;
            try
            {
                using (db = new IEDEntities())
                {
                    if (CheckExists(model.Name, model.Id, model.CompanyId ?? 0, relationCompanyId, db))
                    {
                        flag = true;
                        result.IsSuccess = false;
                        result.Errors.Add(new Error() { MemberName = "Create  ", Message = "Tên Bậc Thợ này Đã Tồn Tại, Vui Lòng Chọn Tên Khác" });
                    }

                    if (!flag)
                    {
                        SWorkerLevel obj = null;
                        if (model.Id == 0)
                        {
                            obj = new SWorkerLevel();
                            Parse.CopyObject(model, ref obj);
                            obj.CreatedDate = DateTime.Now;
                            obj.CompanyId = null;
                            if (model.IsPrivate)
                                obj.CompanyId = model.CompanyId;
                            db.SWorkerLevels.Add(obj);
                        }
                        else
                        {
                            obj = db.SWorkerLevels.FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                            if (obj != null)
                            {
                                obj.Coefficient = model.Coefficient;
                                obj.CompanyId = null;
                                if (model.IsPrivate)
                                    obj.CompanyId = model.CompanyId;
                                obj.Name = model.Name;
                                obj.Note = model.Note;
                                obj.UpdatedDate = DateTime.Now;
                                obj.UpdatedUser = model.ActionUser;
                            }
                            else
                            {
                                result.IsSuccess = false;
                                result.Errors.Add(new Error() { MemberName = "Update ", Message = "Dữ Liệu Bậc Thợ bạn đang thao tác không tồn tại hoặc đã bị xóa.\nVui lòng kiểm tra lại!" });
                            }
                        }
                        db.SaveChanges(); ;
                        result.IsSuccess = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public ResponseBase Delete(int id, int userId)
        {
            ResponseBase responResult;
            try
            {
                using (db = new IEDEntities())
                {
                    responResult = new ResponseBase();
                    var obj = db.SWorkerLevels.Where(c => !c.IsDeleted && c.Id == id).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.IsDeleted = true;
                        obj.DeletedUser = userId;
                        obj.DeletedDate = DateTime.Now;
                        db.SaveChanges(); ;
                        responResult.IsSuccess = true;
                    }
                    else
                    {
                        responResult.IsSuccess = false;
                        responResult.Errors.Add(new Error() { MemberName = "Delete", Message = "Đối Tượng Đã Bị Xóa,Vui Lòng Kiểm Tra Lại" });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responResult;
        }

        public PagedList<WorkerLevelModel> Gets(string keyWord, int startIndexRecord, int pageSize, string sorting, int companyId, int[] relationCompanyId)
        {
            try
            {
                using (db = new IEDEntities())
                {
                    var objs = new List<WorkerLevelModel>();
                    if (string.IsNullOrEmpty(sorting))
                        sorting = "CreatedDate DESC";

                    if (!string.IsNullOrEmpty(keyWord))
                        objs.AddRange(db.SWorkerLevels.Where(c => !c.IsDeleted && (c.CompanyId == null || c.CompanyId == companyId || relationCompanyId.Contains(c.CompanyId ?? 0)) && c.Name.Trim().ToUpper().Contains(keyWord.Trim().ToUpper())).OrderByDescending(x => x.CreatedDate).Select(x => new WorkerLevelModel()
                        {
                            Id = x.Id,
                            CompanyId = x.CompanyId,
                            Coefficient = x.Coefficient,
                            Name = x.Name,
                            Note = x.Note,
                            IsPrivate = (x.CompanyId != null ? true : false)
                        }).ToList());
                    else
                        objs.AddRange(db.SWorkerLevels.Where(c => !c.IsDeleted && (c.CompanyId == null || c.CompanyId == companyId || relationCompanyId.Contains(c.CompanyId ?? 0))).OrderByDescending(x => x.CreatedDate).Select(x => new WorkerLevelModel()
                        {
                            Id = x.Id,
                            CompanyId = x.CompanyId,
                            Coefficient = x.Coefficient,
                            Name = x.Name,
                            Note = x.Note,
                            IsPrivate = (x.CompanyId != null ? true : false)
                        }).ToList());
                    var pageNumber = (startIndexRecord / pageSize) + 1;
                    return new PagedList<WorkerLevelModel>(objs, pageNumber, pageSize);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ModelSelectItem> Gets(int companyId, int[] relationCompanyId)
        {
            using (db = new IEDEntities())
            {
                var objs = new List<ModelSelectItem>();
                try
                {
                    objs.AddRange(db.SWorkerLevels.Where(x => !x.IsDeleted && (x.CompanyId == null || x.CompanyId == companyId || relationCompanyId.Contains(x.CompanyId ?? 0))).Select(x => new ModelSelectItem() { Value = x.Id, Name = x.Name, Double = x.Coefficient }));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return objs;
            }

        }

    }
}
