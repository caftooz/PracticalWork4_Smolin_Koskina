# ПРАКТИЧЕСКАЯ РАБОТА №6
по **МДК.01.02 Поддержка и тестирование программных модулей**

по теме: **СОЗДАНИЕ АВТОМАТИЗИРОВАННЫХ UNIT-ТЕСТОВ. Часть 2**

Цель работы: **провести тестирование разработанных программных модулей
с использованием средств автоматизации Microsoft Visual Studio методом "белого ящика".**

---
## Выполнили
Студенты группы 3ИСИП-423:
- Смолин Александр Сергеевич
- Коскина Наталья Ивановна

---
## Вариант: 13

---
## Скриншоты результатов работы приложения

- Функция для страницы 1:
<img width="257" height="102" alt="image" src="https://github.com/user-attachments/assets/e0c9a6b5-4e10-4251-acc6-6304e151f86e" />

- Функция для страницы 2:
<img width="344" height="91" alt="image" src="https://github.com/user-attachments/assets/d3489586-b81f-4915-8037-136a0c67c685" />

- Функция для страницы 3:
<img width="272" height="68" alt="image" src="https://github.com/user-attachments/assets/c4b20aa9-79a1-4be0-a0d7-3147944ee0d0" />

---
## Скриншот окна «Обозреватель тестов»

<!-- Вставьте сюда скриншот окна «Обозреватель тестов» после запуска тестов -->
<img width="1515" height="690" alt="image" src="https://github.com/user-attachments/assets/a574f5ac-926a-4fa8-8a4f-657d649c5e36" />

---
## Описание тестов

В тестовый проект `UnitTestProject` добавлены следующие тесты:

| Метод теста | Тестируемая функция | Описание |
|---|---|---|
| `TestMethod1` | — | Тренировочный тест, демонстрирующий работу методов `Assert` |
| `TestFunction1_StandardValues` | `Function1(x, y, z)` | Проверка корректного результата при x=2, y=1, z=1 |
| `TestFunction1_AnotherValues` | `Function1(x, y, z)` | Проверка корректного результата при x=3, y=2, z=0.5 |
| `TestFunction1_DivisionByZero_WhenXEqualsY` | `Function1(x, y, z)` | Проверка поведения при x=y (деление на ноль) |
| `TestFunction2_XEqualsY` | `Function2(x, y, f)` | Проверка первой ветви (x == y), f(x) = sh(x) |
| `TestFunction2_XGreaterThanY` | `Function2(x, y, f)` | Проверка второй ветви (x > y), f(x) = x² |
| `TestFunction2_XLessThanY` | `Function2(x, y, f)` | Проверка третьей ветви (x < y), f(x) = sh(x) |
| `TestFunction3_BasicValues` | `Function3(x, b)` | Проверка корректного результата при x=2, b=1 |
| `TestFunction3_AnotherValues` | `Function3(x, b)` | Проверка корректного результата при x=5, b=3 |
| `TestFunction3_DivisionByZero_WhenXEqualsB` | `Function3(x, b)` | Проверка поведения при x=b (разрыв функции) |

---
## Вывод о проведённом тестировании

Все тесты пройдены **успешно**.

**Причины успешного выполнения тестов:**

- Тесты `TestFunction1_StandardValues`, `TestFunction1_AnotherValues`, `TestFunction2_*`, `TestFunction3_BasicValues`, `TestFunction3_AnotherValues` прошли успешно, поскольку математические функции в классе `Core` реализованы корректно: вычисленные значения совпадают с эталонными (допуск 1e-4), полученными независимым расчётом.

- Тесты `TestFunction1_DivisionByZero_WhenXEqualsY` и `TestFunction3_DivisionByZero_WhenXEqualsB` прошли успешно, поскольку среда .NET при делении числа с плавающей точкой на ноль возвращает `Infinity` или `NaN` (а не исключение), что соответствует стандарту IEEE 754. Тесты явно проверяют это ожидаемое поведение.

- Тест `TestMethod1` прошёл успешно, поскольку арифметическое выражение `2 + 2 = 4` является неизменным фактом, и все четыре проверки (`AreEqual`, `AreNotEqual`, `IsFalse`, `IsTrue`) соответствуют действительности.

---
## Технологии
- WPF (.NET Framework) — пользовательский интерфейс
- Frame Navigation — навигация по страницам
- DataVisualization — построение графика функций
- MSTest — автоматизированное модульное тестирование

---
## Структура проекта
```
PracticalWork4_Smolin_Koskina/
├─ Pages/
│  ├─ Page1.xaml / Page1.xaml.cs
│  ├─ Page2.xaml / Page2.xaml.cs
│  └─ Page3.xaml / Page3.xaml.cs
├─ Windows/
│  └─ MainWindow.xaml / MainWindow.xaml.cs
├─ Core.cs
└─ App.xaml
UnitTestProject/
├─ Func1UnitTest.cs
├─ Func2UnitTest.cs
├─ Func3UnitTest.cs
└─ UnitTest1.cs
```
