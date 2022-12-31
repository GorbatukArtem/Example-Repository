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
Task<int> AliveTotalAsync();
Task<int> DeathTotalAsync();
```
```C#
В паттерне Repository вы увидете метод        
Task<int> TotalAsync(TotalSpecification specification);

реализация TotalSpecification будет выгялядеть примерно следующим образом

public interface TotalSpecification { 
    boolean Specified(People people);     
}

public class PeopleSpecificationTotalAlive : TotalSpecification {
    private Type type; 
    public PeopleSpecificationTotalAlive(Type type) {
      this.type = type
    }    
    public boolean specified(People people) {
      /// реализация
    } 
    public Criterion ToCriteria() {
      /// реализация
    } 
}

public class PeopleSpecificationTotalDeath : TotalSpecification {
    private Type type; 
    public PeopleSpecificationTotalAlive(Type type) {
      this.type = type
    }    
    public boolean specified(People people) {
      /// реализация
    } 
    public Criterion ToCriteria() {
      /// реализация
    } 
}
```

**Вот то, как обычно заканчиваются рассуждения об DAO pattern vs Repository pattern.**
>Паттерн DAO предоставляет размытое описание контракта. Используя его, выполучаете потенциально неверно используемые и раздутые реализации классов. Паттерн Репозиторий использует метафору коллекции, которая дает нам жесткий контракт и делает понимание вашего кода проще.

**Я изучив предлагаемое сообществом пришел к выводу**
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
