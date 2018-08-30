using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFramework.Event;
using IFramework.UnitOfWork;
using IFrameworkDemo.Controllers;
using IFrameworkDemo.Models;

namespace IFrameworkDemo.EventSubscribers
{
    public class CreateBlogDomainEventSubscriber: IEventAsyncSubscriber<CreateBlogDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IDemoRepository _demoRepository;

        public CreateBlogDomainEventSubscriber(IDemoRepository demoRepository, IUnitOfWork unitOfWork)
        {
            _demoRepository = demoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateBlogDomainEvent message)
        {
            var blogHistory = new BlogHistory(message.BlogId, 
                message.BlogTitle, 
                message.BlogContent,
                message.BlogCreateTime);
            _demoRepository.Add(blogHistory);
            await _unitOfWork.CommitAsync();
        }
    }
}
