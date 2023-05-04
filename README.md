Инструкция по запуску:
1) Добавьте строку подключения к базам данных в \OrdersService\OrdersService.WebApi\appsettings.json
2) Если хотите запустить интеграционные тесты поменяйте IsTestRun на true в \OrdersService\OrdersService.WebApi\appsettings.json (отключает swagger)
3) Введите путь для логгирования в файл в \OrdersService\OrdersService.WebApi\appsettings.json в секции "Serilog:WriteTo[1]:Args:path"
3) Запускайте!
