using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class TicketsServices : IRepositoryServices<Ticket>
    {
        private IRepository<Ticket> _repository;

        public TicketsServices()
            : this(new Repository<Ticket>())
        {
        }

        public TicketsServices(IRepository<Ticket> repository)
        {
            _repository = repository;
        }

        public bool Add(Ticket entity)
        {
            try
            {
                _repository.Add(entity);
                _repository.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IQueryable<Ticket> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public Ticket GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Ticket GetByEmail(string email)
        {
            return _repository.GetAllRecords().Where(d => d.Email == email).FirstOrDefault();
        }

        public bool IsAlredyExists(string email)
        {
            Ticket t = GetByEmail(email);
            if (t == null)
            {
                return false;
            }
            return true;
        }
        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

    }
}