using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;


namespace ServiceModel
{
    [ServiceContract]
    internal interface ICalanderService
    {
        [OperationContract] CityList GetAllCities();
        [OperationContract] int InsertCity(City city);
        [OperationContract] int UpdateCity(City city);
        [OperationContract] int DeleteCity(City city);

        [OperationContract] EventList GetAllEvents();
        [OperationContract] int InsertEvent(Event events);
        [OperationContract] int UpdateEvent(Event events);
        [OperationContract] int DeleteEvent(Event events);
        [OperationContract] EventList GetEventsByUser(Users user);

        [OperationContract] GroupsList GetAllGroups();
        [OperationContract] int InsertGroup(Groups group);
        [OperationContract] int UpdateGroup(Groups group);
        [OperationContract] int DeleteGroup(Groups group);
        [OperationContract] GroupsList GetGroupsByUser(Users user);

        [OperationContract] UsersList GetAllUsers();
        [OperationContract] int InsertUser(Users user);
        [OperationContract] int UpdateUser(Users user);
        [OperationContract] int DeleteUser(Users user);
        [OperationContract] UsersList GetUsersByGroup(Groups group);
        [OperationContract] UsersList GetUsersByEvent(Event events);
        [OperationContract] Users Login(Users user);
        [OperationContract] bool IsUsernameFree(string username);
    }
}
