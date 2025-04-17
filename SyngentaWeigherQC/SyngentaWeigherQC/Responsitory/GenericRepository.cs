using Microsoft.EntityFrameworkCore;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Control
{
  public class GenericRepository<TEntity, TContext> where TEntity : BaseModel where TContext : DbContext
  {
    public DbContext Context { get; set; }
    public GenericRepository(DbContext context)
    {
      this.Context = context;
    }

    public GenericRepository()
    {

    }


    public async Task<List<TEntity>> GetAllAsync()
    {
      return await this.Context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetAsync()
    {
      return await this.Context.Set<TEntity>().FirstOrDefaultAsync();
    }


    public async Task<TEntity> Add(TEntity datas)
    {
      try
      {
        await Context.Database.BeginTransactionAsync();
        await Context.Database.EnsureCreatedAsync();
        var rs = await Context.Set<TEntity>().AddAsync(datas);
        await Context.SaveChangesAsync();
        Context.Database.CommitTransaction();

        return rs.Entity;
      }
      catch (Exception ex)
      {
        Context.Database.RollbackTransaction();
        throw ex;
      }
    }

    public async Task Remove(TEntity datas)
    {
      try
      {
        await Context.Database.BeginTransactionAsync();
        await Context.Database.EnsureCreatedAsync();
        Context.Set<TEntity>().Remove(datas);
        await Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
      }
      catch (Exception)
      {
        Context.Database.RollbackTransaction();
      }
    }

    public async Task<bool> Update(TEntity data)
    {
      try
      {
        await Context.Database.BeginTransactionAsync();
        await Context.Database.EnsureCreatedAsync();
        Context.Set<TEntity>().Update(data);
        await Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
        return true;
      }
      catch (Exception)
      {
        Context.Database.RollbackTransaction();
        return false;
      }
    }

    public async Task<bool> UpdateRange(List<TEntity> datas)
    {
      try
      {
        await Context.Database.BeginTransactionAsync();
        await Context.Database.EnsureCreatedAsync();
        Context.Set<TEntity>().UpdateRange(datas);
        await Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
        return true;
      }
      catch (Exception)
      {
        Context.Database.RollbackTransaction();
        return false;
      }
    }


    public async Task Delete(TEntity datas)
    {
      try
      {
        await Context.Database.BeginTransactionAsync();
        var dbset = Context.Set<TEntity>();
        dbset.Remove(datas);
        await Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
      }
      catch (Exception)
      {
        Context.Database.RollbackTransaction();
      }
    }


    public async Task AddRange(List<TEntity> datas)
    {
      try
      {
        await Context.Database.EnsureCreatedAsync();
        await Context.Database.BeginTransactionAsync();
        await Context.Set<TEntity>().AddRangeAsync(datas);
        await Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
      }
      catch (Exception ex)
      {
        Context.Database.RollbackTransaction();
        throw ex;
      }

    }


    public virtual Task<bool> UpdateRange(TEntity entity)
    {
      throw new NotImplementedException();
    }

    public virtual Task<bool> UpdateActiveProduct(string FGs)
    {
      throw new NotImplementedException();
    }
    public virtual Task<bool> UpdateClearActiveProduct()
    {
      throw new NotImplementedException();
    }

    public virtual Task<bool> UpdateRangeByIsDelete(List<Production> data)
    {
      throw new NotImplementedException();
    }

    public virtual Task<bool> UpdateSample(Sample sample)
    {
      throw new NotImplementedException();
    }

    public virtual Task<bool> UpdateDatalog(DatalogWeight datalog)
    {
      throw new NotImplementedException();
    }

    //LoadAllDatalogsByGroupId
    public virtual Task<List<DatalogWeight>> LoadAllDatalogsByGroupId(int groupId)
    {
      throw new NotImplementedException();
    }

    //LoadAllDatalogsByProductIdId
    public virtual Task<List<DatalogWeight>> LoadAllDatalogsByProductId(int groupId)
    {
      throw new NotImplementedException();
    }
    public virtual Task<bool> CheckGroupIdDatalog(int groupId)
    {
      throw new NotImplementedException();
    }
    //CheckGroupIdDatalog
    public virtual Task<List<Sample>> LoadAllSamplesByDatalogId(List<int> datalogId)
    {
      throw new NotImplementedException();
    }

    public virtual Task<List<Sample>> LoadAllSamplesByGroupId(int datalogId)
    {
      throw new NotImplementedException();
    }
    public virtual Task<List<Sample>> LoadAllSamplesByGroupId_V2(int datalogId)
    {
      throw new NotImplementedException();
    }
    public virtual Task<bool> UpdateProductChoose(List<Production> productions)
    {
      throw new NotImplementedException();
    }



    public virtual Task<bool> UpdateUserChoose(List<ShiftLeader> users)
    {
      throw new NotImplementedException();
    }

    public virtual Task<bool> UpdateShiftTypesChoose(List<ShiftType> shiftTypes)
    {
      throw new NotImplementedException();
    }


    public virtual Task<List<Production>> LoadAllProducts()
    {
      throw new NotImplementedException();
    }

    public virtual Task<List<DatalogWeight>> LoadDatalogsByShiftId(int shiftId)
    {
      throw new NotImplementedException();
    }

    public virtual Task<bool> GetDatalogSampleByShift(int shiftId)
    {
      throw new NotImplementedException();
    }



    public virtual Task<List<DatalogWeight>> GetDatalogReport()
    {
      throw new NotImplementedException();
    }
    public virtual Task<List<Sample>> GetSampleReport()
    {
      throw new NotImplementedException();
    }

    //public virtual Task<List<Datalog>> GetDataReportByFilterV2(int productId, DateTime from, DateTime to, int frmHour, int fromMinute, int fromSecond, int toHour, int toMinute, int toSecond, int shiftId, eDataLoad eDataLoad, int getNumber)
    //{
    //  throw new NotImplementedException();
    //}


    public virtual Task<bool> CheckSTTExits(ulong STT, int shiftId)
    {
      throw new NotImplementedException();
    }

  }
}
