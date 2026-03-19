# 🛡️ SentinelGear

> 🇧🇬 Уеб приложение за управление на онлайн магазин за военна и тактическа екипировка – продукти, поръчки, количка и административен панел.

![.NET Version](https://img.shields.io/badge/.NET-8.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-blue)
![EF Core](https://img.shields.io/badge/EF_Core-Code_First-informational)
![SQL Server](https://img.shields.io/badge/Database-SQL_Server-red)
![JavaScript](https://img.shields.io/badge/JavaScript-ES6-yellow)
![AJAX](https://img.shields.io/badge/AJAX-Asynchronous-green)
![Military Store](https://img.shields.io/badge/Domain-Military_Gear-black)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📋 Съдържание

- [📌 Обща информация](#-обща-информация)  
- [🛠️ Технологии](#️-технологии)  
- [🔐 Роли](#-роли)  
- [🏗️ Архитектура](#️-архитектура)  
- [🗄️ База данни](#️-база-данни)  
- [🔄 Миграции](#-миграции)  
- [🚀 Стартиране](#-стартиране)  
- [⚠️ Стартиране на друга машина](#️-стартиране-на-друга-машина)  
- [🌐 Порт](#-порт)  
- [🛒 Функционалности](#-основни-функционалности)  
- [🔑 Админ достъп](#-достъп-като-администратор)  
- [🎯 Цел на проекта](#-цел-на-проекта)  

---

## 📌 Обща информация

**SentinelGear** е уеб базирано приложение за управление на онлайн магазин за **военна и тактическа екипировка**, разработено като дипломeн проект по зададени изисквания.

Приложението реализира цялостна система за:

- управление на продукти  
- обработка на поръчки  
- потребителска количка  
- административен контрол  

---

## 🛠️ Технологии

- .NET 8  
- ASP.NET Core MVC  
- Entity Framework Core (Code First)  
- ASP.NET Core Identity  
- SQL Server  
- JavaScript (ES6)  
- AJAX  
- Bootstrap 5  
- Razor Views  

---

## 🔐 Роли

Поддържани роли:

- User  
- Admin  

✔ Само **Admin** има достъп до административния панел  

---

## 🏗️ Архитектура

Controllers → Business Logic → Data Layer → Database  
Views → Razor Rendering  
ViewModels → Presentation Layer  

Допълнително:

- Helpers  
- Admin Area  
- Identity  

---

## 🗄️ База данни

Използва се **Entity Framework Core – Code First**.

Основни модели:

- Product  
- Category  
- Order  
- OrderItem  
- CartItem  
- ContactMessage  

Enum:

- OrderStatus  

---

## 🔄 Миграции

При стартиране на приложението автоматично се изпълнява:

```csharp
await dbContext.Database.MigrateAsync();
```

✔ Създава базата данни  
✔ Прилага всички миграции  
✔ Seed-ва начални данни и роли  

---

## 🚀 Стартиране

```bash
git clone <repo>
cd SentinelGear
dotnet restore
dotnet run
```

---

## ⚠️ Стартиране на друга машина

### 🔧 Промяна на Connection String

Файл:

```
appsettings.json
```

Пример:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\MSSQLLocalDB;Database=SentinelGear;Trusted_Connection=True;"
}
```

👉 Всеки потребител трябва да го адаптира според своя SQL Server.

---

### ▶ Стартиране

```bash
dotnet run
```

✔ Базата ще се създаде автоматично чрез миграции  

---

## 🌐 Порт

Приложението **НЕ използва фиксиран порт**.

Портът се задава автоматично от:

- launchSettings.json  
- ASP.NET Core runtime  

---

## 🛒 Основни функционалности

### 👤 Потребител

- Регистрация и вход  
- Разглеждане на продукти  
- Филтриране по категории  
- Добавяне в количка  
- Завършване на поръчка  

### 🛠️ Администратор

- CRUD операции за продукти  
- CRUD операции за категории  
- Управление на поръчки  
- Промяна на статуси  

---

## 🎯 Цел на проекта

Проектът демонстрира:

- Изграждане на пълно уеб приложение  
- MVC архитектура  
- Работа с Entity Framework Core  
- Authentication и Authorization (Identity)  
- Работа със сесии (количка)  
- Използване на JavaScript и AJAX за динамично съдържание  
- Административен панел с role-based достъп

---

## 🔑 Достъп като администратор

За вход като **Admin** потребител:

📄 Файл:

```
appsettings.Development.json
```

В него са зададени:

- Администраторски имейл  
- Администраторска парола  

👉 Използвайте тези данни за вход в системата с администраторски права.

