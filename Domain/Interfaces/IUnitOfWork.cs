using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IFoodRepository Foods { get; set; }
        public IBookFoodRepository BookFoods { get; set; }
        public IAuthRepository Auths { get; set; }
        public IPurchaseRespository Purchase { get; set; }
        public IMailContentRepo MailContents { get; set; }
        public IMailSettingRepo MailSettings { get; set; }
    }
}
