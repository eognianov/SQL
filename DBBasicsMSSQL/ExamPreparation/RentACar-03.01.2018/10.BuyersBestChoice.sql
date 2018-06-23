SELECT Manufacturer, Model, SUM(CountOfOrderById) AS TimesOrdered
FROM(
	SELECT [mod].Manufacturer, [mod].Model, veh.Id, COUNT(veh.Id) AS CountOfOrderById
	FROM dbo.Orders ord
	LEFT JOIN dbo.Vehicles veh ON ord.VehicleId = veh.Id
	RIGHT JOIN dbo.Models [mod] ON veh.ModelId = [mod].Id
	GROUP BY [mod].Manufacturer, [mod].Model, veh.Id
) AS H1
GROUP BY Manufacturer, Model
ORDER BY TimesOrdered DESC, Manufacturer DESC, Model 