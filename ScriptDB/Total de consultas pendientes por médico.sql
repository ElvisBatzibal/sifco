SELECT
T0.FullName
, COUNT(T1.Status) TotalCount
FROM 
[Medic] T0
INNER JOIN [Appointments_medical] T1 ON T0.AccountNum=T1.IdMedic
WHERE T1.Status='Pendiente'
GROUP BY FullName