﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Commonlayer;
using DataAccessLayer;
using System.Transactions;
using Business_Layer;

namespace UnitTestsRole
{
    [TestClass]
    public class UnitTest1
    {
        private TransactionScope TransactionS;
        //private RoleRepository roleRep;
        private Role role = null;
        private RoleService1 roleService;
        

        [TestInitialize]
        public void StartUp()
        {
            TransactionS = new TransactionScope();
            roleService = new RoleService1();
            role = new Role();
        }

        [TestCleanup]
        public void CleanUp()
        {
            TransactionS.Dispose();
        }

        [TestMethod]
        public void CreateRole_Successful()
        {
            string roleN = "RoleTest";
            roleService.AddRole(roleN);
            Assert.IsNotNull(roleService.GetRoles(roleN));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateRole_Unsuccessful()
        {
            string roleN = null;
            roleService.AddRole(roleN);
            Assert.IsNull(roleService.GetRoles(roleN));
        }

        [TestMethod]
        public void CreateRole_ValidNameNumbers()
        {
            string roleN = "1232";
            roleService.AddRole(roleN);
            Assert.IsNotNull(roleService.GetRoles(roleN));
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Infrastructure.DbUpdateException))]
        public void DeleteRole_ImpRole()
        {
            roleService.DeleteRole(4);
            Assert.IsNotNull(roleService.GetRoles("Seller"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteRole_InvalidRole()
        {
            roleService.DeleteRole(6);
            Assert.IsNull(roleService.GetRole(6));
        }

        [TestMethod]
        public void DeleteRole_SuccessfulRole()
        {
            roleService.DeleteRole(1036);
            Assert.IsNull(roleService.GetRole(1036));
        }

        [TestMethod]
        public void ReadRoles_Successful()
        {
            Assert.IsNotNull(roleService.GetAllRoles());
        }

        [TestMethod]
        public void GetRoleByName_Successful()
        {
            Assert.IsNotNull(roleService.GetRoles("Guest"));
        }

        [TestMethod]
        public void GetRoleByName_NoneExistingRole()
        {
            Assert.IsNull(roleService.GetRoles("Player"));
        }

        [TestMethod]
        public void UpdateRole_Successful()
        {
            roleService.UpdateRole(1036, "TestR2");
            Assert.IsNotNull(roleService.GetRoles("TestR2"));
        }

        [TestMethod]
        public void UpdateRole_MainRole()
        {
            roleService.UpdateRole(1, "Guest2");
            Assert.IsNotNull(roleService.GetRoles("Guest2"));
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException))]
        public void UpdateRole_Unsuccessfull()
        {
            roleService.UpdateRole(1, null);
            Assert.IsNotNull(roleService.GetRoles("Guest"));
        }

        [TestMethod]
        public void UpdateRole_NumberRole()
        {
            roleService.UpdateRole(1, "123");
            Assert.IsNotNull(roleService.GetRoles("123"));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateRole_InvalidID()
        {
            roleService.UpdateRole(16, "Test");
        }

    }
}
