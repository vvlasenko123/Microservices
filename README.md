https://docs.google.com/document/d/1792HduPeco6stZEjyrJeHupNFwXXfcgNyJcXmNpCI5o/edit?usp=sharing


Маркетплейс заказов

Функциональные требования

1) Регистрация/Авторизация: покупатель имеет возможность зарегистрироваться, входить и выходить из  своей учетной записи
2) Создание заказа: покупатель оформляет заказ из списка выбранных товаров с указанием адреса и способа оплаты.
3) Резервирование покупок: при заказе система резервируется товар на складе. При отмене – снимает резерв.
4) Оплата: Сервис принимает оплату по карте или номеру телефона. При неуспехе – заказ переводится на отмену и снимается резерв.
5) Уведомления: Email/Sms при событиях: создание заказа, оплата заказа, подтверждение, отмена заказа.
6) История заказов: покупатель видит список своих заказов и детали каждого из них.

Сервисы:
1) IdentityApi – регистрация, авторизация, хранение логинов и паролей.
2) ProductOrderApi – хранит заказы и позиции, управляет статусами, инициирует подтверждение и заказы.
3) ProductCatalogApi – товары, цены, характеристики.
4) ReserverProductApi – учет остатков и резервов.
5) PayServiceApi – прием платежей или возвратов.
6) NotificationApi – отправка Email/Sms по событиям.




Cущности IdentityApi (Реляционный и трехслойный сервис):
1) User – учетная запись пользователя
2) UserCredential – учетные данные пароля
3) UserToken – токены пользователя
4) PasswordHasher – для хранения хэша пароля
5) PasswordResetToken – токен для сброса паролей


<img width="974" height="765" alt="image" src="https://github.com/user-attachments/assets/b545caf6-6c67-4692-b3b1-0747a46d93a8" />


REST:

`IdentityApi`:
(метод, путь, тело)

(Регистрация по email)  
POST /api/v1/auth/register/email (firstName, lastName, email, password)  

(Регистрация по phone)  
POST /api/v1/auth/register/phone (firstName, lastName, phone, password)  

(Вход по email)  
POST /api/v1/auth/login/email (email, password)  

(Вход по phone)  
POST /api/v1/auth/login/phone (phone, password)  

(Обновление токена)  
POST /api/v1/auth/refresh (refreshToken)  

(Выход)  
POST /api/v1/auth/logout (refreshToken)  

(Отправить код подтверждения email)  
POST /api/v1/auth/verify/email/send (email)  

(Подтвердить email)  
POST /api/v1/auth/verify/email/confirm (email, code)  


(Отправить код подтверждения телефона)  
POST /api/v1/auth/verify/phone/send (phone)  

(Подтвердить телефон)  
POST /api/v1/auth/verify/phone/confirm (phone, code)  

(Сменить пароль)  
POST /api/v1/auth/password/change (currentPassword, newPassword)  

(Запрос на сброс пароля)  
POST /api/v1/auth/password/reset/request (email?, phone?)  

(Подтверждение сброса пароля)  
POST /api/v1/auth/password/reset/confirm (resetToken, newPassword)  

(Профиль пользователя)  
GET /api/v1/users/me (id, firstName, lastName, email, phone, emailVerified, phoneVerified)  

(Обновить профиль)  
PATCH /api/v1/users/me (id, firstName, lastName)  
  
`ProductOrderApi`:  
(Создание заказа)  
POST /api/v1/orders (productId, country, region, city, street, card?, phone?)  
Результат: (orderId, status, totalAmount, currency, items[], createdAt)  
  
(Список заказов)  
GET /api/v1/orders/me (id, status, totalAmount, currency, createdAt)  
  
(Детали заказа)  
GET /api/v1/orders/{orderId} (id, status, totalAmount, currency, items[], country, region, city, street, createdAt, updatedAt)  
  
(Резервирование заказа)  
POST /api/v1/orders/{orderId}/reserve  
  
(Оплата заказа)  
POST /api/v1/orders/{orderId}/pay  
Результат: (paymentId, status)  
  
(Подтверждение заказа)  
POST /api/v1/orders/{orderId}/confirm  
  
(Отмена заказа)  
POST /api/v1/orders/{orderId}/cancel (reason)  
  
(Статус оплаты — входящий колбэк)  
POST /api/v1/orders/{orderId}/payment-status (paymentId, status, failureCode?, failureMessage?)  
  
`ProductCatalogApi`:  
  
(Список товаров)  
GET /api/v1/products (id, name, stock, price, currency)  
  
(Товар)  
GET /api/v1/products/{id} (id, name, description, stock, price, currency, attributes, isActive)  
  
(Цена товара)  
GET /api/v1/products/{id}/price (productId, price, currency)  
  
(Остаток товара)  
GET /api/v1/products/{id}/stock (productId, available)  
  
`ReserverProductApi`:  
  
(Остатки по товарам)  
GET /api/v1/stocks (productId, available)  
  
(Создать резерв)  
POST /api/v1/reservations (orderId, items[productId, quantity])  
Результат: (reservationId, status)  
  
(Резерв)  
GET /api/v1/reservations/{reservationId} (reservationId, orderId, status, items[productId, reservedQuantity], createdAt, expiresAt?)  
  
(Подтвердить резерв)  
POST /api/v1/reservations/{reservationId}/confirm  
  
(Снять резерв)  
POST /api/v1/reservations/{reservationId}/release  
  
`PayServiceApi`:  
  
(Создать платеж)  
POST /api/v1/payments (orderId, amount, currency, method, cardToken?, phone?)  
Результат: (paymentId, status)  
  
(Платеж)  
GET /api/v1/payments/{paymentId} (paymentId, orderId, status, amount, currency, method)  
  
(Создать возврат)  
POST /api/v1/refunds (orderId, paymentId, amount)  
Результат: (refundId, status)  
  
(Возврат)  
GET /api/v1/refunds/{refundId} (refundId, paymentId, orderId, status, amount, currency)  
  
(Уведомление о статусе платежа)  
POST /api/v1/payments/{paymentId}/notify (status, failureCode?, failureMessage?)  
  
`NotificationApi`:  
  
(Отправить уведомление)  
POST /api/v1/notifications/send (channel, to, template, variables)  
Результат: (notificationId, status)  
  
(Статус уведомления)  
GET /api/v1/notifications/{notificationId} (notificationId, status, channel, to, template, createdAt, errorMessage?)  


