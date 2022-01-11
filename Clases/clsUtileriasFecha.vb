Public Class clsUtileriasFecha

	Public Property FechaInicio As Date
	Public Property FechaFin As Date
	Public Property NumeroSemana As Integer
	Public Property FechaInicioSql As String
	Public Property FechaFinSql As String
	Public Property añoSql As String

	Public Shared Function datosFecha(fecha As Date) As clsUtileriasFecha
		Dim clFecha As New clsUtileriasFecha
		clFecha.NumeroSemana = DatePart(DateInterval.WeekOfYear, fecha, Microsoft.VisualBasic.FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1)
		Dim fecha1 As New DateTime(fecha.Year, 1, 1)
		Dim fecha2 As DateTime = fecha1.AddDays(7 * (clFecha.NumeroSemana - 1))
		While fecha2.DayOfWeek <> DayOfWeek.Sunday
			fecha2 = fecha2.AddDays(-1)
		End While
		clFecha.FechaInicio = CDate(fecha2)
		clFecha.FechaInicioSql = clFecha.FechaInicio.Year & Mid(clFecha.FechaInicio.Month + 100, 2, 2) & Mid(clFecha.FechaInicio.Day + 100, 2, 2)
		clFecha.FechaFin = fecha2.AddDays(6)
		clFecha.FechaFinSql = clFecha.FechaFin.Year & Mid(clFecha.FechaFin.Month + 100, 2, 2) & Mid(clFecha.FechaFin.Day + 100, 2, 2)
		clFecha.añoSql = clFecha.FechaFin.Year
		If clFecha.FechaFin.Year - clFecha.FechaInicio.Year = 1 Then
			clFecha.NumeroSemana = 1
		End If
		Return clFecha
	End Function

	Public Shared Function rangoFechas(semana As Integer, ann As Integer) As clsUtileriasFecha
		Dim clfechaI As New clsUtileriasFecha
		clfechaI = datosFecha(CDate("01/01/" & ann))
		Dim clFecha As New clsUtileriasFecha
		clFecha.FechaInicio = DateAdd(DateInterval.WeekOfYear, semana - 1, clfechaI.FechaInicio)
        clFecha.FechaFin = clFecha.FechaInicio.AddDays(6)
        clFecha.NumeroSemana = semana
		Return clFecha
	End Function
End Class
