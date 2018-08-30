using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFramework.DependencyInjection;
using IFramework.Repositories;

namespace IFrameworkDemo.Controllers
{
    public interface IDemoRepository:IDomainRepository
    {
    }

    public class DemoRepository : DomainRepository, IDemoRepository
    {
        public DemoRepository(IObjectProvider objectProvider)
            : base(objectProvider) { }
    }
}
