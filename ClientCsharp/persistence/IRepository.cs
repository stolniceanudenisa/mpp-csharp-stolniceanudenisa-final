using System;
using System.Collections.Generic;
using mpp_csharp_stolniceanudenisa_final.model;

namespace ClientCsharp.persistence
{
    public interface IRepository<ID, E> where E : Entity<ID>
    {
        E findOne(ID id); //public
        IEnumerable<E> GetAll();
        void Add(E entity);
        void Clear();
        void Update(E entity);
        void Delete(ID id);
        
    }
    
    public class RepositoryException : ApplicationException
    {
        public RepositoryException() { }
        public RepositoryException(String mess) : base(mess){}
        public RepositoryException(String mess, Exception e) : base(mess, e) { }
    }
}