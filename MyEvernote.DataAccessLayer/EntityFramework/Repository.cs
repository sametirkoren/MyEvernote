﻿using MyEvernote.Common;
using MyEvernote.Core.DataAccess;
using MyEvernote.DataAccessLayer;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    public class Repository<T> : RepositoryBase , IDataAccess<T> where T: class
    {
      
        private DbSet<T> _objectSet;
        public Repository()
        {
            _objectSet = context.Set<T>();
        }

        public List<T> List()
        {
            // gelen tip neyse o tipin setini bul ve ona ToListi uygulana ve dönen listeyi geriye döndür.
            return  _objectSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            if(obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime now = DateTime.Now;

                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUsername =  App.Common.GetUsername(); // TODO : İşlem yapan kullanıcı adı yazılmalı...
            }

            return Save();
        }

        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
               
                o.ModifiedOn = DateTime.Now;
                o.ModifiedUsername = App.Common.GetUsername(); // TODO : İşlem yapan kullanıcı adı yazılmalı...
            }

            return Save();
        }

        public int Delete(T obj)
        {
           
            _objectSet.Remove(obj);
            return Save();
        }

        public List<T> List(Expression<Func<T,bool>>where)
        {
            return _objectSet.Where(where).ToList();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public T Find(Expression<Func<T,bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }
    }
}
