
SELECT TOP 1
T0.AccountNum
, T0.FullName
, T0.Gender
, COUNT(T2.id)TotalRecetado
FROM 
Patients T0
INNER JOIN Appointments_medical T1 ON T1.IdPatient=T0.AccountNum
INNER JOIN Appointments_attached T2 ON T2.IdApp_medical=T1.ID
GROUP BY 
T0.AccountNum
, T0.FullName
, T0.Gender
ORDER BY TotalRecetado DESC
