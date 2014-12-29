﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TraderMarket.RoleService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RoleService.IRoleService1")]
    public interface IRoleService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/AddRole", ReplyAction="http://tempuri.org/IRoleService1/AddRoleResponse")]
        void AddRole(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/AddRole", ReplyAction="http://tempuri.org/IRoleService1/AddRoleResponse")]
        System.Threading.Tasks.Task AddRoleAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/GetRoles", ReplyAction="http://tempuri.org/IRoleService1/GetRolesResponse")]
        Commonlayer.Role GetRoles(string roleN);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/GetRoles", ReplyAction="http://tempuri.org/IRoleService1/GetRolesResponse")]
        System.Threading.Tasks.Task<Commonlayer.Role> GetRolesAsync(string roleN);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/DeleteRole", ReplyAction="http://tempuri.org/IRoleService1/DeleteRoleResponse")]
        void DeleteRole(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/DeleteRole", ReplyAction="http://tempuri.org/IRoleService1/DeleteRoleResponse")]
        System.Threading.Tasks.Task DeleteRoleAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/GetMatchingRoles", ReplyAction="http://tempuri.org/IRoleService1/GetMatchingRolesResponse")]
        Commonlayer.Views.RolesView[] GetMatchingRoles(string roles);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/GetMatchingRoles", ReplyAction="http://tempuri.org/IRoleService1/GetMatchingRolesResponse")]
        System.Threading.Tasks.Task<Commonlayer.Views.RolesView[]> GetMatchingRolesAsync(string roles);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/GetRole", ReplyAction="http://tempuri.org/IRoleService1/GetRoleResponse")]
        Commonlayer.Role GetRole(int roleID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/GetRole", ReplyAction="http://tempuri.org/IRoleService1/GetRoleResponse")]
        System.Threading.Tasks.Task<Commonlayer.Role> GetRoleAsync(int roleID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/GetAllRoles", ReplyAction="http://tempuri.org/IRoleService1/GetAllRolesResponse")]
        Commonlayer.Role[] GetAllRoles();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/GetAllRoles", ReplyAction="http://tempuri.org/IRoleService1/GetAllRolesResponse")]
        System.Threading.Tasks.Task<Commonlayer.Role[]> GetAllRolesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/UpdateRole", ReplyAction="http://tempuri.org/IRoleService1/UpdateRoleResponse")]
        void UpdateRole(int roleid, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/UpdateRole", ReplyAction="http://tempuri.org/IRoleService1/UpdateRoleResponse")]
        System.Threading.Tasks.Task UpdateRoleAsync(int roleid, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/GetAllRolesV", ReplyAction="http://tempuri.org/IRoleService1/GetAllRolesVResponse")]
        Commonlayer.Views.RolesView[] GetAllRolesV();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/GetAllRolesV", ReplyAction="http://tempuri.org/IRoleService1/GetAllRolesVResponse")]
        System.Threading.Tasks.Task<Commonlayer.Views.RolesView[]> GetAllRolesVAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/GetRoleV", ReplyAction="http://tempuri.org/IRoleService1/GetRoleVResponse")]
        Commonlayer.Views.RolesView GetRoleV(int roleid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService1/GetRoleV", ReplyAction="http://tempuri.org/IRoleService1/GetRoleVResponse")]
        System.Threading.Tasks.Task<Commonlayer.Views.RolesView> GetRoleVAsync(int roleid);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRoleService1Channel : TraderMarket.RoleService.IRoleService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RoleService1Client : System.ServiceModel.ClientBase<TraderMarket.RoleService.IRoleService1>, TraderMarket.RoleService.IRoleService1 {
        
        public RoleService1Client() {
        }
        
        public RoleService1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RoleService1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RoleService1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RoleService1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddRole(string name) {
            base.Channel.AddRole(name);
        }
        
        public System.Threading.Tasks.Task AddRoleAsync(string name) {
            return base.Channel.AddRoleAsync(name);
        }
        
        public Commonlayer.Role GetRoles(string roleN) {
            return base.Channel.GetRoles(roleN);
        }
        
        public System.Threading.Tasks.Task<Commonlayer.Role> GetRolesAsync(string roleN) {
            return base.Channel.GetRolesAsync(roleN);
        }
        
        public void DeleteRole(int id) {
            base.Channel.DeleteRole(id);
        }
        
        public System.Threading.Tasks.Task DeleteRoleAsync(int id) {
            return base.Channel.DeleteRoleAsync(id);
        }
        
        public Commonlayer.Views.RolesView[] GetMatchingRoles(string roles) {
            return base.Channel.GetMatchingRoles(roles);
        }
        
        public System.Threading.Tasks.Task<Commonlayer.Views.RolesView[]> GetMatchingRolesAsync(string roles) {
            return base.Channel.GetMatchingRolesAsync(roles);
        }
        
        public Commonlayer.Role GetRole(int roleID) {
            return base.Channel.GetRole(roleID);
        }
        
        public System.Threading.Tasks.Task<Commonlayer.Role> GetRoleAsync(int roleID) {
            return base.Channel.GetRoleAsync(roleID);
        }
        
        public Commonlayer.Role[] GetAllRoles() {
            return base.Channel.GetAllRoles();
        }
        
        public System.Threading.Tasks.Task<Commonlayer.Role[]> GetAllRolesAsync() {
            return base.Channel.GetAllRolesAsync();
        }
        
        public void UpdateRole(int roleid, string name) {
            base.Channel.UpdateRole(roleid, name);
        }
        
        public System.Threading.Tasks.Task UpdateRoleAsync(int roleid, string name) {
            return base.Channel.UpdateRoleAsync(roleid, name);
        }
        
        public Commonlayer.Views.RolesView[] GetAllRolesV() {
            return base.Channel.GetAllRolesV();
        }
        
        public System.Threading.Tasks.Task<Commonlayer.Views.RolesView[]> GetAllRolesVAsync() {
            return base.Channel.GetAllRolesVAsync();
        }
        
        public Commonlayer.Views.RolesView GetRoleV(int roleid) {
            return base.Channel.GetRoleV(roleid);
        }
        
        public System.Threading.Tasks.Task<Commonlayer.Views.RolesView> GetRoleVAsync(int roleid) {
            return base.Channel.GetRoleVAsync(roleid);
        }
    }
}
