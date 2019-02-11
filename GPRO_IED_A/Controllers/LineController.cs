﻿using GPRO.Core.Mvc;
using GPRO_IED_A.Business;
using GPRO_IED_A.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GPRO_IED_A.Controllers
{
    public class LineController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            ResponseBase responseResult;
            try
            {
                responseResult = BLLLine.Instance.Delete(Id, UserContext.UserID);
                if (responseResult.IsSuccess)
                    JsonDataResult.Result = "OK";
                else
                {
                    JsonDataResult.Result = "ERROR";
                    JsonDataResult.ErrorMessages.AddRange(responseResult.Errors);
                }
            }
            catch (Exception ex)
            {
                //add error
                JsonDataResult.Result = "ERROR";
                JsonDataResult.ErrorMessages.Add(new Error() { MemberName = "Delete Area", Message = "Lỗi: " + ex.Message });
            }
            return Json(JsonDataResult);
        }

        public JsonResult Gets(string keyword, int searchBy, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                var listLine = BLLLine.Instance.Gets(keyword, searchBy, jtStartIndex, jtPageSize, jtSorting, UserContext.CompanyId, UserContext.ChildCompanyId);
                JsonDataResult.Records = listLine;
                JsonDataResult.Result = "OK";
                JsonDataResult.TotalRecordCount = listLine.TotalItemCount;
            }
            catch (Exception ex)
            {
                JsonDataResult.Result = "ERROR";
                JsonDataResult.ErrorMessages.Add(new Error() { MemberName = "Get List ObjectType", Message = "Lỗi: " + ex.Message });
            }
            return Json(JsonDataResult);
        }


        public JsonResult Save(LineModel modelLine)
        {
            ResponseBase responseResult;
            try
            {
                modelLine.ActionUser = UserContext.UserID; 
                responseResult = BLLLine.Instance.InsertOrUpdate(modelLine);
                if (!responseResult.IsSuccess)
                {
                    JsonDataResult.Result = "ERROR";
                    JsonDataResult.ErrorMessages.AddRange(responseResult.Errors);
                }
                else
                    JsonDataResult.Result = "OK";
            }
            catch (Exception ex)
            {
                //add error
                JsonDataResult.Result = "ERROR";
                JsonDataResult.ErrorMessages.Add(new Error() { MemberName = "Update ", Message = "Lỗi: " + ex.Message });
            }
            return Json(JsonDataResult);
        }


        [HttpPost]
        public JsonResult GetSelect(int workshopId)
        {
            try
            {
                JsonDataResult.Result = "OK";
                JsonDataResult.Data = BLLLine.Instance.Gets(workshopId);
            }
            catch (Exception ex)
            {
                JsonDataResult.Result = "ERROR";
                JsonDataResult.ErrorMessages.Add(new Error() { MemberName = "Get List ObjectType", Message = "Lỗi: " + ex.Message });
            }
            return Json(JsonDataResult);
        }


    }
}