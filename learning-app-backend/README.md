# learning-app

Цей проєкт є серверною реалізацією застосунку для відстежування власного прогресу. Проєкт представляє собою API сервіс. Проєкт використовує наступні технології:
- .NET 8
- ASP.NET Core WebAPI
- Entity Framework(ORM фреймворк)
- BCrypt(для генерації хешованих паролей)
- JWTBearer(для генерації токенів)
- Swagger(API документація та тестування)

## Розгортання проєкту

#### За допомогою IDE(Visual Studio):

1. Запустити Visual Studio
2. Обрати файл з розширенням .sln(LearningApp.Presentation.sln)
3. Запустити режим отладки через https

#### Створення БД:
1. Запустити MS SQL Server Managment Studio
2. Обрати пункт 'База даних' -> 'Створити базу даних' -> 'learning-app'
3. В Visual Studio, запустити 'Консоль диспетчера пакетів' -> Обрати проєкт LearningApp.Presentation -> запустити команду Update-Database для того, щоб усі міграції застосувались

### За допомогою Docker:
Цей проєкт містить Dockerfile з налаштуваннями проєкту для запуску в образі Docker. Для початку треба зробити build проєкту:
```sh 
docker build -t learning-app-backend .
```
Далі запустити сам образ:
```sh
docker run -it -p 8081:8081 learning-app-backend
```

Демо проєкту:

1. /api/User/register для реєстрації користувача(включаючи валідацію пошти, перевірки на помилки):
![](videos/backend/login.gif)

2. /api/User/login для авторизації користувача(валідацая паролю). Повертається JWT токен, який може бути потім записан в local storage, в випадку с Swagger - записується в cookies:
<video controls src="https://i.imgur.com/5EuTMfh.mp4" title="[Download video](https://i.imgur.com/5EuTMfh.mp4)"></video>

3. /api/User/getUserByEmail з отриманням даних користувача(включаючи перевірку на те, що авторизований користувач запрошує інформацію про себе):
<video controls src="https://i.imgur.com/5BJuNeC.mp4" title="[Download video](https://i.imgur.com/5EuTMfh.mp4)"></video>

4. /api/User/getUsers для отримання даних про усіх користувачів у вигляді списку. Для даного ендпоінту є доступ тільки у користувачей з правами адміністратору. Якщо звичайний користувач намагається зробити запит:
<video controls src="https://i.imgur.com/1AUNyEf.mp4" title="[Download video](https://i.imgur.com/1AUNyEf.mp4)"></video> Якщо авторизуватися як адміністратор:
<video controls src="https://i.imgur.com/Fph7FAU.mp4" title="[Download video](https://i.imgur.com/Fph7FAU.mp4)"></video>

5. /api/User/update для оновлення інформації про користувача(доступно тільки адміну або авторизованому користувачу).
<video controls src="https://i.imgur.com/vvFf84v.mp4" title="[Download video](https://i.imgur.com/vvFf84v.mp4)"></video>

6. /api/User/delete для оновлення інформації про користувача(доступно тільки адміну).
<video controls src="https://i.imgur.com/TaVoEKD.mp4" title="[Download video](https://i.imgur.com/TaVoEKD.mp4)"></video>