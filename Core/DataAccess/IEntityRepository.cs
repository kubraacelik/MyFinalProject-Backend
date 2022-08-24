using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository <T> where T:class,IEntity,new() //T benim istediğim tipte yazılandı. Ama ben her istediğimin değil de
                                                                       //gerçekten kodda olanları yazmasını istiyorum. O yüzden en başta class olsun
                                                                       //istedim daha sonra product,customer ve category'nin ortak özelliği IEntitiy
                                                                       //olmalarıydı onu da yazdım ki benim yazdıklarım yazılsın. IEntity kendisi
                                                                       //olmasın diye new() koydum. Şimdi sistemimiz gerçekten veritabanı nesneleriyle
                                                                       //çalışan bir repository oldu
    {
        List<T> GetAll(Expression<Func<T,bool>> filter = null); //Expression = filtreleme , filtreleme ile istenilen dataları getirir.
                                                                //filter getirmeyebilirim de. filter yoksa tüm data'yı istiyordur.
                                                                //istiyorsa filtreleyip veririm.
                                                                //Expression<Func<T,bool>> filter = null ---> bu yapı p=>p.CategoryId==2 gibi işlemleri yapmamı sağlar kısaca

        T Get(Expression<Func<T,bool>> filter); //tek 1 data getirmek içindir. burada filter zorunlu
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
