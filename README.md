### Martin Fowler 

Martin Fowler и его книги : https://martinfowler.com/books/

Одна из книг где Martin Fowler принял участие P of EAA / or <b>Patterns of Enterprise Application Architecture</b> : https://martinfowler.com/books/eaa.html

В данной книге Martin Fowler с коллегами описал подход к построениею архитектуры приложения : https://martinfowler.com/eaaCatalog/

Здесь выдержка / мысли какие проблемы решает паттерн Repository в N-Tier architectures https://martinfowler.com/eaaCatalog/repository.html

Здесь выдержка / мысли какие проблемы решает паттерн UnitOfWork в N-Tier architectures https://martinfowler.com/eaaCatalog/unitOfWork.html

### Eric Evans

Eric Evans в книге **Domain-Driven Design Tackling Complexity in the Heart of Software** разделил подход на 2 вида : 
+ Data Access Objects (DAO) pattern
+ Repository pattern

**Коротко суть в том как создаются методы на примере RepositoryPeople.**

```C#
В паттерне DAO вы увидете методы
Task<int> FindAliveTotalAsync();
Task<int> FindDeathTotalAsync();
```
```C#
В паттерне Repository вы увидете метод        
Task<int> FindTotalAsync(FindTotalSpecification specification){
    Task<int> = specification.Execute();
}

реализация FindTotalSpecification будет выгялядеть примерно следующим образом

public interface FindTotalSpecification { 
    здесь могут быть поля по которым может происходить фильтрация. 
    в данном пример этого нет.
    Task<int> Execute();     
}

public class FindAliveTotalSpecification : FindTotalSpecification {
    public Task<int> Execute() {
            return Context.Set<Person>()
                .CountAsync(p => p.Death == null && p.Birth != null && p.Birth.Value.Year + 120 > DateTime.Now.Year);
    } 
}

public class FindDeathTotalSpecification : FindTotalSpecification {
    public Task<int> Execute() {
            return Context.Set<Person>()
                .CountAsync(p => p.Death != null || (p.Birth != null && p.Birth.Value.Year + 120 < DateTime.Now.Year));
    } 
}
```

**Вот то, как обычно заканчиваются рассуждения об DAO pattern vs Repository pattern.**
>Паттерн DAO предоставляет размытое описание контракта. Используя его, выполучаете потенциально неверно используемые и раздутые реализации классов. Паттерн Репозиторий использует метафору коллекции, которая дает нам жесткий контракт и делает понимание вашего кода проще.

**Вот например мнение автора Marcin Chwedczuk в целом о проблемах этих паттернов** http://blog.marcinchwedczuk.pl/repository-pattern-my-way
+ Нарушение Принципа Сегрегации Интерфейсов (Interface Segregation Principle). Они выражают полный набор CRUD-операций даже для тех сущностей, для которых операции удаления не имеют никакого смысла. Например, когда вы деактивируете пользователей вместо удаления их записей из базы данных.
+ Имплементация IRepository почти всегда регистрируется в IoC-контейнере и может быть внедрена в ваши сервисы точно так же, как и любая другая зависимость.
+ Возвращение IQueryable<TEntity> в методе GetAll() репозитория плохая идея. Вот здесь в статье на сайте microsoft есть поддтверждение его слов https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core Although we don't recommend returning IQueryable from a repository, it's perfectly fine to use them within the repository to build up a set of results. You can see this approach used in the List method above, which uses intermediate IQueryable expressions to build up the query's list of includes before executing the query with the specification's criteria on the last line.

**Изучив предлагаемое сообществом пришел к выводу**
>В данном вопросе каждый имеет свое мнение о разнице между этими двумя паттернами.</br>
Вот один из многих примеров : https://ducmanhphan.github.io/2019-04-28-Repository-pattern/

**ef packages:** </br>
microsoft.entityframeworkcore<br/>
microsoft.entityframeworkcore.Abstractions<br/>
microsoft.entityframeworkcore.Analyzers<br/>
microsoft.entityframeworkcore.Proxies<br/>
microsoft.entityframeworkcore.Relational<br/>
microsoft.entityframeworkcore.SqlServer<br/>
microsoft.entityframeworkcore.Tools
