
SELECT TOP 1
T0.AccountNum
, T0.FullName
, T0.Gender
, COUNT(T2.Gender)TotalPacienteFemenino
FROM 
Medic T0
INNER JOIN Appointments_medical T1 ON T1.IdMedic=T0.AccountNum
INNER JOIN Patients T2 ON T2.AccountNum=T1.IdPatient
WHERE 
T0.Gender='Masculino'
AND T2.Gender='Femenino'
GROUP BY 
T0.AccountNum
, T0.FullName
, T0.Gender
ORDER BY TotalPacienteFemenino DESC
