using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAuthRepository Auths { get; set; }
        public IMailContentRepo MailContents { get; set; }
        public IMailSettingRepo MailSettings { get; set; }
        public IFoodRepository Foods { get; set; }
        public IBookFoodRepository BookFoods { get; set; }
        public IPurchaseRespository Purchase { get; set; }

        public UnitOfWork( IAuthRepository authRepository, IMailContentRepo mailContents, IMailSettingRepo mailSettings, IFoodRepository foods, IBookFoodRepository bookFoods, IPurchaseRespository purchase)
        {
            Auths = authRepository;
            MailContents = mailContents;
            MailSettings = mailSettings;
            Foods = foods;
            BookFoods = bookFoods;
            Purchase = purchase;
        }
    }
}
