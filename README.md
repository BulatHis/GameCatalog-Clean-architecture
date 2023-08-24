1 ORM - бд - 5 сущностей (без промежуточных) настройка моделей есть через атрибуты и FluentAPI (смотреть в папке Entities/GameCatalogDomain/Entity/) репо к ним тоже есть

![Снимок экрана (171)](https://github.com/kislovka-teach/homeworks-BulatHis/assets/126420480/761c2c30-efea-4b3c-b8b8-3f8befce6778)

2 Auth - есть через JWT Tokens 

3 Security - есть валидация на фронте и беке (полная защита от инъекций), обработчик ошибок фильтрует ответ ошибок и не дает инфы для взлома в ответе

4 Logs - логирование в приложение Log2Console 
![Снимок экрана (170)](https://github.com/kislovka-teach/homeworks-BulatHis/assets/126420480/f63e384a-f7e3-4313-9eeb-c3820277f4d1)

5  - переделал Clean Разделил на 4 основные слоя: Entities, Frameworks, Interactor и UseCase (+ еще Tests)
![Снимок экрана (173)](https://github.com/kislovka-teach/homeworks-BulatHis/assets/126420480/c68bebd6-2105-425c-a0f5-ee8be42610ed)
1) в Entities бизнес объекты и их единая логика работы
2) в Frameworks все внешнее (контроллеры и картинки)
3) в Interactor сборки, которые реализуют все наши внешние зависимости (бд + SMTP)
4) в UseCase бизнес логика приложения

6 Полностью описал свагер (небольшой отрывок для примера)
![Снимок экрана (167)](https://github.com/kislovka-teach/homeworks-BulatHis/assets/126420480/1baf565e-bfa1-4631-a2c5-1c4c9dfc7ed8)


![Снимок экрана (166)](https://github.com/kislovka-teach/homeworks-BulatHis/assets/126420480/f06a00d7-3977-479d-a89a-3f290067b3c0)
