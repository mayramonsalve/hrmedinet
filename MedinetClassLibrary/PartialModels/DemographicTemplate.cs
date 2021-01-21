using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.CustomClasses;
using MedinetClassLibrary.Models;

namespace MedinetClassLibrary.Services
{
    public class DemographicTemplate
    {
        public Company Company;
        private int Language;
        private bool Demo;
        public bool Ok;
        public int DemoId;

        private int companyId;
        private DateTime creationDate;

        private string companyName;
        private string email;
        private int countryId;
        private int weeks;
        private int employees;
        private int fotId;

        public DemographicTemplate(int Language, string companyName, int countryId, int weeks, int employees)
        {
            this.Ok = true;
            this.Language = Language;
            this.companyName = companyName;
            this.countryId = countryId;
            this.weeks = weeks;
            this.employees = employees;
            this.Demo = true;
            this.creationDate = DateTime.Now; 
            CreateDemo();
        }

        public DemographicTemplate(string companyName, string email, int weeks, int employees)
        {
            this.Ok = true;
            this.companyName = companyName;
            this.email = email;
            this.weeks = weeks;
            this.employees = employees;
            this.Demo = true;
            this.creationDate = DateTime.Now;
            CreateDemo();
        }

        public DemographicTemplate(int companyId, int countryId, int Language)
        {
            this.Ok = true;
            this.Language = Language;
            this.Company = new CompaniesServices().GetById(companyId);
            this.countryId = countryId;
            Demo = false;
            CreateDemographics();
        }

        public void CreateDemo()
        {
            CreateCompany();
            if(this.Ok)
                CreateUserManager();
            if (this.Ok)
                CreateBranch();
            if (this.Ok)
                CreateFunctionalOrganizatons();
            if (this.Ok)
                CreatePositionLevels();
            if (this.Ok)
                CreateInstructionLevels();
            if (this.Ok)
                CreateAgeRanges();
            //if (this.Ok)
            //    CreateSeniorityRanges();
            if (this.Ok)
                CreateTest();
        }

        private void CreateCompany()
        {
            Company company = new Company();
            company.Address = "";
            company.CompanyAssociated_Id = 2;
            company.CompanySector_Id = 3;
            company.CompanyType_Id = 3;
            company.Contact = "Mayra Monsalve";
            company.Name = companyName;
            company.Number = "1234567";
            company.Phone = "1234567";
            company.ShowClimate = true;
            if (new CompaniesServices().Add(company))
                this.Company = company;
            else
                this.Ok = false;
        }

        private void CreateDemographics()
        {
            //CreateBranch();
            if (this.Ok)
                CreateFunctionalOrganizatons();
            if (this.Ok)
                CreatePositionLevels();
            if (this.Ok)
                CreateInstructionLevels();
            if (this.Ok)
                CreateAgeRanges();
            //if (this.Ok)
            //    CreateSeniorityRanges();
        }

        private void CreateUserManager()
        {
            User manager = new User();
            manager.Address = "";
            manager.Company_Id = Company.Id;
            manager.ContactPhone = "1234567";
            manager.CreationDate = DateTime.Now;
            manager.Email = email;
            manager.FirstName = "Manager";
            manager.IdNumber = "0";
            manager.LastName = Company.Name;
            manager.Role_Id = 7;
            manager.UserName = "manager" + Company.Id;
            manager.Password = this.creationDate.ToString("ddHmmss");
            if (!new UsersServices().Add(manager))
                this.Ok = false;
        }

        private void CreateBranch()
        {
            Location location = new Location();
            location.Company_Id = Company.Id;
            location.Name = "Oficina Bogotá";
            location.ShortName = "Bogotá";
            location.State_Id = 150;
            if (!new LocationsServices().Add(location))
                this.Ok = false;
        }

        private void CreateFunctionalOrganizatons()
        {
            this.fotId = CreateType();
            if(this.Ok)
                CreateEntities(fotId);
        }

        private int CreateType()
        {
            FunctionalOrganizationType type = new FunctionalOrganizationType();
            type.Company_Id = Company.Id;
            type.Name = "Áreas";
            type.ShortName = "Área";
            if (!new FunctionalOrganizationTypesServices().Add(type))
            {
                this.Ok = false;
                return 0;
            }
            return type.Id;
        }

        private void CreateEntities(int type)
        {
            FunctionalOrganizationsServices fos = new FunctionalOrganizationsServices();


            foreach (FunctionalOrganization functorg in fos.GetByCompany(1117))
            {
                FunctionalOrganization newFunctOrg = new FunctionalOrganization();
                newFunctOrg.Name = functorg.Name;
                newFunctOrg.ShortName = functorg.ShortName;
                newFunctOrg.Type_Id = type;
                if (!fos.Add(newFunctOrg))
                    this.Ok = false;
            }
        }

        private void CreatePositionLevels()
        {
            PositionLevelsServices pls = new PositionLevelsServices();
            foreach (PositionLevel level in pls.GetByCompany(1117))
            {
                PositionLevel newLevel = new PositionLevel();
                newLevel.Company_Id = Company.Id;
                newLevel.Level = level.Level;
                newLevel.Name = level.Name;
                newLevel.ShortName = level.ShortName;
                if (!pls.Add(newLevel))
                    this.Ok = false;
            }
        }

        private void CreateInstructionLevels()
        {
            InstructionLevelsServices ils = new InstructionLevelsServices();
            foreach (InstructionLevel level in ils.GetByCompany(1117))
            {
                InstructionLevel newLevel = new InstructionLevel();
                newLevel.Company_Id = Company.Id;
                newLevel.Name = level.Name;
                newLevel.ShortName = level.ShortName;
                newLevel.Level = level.Level;
                if (!ils.Add(newLevel))
                    this.Ok = false;
            }
        }

        private void CreateAgeRanges()
        {
            AgesServices ars = new AgesServices();
            foreach (Age range in ars.GetByCompany(1117))
            {
                Age newRange = new Age();
                newRange.Company_Id = Company.Id;
                newRange.Name = range.Name;
                newRange.ShortName = range.ShortName;
                newRange.Level = range.Level;
                if (!ars.Add(newRange))
                    this.Ok = false;
            }
        }

        private void CreateSeniorityRanges()
        {
            SenioritiesServices srs = new SenioritiesServices();
            foreach (Seniority range in srs.GetByCompany(1117))
            {
                Seniority newRange = new Seniority();
                newRange.Company_Id = Company.Id;
                newRange.Name = range.Name;
                newRange.ShortName = range.ShortName;
                newRange.Level = range.Level;
                if (!srs.Add(newRange))
                    this.Ok = false;
            }
        }

        private void CreateTest()
        {
            Test test = new Test();
            test.Code = ShortGuid.NewGuid().ToString();
            test.Company_Id = Company.Id;
            test.CreationDate = this.creationDate;
            test.CurrentEvaluations = 0;
            test.Disordered = true;
            test.OneQuestionnaire = true;
            test.EvaluationNumber = employees;
            test.EvaluationsLefts = employees;
            test.GroupByCategories = false;
            test.MinimumPeople = 2;
            test.ConfidenceLevel_Id = 1;
            test.StandardError_Id = 6;
            test.Name = "Medición de Engagement Organizacional - " + Company.Name;
            test.Questionnaire_Id = 74;
            test.RecordsPerPage = 7;
            test.StartDate = DateTime.Now;
            test.EndDate = DateTime.Now.AddDays(weeks * 7);
            test.Text = "Medición Demo de Engagement Organizacional para " + Company.Name;
            test.User_Id = 8;
            test.Weighted = false;
            if (!new TestsServices().Add(test))
                this.Ok = false;
            else
                CreateDemographicsInTest(test.Id);
        }

        private void CreateDemographicsInTest(int test_id)
        {
            DemographicsInTestsServices dits = new DemographicsInTestsServices();
            DemographicsInTest dit;
            int[] demographics = new int[7] { 1, 2, 3, 5, 7, 10, 11 };
            bool b = true;
            foreach(int i  in demographics)
            {
                dit = new DemographicsInTest();
                dit.Demographic_Id = i;
                dit.Selector = false;
                dit.Test_Id = test_id;
                if(i == 11)
                    dit.FOT_Id = this.fotId;
                b = b && dits.Add(dit);
            }
            this.Ok = b;
        }
    }
}
