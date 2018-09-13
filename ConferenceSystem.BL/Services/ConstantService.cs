using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using ConferencySystem.BL.DTO;
using ConferencySystem.DAL.Data;

namespace ConferencySystem.BL.Services
{
    public class ConstantService
    {
        public ConstantDTO GetConstant(int id)
        {
            using (var db = new DbContext())
            {
                return Mapper.Map<Constant, ConstantDTO>(db.Constant.SingleOrDefault(e => e.Id == id));
            }
        }

        public void SaveConstant(ConstantDTO constantDTO)
        {
            using (var db = new DbContext())
            {
                Constant constant = Mapper.Map<ConstantDTO, Constant>(constantDTO);
                db.Constant.AddOrUpdate(constant);
                db.SaveChanges();
            }
        }
    }
}