using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;
using System.Web.Mvc;

namespace MedinetClassLibrary.Services
{
    public class GraphicsDetailsServices: IRepositoryServices<GraphicDetail>
    {
        private IRepository<GraphicDetail> _repository;

        public GraphicsDetailsServices()
            : this(new Repository<GraphicDetail>())
        {
        }

        public GraphicsDetailsServices(IRepository<GraphicDetail> repository)
        {
            _repository = repository;
        }

        public bool Add(GraphicDetail entity)
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

        public IQueryable<GraphicDetail> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public GraphicDetail[] GetDetailsByTest(int test_id)
        {
            var _details= this._repository.GetAllRecords().Where(o => o.Test_Id==test_id).OrderBy(o=> o.Graphic_Id);
            GraphicDetail[] details = new GraphicDetail[new GraphicsServices().GetAllRecords().Count()];
            for (int i = 0; i < details.Length; i++)
            {
                details[i] = new GraphicDetail();
                details[i].Text = "";
                details[i].Graphic_Id = i + 1;
            }
          
            foreach(var detail in _details){
                details[detail.Graphic.Order]=detail ;
            }
            return details;
        }


        public GraphicDetail GetDetailsOrNewByGraphicId(int id)//obtiene los detalles del gráfico,se le puede colocar nombre,comentario,ejex,ejey
        {
            GraphicDetail graph = _repository.GetAllRecords().Where(g => g.Graphic_Id == id).FirstOrDefault();
            if (graph == null)
            {
                graph = new GraphicDetail();
                graph.Title = "";
                graph.AxisXName = "";
                graph.AxisYName = "";
                graph.Text = "";
            }
            return graph;
        }

        public GraphicDetail GetById(int id)
        {
            return _repository.GetById(id);
        }

        public GraphicDetail GetDetailsByGraphic(int graphic_id, int test_id)
        {
            if (_repository.GetAllRecords().Where(o => o.Graphic_Id == graphic_id && o.Test_Id == test_id).Count() > 0)
            {
                return _repository.GetAllRecords().Where(o => o.Graphic_Id == graphic_id && o.Test_Id == test_id).Single();
            }
            else
            {
                return null;
            }
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
