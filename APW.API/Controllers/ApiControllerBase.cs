using APW.Models;
using APW.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APW.API.Controllers;

public class ApiControllerBase : ControllerBase
{
   protected ComplexObject CreateComplexObject<T>(IEnumerable<T> entities) where T : IEntity => new()
   {
       Entities = entities as IEnumerable<IEntity> ?? [],
       
   };
}
