using DAL;
using IBLL;
using IDAL;
using Models;
using Models.DTO;
using Models.Enums;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ConsumableInfoBLL : IConsumableInfoBLL
    {
        private IConsumableInfoDAL _consumableInfoDAL;
        private RepositorySystemContext _dbContext;
        public ConsumableInfoBLL(RepositorySystemContext dbContext, IConsumableInfoDAL consumableInfoDAL) { _dbContext = dbContext; _consumableInfoDAL = consumableInfoDAL; }

        public bool CreateConsumableInfo(ConsumableInfo entity, out string msg)
        {
            //throw new NotImplementedException();
            if (string.IsNullOrWhiteSpace(entity.ConsumableName))
            {
                msg = "耗材名字不能为空";
                return false;
            }


            ConsumableInfo consumableInfo = _consumableInfoDAL.GetEntities().FirstOrDefault(u => u.ConsumableName == entity.ConsumableName );
            if (consumableInfo != null)
            {
                msg = "耗材已存在";
                return false;
            }

            // 赋值id
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedTime = DateTime.Now;
            try
            {
                _consumableInfoDAL.CreateEntity(entity);
                msg = $"添加{entity.ConsumableName}成功!";
                return true;
            }
            catch (Exception ex)
            {
                msg = "添加分类失败";
                return false;
            }
        }

        public bool DeleteConsumableInfo(string id)
        {
            ConsumableInfo consumable = _consumableInfoDAL.GetEntities().FirstOrDefault(u => u.Id == id);
            if (consumable == null)
            {
                return false;
            }
            consumable.IsDelete = true;
            consumable.DeleteTime = DateTime.Now;

            return _consumableInfoDAL.UpdateEntity(consumable);
        }

        public bool DeleteConsumableInfo(List<string> ids)
        {
            int count = 0;
            foreach (var item in ids)
            {
                ConsumableInfo consumable = _consumableInfoDAL.GetEntities().FirstOrDefault(u => u.Id == item);
                if (consumable == null)
                {
                    continue;
                }
                consumable.IsDelete = true;
                consumable.DeleteTime = DateTime.Now;

                _consumableInfoDAL.UpdateEntity(consumable);
                count++;
            }
            return count > 0;
        }

        public List<GetConsumableInfoDTO> GetAllConsumableInfos(int page, int limit, string id, string ConsumableName, out int count)
        {
            var tempList = (from r in _consumableInfoDAL.GetConsumableInfo().Where(r => r.IsDelete == false)
                            select new GetConsumableInfoDTO
                            {
                                Id = r.Id,
                                Description = r.Description,
                                CategoryId = r.CategoryId,
                                ConsumableName = r.ConsumableName,
                                Specification = r.Specification,
                                Num = r.Num,
                                Unit = r.Unit,
                                Money = r.Money,
                                WarningNum = r.WarningNum,
                                CreateTime =    r.CreatedTime

                            }).ToList();
            count = _consumableInfoDAL.GetConsumableInfo().Count();
            return tempList.OrderBy(u => u.CreateTime).Skip(limit * (page - 1)).Take(limit).ToList(); ;
        }

        public Stream GetDownload(out string FileName)
        {
            var datas = (from cr in _dbContext.ConsumableRecord
                        join c in _dbContext.ConsumableInfo.Where(x => x.IsDelete == false) on cr.ConsumableId equals c.Id
                        into tempCRC
                        from cc in tempCRC.DefaultIfEmpty()

                        join u in _dbContext.UserInfo on cr.Creator equals u.Id into tempCRU
                        from uu in tempCRU.DefaultIfEmpty()
                        select new
                        {
                            cc.ConsumableName,
                            cc.Specification,
                            Type = cr.Type == (int)ConsumableRecordTypeEnums.入库 ? "入库" : "出库",
                            cr.Num,
                            CreateTime = cr.CreatedTime,
                            uu.UserName,

                        }).ToList();
            //排序
            datas = datas.OrderByDescending(it => it.CreateTime).ToList();
            string path = Directory.GetCurrentDirectory();
            string file_name = "出入库记录" + DateTime.Now.ToString("yyyy-MM-dd hh mm ss") + ".xlsx";

            string filePath = Path.Combine(path, file_name);

            IWorkbook wk = null;
            string extension = Path.GetExtension(filePath);

            using (FileStream fs = new FileStream(filePath,FileMode.Create,FileAccess.ReadWrite))
            {
                if(extension.Equals(".xls"))
                {
                    wk = new HSSFWorkbook();
                }
                else
                {
                    wk = new XSSFWorkbook();
                }
                ISheet sheet = wk.CreateSheet("sheet1");

                //创建表头
                IRow row = sheet.CreateRow(0);

                string[] title =
                {
                    "耗材名称",
                    "耗材规格",
                    "出入库类型",
                    "出入库数量",
                    "出入库时间",
                    "操作人"
                };

                for (int i = 0; i < title.Length; i++)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(title[i]);
                }
                //数据主体
                for (int i = 0; i < datas.Count; i++)
                {
                    var data = datas[i];

                    IRow tempRow = sheet.CreateRow(i+1);

                    ICell tempCell = tempRow.CreateCell(0);
                    tempCell.SetCellValue(data.ConsumableName);

                    ICell tempCell2 = tempRow.CreateCell(1);
                    tempCell2.SetCellValue(data.Specification);

                    ICell tempCell3 = tempRow.CreateCell(2);
                    tempCell3.SetCellValue(data.Type);

                    ICell tempCell4 = tempRow.CreateCell(3);
                    tempCell4.SetCellValue(data.Num);

                    ICell tempCell5 = tempRow.CreateCell(4);
                    tempCell5.SetCellValue(data.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"));
                    //tempCell5.SetCellValue("1111111111");

                    ICell tempCell6 = tempRow.CreateCell(5);
                    tempCell6.SetCellValue(data.UserName);
                }
                wk.Write(fs);
                FileStream fileStream = new FileStream(filePath,FileMode.Open,FileAccess.Read);
                FileName = file_name;
                return fileStream;
            }

        }

        public object GetSelectOptions()
        {
            var list = _consumableInfoDAL.GetEntities().Where(x => x.IsDelete == false).Select(x => new 
            {
                value = x.Id,
                title = x.ConsumableName
            }).ToList();
            return list;
        }

        public bool UpdateConsumableInfo(ConsumableInfo entity, out string msg)
        {
            if (string.IsNullOrWhiteSpace(entity.ConsumableName))
            {
                msg = "名字不能为空";
                return false;
            }

            ConsumableInfo consumableInfo = _consumableInfoDAL.GetEntities().FirstOrDefault(u => u.CategoryId == entity.CategoryId);
            if (consumableInfo == null)
            {
                msg = "分类不存在";
                return false;
            }
            consumableInfo.ConsumableName = entity.ConsumableName;
            consumableInfo.Specification = entity.Specification;
            consumableInfo.Unit = entity.Unit;
            consumableInfo.Money = entity.Money;
            consumableInfo.WarningNum = entity.WarningNum;
            consumableInfo.Description = entity.Description;
            consumableInfo.CategoryId = entity.CategoryId;
            try
            {
                _consumableInfoDAL.UpdateEntity(consumableInfo);
                msg = $"更新{consumableInfo.ConsumableName}成功!";
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                msg = "并发更新异常，实体已被修改，请重新加载实体。";
                // 重新加载实体
                _dbContext.Entry(consumableInfo).Reload();
                return false;
            }
            catch (Exception ex)
            {
                msg = "更新分类失败";
                return false;
            }
        }

        public bool Upload(Stream stream, string extension, string id, out string msg)
        {
            //throw new NotImplementedException();
            IWorkbook wk = null;

            if (extension.Equals(".xls"))// 老版本Excel
            {
                wk = new HSSFWorkbook(stream);
            }
            else
            {
                wk = new XSSFWorkbook(stream);
            }

            stream.Close();//释放文件
            stream.Dispose();

            ISheet sheet = wk.GetSheetAt(0);//获取第一页

            int RowNum = sheet.LastRowNum;

            // 开启事务
            using(var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    for (int i = 1; i <= RowNum; i++)
                    {
                        IRow Row = sheet.GetRow(i);

                        ICell Cell = Row.GetCell(0);
                        string value = Cell.ToString();//获取商品名称

                        ICell Cell2 = Row.GetCell(2);
                        string value2 = Cell2.ToString();//实际购买数量

                        int num;
                        bool b = int.TryParse(value2, out num);
                        if (b == false)
                        {
                            transaction.Rollback();//回滚
                            msg = $"第{i + i}行耗材的实际购买数量有误";
                            return false;
                        }
                        // 查询该商品在数据库中的数据
                        ConsumableInfo consumable = _consumableInfoDAL.GetEntities().FirstOrDefault(x => x.ConsumableName == value && x.IsDelete == false);
                        if (consumable == null)
                        {
                            transaction.Rollback();
                            msg = $"第{i + i}行耗材不存在";
                            return false;
                        }

                        ConsumableRecord consumableRecord = new ConsumableRecord()
                        {
                            Id = Guid.NewGuid().ToString(),
                            ConsumableId = consumable.Id,
                            CreatedTime = DateTime.Now,
                            Creator = id,
                            Num = num,
                            Type = (int)ConsumableRecordTypeEnums.入库,
                        };
                        //提交到数据库
                        _dbContext.ConsumableRecord.Add(consumableRecord);
                        bool isOk = _dbContext.SaveChanges() > 0;
                        if (isOk == false)
                        {
                            transaction.Rollback();
                            msg = $"添加第{i + i}行耗材失败";
                            return false;
                        }
                        // 更新耗材信息库存
                        consumable.Num += num;
                        _dbContext.Entry(consumable).State = System.Data.Entity.EntityState.Modified;
                        isOk = _dbContext.SaveChanges() > 0;
                        if (isOk == false)
                        {
                            transaction.Rollback();
                            msg = $"添加第{i + i}行耗材更新";
                            return false;
                        }
                    }
                    // 提交事务
                    transaction.Commit();
                    msg = "入库成功";
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    msg = "出错了:" + ex.Message;
                    return false;
                    throw;
                }                   
            }         
        }
    }
}
