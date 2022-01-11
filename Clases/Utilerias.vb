'Imports System.Data.Linq
Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports DevExpress.Web

Public Class Utilerias

#Region "Controles WEB"

    Public Shared Sub ddlLlenado(Of T)(lst As IList(Of T), ddl As DropDownList, value As String, text As String, Elemento_Seleccione As Boolean)
        Try
            If lst IsNot Nothing Then
                ddl.DataSource = lst
                ddl.DataTextField = text
                ddl.DataValueField = value
                ddl.DataBind()
                If Elemento_Seleccione = True Then
                    Dim seleccione As New ListItem("-- Seleccione --", "0", True)
                    seleccione.Selected = True
                    ddl.Items.Insert(0, seleccione)
                End If
            Else
                ddl.DataSource = String.Empty
                ddl.DataBind()
            End If
        Catch
        End Try
    End Sub
    Public Shared Sub lstLlenado(Of T)(lst As IList(Of T), lstObj As ListBox, value As String, text As String, Elemento_Seleccione As Boolean)
        Try
            If lst IsNot Nothing Then
                lstObj.DataSource = lst
                lstObj.DataTextField = text
                lstObj.DataValueField = value
                lstObj.DataBind()
                If Elemento_Seleccione = True Then
                    Dim seleccione As New ListItem("-- Seleccione --", "0", True)
                    seleccione.Selected = True
                    lstObj.Items.Insert(0, seleccione)
                End If
            Else
                lstObj.DataSource = String.Empty
                lstObj.DataBind()
            End If
        Catch
        End Try
    End Sub
    Public Shared Sub lstLlenadoDevExpDT(lst As DataTable, lstObj As DevExpress.Web.ASPxRadioButtonList, value As String, text As String)
        Try
            If lst IsNot Nothing Then
                lstObj.DataSource = lst
                lstObj.TextField = text
                lstObj.ValueField = value
                lstObj.DataBind()
            Else
                lstObj.DataSource = String.Empty
                lstObj.DataBind()
            End If
        Catch
        End Try
    End Sub
    Public Shared Sub lstChkLlenadoDevExp(Of T)(lst As IList(Of T), lstObj As DevExpress.Web.ASPxCheckBoxList, value As String, text As String)
        Try
            If lst IsNot Nothing Then
                lstObj.DataSource = lst
                lstObj.TextField = text
                lstObj.ValueField = value
                lstObj.DataBind()
            Else
                lstObj.DataSource = String.Empty
                lstObj.DataBind()
            End If
        Catch
        End Try
    End Sub
    Public Shared Sub ddlLlenadoDt(dt As DataTable, ddl As DropDownList, value As String, text As String, Elemento_Seleccione As Boolean)
        Try
            If dt IsNot Nothing Then
                ddl.DataSource = dt
                ddl.DataTextField = text
                ddl.DataValueField = value
                ddl.DataBind()
                If Elemento_Seleccione = True Then
                    Dim seleccione As New ListItem("-- Seleccione --", "0", True)
                    seleccione.Selected = True
                    ddl.Items.Insert(0, seleccione)
                End If
            Else
                ddl.DataSource = String.Empty
                ddl.DataBind()
            End If
        Catch
        End Try
    End Sub
    Public Shared Sub lstLlenadoDt(dt As DataTable, lst As ListBox, value As String, text As String, Elemento_Seleccione As Boolean)
        Try
            If dt IsNot Nothing Then
                lst.DataSource = dt
                lst.DataTextField = text
                lst.DataValueField = value
                lst.DataBind()
                If Elemento_Seleccione = True Then
                    Dim seleccione As New ListItem("-- Seleccione --", "0", True)
                    seleccione.Selected = True
                    lst.Items.Insert(0, seleccione)
                End If
            Else
                lst.DataSource = String.Empty
                lst.DataBind()
            End If
        Catch
        End Try
    End Sub
    Public Shared Sub gvLlenado(Of T)(lst As IList(Of T), gv As GridView)
        Try
            gv.DataSource = Nothing
            gv.DataBind()

            If lst IsNot Nothing Then
                gv.DataSource = lst
            Else
                gv.DataSource = String.Empty
            End If
            gv.DataBind()
        Catch
        End Try
    End Sub
    Public Shared Sub gvLlenadoDt(dt As DataTable, gv As GridView)
        Try
            gv.DataSource = Nothing
            gv.DataBind()

            If dt IsNot Nothing Then
                gv.DataSource = dt
            Else
                gv.DataSource = String.Empty
            End If
            gv.DataBind()
        Catch
        End Try
    End Sub
    Public Shared Sub dgLlenado(Of T)(lst As IList(Of T), gv As DataGrid)
        Try
            gv.DataSource = Nothing
            gv.DataBind()

            If lst IsNot Nothing Then
                gv.DataSource = lst
            Else
                gv.DataSource = String.Empty
            End If
            gv.DataBind()
        Catch
        End Try
    End Sub
    Public Shared Sub rptLlenado(Of T)(lst As IList(Of T), rpt As Repeater)
        Try
            rpt.DataSource = Nothing
            rpt.DataBind()

            If lst IsNot Nothing Then
                rpt.DataSource = lst
            Else
                rpt.DataSource = String.Empty
            End If
            rpt.DataBind()
        Catch
        End Try
    End Sub
#End Region

#Region "Funciones Adicionales"

    Public Shared Function convertirANumero(numero As String) As Double
        Return CDbl(Replace(Replace(numero, "", ","), "", "$"))
    End Function

    Public Shared Function Redondeo(ByVal Numero, ByVal Decimales)
        Redondeo = Int(Numero * 10 ^ Decimales + 1 / 2) / 10 ^ Decimales
    End Function

    Public Shared Function RedondeoInt(ByVal x As Double, Optional ByVal d As Integer = 0) As Double
        Dim m As Double
        m = 10 ^ d
        If x < 0 Then
            RedondeoInt = Fix(x * m - 0.5) / m
        Else
            RedondeoInt = Fix(x * m + 0.5) / m
        End If
    End Function

    Public Shared Function getDataTable(StrSelect As String) As DataTable
        'dim conexion as string = clsconexion.connectionstring
        'using con as new sqlconnection(conexion)
        '    con.open()
        '    dim dt as new datatable
        '    using da as new sqldataadapter(strselect, con)
        '        da.fill(dt)
        '        return dt
        '    end using
        'end using
        Dim conexion As String = clsConexion.ConnectionString
        Dim builder As New SqlConnectionStringBuilder(conexion)
        builder.AsynchronousProcessing = True
        Using Cnn_Hilos As New SqlConnection(builder.ConnectionString)
            Try
                Cnn_Hilos.Open()
                Using Com As New SqlCommand(StrSelect, Cnn_Hilos)
                    Try
                        Dim result As IAsyncResult
                        Dim reader1 As SqlDataReader
                        Dim dt As New DataTable
                        result = Com.BeginExecuteReader
                        While Not result.IsCompleted
                            Threading.Thread.Sleep(100)
                        End While
                        reader1 = Com.EndExecuteReader(result)
                        dt.Clear()
                        dt.Load(reader1)
                        Return dt
                    Catch ex As Exception
                    End Try
                End Using
                Cnn_Hilos.Close()
            Catch ex As Exception
            End Try
        End Using

        'Dim cmd As New ADODB.Command
        'Dim rst As New ADODB.Recordset
        'Dim cn = clsConexion.Open()
        'cmd.ActiveConnection = cn
        'cmd.CommandText = StrSelect
        'rst = cmd.Execute
        'Dim dt As DataTable
        'dt = Utilerias.RsToDt(rst)
        'rst.Close()
        'cn.Close()
        'cmd = Nothing
        'Return dt

    End Function

    Public Shared Function getDataTableAsynchronous(StrSelect As String) As DataTable
        Dim conexion As String = clsConexion.ConnectionString
        Dim builder As New SqlConnectionStringBuilder(conexion)
        Dim dt As New DataTable
        Dim dtpaso As New DataTable
        builder.AsynchronousProcessing = True
        Using Con As New SqlConnection(builder.ConnectionString)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Dim result As IAsyncResult
                Dim reader1 As SqlDataReader
                result = Com.BeginExecuteReader
                While Not result.IsCompleted
                    Threading.Thread.Sleep(100)
                End While
                reader1 = Com.EndExecuteReader(result)
                dtpaso.Clear()
                dtpaso.Load(reader1)
                dt.Merge(dtpaso)
                Return dt
            End Using
        End Using
    End Function


    'Public Shared Function RsToDt(ByVal objRS As ADODB.Recordset) As DataTable
    '    Dim objDA As New OleDbDataAdapter()
    '    Dim objDT As New DataTable()
    '    objDA.Fill(objDT, objRS)
    '    Return objDT
    'End Function

    'Public Shared Function GetDate() As DateTime
    '       Dim Conn As New clsConexion
    '       Dim fecha As DateTime
    '       Using conx = Conn.Conn()
    '           conx.Open()
    '           Dim StrSelect As String = "SELECT GETDATE()"
    '           Dim sqlCmd As SqlCommand = New SqlCommand(StrSelect, conx)
    '           Using myreader As SqlDataReader = sqlCmd.ExecuteReader
    '               If myreader.HasRows Then
    '                   myreader.Read()
    '                   fecha = CDate(myreader(0))
    '               End If
    '           End Using
    '       End Using
    '       Return fecha
    '   End Function

    Public Shared Function IsNumeric(text As String) As Boolean
        Dim regex As New Regex("^[-+]?[0-9]*\.?[0-9]+$")
        Return regex.IsMatch(text)
    End Function

    Public Shared Function EnLetras(ByVal numero As String) As String
        Dim b As Integer, paso As Integer
        Dim expresion As String, entero As String, deci As String, flag As String

        flag = "N"
        For paso = 1 To Len(numero)
            If Mid(numero, paso, 1) = "." Then
                flag = "S"
            Else
                If flag = "N" Then
                    entero = entero + Mid(numero, paso, 1) 'Extae la parte entera del numero
                Else
                    deci = deci + Mid(numero, paso, 1) 'Extrae la parte decimal del numero
                End If
            End If
        Next

        If Len(deci) = 1 Then
            deci = deci & "0"
        End If

        flag = "N"
        If Int(numero) >= -999999999 And Int(numero) <= 999999999 Then 'si el numero esta dentro de 0 a 999.999.999
            Dim NumPaso As Integer
            For paso = Len(entero) To 1 Step -1
                b = Len(entero) - (paso - 1)
                NumPaso = paso
                Select Case NumPaso
                    Case 3, 6, 9
                        Select Case Mid(entero, b, 1)
                            Case "1"
                                If Mid(entero, b + 1, 1) = "0" And Mid(entero, b + 2, 1) = "0" Then
                                    expresion = expresion & "cien "
                                Else
                                    expresion = expresion & "ciento "
                                End If
                            Case "2"
                                expresion = expresion & "doscientos "
                            Case "3"
                                expresion = expresion & "trescientos "
                            Case "4"
                                expresion = expresion & "cuatrocientos "
                            Case "5"
                                expresion = expresion & "quinientos "
                            Case "6"
                                expresion = expresion & "seiscientos "
                            Case "7"
                                expresion = expresion & "setecientos "
                            Case "8"
                                expresion = expresion & "ochocientos "
                            Case "9"
                                expresion = expresion & "novecientos "
                        End Select

                    Case 2, 5, 8
                        Select Case Mid(entero, b, 1)
                            Case "1"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    flag = "S"
                                    expresion = expresion & "diez "
                                End If
                                If Mid(entero, b + 1, 1) = "1" Then
                                    flag = "S"
                                    expresion = expresion & "once "
                                End If
                                If Mid(entero, b + 1, 1) = "2" Then
                                    flag = "S"
                                    expresion = expresion & "doce "
                                End If
                                If Mid(entero, b + 1, 1) = "3" Then
                                    flag = "S"
                                    expresion = expresion & "trece "
                                End If
                                If Mid(entero, b + 1, 1) = "4" Then
                                    flag = "S"
                                    expresion = expresion & "catorce "
                                End If
                                If Mid(entero, b + 1, 1) = "5" Then
                                    flag = "S"
                                    expresion = expresion & "quince "
                                End If
                                If Mid(entero, b + 1, 1) > "5" Then
                                    flag = "N"
                                    expresion = expresion & "dieci"
                                End If

                            Case "2"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "veinte "
                                    flag = "S"
                                Else
                                    expresion = expresion & "veinti"
                                    flag = "N"
                                End If

                            Case "3"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "treinta "
                                    flag = "S"
                                Else
                                    expresion = expresion & "treinta y "
                                    flag = "N"
                                End If

                            Case "4"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "cuarenta "
                                    flag = "S"
                                Else
                                    expresion = expresion & "cuarenta y "
                                    flag = "N"
                                End If

                            Case "5"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "cincuenta "
                                    flag = "S"
                                Else
                                    expresion = expresion & "cincuenta y "
                                    flag = "N"
                                End If

                            Case "6"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "sesenta "
                                    flag = "S"
                                Else
                                    expresion = expresion & "sesenta y "
                                    flag = "N"
                                End If

                            Case "7"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "setenta "
                                    flag = "S"
                                Else
                                    expresion = expresion & "setenta y "
                                    flag = "N"
                                End If

                            Case "8"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "ochenta "
                                    flag = "S"
                                Else
                                    expresion = expresion & "ochenta y "
                                    flag = "N"
                                End If

                            Case "9"
                                If Mid(entero, b + 1, 1) = "0" Then
                                    expresion = expresion & "noventa "
                                    flag = "S"
                                Else
                                    expresion = expresion & "noventa y "
                                    flag = "N"
                                End If
                        End Select

                    Case 1, 4, 7
                        Select Case Mid(entero, b, 1)
                            Case "1"
                                If flag = "N" Then
                                    If paso = 1 Then
                                        expresion = expresion & "un "
                                    Else
                                        If Len(entero) = 4 Then
                                            If Mid(entero, 1, 1) = 1 Then
                                                expresion = expresion & "un "
                                            Else
                                                expresion = expresion & "uno "
                                            End If
                                        Else
                                            expresion = expresion & "uno "
                                        End If
                                    End If
                                End If
                            Case "2"
                                If flag = "N" Then
                                    expresion = expresion & "dos "
                                End If
                            Case "3"
                                If flag = "N" Then
                                    expresion = expresion & "tres "
                                End If
                            Case "4"
                                If flag = "N" Then
                                    expresion = expresion & "cuatro "
                                End If
                            Case "5"
                                If flag = "N" Then
                                    expresion = expresion & "cinco "
                                End If
                            Case "6"
                                If flag = "N" Then
                                    expresion = expresion & "seis "
                                End If
                            Case "7"
                                If flag = "N" Then
                                    expresion = expresion & "siete "
                                End If
                            Case "8"
                                If flag = "N" Then
                                    expresion = expresion & "ocho "

                                End If
                            Case "9"
                                If flag = "N" Then
                                    expresion = expresion & "nueve "
                                End If
                        End Select
                End Select
                If paso = 4 Then
                    If Mid(entero, 6, 1) <> "0" Or Mid(entero, 5, 1) <> "0" Or Mid(entero, 4, 1) <> "0" Or (Mid(entero, 6, 1) = "0" And Mid(entero, 5, 1) = "0" And Mid(entero, 4, 1) = "0" And Len(entero) <= 6) Then
                        expresion = expresion & "mil "
                    End If
                End If
                If paso = 7 Then
                    If Len(entero) = 7 And Mid(entero, 1, 1) = "1" Then
                        expresion = expresion & "millón "
                    Else
                        expresion = expresion & "millones "
                    End If
                End If
            Next
            If deci <> "" Then
                If Mid(entero, 1, 1) = "-" Then 'si el numero es negativo
                    EnLetras = "menos " & expresion & " Pesos " & deci & "/100 m.n. " ' & ""
                Else
                    EnLetras = expresion & " Pesos " & deci & "/100 m.n. " ' & ""
                End If
            Else
                If Mid(entero, 1, 1) = "-" Then 'si el numero es negativo
                    EnLetras = "menos " & expresion & " Pesos 00" & "/100 m.n. "
                Else
                    EnLetras = expresion & " Pesos 00" & "/100 m.n. "
                End If
            End If
        Else 'si el numero a convertir esta fuera del rango superior e inferior
            EnLetras = ""
        End If
    End Function

    Public Shared Function GetDate() As Date
        Using contex As New DataFINANCIADINDataContext
            Return contex.ExecuteQuery(Of Date)("SELECT GETDATE() AS FechaHora").First
        End Using
    End Function

    Public Shared Function Num2Text(ByVal value As Double) As String
        Select Case value
            Case 0 : Num2Text = "CERO"
            Case 1 : Num2Text = "UN"
            Case 2 : Num2Text = "DOS"
            Case 3 : Num2Text = "TRES"
            Case 4 : Num2Text = "CUATRO"
            Case 5 : Num2Text = "CINCO"
            Case 6 : Num2Text = "SEIS"
            Case 7 : Num2Text = "SIETE"
            Case 8 : Num2Text = "OCHO"
            Case 9 : Num2Text = "NUEVE"
            Case 10 : Num2Text = "DIEZ"
            Case 11 : Num2Text = "ONCE"
            Case 12 : Num2Text = "DOCE"
            Case 13 : Num2Text = "TRECE"
            Case 14 : Num2Text = "CATORCE"
            Case 15 : Num2Text = "QUINCE"
            Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
            Case 20 : Num2Text = "VEINTE"
            Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
            Case 30 : Num2Text = "TREINTA"
            Case 40 : Num2Text = "CUARENTA"
            Case 50 : Num2Text = "CINCUENTA"
            Case 60 : Num2Text = "SESENTA"
            Case 70 : Num2Text = "SETENTA"
            Case 80 : Num2Text = "OCHENTA"
            Case 90 : Num2Text = "NOVENTA"
            Case Is < 100 : Num2Text = Num2Text(Int(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
            Case 100 : Num2Text = "CIEN"
            Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
            Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Int(value \ 100)) & "CIENTOS"
            Case 500 : Num2Text = "QUINIENTOS"
            Case 700 : Num2Text = "SETECIENTOS"
            Case 900 : Num2Text = "NOVECIENTOS"
            Case Is < 1000 : Num2Text = Num2Text(Int(value \ 100) * 100) & " " & Num2Text(value Mod 100)
            Case 1000 : Num2Text = "MIL"
            Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
            Case Is < 1000000 : Num2Text = Num2Text(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
            Case 1000000 : Num2Text = "UN MILLON"
            Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
            Case Is < 1000000000000.0# : Num2Text = Num2Text(Int(value / 1000000)) & " MILLONES "
                If (value - Int(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000) * 1000000)
            Case 1000000000000.0# : Num2Text = "UN BILLON"
            Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : Num2Text = Num2Text(Int(value / 1000000000000.0#)) & " BILLONES"
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
        End Select
        Return Num2Text
    End Function

#End Region

#Region "Insert-Update-Delete"

    '  Public Shared Sub Insertar(Of T As Class)(ByVal entidad As T)
    'Using db As New DataLinqDataContext()
    '	db.GetTable(Of T)().InsertOnSubmit(entidad)
    '	db.SubmitChanges()
    'End Using
    '      'Dim ta As New clsUsuario    
    '      'ta.FirstName = TextBox1.Text     
    '      'ta.LastName = TextBox2.Text     
    '      'Utilerias.Insertar(ta)
    '  End Sub

    '  Public Shared Sub Actualizar(Of T As Class)(ByVal originalEntidad As T, ByVal nuevaEntidad As T)
    'Using db As New DataLinqDataContext()
    '	db.GetTable(Of T)().Attach(nuevaEntidad, originalEntidad)
    '	db.Refresh(RefreshMode.KeepCurrentValues, nuevaEntidad)
    '	db.SubmitChanges()
    'End Using
    '      'Dim original As New clsUsuario <-- Clase Original se le carga el ID    
    '      'original.Id = 3    
    '      'Dim newEntity As New clsUsuario <-- Creas una nueva clase del mismo tipo     
    '      'newEntity.Id = original.Id
    '      'newEntity.FirstName = TextBox1.Text    
    '      'newEntity.LastName = TextBox2.Text    
    '      'Utilerias.Actualizar(original, newEntity)
    '  End Sub

    '  Public Shared Sub Eliminar(Of T As Class)(ByVal entidad As T)
    'Using db As New DataLinqDataContext
    '	db.GetTable(Of T)().Attach(entidad)
    '	db.GetTable(Of T).DeleteOnSubmit(entidad)
    '	db.Refresh(RefreshMode.KeepCurrentValues, entidad)
    '	db.SubmitChanges()
    'End Using
    '      'Dim ta As New clsUsuario    
    '      'ta.Id = 7    
    '      'Utilerias.Eliminar(ta)
    '  End Sub

#End Region

#Region "Encripta Desencripta"

    Private Shared DES As New TripleDESCryptoServiceProvider
    Private Shared MD5 As New MD5CryptoServiceProvider

    Private Shared Function MD5Hash(ByVal value As String) As Byte()
        Return MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value))
    End Function

    Public Shared Function EncriptarMD5(ByVal stringToEncrypt As String, ByVal key As String) As String
        DES.Key = Utilerias.MD5Hash(key)
        DES.Mode = CipherMode.ECB
        Dim Buffer As Byte() = ASCIIEncoding.ASCII.GetBytes(stringToEncrypt)
        Return Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length))
    End Function

    Public Shared Function DesencriptarMD5(ByVal encryptedString As String, ByVal key As String) As String
        Try
            DES.Key = Utilerias.MD5Hash(key)
            DES.Mode = CipherMode.ECB
            Dim Buffer As Byte() = Convert.FromBase64String(encryptedString)
            Return ASCIIEncoding.ASCII.GetString(DES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function Encriptar(ByVal Password As String) As String
        Encriptar = ""
        Try
            Dim Valor As Int32, i As Int32
            Dim StrTmp As String = ""
            If Password.Trim.Length > 0 Then
                For i = 1 To Password.Trim.Length
                    Valor = Asc(Password.Substring(i - 1, 1))
                    Valor = Valor * 2
                    Valor = Valor + (i * 2)
                    If Valor > 255 Then Valor = 255
                    StrTmp = StrTmp & Chr(Valor)
                Next
            End If
            Encriptar = StrTmp
        Catch ex As Exception
            Throw New System.Exception(ex.Message)
        End Try
    End Function

    Public Shared Function Desencriptar(ByVal Password As String) As String
        Desencriptar = ""
        Try
            Dim Valor As Int32, i As Int32
            Dim StrTmp As String = ""
            If Password.Trim.Length > 0 Then
                For i = 0 To Password.Trim.Length - 1

                    Valor = Asc(Password.Substring(i, 1))
                    Valor = Valor / 2
                    Valor = Valor - (i + 1)
                    If Valor > 255 Then Valor = 255
                    StrTmp = StrTmp & Chr(Valor)
                Next
            End If
            Desencriptar = StrTmp
        Catch ex As Exception
            Throw New System.Exception(ex.Message)
        End Try
    End Function

#End Region

#Region "Mensajes Pop-up"
    Public Shared Function MensajeAlerta(mensaje As String, pagina As Page, tieneSM As Boolean) As Boolean
        Dim sb As New System.Text.StringBuilder()
        If tieneSM Then
            sb.Append("swal({")
            sb.Append("html: '" & mensaje & "',")
            sb.Append("type: 'warning'")
            sb.Append("});")
        Else
            sb.Append("<script type = 'text/javascript'>")
            sb.Append("window.onload=function(){")
            sb.Append("swal({")
            sb.Append("html: '" & mensaje & "',")
            sb.Append("type: 'warning'")
            sb.Append("});")
            sb.Append("}")
            sb.Append("</script>")
        End If

        ScriptManager.RegisterClientScriptBlock(pagina, pagina.GetType(), "alert", sb.ToString(), True)
    End Function

    Public Shared Function MensajeConfirmacion(mensaje As String, pagina As Page, tieneSM As Boolean, Optional conTimer As Boolean = True) As Boolean
        Dim sb As New System.Text.StringBuilder()
        If tieneSM Then
            sb.Append("swal({")
            sb.Append("html: '" & mensaje & "',")
            sb.Append("type: 'success',")
            sb.Append("showConfirmButton: false,")
            If conTimer Then sb.Append("timer: 3000")
            sb.Append("});")
        Else
            sb.Append("<script type = 'text/javascript'>")
            sb.Append("window.onload=function(){")
            sb.Append("swal({")
            sb.Append("html: '" & mensaje & "',")
            sb.Append("type: 'success',")
            sb.Append("showConfirmButton: false,")
            If conTimer Then sb.Append("timer: 3000")
            sb.Append("});")
            sb.Append("}")
            sb.Append("</script>")
        End If

        ScriptManager.RegisterClientScriptBlock(pagina, pagina.GetType(), "alert", sb.ToString(), True)
    End Function
#End Region

#Region "FINANCIADIN"
    REM Recibe una fecha como entrada y determina en que grupo de pago pertenece y por tanto, la fecha de pago MARTES(Lunes,Martes y Miercoles) VIERNES(Jueves y Viernes)
    Public Shared Function calcularDiaDePago(fecha As Date) As Date
        Select Case fecha.DayOfWeek
            Case DayOfWeek.Monday, DayOfWeek.Thursday
                fecha = fecha.AddDays(1)
            Case DayOfWeek.Tuesday, DayOfWeek.Friday
                fecha = fecha
            Case DayOfWeek.Wednesday
                fecha = fecha.AddDays(-1)
            Case DayOfWeek.Saturday
                fecha = fecha.AddDays(3)
            Case DayOfWeek.Sunday
                fecha = fecha.AddDays(2)
        End Select

        While clsDiaInhabil.esInhabil(fecha)
            fecha = fecha.AddDays(1)
            If fecha.DayOfWeek = 6 Then
                fecha = fecha.AddDays(2)
            ElseIf fecha.DayOfWeek = 7 Then
                fecha = fecha.AddDays(1)
            End If
        End While

        Return fecha

    End Function

    Public Shared Function calcularFechaDePago(fechaInicio As Date, numPagos As Integer, plazo As Integer) As Date
        Dim fechaPago As DateTime

        fechaPago = If(plazo = 1, fechaInicio.AddDays(numPagos * 7),
            If(plazo = 3, fechaInicio.AddMonths(numPagos),
            fechaInicio.AddDays(numPagos * 15)))

        Return If(plazo = 3, fechaPago, calcularDiaDePago(fechaPago))

    End Function


    Public Shared Function calcularFechaDePagoEmp(fechaInicio As Date, plazo As Integer) As Date
        Dim fechaPago As DateTime
        If plazo = 2 Then
            fechaPago = fechaInicio.AddDays(15)
        End If


        Return fechaPago

    End Function

    Public Shared Function agregarDiasHabiles(fecha As Date, diasAgregados As Integer) As Date

        fecha = fecha.AddDays(2)
        If fecha.DayOfWeek = DayOfWeek.Sunday Then fecha = fecha.AddDays(1)

        Return fecha

    End Function

    Public Shared Function crearTablaDePagos(FecInicio As DateTime, Prestamo As Double, Adeudo As Double, NumPagos As Integer, Plazo As Integer) As DataTable

        Dim interes = Adeudo - Prestamo
        Dim pago = Math.Ceiling(Adeudo / NumPagos)
        Dim pagoCap = Math.Round(Prestamo / NumPagos, 2)
        Dim pagInteres = Math.Round((interes / NumPagos) * (0.8621), 2)
        Dim pagIva = Math.Round((interes / NumPagos) * (0.1379), 2)

        pagoCap = pagoCap + (pago - (pagoCap + pagInteres + pagIva))

        'Dim ds As New DataSet
        Dim dt As DataTable = New DataTable()
        dt.TableName = "TABLADEAMORTIZACION"
        Dim dr As DataRow

        Dim numPago As DataColumn = New DataColumn("#Pago", Type.GetType("System.Int32"))
        Dim fechaPago As DataColumn = New DataColumn("Fecha", Type.GetType("System.DateTime"))
        Dim totalPago As DataColumn = New DataColumn("TotalPago", Type.GetType("System.Double"))
        Dim pagoCapital As DataColumn = New DataColumn("Capital", Type.GetType("System.Double"))
        Dim pagoInteres As DataColumn = New DataColumn("Interes", Type.GetType("System.Double"))
        Dim pagoIva As DataColumn = New DataColumn("IVA", Type.GetType("System.Double"))
        Dim saldo As DataColumn = New DataColumn("Saldo", Type.GetType("System.Double"))
        Dim saldoCapital As DataColumn = New DataColumn("SaldoCapital", Type.GetType("System.Double"))

        dt.Columns.Add(numPago)
        dt.Columns.Add(fechaPago)
        dt.Columns.Add(totalPago)
        dt.Columns.Add(pagoCapital)
        dt.Columns.Add(pagoInteres)
        dt.Columns.Add(pagoIva)
        dt.Columns.Add(saldo)
        dt.Columns.Add(saldoCapital)

        For pagoActual As Integer = 1 To NumPagos
            dr = dt.NewRow()
            dr("#Pago") = pagoActual
            dr("Fecha") = Utilerias.calcularFechaDePago(FecInicio, pagoActual, Plazo)
            dr("TotalPago") = If(pagoActual = NumPagos, Adeudo - (pago * (pagoActual - 1)), pago)
            dr("Capital") = If(pagoActual = NumPagos, dr("TotalPago") - pagInteres - pagIva, pagoCap)
            dr("Interes") = pagInteres
            dr("IVA") = pagIva
            dr("Saldo") = If(pagoActual = NumPagos, 0, Adeudo - (pago * pagoActual))
            dr("SaldoCapital") = If(pagoActual + 1 = NumPagos, dr("Saldo") - pagInteres - pagIva, If(pagoActual = NumPagos, 0, Prestamo - (pagoCap * (pagoActual - 1)) - dr("Capital")))
            dt.Rows.Add(dr)
        Next pagoActual

        'ds.Tables.Add(dt)
        'ds.Tables(0).TableName = "TABLADEAMORTIZACION"

        Return dt
    End Function

    Public Shared Function crearTablaDePagosProgresiva(FecInicio As DateTime, Prestamo As Double, Adeudo As Double, Sobretasa As Double, NumPagos As Integer, Plazo As Integer) As DataTable

        Dim saldoCap = Prestamo
        Dim pago = Math.Abs(Pmt(Sobretasa, NumPagos, Prestamo))
        Dim pagoCap
        Dim pagInteres
        Dim pagIva

        'pagoCap = pagoCap + (pago - (pagoCap + pagInteres + pagIva))

        'Dim ds As New DataSet
        Dim dt As DataTable = New DataTable()
        dt.TableName = "TABLADEAMORTIZACION"
        Dim dr As DataRow

        Dim numPago As DataColumn = New DataColumn("#Pago", Type.GetType("System.Int32"))
        Dim fechaPago As DataColumn = New DataColumn("Fecha", Type.GetType("System.DateTime"))
        Dim totalPago As DataColumn = New DataColumn("TotalPago", Type.GetType("System.Double"))
        Dim pagoCapital As DataColumn = New DataColumn("Capital", Type.GetType("System.Double"))
        Dim pagoInteres As DataColumn = New DataColumn("Interes", Type.GetType("System.Double"))
        Dim pagoIva As DataColumn = New DataColumn("IVA", Type.GetType("System.Double"))
        Dim saldo As DataColumn = New DataColumn("Saldo", Type.GetType("System.Double"))
        Dim saldoCapital As DataColumn = New DataColumn("SaldoCapital", Type.GetType("System.Double"))

        dt.Columns.Add(numPago)
        dt.Columns.Add(fechaPago)
        dt.Columns.Add(totalPago)
        dt.Columns.Add(pagoCapital)
        dt.Columns.Add(pagoInteres)
        dt.Columns.Add(pagoIva)
        dt.Columns.Add(saldo)
        dt.Columns.Add(saldoCapital)

        For pagoActual As Integer = 1 To NumPagos
            Dim interes = Math.Abs(IPmt(Sobretasa, pagoActual, NumPagos, Prestamo))
            pagInteres = Math.Round(Math.Abs(IPmt(Sobretasa, pagoActual, NumPagos, Prestamo)) * (0.8621), 2)
            pagIva = Math.Round(Math.Abs(IPmt(Sobretasa, pagoActual, NumPagos, Prestamo)) * (0.1379), 2)
            pagoCap = pago - pagInteres - pagIva
            dr = dt.NewRow()
            dr("#Pago") = pagoActual
            dr("Fecha") = Utilerias.calcularFechaDePago(FecInicio, pagoActual, Plazo)
            dr("TotalPago") = pago 'If(pagoActual = NumPagos, Adeudo - (pago * (pagoActual - 1)), pago)
            Adeudo = Adeudo - dr("TotalPago")
            dr("Capital") = If(pagoActual = NumPagos, saldoCap, pagoCap)
            saldoCap = saldoCap - dr("Capital")
            dr("Interes") = pagInteres
            dr("IVA") = pagIva
            dr("Saldo") = Adeudo
            dr("SaldoCapital") = saldoCap 'If(pagoActual + 1 = NumPagos, dr("Saldo") - pagInteres - pagIva, If(pagoActual = NumPagos, 0, Prestamo - (pagoCap * (pagoActual - 1)) - dr("Capital")))
            dt.Rows.Add(dr)
        Next pagoActual

        'ds.Tables.Add(dt)
        'ds.Tables(0).TableName = "TABLADEAMORTIZACION"

        Return dt
    End Function

    'Public Shared Function crearTablaDePagos(credito As clsCredito, Optional soloHistorial As Boolean = False) As DataTable

    '    Dim pagos = clsPago.obtenerHistorial(credito.Id)

    '    Dim deuda = credito.Adeudo
    '    Dim interes = credito.Adeudo - credito.MontoPrestado
    '    Dim capital = credito.MontoPrestado
    '    Dim pagoTotal = Math.Ceiling(credito.Adeudo / credito.NumPagos)
    '    Dim pagInteres = Math.Round((interes / credito.NumPagos) * (0.8621), 2)
    '    Dim pagIva = Math.Round((interes / credito.NumPagos) * (0.1379), 2)
    '    Dim pagoCap = pagoTotal - pagInteres - pagIva


    '    'Dim ds As New DataSet
    '    Dim dt As DataTable = New DataTable()
    '    dt.TableName = "TABLADEAMORTIZACION"
    '    Dim dr As DataRow

    '    Dim numPago As DataColumn = New DataColumn("#Pago", Type.GetType("System.Int32"))
    '    Dim fechaEsperada As DataColumn = New DataColumn("FechaEsperada", Type.GetType("System.DateTime"))
    '    Dim fechaPago As DataColumn = New DataColumn("FechaPago", Type.GetType("System.DateTime"))
    '    Dim totalPago As DataColumn = New DataColumn("TotalPago", Type.GetType("System.Double"))
    '    Dim pagoACredito As DataColumn = New DataColumn("Pago", Type.GetType("System.Double"))
    '    Dim recargo As DataColumn = New DataColumn("Recargo", Type.GetType("System.Double"))
    '    Dim pagoCapital As DataColumn = New DataColumn("Capital", Type.GetType("System.Double"))
    '    Dim pagoInteres As DataColumn = New DataColumn("Interes", Type.GetType("System.Double"))
    '    Dim pagoIva As DataColumn = New DataColumn("IVA", Type.GetType("System.Double"))
    '    Dim saldo As DataColumn = New DataColumn("Saldo", Type.GetType("System.Double"))
    '    Dim saldoCapital As DataColumn = New DataColumn("SaldoCapital", Type.GetType("System.Double"))
    '    Dim id As DataColumn = New DataColumn("id", Type.GetType("System.Int32"))
    '    Dim movto As DataColumn = New DataColumn("Movimiento", Type.GetType("System.Int32"))

    '    dt.Columns.Add(numPago)
    '    dt.Columns.Add(fechaEsperada)
    '    dt.Columns.Add(fechaPago)
    '    dt.Columns.Add(totalPago)
    '    dt.Columns.Add(pagoACredito)
    '    dt.Columns.Add(recargo)
    '    dt.Columns.Add(pagoCapital)
    '    dt.Columns.Add(pagoInteres)
    '    dt.Columns.Add(pagoIva)
    '    dt.Columns.Add(saldo)
    '    dt.Columns.Add(saldoCapital)
    '    dt.Columns.Add(id)
    '    dt.Columns.Add(movto)
    '    'If soloHistorial Then dt.Columns.Add(recargo)

    '    Dim pagosMismaLetra As New List(Of clsPago)
    '    Dim garantia = pagos.Find(Function(clsPago) clsPago.EsGarantia = True)
    '    Dim pagoAbonoCap = pagos.FindAll(Function(clsPago) clsPago.EsAbonoCapital = True)
    '    Dim fecPagoAnterior As DateTime
    '    Dim saldoAnterior As Double

    '    Dim abonosAEliminar As New List(Of clsPago)

    '    For Each abonocap In pagoAbonoCap
    '        Dim pagosAntAbCap = pagos.FindAll(Function(clsPago) clsPago.Id < abonocap.Id)
    '        If pagosAntAbCap.Count = 0 Then
    '            dr = dt.NewRow()
    '            dr("#Pago") = 0
    '            dr("FechaEsperada") = abonocap.FecEsperada
    '            dr("FechaPago") = abonocap.FecPago
    '            dr("TotalPago") = abonocap.Monto
    '            capital = capital - dr("TotalPago") + saldoAnterior
    '            saldoAnterior = 0
    '            deuda = calcularNuevoSaldo(credito, 0, capital)
    '            interes = deuda - capital
    '            pagoTotal = Math.Ceiling(deuda / (credito.NumPagos - (0)))
    '            pagInteres = Math.Round((interes / (credito.NumPagos - (0))) * (0.8621), 1)
    '            pagIva = Math.Round((interes / (credito.NumPagos - (0))) * (0.1379), 1)
    '            pagoCap = pagoTotal - pagInteres - pagIva
    '            dr("Pago") = abonocap.Monto
    '            dr("Recargo") = 0
    '            dr("Capital") = abonocap.Monto
    '            dr("Interes") = 0
    '            dr("IVA") = 0
    '            dr("SaldoCapital") = abonocap.SaldoCapital
    '            capital = dr("SaldoCapital")
    '            dr("Saldo") = If(abonocap.Saldo <= 0, 0, abonocap.Saldo)
    '            dr("id") = abonocap.Id
    '            dr("Movimiento") = abonocap.Id_Mov
    '            dt.Rows.Add(dr)
    '            abonosAEliminar.Add(abonocap)
    '        End If
    '    Next abonocap

    '    If IsNothing(abonosAEliminar) = False Then pagoAbonoCap.RemoveAll(Function(e) abonosAEliminar.Exists(Function(f) f.Id = e.Id))

    '    For pagoActual As Integer = 1 To credito.NumPagos

    '        If IsNothing(garantia) = False AndAlso (garantia.GarantiaAplicada = False Or IsNothing(garantia.GarantiaAplicada)) Then
    '            dr = dt.NewRow()
    '            dr("#Pago") = garantia.NumPago
    '            dr("FechaEsperada") = garantia.FecEsperada
    '            dr("FechaPago") = garantia.FecPago
    '            dr("TotalPago") = garantia.Monto
    '            dr("Pago") = garantia.Monto
    '            dr("Recargo") = 0
    '            dr("Capital") = 0
    '            dr("Interes") = 0
    '            dr("IVA") = 0
    '            dr("Saldo") = garantia.Saldo
    '            dr("SaldoCapital") = garantia.SaldoCapital
    '            dr("id") = garantia.Id
    '            dr("Movimiento") = garantia.Id_Mov
    '            dt.Rows.Add(dr)
    '            garantia = Nothing
    '        End If

    '        pagosMismaLetra = pagos.FindAll(Function(clsPago) clsPago.NumPago = pagoActual)
    '        'Dim abonocap = pagoAbonoCap.Find(Function(clsPago) clsPago.EsGarantia = True)
    '        Dim totalLetra As Double

    '        For Each pago In pagosMismaLetra

    '            If pagosMismaLetra.IndexOf(pago) = 0 Then
    '                deuda = deuda - pago.Monto
    '                Dim restante = pago.Monto
    '                dr = dt.NewRow()
    '                dr("#Pago") = pago.NumPago
    '                dr("FechaEsperada") = pago.FecEsperada
    '                dr("FechaPago") = pago.FecPago
    '                dr("Interes") = pago.AbonoInteres
    '                'If(restante > pagInteres, pagInteres, restante)
    '                'restante = restante - dr("Interes")
    '                dr("IVA") = pago.AbonoIVA
    '                'If(restante > pagIva, pagIva, restante)
    '                'restante = restante - dr("IVA")
    '                dr("Capital") = pago.AbonoCapital
    '                dr("TotalPago") = pago.Monto + pago.RecargoCobrado
    '                totalLetra = totalLetra + pago.Monto
    '                saldoAnterior = saldoAnterior + (pagoTotal - pago.Monto)
    '                dr("Pago") = pago.Monto
    '                dr("Recargo") = pago.RecargoCobrado
    '                dr("Saldo") = If(pago.Saldo < 0, 0, pago.Saldo)
    '                dr("SaldoCapital") = If(pago.SaldoCapital < 0, 0, pago.SaldoCapital)
    '                capital = dr("SaldoCapital")
    '                dr("id") = pago.Id
    '                If soloHistorial Then dr("Recargo") = pago.Recargo
    '                dr("Movimiento") = pago.Id_Mov
    '                dt.Rows.Add(dr)
    '                fecPagoAnterior = dr("FechaPago")
    '            Else
    '                deuda = deuda - pago.Monto
    '                Dim restante = pago.Monto
    '                Dim pagosAnteriores = totalLetra
    '                dr = dt.NewRow()
    '                dr("#Pago") = pago.NumPago
    '                dr("FechaEsperada") = pago.FecEsperada
    '                dr("FechaPago") = pago.FecPago
    '                dr("Interes") = pago.AbonoInteres
    '                'If(pagosAnteriores >= pagInteres, 0, If(restante > pagInteres - pagosAnteriores, 0, restante))
    '                'pagosAnteriores = pagosAnteriores - dr("Interes")
    '                'restante = restante - dr("Interes")
    '                dr("IVA") = pago.AbonoIVA
    '                'If(pagosAnteriores >= pagIva, 0, If(restante > pagIva - pagosAnteriores, 0, restante))
    '                'pagosAnteriores = pagosAnteriores - dr("IVA")
    '                restante = restante - dr("IVA")
    '                dr("TotalPago") = pago.Monto + pago.RecargoCobrado
    '                totalLetra = totalLetra + pago.Monto
    '                saldoAnterior = saldoAnterior - pago.Monto
    '                dr("Pago") = pago.Monto
    '                dr("Recargo") = pago.RecargoCobrado
    '                dr("Capital") = pago.AbonoCapital
    '                dr("Saldo") = If(pago.Saldo < 0, 0, pago.Saldo)
    '                dr("SaldoCapital") = If(pago.SaldoCapital < 0, 0, pago.SaldoCapital)
    '                capital = dr("SaldoCapital")
    '                dr("id") = pago.Id
    '                If soloHistorial Then dr("Recargo") = pago.Recargo
    '                dr("Movimiento") = pago.Id_Mov
    '                dt.Rows.Add(dr)
    '                fecPagoAnterior = dr("FechaPago")
    '            End If
    '        Next

    '        For Each abonocap In pagoAbonoCap
    '            Dim pagoAct = If(pagosMismaLetra.Count = 0, Nothing, pagosMismaLetra.Last())
    '            Dim pagosAnt = pagos.FindAll(Function(clsPago) clsPago.FecPago <= abonocap.FecPago And clsPago.Saldo > abonocap.Saldo And clsPago.EsGarantia = 0)
    '            Dim pagosAntObl = pagos.FindAll(Function(clsPago) clsPago.FecPago <= abonocap.FecPago And clsPago.Saldo > abonocap.Saldo And clsPago.EsGarantia = 0 And clsPago.EsAbonoCapital = 0)
    '            Dim pagoAnt = pagosAnt.OrderBy(Function(clsPago) clsPago.SaldoCapital).FirstOrDefault
    '            'If IsNothing(pagoAnt) OrElse (pagoAnt.EsAbonoCapital) OrElse ((pagoAnt.FecPago <= abonocap.FecPago) And (Not IsNothing((pagosMismaLetra.Find(Function(clsPago) clsPago.Id = pagoAnt.Id))))) Then
    '            If IsNothing(pagoAnt) OrElse (pagosAntObl.Last().Id = pagoAct.Id AndAlso (pagoAct.Id = pagoAnt.Id OrElse pagoAnt.EsAbonoCapital)) Then
    '                dr = dt.NewRow()
    '                dr("#Pago") = 0
    '                dr("FechaEsperada") = abonocap.FecEsperada
    '                dr("FechaPago") = abonocap.FecPago
    '                dr("TotalPago") = abonocap.Monto
    '                capital = capital - dr("TotalPago") + saldoAnterior
    '                saldoAnterior = 0
    '                If IsNothing(pagoAnt) Then pagoActual = 0
    '                deuda = calcularNuevoSaldo(credito, pagoActual, capital)
    '                interes = deuda - capital
    '                pagoTotal = Math.Ceiling(deuda / (credito.NumPagos - (pagoActual)))
    '                pagInteres = Math.Round((interes / (credito.NumPagos - (pagoActual))) * (0.8621), 1)
    '                pagIva = Math.Round((interes / (credito.NumPagos - (pagoActual))) * (0.1379), 1)
    '                If IsNothing(pagoAnt) Then pagoActual = 1
    '                pagoCap = pagoTotal - pagInteres - pagIva
    '                dr("Pago") = abonocap.Monto
    '                dr("Recargo") = 0
    '                dr("Capital") = abonocap.Monto
    '                dr("Interes") = 0
    '                dr("IVA") = 0
    '                dr("SaldoCapital") = abonocap.SaldoCapital
    '                capital = dr("SaldoCapital")
    '                dr("Saldo") = If(abonocap.Saldo <= 0, 0, abonocap.Saldo)
    '                dr("id") = abonocap.Id
    '                dr("Movimiento") = abonocap.Id_Mov
    '                dt.Rows.Add(dr)
    '                abonosAEliminar.Add(abonocap)
    '                totalLetra = 0
    '                If capital <= 0 Then Exit For
    '            End If
    '        Next

    '        If IsNothing(abonosAEliminar) = False Then pagoAbonoCap.RemoveAll(Function(e) abonosAEliminar.Exists(Function(f) f.Id = e.Id))

    '        If totalLetra <> 0 And pagoTotal - totalLetra > 0 And IsNothing(pagos.Find(Function(clsPago) clsPago.NumPago = pagoActual + 1)) Then
    '            dr = dt.NewRow()
    '            dr("#Pago") = pagoActual
    '            dr("FechaEsperada") = pagosMismaLetra.ElementAt(0).FecEsperada
    '            dr("FechaPago") = DBNull.Value
    '            dr("TotalPago") = If(pagoActual = credito.NumPagos Or deuda < (pagoTotal - totalLetra), deuda, If(saldoAnterior > (pagoTotal - totalLetra), saldoAnterior, (pagoTotal - totalLetra)))
    '            'dr("TotalPago") = pagoTotal - totalLetra
    '            saldoAnterior = If(pagoActual = credito.NumPagos, 0, If(saldoAnterior < 0, saldoAnterior, If((pagoTotal - totalLetra) + saldoAnterior <= 0, (pagoTotal - totalLetra) + saldoAnterior, 0)))
    '            deuda = deuda - dr("TotalPago")
    '            dr("Pago") = DBNull.Value
    '            dr("Recargo") = DBNull.Value
    '            Dim interesCubierto = pagosMismaLetra.Sum(Function(clsPago) clsPago.AbonoInteres)
    '            Dim ivaCubierto = pagosMismaLetra.Sum(Function(clsPago) clsPago.AbonoIVA)
    '            dr("Interes") = pagInteres - interesCubierto '+ ((credito.NumPagos - pagoActual) * pagInteres)
    '            dr("IVA") = pagIva - ivaCubierto '+ ((credito.NumPagos - pagoActual) * pagIva)
    '            dr("Capital") = dr("TotalPago") - dr("Interes") - dr("IVA")
    '            capital = If(capital - dr("Capital") <= 0, 0, capital - dr("Capital"))
    '            dr("Saldo") = deuda
    '            'dr("SaldoCapital") = If(capital <= 0, 0, If(pagoActual + 1 = credito.NumPagos, dr("Saldo") - dr("Interes") - dr("IVA"), If(pagoActual = credito.NumPagos, 0, capital)))
    '            dr("SaldoCapital") = capital

    '            If dr("Saldo") < dr("SaldoCapital") Then
    '                Dim dif = dr("SaldoCapital") - dr("Saldo")
    '                'dr("TotalPago") = dr("TotalPago") + dif
    '                dr("Capital") = dr("Capital") + dif
    '                dr("SaldoCapital") = dr("SaldoCapital") - dif
    '                dr("Interes") = dr("Interes") - Math.Round(dif * (0.8621), 2)
    '                dr("IVA") = dr("IVA") - Math.Round(dif * (0.1379), 2)
    '            End If

    '            dt.Rows.Add(dr)
    '        End If

    '        'Dim pagoAnterior = pagos.Find(Function(clsPago) clsPago.NumPago = pagoActual - 1)

    '        If IsNothing(garantia) = False AndAlso (garantia.GarantiaAplicada = True And pagos.Max(Function(clsPago) clsPago.NumPago) <= pagoActual) Then
    '            dr = dt.NewRow()
    '            dr("#Pago") = garantia.NumPago
    '            dr("FechaEsperada") = garantia.FecEsperada
    '            dr("FechaPago") = garantia.FecAplicGarantia
    '            dr("TotalPago") = garantia.Monto
    '            dr("Pago") = garantia.Monto
    '            dr("Recargo") = 0
    '            dr("Capital") = garantia.AbonoCapital
    '            dr("Interes") = garantia.AbonoInteres
    '            dr("IVA") = garantia.AbonoIVA
    '            dr("Saldo") = If(garantia.Saldo < 0, 0, garantia.Saldo)
    '            deuda = dr("Saldo")
    '            dr("SaldoCapital") = If(garantia.SaldoCapital < 0, 0, garantia.SaldoCapital)
    '            capital = dr("SaldoCapital")
    '            dr("id") = garantia.Id
    '            dr("Movimiento") = garantia.Id_Mov
    '            dt.Rows.Add(dr)
    '            garantia = Nothing
    '            If capital <= 0 Then Exit For
    '        End If



    '        totalLetra = 0
    '        'If deuda <= 0 Or capital <= 0 And (IsNothing(garantia) = False AndAlso Not garantia.GarantiaAplicada) Then
    '        '    Exit For
    '        'ElseIf pagosMismaLetra.Count = 0 And soloHistorial = True Then
    '        '    dr = dt.NewRow()
    '        '    dr("#Pago") = pagoActual
    '        '    dr("FechaEsperada") = Utilerias.calcularDiaDePago(If(credito.Plazo = 1, credito.FecInicio.AddDays(pagoActual * 7), If(credito.Plazo = 3, credito.FecInicio.AddMonths(pagoActual), credito.FecInicio.AddDays(pagoActual * 15))))
    '        '    dr("FechaPago") = DBNull.Value
    '        '    dr("TotalPago") = If(pagoActual = credito.NumPagos, If(deuda + saldoAnterior <= 0, 0, deuda + saldoAnterior), If(pagoTotal + saldoAnterior <= 0, 0, pagoTotal + saldoAnterior))
    '        '    saldoAnterior = If(pagoActual = credito.NumPagos, If(deuda + saldoAnterior <= 0, deuda + saldoAnterior, 0), If(pagoTotal + saldoAnterior <= 0, pagoTotal + saldoAnterior, 0))
    '        '    deuda = deuda - dr("TotalPago")
    '        '    dr("Pago") = DBNull.Value
    '        '    dr("Recargo") = DBNull.Value
    '        '    dr("Interes") = If(dr("TotalPago") = 0, 0, If(dr("TotalPago") >= pagInteres, pagInteres, dr("TotalPago")))
    '        '    dr("IVA") = If(dr("TotalPago") = 0, 0, If(dr("TotalPago") - dr("Interes") >= pagIva, pagIva, dr("TotalPago")))
    '        '    dr("Capital") = dr("TotalPago") - dr("Interes") - dr("IVA")
    '        '    capital = capital - dr("Capital")
    '        '    dr("Saldo") = deuda
    '        '    dr("SaldoCapital") = If(pagoActual + 1 = credito.NumPagos, dr("Saldo") - pagInteres - pagIva, If(pagoActual = credito.NumPagos, 0, capital))
    '        '    dt.Rows.Add(dr)
    '        '    Exit For
    '        'End If

    '        If deuda <= 0 And capital <= 0 And (IsNothing(garantia) = False AndAlso Not garantia.GarantiaAplicada) Then
    '            Exit For
    '        ElseIf pagosMismaLetra.Count = 0 And soloHistorial = False Then
    '            dr = dt.NewRow()
    '            dr("#Pago") = pagoActual
    '            'dr("FechaEsperada") = Utilerias.calcularDiaDePago(If(credito.Plazo = 1, credito.FecInicio.AddDays(pagoActual * 7), If(credito.Plazo = 3, credito.FecInicio.AddMonths(pagoActual), credito.FecInicio.AddDays(pagoActual * 15))))
    '            dr("FechaEsperada") = Utilerias.calcularFechaDePago(credito.FecInicio, pagoActual, credito.Plazo)
    '            dr("FechaPago") = DBNull.Value
    '            'dr("TotalPago") = If(pagoActual = credito.NumPagos, If(deuda + saldoAnterior <= 0, 0, deuda + saldoAnterior), If(pagoTotal + saldoAnterior <= 0, 0, pagoTotal + saldoAnterior))
    '            dr("TotalPago") = If(pagoActual = credito.NumPagos Or deuda < pagoTotal, deuda, If(pagoTotal + If(saldoAnterior > 0, saldoAnterior, 0) <= 0, 0, pagoTotal + If(saldoAnterior > 0, saldoAnterior, 0)))
    '            saldoAnterior = If(pagoActual = credito.NumPagos, 0, If(saldoAnterior < 0, saldoAnterior, If(pagoTotal + saldoAnterior <= 0, pagoTotal + saldoAnterior, 0)))
    '            deuda = deuda - dr("TotalPago")
    '            dr("Pago") = DBNull.Value
    '            dr("Recargo") = DBNull.Value
    '            dr("Interes") = If(dr("TotalPago") = 0, 0, If(dr("TotalPago") >= pagInteres, pagInteres, dr("TotalPago"))) '+ ((credito.NumPagos - pagoActual) * pagInteres)
    '            dr("IVA") = If(dr("TotalPago") = 0, 0, If(dr("TotalPago") - dr("Interes") >= pagIva, pagIva, dr("TotalPago"))) '+ ((credito.NumPagos - pagoActual) * pagIva)
    '            dr("Capital") = dr("TotalPago") - dr("Interes") - dr("IVA")
    '            Dim capitalAnt = capital
    '            capital = If(capital - dr("Capital") <= 0, 0, capital - dr("Capital"))
    '            dr("Saldo") = deuda
    '            'dr("SaldoCapital") = If(capital <= 0, 0, If(pagoActual + 1 = credito.NumPagos, dr("Saldo") - dr("Interes") - dr("IVA"), If(pagoActual = credito.NumPagos, 0, capital)))
    '            dr("SaldoCapital") = capital

    '            If pagoActual = credito.NumPagos Then
    '                Dim dif = dr("Capital") - capitalAnt
    '                'dr("TotalPago") = dr("TotalPago") + dif
    '                dr("Capital") = dr("Capital") - dif
    '                If dr("SaldoCapital") > 0 Then dr("SaldoCapital") = dr("SaldoCapital") + dif
    '                dr("Interes") = Math.Round((dr("TotalPago") - dr("Capital")) * (0.8621), 2)
    '                dr("IVA") = Math.Round((dr("TotalPago") - dr("Capital")) * (0.1379), 2)
    '            End If

    '            dt.Rows.Add(dr)
    '            If soloHistorial Then Exit For
    '        End If

    '    Next pagoActual

    '    'ds.Tables.Add(dt)
    '    'ds.Tables(0).TableName = "TABLADEAMORTIZACION"

    '    Return dt

    'End Function

    Public Shared Function crearTablaDePagos(credito As clsCredito, pagos As List(Of clsPago)) As DataTable

        'Dim ds As New DataSet
        Dim historial As New List(Of clsPago)(pagos)
        Dim dt As DataTable = New DataTable()
        dt.TableName = "TABLADEAMORTIZACION"
        Dim dr As DataRow

        Dim numPago As DataColumn = New DataColumn("#Pago", Type.GetType("System.Int32"))
        Dim fechaEsperada As DataColumn = New DataColumn("FechaEsperada", Type.GetType("System.DateTime"))
        Dim fechaPago As DataColumn = New DataColumn("FechaPago", Type.GetType("System.DateTime"))
        Dim totalPago As DataColumn = New DataColumn("TotalPago", Type.GetType("System.Double"))
        Dim pagoACredito As DataColumn = New DataColumn("Pago", Type.GetType("System.Double"))
        Dim recargo As DataColumn = New DataColumn("Recargo", Type.GetType("System.Double"))
        Dim pagoCapital As DataColumn = New DataColumn("Capital", Type.GetType("System.Double"))
        Dim pagoInteres As DataColumn = New DataColumn("Interes", Type.GetType("System.Double"))
        Dim pagoIva As DataColumn = New DataColumn("IVA", Type.GetType("System.Double"))
        Dim saldo As DataColumn = New DataColumn("Saldo", Type.GetType("System.Double"))
        Dim saldoCapital As DataColumn = New DataColumn("SaldoCapital", Type.GetType("System.Double"))
        Dim id As DataColumn = New DataColumn("id", Type.GetType("System.Int32"))
        Dim movto As DataColumn = New DataColumn("Movimiento", Type.GetType("System.Int32"))

        dt.Columns.Add(numPago)
        dt.Columns.Add(fechaEsperada)
        dt.Columns.Add(fechaPago)
        dt.Columns.Add(totalPago)
        dt.Columns.Add(pagoACredito)
        dt.Columns.Add(recargo)
        dt.Columns.Add(pagoCapital)
        dt.Columns.Add(pagoInteres)
        dt.Columns.Add(pagoIva)
        dt.Columns.Add(saldo)
        dt.Columns.Add(saldoCapital)
        dt.Columns.Add(id)
        dt.Columns.Add(movto)

        For Each pago In historial
            dr = dt.NewRow()
            dr("#Pago") = pago.NumPago
            dr("FechaEsperada") = pago.FecEsperada
            dr("FechaPago") = pago.FecPago
            dr("Interes") = pago.AbonoInteres
            dr("IVA") = pago.AbonoIVA
            dr("Capital") = pago.AbonoCapital
            dr("TotalPago") = pago.Monto + pago.RecargoCobrado
            dr("Pago") = pago.Monto
            dr("Recargo") = pago.RecargoCobrado
            dr("Saldo") = If(pago.Saldo < 0, 0, pago.Saldo)
            dr("SaldoCapital") = If(pago.SaldoCapital < 0, 0, pago.SaldoCapital)
            dr("id") = pago.Id
            dr("Movimiento") = pago.Id_Mov
            dt.Rows.Add(dr)
        Next pago

        Dim pagoIncompleto = False
        'Dim sobretasa = (credito.Sobretasa / 12) / 100
        Dim pagoLetra = 0
        Dim interes
        Dim iva
        Dim capital

        Dim ultPago = historial.OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
        Dim ultPagoObli = historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = False And clsPago.EsGarantia = False).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
        Dim ultPagoCap = historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = True).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
        Dim ultPagFijAntCap = If(IsNothing(ultPagoCap), Nothing, historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = False And clsPago.Id_Mov < ultPagoCap.Id_Mov).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault)

        Dim saldoCap = If(IsNothing(ultPago), credito.MontoPrestado, ultPago.SaldoCapital)
        Dim adeudo = If(IsNothing(ultPago), credito.Adeudo, ultPago.Saldo)

        Dim totalUltLetra As Double = 0
        Dim interesUltLetra As Double = 0
        Dim ivaUltLetra As Double = 0

        If IsNothing(ultPago) = False AndAlso ultPago.EsAbonoCapital = False AndAlso ultPago.EsGarantia = False Then
            totalUltLetra = historial.FindAll(Function(pago) pago.NumPago = ultPago.NumPago).Sum(Function(pago) pago.Monto)
            interesUltLetra = historial.FindAll(Function(pago) pago.NumPago = ultPago.NumPago).Sum(Function(pago) pago.AbonoInteres)
            ivaUltLetra = historial.FindAll(Function(pago) pago.NumPago = ultPago.NumPago).Sum(Function(pago) pago.AbonoIVA)
        End If

        If IsNothing(ultPagFijAntCap) = False Then
            Dim ultLetra = historial.FindAll(Function(pago) pago.NumPago = ultPagFijAntCap.NumPago).Sum(Function(pago) pago.Monto)
            Dim pagCap = historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = True And clsPago.Id_Mov < ultPagFijAntCap.Id_Mov).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
            Dim pagFijAntCap = If(IsNothing(pagCap), Nothing, historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = False And clsPago.Id_Mov < pagCap.Id_Mov).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault)
            Dim letra

            'If Not IsNothing(pagCap) Then
            '    letra = Math.Ceiling(pagCap.Saldo / credito.NumPagos - If(IsNothing(pagFijAntCap), 0, pagFijAntCap.NumPago))
            'Else
            '    letra = Math.Ceiling(credito.Adeudo / credito.NumPagos)
            'End If

            Dim ultPagoCap2 = pagos.OrderBy(Function(clsPago) clsPago.Saldo).ThenBy(
                Function(clsPago)
                    Return clsPago.Id_Mov
                End Function).ToList().FindAll(
                Function(clsPago)
                    Return clsPago.EsAbonoCapital = True
                End Function).FirstOrDefault

            Dim pagoAntesAbonoCapital = historial.OrderBy(Function(clsPago) clsPago.SaldoCapital).ThenBy(Function(clsPago) clsPago.Id_Mov).ToList().FindAll(Function(clsPago) clsPago.EsGarantia = False And clsPago.EsAbonoCapital = False And clsPago.Saldo >= ultPagoCap.Saldo).FirstOrDefault


            If Not IsNothing(ultPagoCap2) Then
                letra = Math.Ceiling(ultPagoCap2.Saldo / credito.NumPagos - If(IsNothing(ultPagoCap2), 0, ultPagoCap2.NumPago))
            Else
                letra = Math.Ceiling(credito.Adeudo / credito.NumPagos)
            End If

            '' Valida si se cubrio la letra con el ultimo pago
            If ultLetra < letra Then pagoIncompleto = True
        End If
        '' el ultimo pago fue un abono a capital


        If IsNothing(ultPago) Or If(IsNothing(ultPago), False, ultPago.EsGarantia) = True Then
            pagoLetra = Math.Ceiling(credito.Adeudo / credito.NumPagos)
            interes = Math.Round(((credito.Adeudo - credito.MontoPrestado) / credito.NumPagos) * (0.8621), 2)
            iva = Math.Round(((credito.Adeudo - credito.MontoPrestado) / credito.NumPagos) * (0.1379), 2)
            capital = pagoLetra - interes - iva


        ElseIf Not IsNothing(ultPagoCap) And If(IsNothing(ultPago), False, ultPago.EsAbonoCapital) = True Then
            Dim pagosRestantes = credito.NumPagos - If(IsNothing(ultPagFijAntCap), 0, ultPagFijAntCap.NumPago)
            If pagoIncompleto Then pagosRestantes = pagosRestantes + 1
            pagoLetra = Math.Ceiling(ultPagoCap.Saldo / pagosRestantes)
            interes = Math.Round(((ultPagoCap.Saldo - ultPagoCap.SaldoCapital) / pagosRestantes) * (0.8621), 2)
            iva = Math.Round(((ultPagoCap.Saldo - ultPagoCap.SaldoCapital) / pagosRestantes) * (0.1379), 2)
            capital = pagoLetra - interes - iva
            ' el ultimo pago fue abono a la letra 
        ElseIf If(IsNothing(ultPago), True, ultPago.EsAbonoCapital) = False And ultPago.EsGarantia = False Then
            Dim letra = 0
            Dim pagosRestantes = credito.NumPagos - ultPago.NumPago 'If(IsNothing(ultPagFijAntCap), 0, ultPagFijAntCap.NumPago)

            'No hay pagos a capital y aun quedan letras pendientes
            If IsNothing(ultPagoCap) And pagosRestantes <> 0 Then
                letra = Math.Ceiling(credito.Adeudo / credito.NumPagos)

                'Si hubo pago a capital y se determina el interes para las letras siguientes
            ElseIf Not IsNothing(ultPagoCap) And pagosRestantes <> 0 Then
                pagosRestantes = credito.NumPagos - ultPagFijAntCap.NumPago + 1
                letra = Math.Ceiling(ultPagoCap.Saldo / pagosRestantes)

                If ultPago.EsAbonoCapital = False Then
                    pagosRestantes = pagosRestantes - 1
                End If

            Else


                letra = Math.Ceiling(ultPago.Saldo / 1)
            End If


            'Validar si la letra actual esta pendiente y si hubo algun pago a capital
            If (totalUltLetra < letra) And Not IsNothing(ultPagoCap) Then

                If pagosRestantes = 0 Then
                    pagosRestantes = +1
                    pagoLetra = Math.Ceiling(ultPago.Saldo)

                ElseIf IsNothing(ultPagoCap) Then

                    pagoLetra = Math.Ceiling((ultPago.Saldo - (letra - totalUltLetra)) / pagosRestantes)
                Else
                    pagoLetra = letra
                End If

                interes = Math.Round(((ultPago.Saldo - ultPago.SaldoCapital) / pagosRestantes) * (0.8621), 2)
                iva = Math.Round(((ultPago.Saldo - ultPago.SaldoCapital) / pagosRestantes) * (0.1379), 2)
                capital = pagoLetra - interes - iva
            Else
                If Not IsNothing(ultPagoCap) Then
                    pagoLetra = Math.Ceiling((ultPago.Saldo) / pagosRestantes)
                    interes = Math.Round(((ultPago.Saldo - ultPago.SaldoCapital) / pagosRestantes) * (0.8621), 2)
                    iva = Math.Round(((ultPago.Saldo - ultPago.SaldoCapital) / pagosRestantes) * (0.1379), 2)
                    capital = pagoLetra - interes - iva
                Else
                    pagoLetra = Math.Ceiling(credito.Adeudo / credito.NumPagos)
                    interes = Math.Round(((credito.Adeudo - credito.MontoPrestado) / credito.NumPagos) * (0.8621), 2)
                    iva = Math.Round(((credito.Adeudo - credito.MontoPrestado) / credito.NumPagos) * (0.1379), 2)
                    capital = pagoLetra - interes - iva
                End If


            End If




            'pagoLetra = Math.Ceiling(credito.Adeudo / credito.NumPagos)




        End If

            Dim numPagoActual = If(IsNothing(ultPagoObli), 0, ultPagoObli.NumPago)

        If totalUltLetra <> 0 AndAlso totalUltLetra < pagoLetra Then
            Dim adeudoLetra = If(numPagoActual = credito.NumPagos, adeudo, pagoLetra - totalUltLetra)
            If numPagoActual = credito.NumPagos Then capital = capital - (pagoLetra - (adeudoLetra + totalUltLetra))
            'dr = dt.NewRow()
            'dr("#Pago") = numPagoActual
            'dr("FechaEsperada") = Utilerias.calcularFechaDePago(credito.FecInicio, numPagoActual, credito.Plazo)
            'dr("FechaPago") = DBNull.Value
            'dr("Interes") = If(totalUltLetra >= interes, 0, interes - totalUltLetra)
            'totalUltLetra = totalUltLetra - If(dr("Interes") = 0, interes, totalUltLetra)
            'dr("IVA") = If(totalUltLetra >= iva, 0, iva - totalUltLetra)
            'totalUltLetra = totalUltLetra - If(dr("IVA") = 0, iva, totalUltLetra)
            'dr("Capital") = If(totalUltLetra >= capital, 0, capital - totalUltLetra)
            'totalUltLetra = totalUltLetra - If(dr("Capital") = 0, capital, totalUltLetra)
            'dr("TotalPago") = dr("Interes") + dr("IVA") + dr("Capital")
            'dr("Pago") = DBNull.Value
            'dr("Recargo") = DBNull.Value
            'dr("Saldo") = If(adeudo - dr("TotalPago") < 0, 0, adeudo - dr("TotalPago"))
            'adeudo = dr("Saldo")
            'dr("SaldoCapital") = If(saldoCap - dr("Capital") < 0, 0, saldoCap - dr("Capital"))
            'saldoCap = dr("SaldoCapital")
            'dr("id") = DBNull.Value
            'dr("Movimiento") = DBNull.Value
            'dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("#Pago") = numPagoActual
            dr("FechaEsperada") = Utilerias.calcularFechaDePago(credito.FecInicio, numPagoActual, credito.Plazo)
            dr("FechaPago") = DBNull.Value
            dr("TotalPago") = adeudoLetra
            adeudo = adeudo - dr("TotalPago")
            dr("Pago") = DBNull.Value
            dr("Recargo") = DBNull.Value
            dr("Interes") = interes - interesUltLetra '+ ((credito.NumPagos - pagoActual) * pagInteres)
            dr("IVA") = iva - ivaUltLetra '+ ((credito.NumPagos - pagoActual) * pagIva)
            dr("Capital") = dr("TotalPago") - dr("Interes") - dr("IVA")
            saldoCap = If(saldoCap - dr("Capital") <= 0, 0, saldoCap - dr("Capital"))
            dr("Saldo") = adeudo
            'dr("SaldoCapital") = If(capital <= 0, 0, If(pagoActual + 1 = credito.NumPagos, dr("Saldo") - dr("Interes") - dr("IVA"), If(pagoActual = credito.NumPagos, 0, capital)))
            dr("SaldoCapital") = saldoCap
            dr("id") = DBNull.Value
            dr("Movimiento") = DBNull.Value

            If dr("Saldo") < dr("SaldoCapital") Then
                Dim dif = dr("SaldoCapital") - dr("Saldo")
                'dr("TotalPago") = dr("TotalPago") + dif
                dr("Capital") = dr("Capital") + dif
                dr("SaldoCapital") = dr("SaldoCapital") - dif
                saldoCap = dr("SaldoCapital")
                dr("Interes") = dr("Interes") - Math.Round(dif * (0.8621), 2)
                dr("IVA") = dr("IVA") - Math.Round(dif * (0.1379), 2)
            End If

            If dr("Saldo") <= 0 And dr("SaldoCapital") <= 0 Then
                dr("Saldo") = 0
                dr("SaldoCapital") = 0
                dt.Rows.Add(dr)
                Return dt
            End If

            dt.Rows.Add(dr)
        End If

        If pagoIncompleto Then numPagoActual = numPagoActual - 1

        For pagoActual As Integer = numPagoActual + 1 To credito.NumPagos
            dr = dt.NewRow()
            dr("#Pago") = pagoActual
            dr("FechaEsperada") = Utilerias.calcularFechaDePago(credito.FecInicio, pagoActual, credito.Plazo)
            dr("FechaPago") = DBNull.Value
            dr("TotalPago") = If(pagoActual = credito.NumPagos, adeudo, pagoLetra)
            adeudo = adeudo - dr("TotalPago")
            dr("Capital") = If(saldoCap < capital, saldoCap, capital)
            saldoCap = saldoCap - dr("Capital")
            dr("Interes") = interes
            dr("IVA") = iva
            dr("Pago") = DBNull.Value
            dr("Recargo") = DBNull.Value
            dr("Saldo") = adeudo
            dr("SaldoCapital") = saldoCap 'If(pagoActual + 1 = NumPagos, dr("Saldo") - pagInteres - pagIva, If(pagoActual = NumPagos, 0, Prestamo - (pagoCap * (pagoActual - 1)) - dr("Capital")))
            dr("id") = DBNull.Value
            dr("Movimiento") = DBNull.Value

            If dr("Saldo") <= 0 And dr("SaldoCapital") <= 0 Then
                dr("Saldo") = 0
                dr("SaldoCapital") = 0
                dt.Rows.Add(dr)
                Exit For
            End If

            dt.Rows.Add(dr)
        Next pagoActual

        Return dt
    End Function

    Public Shared Function crearTablaDePagosProgresiva(credito As clsCredito, pagos As List(Of clsPago)) As DataTable

        'Dim ds As New DataSet
        Dim historial As New List(Of clsPago)(pagos)
        Dim dt As DataTable = New DataTable()
        dt.TableName = "TABLADEAMORTIZACION"
        Dim dr As DataRow

        Dim numPago As DataColumn = New DataColumn("#Pago", Type.GetType("System.Int32"))
        Dim fechaEsperada As DataColumn = New DataColumn("FechaEsperada", Type.GetType("System.DateTime"))
        Dim fechaPago As DataColumn = New DataColumn("FechaPago", Type.GetType("System.DateTime"))
        Dim totalPago As DataColumn = New DataColumn("TotalPago", Type.GetType("System.Double"))
        Dim pagoACredito As DataColumn = New DataColumn("Pago", Type.GetType("System.Double"))
        Dim recargo As DataColumn = New DataColumn("Recargo", Type.GetType("System.Double"))
        Dim pagoCapital As DataColumn = New DataColumn("Capital", Type.GetType("System.Double"))
        Dim pagoInteres As DataColumn = New DataColumn("Interes", Type.GetType("System.Double"))
        Dim pagoIva As DataColumn = New DataColumn("IVA", Type.GetType("System.Double"))
        Dim saldo As DataColumn = New DataColumn("Saldo", Type.GetType("System.Double"))
        Dim saldoCapital As DataColumn = New DataColumn("SaldoCapital", Type.GetType("System.Double"))
        Dim id As DataColumn = New DataColumn("id", Type.GetType("System.Int32"))
        Dim movto As DataColumn = New DataColumn("Movimiento", Type.GetType("System.Int32"))

        dt.Columns.Add(numPago)
        dt.Columns.Add(fechaEsperada)
        dt.Columns.Add(fechaPago)
        dt.Columns.Add(totalPago)
        dt.Columns.Add(pagoACredito)
        dt.Columns.Add(recargo)
        dt.Columns.Add(pagoCapital)
        dt.Columns.Add(pagoInteres)
        dt.Columns.Add(pagoIva)
        dt.Columns.Add(saldo)
        dt.Columns.Add(saldoCapital)
        dt.Columns.Add(id)
        dt.Columns.Add(movto)

        For Each pago In historial
            dr = dt.NewRow()
            dr("#Pago") = pago.NumPago
            dr("FechaEsperada") = pago.FecEsperada
            dr("FechaPago") = pago.FecPago
            dr("Interes") = pago.AbonoInteres
            dr("IVA") = pago.AbonoIVA
            dr("Capital") = pago.AbonoCapital
            dr("TotalPago") = pago.Monto + pago.RecargoCobrado
            dr("Pago") = pago.Monto
            dr("Recargo") = pago.RecargoCobrado
            dr("Saldo") = If(pago.Saldo < 0, 0, pago.Saldo)
            dr("SaldoCapital") = If(pago.SaldoCapital < 0, 0, pago.SaldoCapital)
            dr("id") = pago.Id
            dr("Recargo") = pago.Recargo
            dr("Movimiento") = pago.Id_Mov
            dt.Rows.Add(dr)
        Next pago

        Dim pagoIncompleto = False
        Dim sobretasa = (credito.Tasa * 12) / 365
        sobretasa = Math.Round(If(credito.Plazo = 1, sobretasa * 7, If(credito.Plazo = 3, sobretasa * 30, sobretasa * 15)), 3)
        Dim interes
        Dim iva
        Dim capital

        Dim ultPago = historial.OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
        Dim ultPagoObli = historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = False).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
        Dim ultPagoCap = historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = True).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
        Dim ultPagFijAntCap = If(IsNothing(ultPagoCap), Nothing, historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = False And clsPago.Id_Mov < ultPagoCap.Id_Mov).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault)

        Dim totalUltLetra As Double

        If IsNothing(ultPago) = False AndAlso ultPago.EsAbonoCapital = False Then
            totalUltLetra = historial.FindAll(Function(pago) pago.NumPago = ultPago.NumPago).Sum(Function(pago) pago.Monto)
        End If

        Dim saldoCap = If(IsNothing(ultPago), credito.MontoPrestado, ultPago.SaldoCapital)
        Dim adeudo = If(IsNothing(ultPago), credito.Adeudo, ultPago.Saldo)
        Dim pagoLetra = Math.Abs(Pmt(sobretasa, credito.NumPagos, credito.MontoPrestado))



        If IsNothing(ultPagFijAntCap) = False Then
            Dim ultLetra = historial.FindAll(Function(pago) pago.NumPago = ultPagFijAntCap.NumPago).Sum(Function(pago) pago.Monto)
            Dim pagCap = historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = True And clsPago.Id_Mov < ultPagFijAntCap.Id_Mov).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
            Dim pagFijAntCap = If(IsNothing(pagCap), Nothing, historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = False And clsPago.Id_Mov < pagCap.Id_Mov).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault)
            Dim letra

            If Not IsNothing(pagCap) Then
                letra = Math.Abs(Pmt(sobretasa, credito.NumPagos - If(IsNothing(pagFijAntCap), 0, pagFijAntCap.NumPago), pagCap.SaldoCapital))
            Else
                letra = Math.Abs(Pmt(sobretasa, credito.NumPagos, credito.MontoPrestado))
            End If

            If ultLetra < letra Then pagoIncompleto = True
        End If

        If Not IsNothing(ultPagoCap) Then
            pagoLetra = Math.Abs(Pmt(sobretasa, credito.NumPagos - If(IsNothing(ultPagFijAntCap), 0, ultPagFijAntCap.NumPago - If(pagoIncompleto, 1, 0)), ultPagoCap.SaldoCapital))
        End If

        Dim numPagoActual = If(IsNothing(ultPagoObli), 0, ultPagoObli.NumPago)

        If totalUltLetra <> 0 AndAlso totalUltLetra < pagoLetra Then
            Dim adeudoLetra = pagoLetra - totalUltLetra
            dr = dt.NewRow()
            interes = Math.Abs(If(IsNothing(ultPagoCap), IPmt(sobretasa, numPagoActual, credito.NumPagos, credito.MontoPrestado), IPmt(sobretasa, numPagoActual - ultPagFijAntCap.NumPago, credito.NumPagos - ultPagFijAntCap.NumPago, ultPagoCap.SaldoCapital))) * (0.8621)
            iva = Math.Abs(If(IsNothing(ultPagoCap), IPmt(sobretasa, numPagoActual, credito.NumPagos, credito.MontoPrestado), IPmt(sobretasa, numPagoActual - ultPagFijAntCap.NumPago, credito.NumPagos - ultPagFijAntCap.NumPago, ultPagoCap.SaldoCapital))) * (0.1379)
            capital = pagoLetra - interes - iva
            dr("#Pago") = numPagoActual
            dr("FechaEsperada") = Utilerias.calcularFechaDePago(credito.FecInicio, numPagoActual, credito.Plazo)
            dr("FechaPago") = DBNull.Value
            dr("Interes") = If(totalUltLetra >= interes, 0, interes - totalUltLetra)
            totalUltLetra = totalUltLetra - If(dr("Interes") = 0, interes, totalUltLetra)
            dr("IVA") = If(totalUltLetra >= iva, 0, iva - totalUltLetra)
            totalUltLetra = totalUltLetra - If(dr("IVA") = 0, iva, totalUltLetra)
            dr("Capital") = If(totalUltLetra >= capital, 0, capital - totalUltLetra)
            totalUltLetra = totalUltLetra - If(dr("Capital") = 0, capital, totalUltLetra)
            dr("TotalPago") = dr("Interes") + dr("IVA") + dr("Capital")
            dr("Pago") = DBNull.Value
            dr("Recargo") = DBNull.Value
            dr("Saldo") = ultPago.Saldo - dr("TotalPago")
            adeudo = dr("Saldo")
            dr("SaldoCapital") = saldoCap - dr("Capital")
            saldoCap = dr("SaldoCapital")
            dr("id") = DBNull.Value
            dr("Movimiento") = DBNull.Value
            dt.Rows.Add(dr)
        End If

        If pagoIncompleto Then numPagoActual = numPagoActual - 1

        For pagoActual As Integer = numPagoActual + 1 To credito.NumPagos
            interes = Math.Abs(If(IsNothing(ultPagoCap), IPmt(sobretasa, pagoActual, credito.NumPagos, credito.MontoPrestado), IPmt(sobretasa, pagoActual - If(pagoIncompleto, ultPagFijAntCap.NumPago - 1, ultPagFijAntCap.NumPago), credito.NumPagos - If(pagoIncompleto, ultPagFijAntCap.NumPago - 1, ultPagFijAntCap.NumPago), ultPagoCap.SaldoCapital))) * (0.8621)
            iva = Math.Abs(If(IsNothing(ultPagoCap), IPmt(sobretasa, pagoActual, credito.NumPagos, credito.MontoPrestado), IPmt(sobretasa, pagoActual - If(pagoIncompleto, ultPagFijAntCap.NumPago - 1, ultPagFijAntCap.NumPago), credito.NumPagos - If(pagoIncompleto, ultPagFijAntCap.NumPago - 1, ultPagFijAntCap.NumPago), ultPagoCap.SaldoCapital))) * (0.1379)
            capital = pagoLetra - interes - iva
            dr = dt.NewRow()
            dr("#Pago") = pagoActual
            dr("FechaEsperada") = Utilerias.calcularFechaDePago(credito.FecInicio, pagoActual, credito.Plazo)
            dr("FechaPago") = DBNull.Value
            dr("TotalPago") = pagoLetra 'If(pagoActual = NumPagos, Adeudo - (pago * (pagoActual - 1)), pago)
            adeudo = adeudo - dr("TotalPago")
            dr("Capital") = capital
            saldoCap = saldoCap - dr("Capital")
            dr("Interes") = interes
            dr("IVA") = iva
            dr("Pago") = DBNull.Value
            dr("Recargo") = DBNull.Value
            dr("Saldo") = adeudo
            dr("SaldoCapital") = saldoCap 'If(pagoActual + 1 = NumPagos, dr("Saldo") - pagInteres - pagIva, If(pagoActual = NumPagos, 0, Prestamo - (pagoCap * (pagoActual - 1)) - dr("Capital")))
            dr("id") = DBNull.Value
            dr("Movimiento") = DBNull.Value
            dt.Rows.Add(dr)
        Next pagoActual

        Return dt
    End Function

    Public Shared Function obtenerTablaPagados(credito As clsCredito, pagos As List(Of clsPago)) As DataTable

        'Dim ds As New DataSet
        Dim historial As New List(Of clsPago)(pagos)
        Dim dt As DataTable = New DataTable()
        dt.TableName = "TABLADEAMORTIZACION"
        Dim dr As DataRow

        Dim numPago As DataColumn = New DataColumn("#Pago", Type.GetType("System.Int32"))
        Dim fechaEsperada As DataColumn = New DataColumn("FechaEsperada", Type.GetType("System.DateTime"))
        Dim fechaPago As DataColumn = New DataColumn("FechaPago", Type.GetType("System.DateTime"))
        Dim totalPago As DataColumn = New DataColumn("TotalPago", Type.GetType("System.Double"))
        Dim pagoACredito As DataColumn = New DataColumn("Pago", Type.GetType("System.Double"))
        Dim recargo As DataColumn = New DataColumn("Recargo", Type.GetType("System.Double"))
        Dim pagoCapital As DataColumn = New DataColumn("Capital", Type.GetType("System.Double"))
        Dim pagoInteres As DataColumn = New DataColumn("Interes", Type.GetType("System.Double"))
        Dim pagoIva As DataColumn = New DataColumn("IVA", Type.GetType("System.Double"))
        Dim saldo As DataColumn = New DataColumn("Saldo", Type.GetType("System.Double"))
        Dim saldoCapital As DataColumn = New DataColumn("SaldoCapital", Type.GetType("System.Double"))
        Dim id As DataColumn = New DataColumn("id", Type.GetType("System.Int32"))
        Dim movto As DataColumn = New DataColumn("Movimiento", Type.GetType("System.Int32"))

        dt.Columns.Add(numPago)
        dt.Columns.Add(fechaEsperada)
        dt.Columns.Add(fechaPago)
        dt.Columns.Add(totalPago)
        dt.Columns.Add(pagoACredito)
        dt.Columns.Add(recargo)
        dt.Columns.Add(pagoCapital)
        dt.Columns.Add(pagoInteres)
        dt.Columns.Add(pagoIva)
        dt.Columns.Add(saldo)
        dt.Columns.Add(saldoCapital)
        dt.Columns.Add(id)
        dt.Columns.Add(movto)

        Dim garantia = historial.Find(Function(clsPago) clsPago.EsGarantia = True AndAlso (IsNothing(clsPago.GarantiaAplicada) OrElse clsPago.GarantiaAplicada = False))

        If Not IsNothing(garantia) AndAlso garantia.GarantiaAplicada = False Then
            dr = dt.NewRow()
            dr("#Pago") = garantia.NumPago
            dr("FechaEsperada") = garantia.FecEsperada
            dr("FechaPago") = garantia.FecPago
            dr("TotalPago") = garantia.Monto
            dr("Pago") = garantia.Monto
            dr("Recargo") = 0
            dr("Capital") = 0
            dr("Interes") = 0
            dr("IVA") = 0
            dr("Saldo") = garantia.Saldo
            dr("SaldoCapital") = garantia.SaldoCapital
            dr("id") = garantia.Id
            dr("Movimiento") = garantia.Id_Mov
            dt.Rows.Add(dr)
            historial.Remove(garantia)
        End If

        For Each pago In historial
            dr = dt.NewRow()
            dr("#Pago") = pago.NumPago
            dr("FechaEsperada") = pago.FecEsperada
            dr("FechaPago") = pago.FecPago
            dr("Interes") = pago.AbonoInteres
            dr("IVA") = pago.AbonoIVA
            dr("Capital") = pago.AbonoCapital
            dr("TotalPago") = pago.Monto + pago.RecargoCobrado
            dr("Pago") = pago.Monto
            dr("Recargo") = pago.RecargoCobrado
            dr("Saldo") = If(pago.Saldo < 0, 0, pago.Saldo)
            dr("SaldoCapital") = If(pago.SaldoCapital < 0, 0, pago.SaldoCapital)
            dr("id") = pago.Id
            dr("Recargo") = pago.Recargo
            dr("Movimiento") = pago.Id_Mov
            dt.Rows.Add(dr)
        Next pago

        If Not IsNothing(garantia) AndAlso garantia.GarantiaAplicada = True Then
            dr = dt.NewRow()
            dr("#Pago") = garantia.NumPago
            dr("FechaEsperada") = garantia.FecEsperada
            dr("FechaPago") = garantia.FecPago
            dr("TotalPago") = garantia.Monto
            dr("Pago") = garantia.Monto
            dr("Recargo") = 0
            dr("Capital") = 0
            dr("Interes") = 0
            dr("IVA") = 0
            dr("Saldo") = garantia.Saldo
            dr("SaldoCapital") = garantia.SaldoCapital
            dr("id") = garantia.Id
            dr("Movimiento") = garantia.Id_Mov
            dt.Rows.Add(dr)
            historial.Remove(garantia)
        End If

        Return dt
    End Function

    Public Shared Function crearTablaPagosFaltantes(credito As clsCredito, ultimoPago As clsPago) As DataTable

        Dim dt As DataTable = New DataTable()
        dt.TableName = "TABLADEAMORTIZACION"
        Dim dr As DataRow

        Dim numPago As DataColumn = New DataColumn("#Pago", Type.GetType("System.Int32"))
        Dim fechaEsperada As DataColumn = New DataColumn("FechaEsperada", Type.GetType("System.DateTime"))
        Dim fechaPago As DataColumn = New DataColumn("FechaPago", Type.GetType("System.DateTime"))
        Dim totalPago As DataColumn = New DataColumn("TotalPago", Type.GetType("System.Double"))
        Dim pagoACredito As DataColumn = New DataColumn("Pago", Type.GetType("System.Double"))
        Dim recargo As DataColumn = New DataColumn("Recargo", Type.GetType("System.Double"))
        Dim pagoCapital As DataColumn = New DataColumn("Capital", Type.GetType("System.Double"))
        Dim pagoInteres As DataColumn = New DataColumn("Interes", Type.GetType("System.Double"))
        Dim pagoIva As DataColumn = New DataColumn("IVA", Type.GetType("System.Double"))
        Dim saldo As DataColumn = New DataColumn("Saldo", Type.GetType("System.Double"))
        Dim saldoCapital As DataColumn = New DataColumn("SaldoCapital", Type.GetType("System.Double"))
        Dim id As DataColumn = New DataColumn("id", Type.GetType("System.Int32"))
        Dim movto As DataColumn = New DataColumn("Movimiento", Type.GetType("System.Int32"))

        dt.Columns.Add(numPago)
        dt.Columns.Add(fechaEsperada)
        dt.Columns.Add(fechaPago)
        dt.Columns.Add(totalPago)
        dt.Columns.Add(pagoACredito)
        dt.Columns.Add(recargo)
        dt.Columns.Add(pagoCapital)
        dt.Columns.Add(pagoInteres)
        dt.Columns.Add(pagoIva)
        dt.Columns.Add(saldo)
        dt.Columns.Add(saldoCapital)
        dt.Columns.Add(id)
        dt.Columns.Add(movto)

        'Dim garantia = historial.Find(Function(clsPago) clsPago.EsGarantia = True AndAlso clsPago.GarantiaAplicada = False)

        'If Not IsNothing(garantia) And garantia.GarantiaAplicada = False Then
        '    dr = dt.NewRow()
        '    dr("#Pago") = garantia.NumPago
        '    dr("FechaEsperada") = garantia.FecEsperada
        '    dr("FechaPago") = garantia.FecPago
        '    dr("TotalPago") = garantia.Monto
        '    dr("Pago") = garantia.Monto
        '    dr("Recargo") = 0
        '    dr("Capital") = 0
        '    dr("Interes") = 0
        '    dr("IVA") = 0
        '    dr("Saldo") = garantia.Saldo
        '    dr("SaldoCapital") = garantia.SaldoCapital
        '    dr("id") = garantia.Id
        '    dr("Movimiento") = garantia.Id_Mov
        '    dt.Rows.Add(dr)
        '    historial.Remove(garantia)
        'End If

        'For Each pago In historial
        '    dr = dt.NewRow()
        '    dr("#Pago") = pago.NumPago
        '    dr("FechaEsperada") = pago.FecEsperada
        '    dr("FechaPago") = pago.FecPago
        '    dr("Interes") = pago.AbonoInteres
        '    dr("IVA") = pago.AbonoIVA
        '    dr("Capital") = pago.AbonoCapital
        '    dr("TotalPago") = pago.Monto + pago.RecargoCobrado
        '    dr("Pago") = pago.Monto
        '    dr("Recargo") = pago.RecargoCobrado
        '    dr("Saldo") = If(pago.Saldo < 0, 0, pago.Saldo)
        '    dr("SaldoCapital") = If(pago.SaldoCapital < 0, 0, pago.SaldoCapital)
        '    dr("id") = pago.Id
        '    dr("Recargo") = pago.Recargo
        '    dr("Movimiento") = pago.Id_Mov
        '    dt.Rows.Add(dr)
        'Next pago

        'If Not IsNothing(garantia) And garantia.GarantiaAplicada = True Then
        '    dr = dt.NewRow()
        '    dr("#Pago") = garantia.NumPago
        '    dr("FechaEsperada") = garantia.FecEsperada
        '    dr("FechaPago") = garantia.FecPago
        '    dr("TotalPago") = garantia.Monto
        '    dr("Pago") = garantia.Monto
        '    dr("Recargo") = 0
        '    dr("Capital") = 0
        '    dr("Interes") = 0
        '    dr("IVA") = 0
        '    dr("Saldo") = garantia.Saldo
        '    dr("SaldoCapital") = garantia.SaldoCapital
        '    dr("id") = garantia.Id
        '    dr("Movimiento") = garantia.Id_Mov
        '    dt.Rows.Add(dr)
        '    historial.Remove(garantia)
        'End If

        Return dt
    End Function

    Public Shared Function calcularInteresPorPago(credito As clsCredito) As Double
        Dim interes = (credito.Adeudo - credito.MontoPrestado)
        Dim pagInteres = Math.Round((interes / credito.NumPagos) * (0.8621), 2)
        Dim pagIva = Math.Round((interes / credito.NumPagos) * (0.1379), 2)
        Return pagInteres + pagIva
    End Function

    Public Shared Function calcularNuevoSaldo(credito As clsCredito, pagoActual As Integer, saldoCapital As Double) As Double
        'Dim duracionMeses = If(credito.Plazo = 1, (credito.NumPagos - pagoActual) / 4, If(credito.Plazo = 2, (credito.NumPagos - pagoActual), (credito.NumPagos - pagoActual) / 2))
        Dim duracionDias = If(credito.Plazo = 1, (credito.NumPagos - pagoActual) * 7, If(credito.Plazo = 2, (credito.NumPagos - pagoActual) * 15, (credito.NumPagos - pagoActual) * 30))
        Dim interes = (saldoCapital * (((credito.Tasa * 12) / 365) * (duracionDias)))
        If Not credito.IncluyeIVA Then interes = interes * 1.16
        Dim deuda = Utilerias.Redondeo(saldoCapital + interes, 2)
        Return deuda
    End Function


    Public Shared Function calcularNuevoSaldoEmp(credito As clsCredito, saldoCapital As Double) As Double
        'Dim duracionMeses = If(credito.Plazo = 1, (credito.NumPagos - pagoActual) / 4, If(credito.Plazo = 2, (credito.NumPagos - pagoActual), (credito.NumPagos - pagoActual) / 2))

        Dim interes = saldoCapital * credito.Tasa
        Dim deuda = Utilerias.Redondeo(saldoCapital + interes, 2)
        Return deuda
    End Function


#End Region

    Public Shared Function lst_OpcionesSeleccionadas(Lstbox As ListBox) As List(Of String)
        Try
            Dim lstCuentas As New List(Of String)
            For Each item As System.Web.UI.WebControls.ListItem In Lstbox.Items
                If item.Selected Then
                    lstCuentas.Add(item.Value)
                End If
            Next
            If lstCuentas.Count <> 0 Then
                Dim a As String = Join(lstCuentas.ToArray(), ",")
                REM buscar si solo tiene el valor cero
                lstCuentas.Clear()
                For Each item As System.Web.UI.WebControls.ListItem In Lstbox.Items
                    If item.Selected Then
                        lstCuentas.Add(item.Value)
                    End If
                Next
            End If
            Return lstCuentas
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function chklst_OpcSelecDevExpres(Lstbox As DevExpress.Web.ASPxCheckBoxList) As List(Of String)
        Try
            Dim lstCuentas As New List(Of String)
            For Each item As DevExpress.Web.ListEditItem In Lstbox.Items
                If item.Selected Then
                    lstCuentas.Add(item.Value)
                End If
            Next
            If lstCuentas.Count <> 0 Then
                Dim a As String = Join(lstCuentas.ToArray(), ",")
                REM buscar si solo tiene el valor cero
                lstCuentas.Clear()
                For Each item As DevExpress.Web.ListEditItem In Lstbox.Items
                    If item.Selected Then
                        lstCuentas.Add(item.Value)
                    End If
                Next
            End If
            Return lstCuentas
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub HtmlRowCreated(sender As Object, e As DevExpress.Web.ASPxGridViewTableRowEventArgs, _
                             Identificador As String, ddl As String)
        Try
            If sender.IsNewRowEditing Then
                Dim cb = CType(sender.FindEditRowCellTemplateControl(sender.Columns(Identificador), ddl), DropDownList)
                cb.SelectedValue = 0
            ElseIf sender.IsEditing Then
                Dim cb = CType(sender.FindEditRowCellTemplateControl(sender.Columns(Identificador), ddl), DropDownList)
                If IsNothing(cb) = False Then
                    'Dim gridView As ASPxGridView = sender
                    Dim dataRow As System.Data.DataRowView = CType(sender.GetRow(e.VisibleIndex), System.Data.DataRowView)
                    Dim Select_ID = dataRow(Identificador)
                    cb.SelectedValue = Select_ID
                Else
                    Exit Sub
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub AddError(ByVal errors As Dictionary(Of GridViewColumn, String), ByVal column As GridViewColumn, ByVal errorText As String)
        If Not errors.ContainsKey(column) Then
            errors(column) = errorText
        End If
    End Sub

    Public Shared Sub validaSeleccionado(campo As String, cb As DropDownList, sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
        If cb.SelectedIndex = 0 Then Utilerias.AddError(e.Errors, sender.Columns(campo), "Seleccione un elemento")
    End Sub

    Public Shared Sub validaVacio(campo As String, sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
        If e.NewValues(campo) Is Nothing Then Utilerias.AddError(e.Errors, sender.Columns(campo), "Campo Vacío")
    End Sub

    Public Shared Sub validaLenTexto(campo As String, digitos As Integer, sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
        If Len(e.NewValues(campo)) > digitos Then Utilerias.AddError(e.Errors, sender.Columns(campo), "Cadena sobrepasa los " & digitos & " dígitos válidos")
    End Sub

    Public Shared Sub getDatareaderGvPv(StrSelect As String, gv As DevExpress.Web.ASPxPivotGrid.ASPxPivotGrid)
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using RDR = Com.ExecuteReader()
                    If RDR.HasRows Then
                        gv.DataSource = RDR
                        gv.DataBind()
                    End If
                End Using
            End Using
            Con.Close()
        End Using
    End Sub

    Public Shared Sub getDatareaderGv(StrSelect As String, gv As System.Web.UI.WebControls.GridView)
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using RDR = Com.ExecuteReader()
                    If RDR.HasRows Then
                        gv.DataSource = RDR
                        gv.DataBind()
                    End If
                End Using
            End Using
            Con.Close()
        End Using
    End Sub

    Public Shared Sub getDatareaderGvAspx(StrSelect As String, gv As DevExpress.Web.ASPxGridView)
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using RDR = Com.ExecuteReader()
                    If RDR.HasRows Then
                        gv.DataSource = RDR
                        gv.DataBind()
                    End If
                End Using
            End Using
            Con.Close()
        End Using
    End Sub

    Public Shared Function setUpdInsDel(sqlUpdInsDel As String) As Boolean
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Try
                Dim cmd As New SqlCommand(sqlUpdInsDel, Con)
                cmd.ExecuteNonQuery()
                Con.Close()
                Return True
            Catch ex As Exception
                Con.Close()
                Return False
            End Try
        End Using
    End Function

    Public Shared Function getSelectPrimerCampo(sqlUpdInsDel As String)
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Dim cmd As New SqlCommand(sqlUpdInsDel, Con)
            getSelectPrimerCampo = cmd.ExecuteScalar()
            Con.Close()
        End Using
    End Function

    Public Shared Function BuscarCadenaEnTexto(Texto As String, cadenaLetraBuscar As String) As Integer
        BuscarCadenaEnTexto = Texto.IndexOf(cadenaLetraBuscar)
    End Function


End Class
