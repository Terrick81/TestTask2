# Задание 2 «Интерактивная модель».

**Цель:** Реализовать программу для взаимодействия с объектами на сцене. 

**Требования к реализации:**
- Использовать движок разработки: Unity3D (2021.3)
- Осуществлять взаимодействие с отдельными объектами;
- Настраивать прозрачность объекту, менять цвет и скрывать полностью;
- иметь интерфейс (рис. 1) привязанный к этим объектам (рис 1).
  
- логика управления камерой (только с использованием мыши), а именно:
•	Перемещение камеры в пространстве (верх, вниз);
•	Фокусировка на выделенном объекте;
•	Вращение вокруг выделенной модели/объекта;
•	Приближение/отдаление.

**Не обязательно к выполнению:**
•	Автоматическая генерация элементов списка в зависимости от количества объектов
•	Добавить возможность сохранения и загрузки сцены.

 ![image](https://github.com/user-attachments/assets/043fa841-6f98-4715-8eb9-87eb9a01d16d)
 (Рисунок 1)
 
## Реализовано
Все вышеперечисленное

# Работа в приложении
## Логика управления камерой
-*Перемещение камеры в пространстве:* зажать колесико, при таком перемещинии, целевой обьект будет сброшен (слева пропадет возможность выбирать цвет);
-*Фокусировка на выделенном объекте:* двойной щелчок по обьекту 
-*Вращение вокруг выделенной модели/объекта:* зажать левую клавишу мыши с заданным фокусом на обьекте;
-*Приближение/отдаление от обькта:* крутить колесико с заданным фокусом на обьекте;
-*Приближение/отдаление без обькта:* крутить колесико без фокуса на обьекте;
-*Свободный обзор:* зажать левую клавишу мыши  без фокуса на обьекте.

# Структура проекта
- `MainCamera.cs` (реализация управления камерой);
- `Detail.cs` (висит на возможных целях, хранилище дынных об отбьекте во время работы программы, реализация смены цвета, прозрачности);
- `DetailPanel.cs` (реализация интерфейса слева в);
- `Block.cs` (логика обьектв в списке интерфейса: выделение рамки, изменение чекбоксов);
- `Checkbox.cs` (отдельная структура для обьектов - чекбоксов);


