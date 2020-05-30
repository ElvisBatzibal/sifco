SELECT 
T0.Id
, T1.FullName As Medico
, T2.FullName As Paciente
, MAX(DATEDIFF(minute, CAST(T0.Scheduledhour AS time), CAST(T0.Scheduledhourend AS time))) as [MinutosTranscurridos]
FROM 
[Appointments_medical] T0
INNER JOIN [Medic] T1 ON T0.IdMedic=T1.AccountNum
INNER JOIN [Patients] T2 ON T0.IdPatient=T2.AccountNum
WHERE T0.Status='Finalizada'
GROUP BY T0.Id
, T1.FullName 
, T2.FullName 
HAVING MAX(DATEDIFF(minute, CAST(T0.Scheduledhour AS time), CAST(T0.Scheduledhourend AS time)))=
(SELECT MAX(DATEDIFF(minute, CAST(T0.Scheduledhour AS time), CAST(T0.Scheduledhourend AS time))) as [MinutosTranscurridos]
FROM 
[Appointments_medical] T0
WHERE T0.Status='Finalizada'
)
